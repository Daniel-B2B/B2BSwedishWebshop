using Nop.Core.Domain.Catalog;
using System.Collections.Generic;

namespace Nop.Services.Catalog
{
    public interface ICustomPriceClassService
    {
        decimal GetPriceBasedOnQuantity(int quantity, string PriceClass, int pressColor);

        IList<CustomPriceClass> GetAllPriceClass();

        void DeletePriceClass(CustomPriceClass customPriceClass);

        void InsertPriceClass(CustomPriceClass customPriceClass);

        void UpdatePriceClass(CustomPriceClass customPriceClass);

        //void SetPriceClassMargin(int margin);
    }
}
