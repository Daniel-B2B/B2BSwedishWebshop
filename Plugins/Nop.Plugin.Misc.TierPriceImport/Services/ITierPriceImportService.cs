using System.IO;

namespace Nop.Plugin.Misc.TierPriceImport.Services
{
    public interface ITierPriceImportService
    {
        void ImportTierPriceFromXlscx(Stream stream);
    }
}
