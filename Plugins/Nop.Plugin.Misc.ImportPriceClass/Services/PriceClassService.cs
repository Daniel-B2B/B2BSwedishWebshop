using Nop.Core;
using Nop.Core.Data;
using Nop.Data;
using Nop.Plugin.Misc.ImportPriceClass.Domain;
using System;
using System.Linq;

namespace Nop.Plugin.Misc.ImportPriceClass.Services
{
    public class PriceClassService : IPriceClassService
    {
        private readonly IRepository<ImprintPriceClass> _priceClassRepository;
        private readonly IDbContext _dbContext;

        public PriceClassService(
           IRepository<ImprintPriceClass> customPriceClassRepository, IDbContext dbContext)
        {
            this._priceClassRepository = customPriceClassRepository;
            this._dbContext = dbContext;
        }


        public IPagedList<ImprintPriceClass> GetAllPriceClass(int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _priceClassRepository.Table;
                        

            query = query.OrderByDescending(p => p.Id);
            var priceClasses1 = query.ToList();
            var priceClasses = new PagedList<ImprintPriceClass>(priceClasses1, pageIndex, pageSize);

            return priceClasses;
        }
        //public List<ImprintPriceClass> GetExistingPriceClass(string priceClass)
        //{
        //    if (string.IsNullOrEmpty(priceClass))
        //    {
        //        throw new ArgumentNullException("Invalid Price class");
        //    }
        //    var query = _priceClassRepository.Table;
        //    query = query.Where(c => c.PriceClass.ToUpper() == priceClass.ToUpper());

        //    return query.ToList();
        //}

        public ImprintPriceClass GetPriceClassById(int id)
        {
            if (id == 0)
                return null;
           
            return  _priceClassRepository.GetById(id);
        }

        public void DeletePriceClass(ImprintPriceClass priceClass)
        {
            if (priceClass == null)
                throw new ArgumentNullException("Invalid Price class");
            _priceClassRepository.Delete(priceClass);
        }

        public void InsertPriceClass(ImprintPriceClass priceClass)
        {
            if (priceClass == null)
                throw new ArgumentNullException("Invalid Price Class");            
            _priceClassRepository.Insert(priceClass);
        }

        public void UpdatePriceClass(ImprintPriceClass priceClass)
        {
            if (priceClass == null)
                throw new ArgumentNullException("priceClass");
           
            _priceClassRepository.Update(priceClass);            
        }
    }
}
