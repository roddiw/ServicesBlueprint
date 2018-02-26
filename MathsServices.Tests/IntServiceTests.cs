using System.Linq;
using Common.AuditLogging;
using Common.RequestProcessing;
using Common.RequestProcessing.Messages;
using log4net;
using MathServices;
using MathServices.Messages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using Rhino.Mocks;

namespace MathsServices.Tests
{
    [TestClass]
    public class IntServiceTests
    {
        private StandardKernel kernel;
        private IntService intService;
        private ISettings mockSettings;

        [TestInitialize]
        public void TestInitialize()
        {
            IAuditLog auditLogger = MockRepository.GenerateMock<IAuditLog>();
            ILog logger = MockRepository.GenerateMock<ILog>();
            mockSettings = MockRepository.GenerateMock<ISettings>();

            kernel = GetKernel(auditLogger, logger, mockSettings);
            Resolver.Initialize(logger, kernel);

            intService = new IntService();
        }

        [TestMethod]
        public void AddInts_ResponseHasMaxResult_WhenRequestNumber1AndNumber2GreaterThanMaxResult()
        {
            // arrange
            int number1 = 10;
            int number2 = 20;
            int maxResult = 25;
            mockSettings.Stub(mock => mock.AddIntsMaxNumber).Return(100);
            mockSettings.Stub(mock => mock.AddIntsMaxResult).Return(maxResult);
            var request = new AddIntsRequest("requestID", "requestingSystem", "requestingUser", number1, number2);

            // act
            AddIntsResponse response = intService.AddInts(request);

            // assert
            Assert.IsTrue(!response.Errors.Any());
            Assert.IsTrue(response.Result == maxResult);
        }

        [TestMethod]
        public void AddInts_ResponseHasValidationError_WhenRequestNumber1GreaterThanSettingsAddIntsMaxNumber()
        {
            // arrange
            int maxNumber = 100;
            mockSettings.Stub(mock => mock.AddIntsMaxNumber).Return(maxNumber);
            int number1 = maxNumber + 1;
            int number2 = maxNumber;
            var request = new AddIntsRequest("requestID", "requestingSystem", "requestingUser", number1, number2);

            // act
            AddIntsResponse response = intService.AddInts(request);

            // assert
            Assert.IsTrue(response.Errors.Any(error => error.Code == ErrorCode.ValidationError && error.Subcode == "Number1"));
        }

        private StandardKernel GetKernel(
            IAuditLog auditLogger,
            ILog logger,
            ISettings settings)
        {
            var kernel = new StandardKernel();

            kernel.Bind<ILog>().ToConstant(logger);
            kernel.Bind<IAuditLog>().ToConstant(auditLogger);
            kernel.Bind<ISettings>().ToConstant(settings);

            kernel.Bind<RequestProcessor>().ToConstant(new RequestProcessor(logger, auditLogger));

            return kernel;
        }
    }
}
