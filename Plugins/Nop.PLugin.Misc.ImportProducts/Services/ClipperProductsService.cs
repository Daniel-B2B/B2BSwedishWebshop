using Nop.Core;
using Nop.Core.Data;
using Nop.Data;
using Nop.PLugin.Misc.ImportProducts.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nop.PLugin.Misc.ImportProducts.Services
{
    public class ClipperProductsService: IClipperProductsService
    {
        private readonly IRepository<CustomProduct> _customProductRepository;
        private readonly IDbContext _dbContext;

        public ClipperProductsService(
           IRepository<CustomProduct> customProductRepository, IDbContext dbContext)
        {
            this._customProductRepository = customProductRepository;
            this._dbContext = dbContext;
        }

        public virtual IPagedList<CustomProduct> GetAllProducts(int pageIndex = 0, int pageSize = int.MaxValue) {

            var query = from p in _customProductRepository.Table                        
                        select p;

            query = query.OrderByDescending(p => p.Id);
            //var products = query.ToList();
            var products = new PagedList<CustomProduct>(query, pageIndex, pageSize);

            return products;           
        }

        public virtual void InsertProduct(CustomProduct customProduct)
        {
            if (customProduct == null)
                throw new ArgumentNullException("Empty Product");

            _customProductRepository.Insert(customProduct);
        }

        public virtual void ClearTable()
        {
            _dbContext.ExecuteSqlCommand("delete from clipper_products");
        }

        public virtual int ExecuteNopActionProcedure(int countryId, int siteId, string database)
        {
           int res = _dbContext.ExecuteSqlCommand("EXEC [dbo].usp_NOPActions "+ countryId+ "," + siteId + ",'["+ database +"]'");

            return res;
        }
    }
}
