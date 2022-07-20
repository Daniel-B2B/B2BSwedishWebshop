using FluentValidation;
using Nop.Data;
using Nop.Plugin.Misc.GroupBuy.Models;
using Nop.Services.Localization;
using Nop.Web.Framework.Validators;

namespace Nop.Plugin.Misc.GroupBuy.Validators
{
    public class GroupBuyValidator : BaseNopValidator<GroupBuyViewModel>
    {
        public GroupBuyValidator(ILocalizationService localizationService, IDbContext dbContext)
        {            
            RuleFor(x => x.Sku).NotEmpty().WithMessage("Please enter product Id!");
            RuleFor(x => x.StartDate).NotEmpty().WithMessage("Please eneter start date!");
            RuleFor(x => x.EndDate).NotEmpty().WithMessage("Please select end date !");
            RuleFor(x => x.EndDate).GreaterThanOrEqualTo(X => X.StartDate).WithMessage("End date must be greater or equals to start date!");
            SetStringPropertiesMaxLength<GroupBuyViewModel>(dbContext);
        }
    }
}
