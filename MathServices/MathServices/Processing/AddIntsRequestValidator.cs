using FluentValidation;
using MathServices.Messages;

namespace MathServices.Processing
{
    class AddIntsRequestValidator : AbstractValidator<AddIntsRequest>
    {
        public AddIntsRequestValidator(ISettings settings)
        {
            RuleFor(request => request.Number1)
                .InclusiveBetween(settings.AddIntsMinNumber, settings.AddIntsMaxNumber);
            RuleFor(request => request.Number2)
                .InclusiveBetween(settings.AddIntsMinNumber, settings.AddIntsMaxNumber);
        }
    }
}
