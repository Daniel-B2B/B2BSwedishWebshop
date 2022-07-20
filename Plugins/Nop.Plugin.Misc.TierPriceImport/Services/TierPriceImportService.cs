using Nop.Core;
using Nop.Core.Data;
using Nop.Core.Domain.Catalog;
using Nop.Data;
using Nop.Plugin.Misc.TierPriceImport.Models;
using Nop.Services.Catalog;
using Nop.Services.ExportImport;
using Nop.Services.ExportImport.Help;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.TierPriceImport.Services
{
    public class TierPriceImportService : ITierPriceImportService
    {
        #region Fields
        private readonly IProductService _productService;
        #endregion

        #region Ctor
        public TierPriceImportService(IProductService productService)
        {
            _productService = productService;            
        }
        #endregion


        #region Methods
        public virtual void ImportTierPriceFromXlscx(Stream stream)
        {
            using (var xlPackage = new ExcelPackage(stream))
            {
                // get the first worksheet in the workbook
                var worksheet = xlPackage.Workbook.Worksheets.FirstOrDefault();
                if (worksheet == null)
                    throw new NopException("No worksheet found");

                var properties = new[]
                   {
                        new PropertyByName<ImportedDataModel>("ParentSKU"),                     
                        new PropertyByName<ImportedDataModel>("Q1"),
                        new PropertyByName<ImportedDataModel>("Q2"),
                        new PropertyByName<ImportedDataModel>("Q3"),
                        new PropertyByName<ImportedDataModel>("Q4"),
                        new PropertyByName<ImportedDataModel>("Q5"),
                        new PropertyByName<ImportedDataModel>("Q6"),                       
                        new PropertyByName<ImportedDataModel>("P1"),
                        new PropertyByName<ImportedDataModel>("P2"),
                        new PropertyByName<ImportedDataModel>("P3"),
                        new PropertyByName<ImportedDataModel>("P4"),
                        new PropertyByName<ImportedDataModel>("P5"),
                        new PropertyByName<ImportedDataModel>("P6")
                    };
                var manager = new PropertyManager<ImportedDataModel>(properties);              

                var endRow = 2;    
            
                while (true)
                {
                    var allColumnsAreEmpty = manager.GetProperties
                        .Select(property => worksheet.Cells[endRow, property.PropertyOrderPosition])
                        .All(cell => cell == null || cell.Value == null || String.IsNullOrEmpty(cell.Value.ToString()));

                    if (allColumnsAreEmpty)
                        break;
                    manager.ReadFromXlsx(worksheet, endRow);

                    // All Quntities and Prices                 
                    var parentSku = manager.GetProperty("ParentSKU").StringValue;
                    var q1 = manager.GetProperty("Q1").IntValue;
                    var q2 = manager.GetProperty("Q2").IntValue;
                    var q3 = manager.GetProperty("Q3").IntValue;
                    var q4 = manager.GetProperty("Q4").IntValue;
                    var q5 = manager.GetProperty("Q5").IntValue;
                    var q6 = manager.GetProperty("Q6").IntValue;

                    var p1 = manager.GetProperty("P1").DecimalValue;
                    var p2 = manager.GetProperty("P2").DecimalValue;
                    var p3 = manager.GetProperty("P3").DecimalValue;
                    var p4 = manager.GetProperty("P4").DecimalValue;
                    var p5 = manager.GetProperty("P5").DecimalValue;
                    var p6 = manager.GetProperty("P6").DecimalValue;

                    var Product = _productService.GetProductBySku(parentSku);

                    if (Product != null)
                    {
                        //delete existing tier price
                        foreach (var t in Product.TierPrices.ToList())
                        {
                            _productService.DeleteTierPrice(t);
                        }
                        InsertNewTierPrice(Product.Id, q1, p1);
                        InsertNewTierPrice(Product.Id, q2, p2);
                        InsertNewTierPrice(Product.Id, q3, p3);
                        InsertNewTierPrice(Product.Id, q4, p4);
                        InsertNewTierPrice(Product.Id, q5, p5);
                        InsertNewTierPrice(Product.Id, q6, p6);                        
                    }
                    endRow++;
                }
            }
        }
        #endregion

        protected virtual IList<PropertyByName<T>> GetPropertiesByExcelCells<T>(ExcelWorksheet worksheet)
        {
            var properties = new List<PropertyByName<T>>();
            var poz = 1;
            while (true)
            {
                try
                {
                    var cell = worksheet.Cells[1, poz];

                    if (cell == null || cell.Value == null || string.IsNullOrEmpty(cell.Value.ToString()))
                        break;

                    poz += 1;
                    properties.Add(new PropertyByName<T>(cell.Value.ToString()));
                }
                catch
                {
                    break;
                }
            }

            return properties;
        }

        // Insert new tier prices

        public void InsertNewTierPrice(int ProductId, int Quantity,decimal Price)
        {
            if (Quantity > 0 && Price > decimal.Zero)
            {
                TierPrice tierPrice = new TierPrice();
                tierPrice.ProductId = ProductId;
                tierPrice.StoreId = 0;
                tierPrice.CustomerRole = null;
                tierPrice.Quantity = Quantity; // Convert.ToInt32(Quantity);
                // tierPrice.Price = Convert.ToDecimal(Price, new System.Globalization.CultureInfo("en-GB"));
                tierPrice.Price = Price;// Convert.ToDecimal(Price, CultureInfo.InvariantCulture);

                _productService.InsertTierPrice(tierPrice);
            }
        }
    }
}
