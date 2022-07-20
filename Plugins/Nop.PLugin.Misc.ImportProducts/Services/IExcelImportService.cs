using System.IO;

namespace Nop.PLugin.Misc.ImportProducts.Services
{
    public interface IExcelImportService
    {
        void ImportProductsFromXlscx(Stream stream, int coutryId, int siteId, string database);
    }
}
