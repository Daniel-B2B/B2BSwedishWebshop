using Nop.Core;
using Nop.Plugin.Misc.ImportPriceClass.Domain;
using System.Collections.Generic;

namespace Nop.Plugin.Misc.ImportPriceClass.Services
{
    public interface IPriceClassService
    {
        //List<ImprintPriceClass> GetExistingPriceClass(string priceClass);

        void DeletePriceClass(ImprintPriceClass priceClass);

        void InsertPriceClass(ImprintPriceClass priceClass);

        void UpdatePriceClass(ImprintPriceClass priceClass);

        ImprintPriceClass GetPriceClassById(int id);

        IPagedList<ImprintPriceClass> GetAllPriceClass(int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
