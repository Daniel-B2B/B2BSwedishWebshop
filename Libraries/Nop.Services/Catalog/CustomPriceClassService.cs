using Nop.Core.Data;
using Nop.Core.Domain.Catalog;
using Nop.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nop.Services.Catalog
{
    public class CustomPriceClassService : ICustomPriceClassService
    {
        private readonly IRepository<CustomPriceClass> _customPriceClassRepository;
        private readonly IDbContext _dbContext;
        public CustomPriceClassService(
           IRepository<CustomPriceClass> customPriceClassRepository,IDbContext dbContext)
        {
            this._customPriceClassRepository = customPriceClassRepository;
            this._dbContext = dbContext;
        }

        public virtual IList<CustomPriceClass> GetAllPriceClass()
        {
            var query = _customPriceClassRepository.Table;
            var unsortedCategories = query.ToList();

            return unsortedCategories;
        }

        public virtual decimal GetPriceBasedOnQuantity(int quantity, string PriceClass, int pressColor = 0)
        {
            //var query = from c in _customPriceClassRepository.Table
            //            where c.PriceClass == PriceClass
            //            select c;

            // var pClass = query.ToList();

            //var selectedPrice = 0; // decimal.Zero;
            //foreach(var item in pClass)
            //{
            //    decimal quantity1 = Convert.ToDecimal(item.Quantity.Value);
            //    // decimal margin = !string.IsNullOrEmpty(item.Margin) ? Convert.ToDecimal(item.Margin): decimal.Zero;
            //    decimal margin = item.Margin>0 ? item.Margin.Value : decimal.Zero;
            //    if (quantity >= quantity1)
            //    {
            //        //var ccc = System.Globalization.CultureInfo.CurrentCulture;
            //        selectedPrice = Convert.ToDecimal(item.Price, new System.Globalization.CultureInfo("en-US"));
            //        selectedPrice = selectedPrice + (selectedPrice * margin / 100);
            //        selectedPrice = RoundingHelper.RoundPrice(selectedPrice);

            //        var pressQuantity = quantity * pressColor;
            //        selectedPrice = (pressQuantity * selectedPrice)/quantity;

            //        //break;
            //        //selectedPrice =  decimal.Round(selectedPrice, 2, MidpointRounding.AwayFromZero);
            //        //selectedPrice = selectedPrice * quantity;
            //    }
            //}
            decimal selectedPrice = 0;
            if (!string.IsNullOrEmpty(PriceClass))
            {
                // Has to do this to match the exact number. Cannot due switch, as this project doesn't reach the minimum requirements of switch case with bigger or lesser numbers.
                // The minimum C# switch case version with bigger or lesser numbers are 9.0

                if (quantity >= 1 && quantity <= 9)
                {
                    quantity = 1;
                }else if(quantity >= 10 && quantity <= 24 || quantity >= 1 && quantity <= 24)
                {
                    quantity = 10;
                }
                else if(quantity >=25 && quantity <= 54)
                {
                    quantity = 25;
                }else if(quantity >= 50 && quantity <= 99)
                {
                    quantity = 50;
                }
                else if (quantity >= 100 && quantity <= 249)
                {
                    quantity = 100;
                }
                else if (quantity >= 250 && quantity <= 499)
                {
                    quantity = 250;
                }
                else if (quantity >= 500 && quantity <= 999)
                {
                    quantity = 500;
                }
                else if (quantity >= 1000 && quantity <= 2499)
                {
                    quantity = 1000;
                }
                else if (quantity >= 2500 && quantity <= 4999)
                {
                    quantity = 2500;
                }
                else if (quantity >= 5000 && quantity <= 9999)
                {
                    quantity = 5000;
                }
                else if (quantity >= 10000 && quantity <= 19999)
                {
                    quantity = 10000;
                }
                else if (quantity >= 20000)
                {
                    quantity = 20000;
                }

                selectedPrice = _dbContext.SqlQuery<decimal>("select [dbo].[GetPrintCost]('" + PriceClass + "'," + quantity + "," + pressColor + ")").FirstOrDefault();
                selectedPrice = selectedPrice / quantity; //(pressColor * selectedPrice) / quantity;

            }
            return selectedPrice;
        }

        public virtual void DeletePriceClass(CustomPriceClass customPriceClass)
        {           
            _customPriceClassRepository.Delete(customPriceClass);
        }

        public virtual void InsertPriceClass(CustomPriceClass customPriceClass)
        {
            if (customPriceClass == null)
                throw new ArgumentNullException("Price Class");

            _customPriceClassRepository.Insert(customPriceClass);            
        }

        public virtual void UpdatePriceClass(CustomPriceClass customPriceClass)
        {
            if (customPriceClass == null)
                throw new ArgumentNullException("Price Class");
            _customPriceClassRepository.Update(customPriceClass);
        }        
    }
}
