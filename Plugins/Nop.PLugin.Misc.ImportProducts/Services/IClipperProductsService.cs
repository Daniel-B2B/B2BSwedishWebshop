using Nop.Core;
using Nop.PLugin.Misc.ImportProducts.Domain;
using System.Collections.Generic;

namespace Nop.PLugin.Misc.ImportProducts.Services
{
    public interface IClipperProductsService
    {
        void InsertProduct(CustomProduct customProduct);
        void ClearTable();
        int ExecuteNopActionProcedure(int countryId, int siteId, string database);
        IPagedList<CustomProduct> GetAllProducts(int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
