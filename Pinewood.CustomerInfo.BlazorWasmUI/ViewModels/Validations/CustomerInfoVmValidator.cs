using FluentValidation;
namespace Pinewood.CustomerInfo.BlazorWasmUI.ViewModels.Validatiors
{
    public class CustomerInfoVmValidator : AbstractValidator<CustomerInfoVm>
    {
		public CustomerInfoVmValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty();

            RuleFor(x => x.LastName)
                .NotEmpty();

            RuleFor(x => x.Email)
                .EmailAddress();

            RuleFor(x => x.Phone)
                .NotEmpty()
                .Length(10);

            RuleFor(x => x.Postcode)
                .MinimumLength(6)
                .MaximumLength(10);
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<CustomerInfoVm>.CreateWithOptions((CustomerInfoVm)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };

    }
}

