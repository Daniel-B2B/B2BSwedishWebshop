using Nop.Core;
using Nop.Plugin.Misc.GroupBuy.Domain;
using Nop.Plugin.Misc.GroupBuy.Models;

namespace Nop.Plugin.Misc.GroupBuy.Services
{
    public interface IGroupBuyService
    {
        IPagedList<GroupBuyProduct> GetAllGroupBuyProductList(int pageIndex = 0, int pageSize = int.MaxValue);        
        bool CreateProductQuantity(GroupBuyProduct productSellModel);
        GroupBuyProduct GetProductSellQuantityDetails(int id);
        void UpdateProductQunatityAndDates(GroupBuyProduct productSellModel);

        IPagedList<CustomTierPriceModel> GetAllTierPriceById(int id, int pageIndex = 0, int pageSize = int.MaxValue);
        void CreateTierPrice(CustomTierPriceModel customTierPrice);
        void UpdateTierPrice(CustomTierPriceModel customTierPrice);
        void DeleteTierPrice(int id);
        void DeleteProductDetails(int id);
        CustomTierPriceModel GetTierPriceById(int id);
        CustomTierPriceModel GetCustomTierPriceModel(int groupBuyId);
        FrontEndViewModel GetTIerPriceByProductId(int productId);
    }
}
