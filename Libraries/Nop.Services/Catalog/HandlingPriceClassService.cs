using Nop.Core.Data;
using Nop.Core.Domain.Catalog;
using Nop.Data;
using System;
using System.Linq;

namespace Nop.Services.Catalog
{
    public class HandlingPriceClassService : IHandlingPriceClassService
    {
        private readonly IRepository<CustomHandling> _customHandlingPriceClassRepository;
        private readonly IDbContext _dbContext;
        public HandlingPriceClassService(
           IRepository<CustomHandling> customHandlingPriceClassRepository, IDbContext dbContext)
        {
            this._customHandlingPriceClassRepository = customHandlingPriceClassRepository;
            this._dbContext = dbContext;
        }
       
        public virtual decimal GetHandlingPriceBasedOnQuantity(int quantity, string PriceClass)
        {
            var query = from c in _customHandlingPriceClassRepository.Table
                        where c.PriceClass == PriceClass
                        select c;
           // int CountryId = Convert.ToInt32(ConfigurationManager.AppSettings["CountryId"]);

            //var test = _dbContext.SqlQuery<CustomHandling>("select * from dbo.PriceClasses_Handling("+ CountryId +")").ToList();
            //test = test.Where(x => x.PriceClass == PriceClass).ToList();

            var hpClass = query.FirstOrDefault();// test.FirstOrDefault(); //query.FirstOrDefault();

            var selectedPrice = decimal.Zero;
            if (hpClass != null)
            {
                // decimal quantity1 = Convert.ToDecimal(quantity);
                selectedPrice = Convert.ToDecimal(hpClass.Price, new System.Globalization.CultureInfo("en-US"));
                selectedPrice = RoundingHelper.RoundPrice(selectedPrice);
            }

            selectedPrice =  selectedPrice * quantity;           

            return selectedPrice;
        }
    }
}
