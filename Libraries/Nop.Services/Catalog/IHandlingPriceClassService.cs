using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Catalog
{
    public interface IHandlingPriceClassService
    {
        decimal GetHandlingPriceBasedOnQuantity(int quantity, string PriceClass);

        //IList<CustomPriceClass> GetAllPriceClass();
    }
}
