using FluentValidation;
using Nop.Data;
using Nop.Plugin.Misc.GroupBuy.Models;
using Nop.Services.Localization;
using Nop.Web.Framework.Validators;

namespace Nop.Plugin.Misc.GroupBuy.Validators
{
    public class CustomTierPriceValidator : BaseNopValidator<CustomTierPriceModel>
    {
        public CustomTierPriceValidator(ILocalizationService localizationService, IDbContext dbContext)
        {
            RuleFor(x => x.Quantity).NotEmpty().WithMessage("Please enter quantity!");
            RuleFor(x => x.Quantity).GreaterThanOrEqualTo(1).WithMessage("Quantity must be greater than zero!");
            //RuleFor(x => x.Price).NotEmpty().WithMessage("Please enter price!");
           // RuleFor(x => x.Price).GreaterThan(0M).WithMessage("Price must be greater than zero!");
            SetStringPropertiesMaxLength<CustomTierPriceModel>(dbContext);
        }
    }
}
