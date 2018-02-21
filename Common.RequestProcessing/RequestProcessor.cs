using Common.AuditLogging;
using Common.RequestProcessing.Messages;
using FluentValidation;
using FluentValidation.Results;
using log4net;
using System;
using System.Collections.Generic;

namespace Common.RequestProcessing
{
    public class RequestProcessor
    {
        private readonly ILog logger;
        private readonly IAuditLog auditLogger;

        /// <summary>
        /// Creates a request processor
        /// </summary>
        /// <param name="logger">The logger</param>
        /// <param name="auditLogger">The audit logger</param>
        public RequestProcessor(
            ILog logger,
            IAuditLog auditLogger)
        {
            if (logger == null)
            {
                throw new ArgumentException("logger cannot be null");
            }
            this.logger = logger;

            if (auditLogger == null)
            {
                throw new ArgumentException("auditLogger cannot be null");
            }
            this.auditLogger = auditLogger;
        }

        public TResponse Execute<TRequest, TResponse>(
            TRequest request,
            IAction<TRequest, TResponse> action)
            where TRequest : BaseRequest
            where TResponse : BaseResponse, new()
        {
            TResponse response = null;
            Guid correlationID = Guid.NewGuid();

            try
            {
                //
                // audit log request
                //
                string requestName = request.GetType().FullName;
                try
                {
                    auditLogger.Info(GetAuditLogString(requestName, correlationID, request.ToLoggableRequest()));
                }
                catch (Exception ex)
                {
                    try
                    {
                        logger.Error(String.Format("Exception audit logging request {0}. Correlation: {1}", requestName, correlationID), ex);
                    }
                    catch { }
                }

                //
                // execute request
                //


                List<ResponseError> validationErrors = GetValidationErrors(request);
                if (validationErrors.Count > 0)
                {
                    response = new TResponse();
                    response.Errors = validationErrors;
                }
                else
                {
                    response = action.Execute(request);
                }
            }
            catch (Exception ex)
            {
                try
                {
                    logger.Error("Exception executing request. Correlation: " + correlationID, ex);
                }
                catch { }
                try
                {
                    response = new TResponse { Errors = new ResponseError(ErrorCode.ExecutionException).ToList() };
                }
                catch { }
            }
            finally
            {
                response.RequestID = request.RequestID;

                //
                // audit log response
                //

                if (response != null)
                {
                    string responseName = response.GetType().FullName;
                    try
                    {
                        auditLogger.Info(GetAuditLogString(responseName, correlationID, response.ToLoggableResponse()));
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            logger.Error(String.Format("Exception audit logging response {0}. Correlation: {1}", responseName, correlationID), ex);
                        }
                        catch { }
                    }
                }
                else
                {
                    try
                    {
                        logger.Error("Null response for request with correlation " + correlationID);
                    }
                    catch { }
                }
            }

            return response;
        }

        private string GetAuditLogString(
            string heading,
            Guid correlationID,
            object obj)
        {
            return $"{heading} Correlation: {correlationID}\r\n{Newtonsoft.Json.JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.None)}";
        }

        private List<ResponseError> GetValidationErrors<TRequest>(TRequest request) where TRequest : BaseRequest
        {
            var validationErrors = new List<ResponseError>();

            ValidatorOptions.CascadeMode = CascadeMode.StopOnFirstFailure;
            var baseRequestValidator = new BaseRequestValidator();
            ValidationResult results = baseRequestValidator.Validate((BaseRequest)request);
            if (!results.IsValid)
            {
                foreach (ValidationFailure error in results.Errors)
                {
                    validationErrors.Add(new ResponseError(ErrorCode.ValidationError, error.PropertyName, error.ErrorMessage));
                }
            }

            return validationErrors;
        }
    }
}