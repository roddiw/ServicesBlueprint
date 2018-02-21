using Common.RequestProcessing.Messages;
using FluentValidation;

namespace Common.RequestProcessing
{
    public class BaseRequestValidator : AbstractValidator<BaseRequest>
    {
        public BaseRequestValidator()
        {
            RuleFor(request => request.RequestID)
                .Must(value => !string.IsNullOrWhiteSpace(value) && value.Length < 100)
                .WithMessage("'{PropertyName}' must be non empty and 100 characters or less");
            RuleFor(request => request.RequestingSystem)
                .Must(value => !string.IsNullOrWhiteSpace(value) && value.Length < 100)
                .WithMessage("'{PropertyName}' must be non empty and 100 characters or less");
            RuleFor(request => request.RequestingUser)
                .Must(value => !string.IsNullOrWhiteSpace(value) && value.Length < 100)
                .WithMessage("'{PropertyName}' must be non empty and 100 characters or less");
        }
    }
}