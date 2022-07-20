using Nop.Core;
using Nop.Plugin.Misc.ImportPriceClass.Domain;
using Nop.Plugin.Misc.ImportPriceClass.Models;
using Nop.Services.ExportImport.Help;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Nop.Plugin.Misc.ImportPriceClass.Services
{
    public class PriceClassImportService: IPriceClassImportService
    {
        #region Fields
       private readonly IPriceClassService _priceClassService;
        #endregion

        #region Ctor
        public PriceClassImportService(IPriceClassService priceClassService)
        {
            _priceClassService = priceClassService;
        }
        #endregion

        #region Methods
        //public virtual void ImportPriceClassFromXlscx(Stream stream)
        //{
        //    using (var xlPackage = new ExcelPackage(stream))
        //    {
        //        // get the first worksheet in the workbook
        //        var worksheet = xlPackage.Workbook.Worksheets.FirstOrDefault();
        //        if (worksheet == null)
        //            throw new NopException("No worksheet found");

        //        var properties = new[]
        //           {
        //                new PropertyByName<ImportDataModel>("PriceClass"),
        //                new PropertyByName<ImportDataModel>("Quantity"),
        //                new PropertyByName<ImportDataModel>("Price"),
        //                new PropertyByName<ImportDataModel>("Price2"),
        //                new PropertyByName<ImportDataModel>("Price3"),
        //                new PropertyByName<ImportDataModel>("Price4"),
        //                new PropertyByName<ImportDataModel>("Price5"),
        //                new PropertyByName<ImportDataModel>("Price6"),
        //                new PropertyByName<ImportDataModel>("Price7")                       
        //            };
        //        var manager = new PropertyManager<ImportDataModel>(properties);

        //        var endRow = 2;

        //        while (true)
        //        {
        //            var allColumnsAreEmpty = manager.GetProperties
        //                .Select(property => worksheet.Cells[endRow, property.PropertyOrderPosition])
        //                .All(cell => cell == null || cell.Value == null || String.IsNullOrEmpty(cell.Value.ToString()));

        //            if (allColumnsAreEmpty)
        //                break;
        //            manager.ReadFromXlsx(worksheet, endRow);

        //            // All Quntities and Prices                 
        //            var priceClass = manager.GetProperty("PriceClass").StringValue;
        //            var quanty = manager.GetProperty("Quantity").IntValue;
        //            var price = manager.GetProperty("Price").DecimalValue;
        //            var price2 = manager.GetProperty("Price2").DecimalValue;
        //            var price3 = manager.GetProperty("Price3").DecimalValue;
        //            var price4 = manager.GetProperty("Price4").DecimalValue;
        //            var price5 = manager.GetProperty("Price5").DecimalValue;
        //            var price6 = manager.GetProperty("Price6").DecimalValue;
        //            var price7 = manager.GetProperty("Price7").DecimalValue;


        //            var existingPriceClass = _priceClassService.GetExistingPriceClass(priceClass);
        //            if (existingPriceClass.Count() > 0)
        //            {
        //                foreach(var item in existingPriceClass)
        //                {
        //                    _priceClassService.DeletePriceClass(item);
        //                }
        //            }



        //           // var Product = _productService.GetProductBySku(parentSku);

        //            //if (Product != null)
        //            //{
        //            //delete existing tier price
        //            //foreach (var t in Product.TierPrices.ToList())
        //            //{
        //            //    _productService.DeleteTierPrice(t);
        //            //}
        //            //InsertNewTierPrice(Product.Id, q1, p1);
        //            //InsertNewTierPrice(Product.Id, q2, p2);
        //            //InsertNewTierPrice(Product.Id, q3, p3);
        //            //InsertNewTierPrice(Product.Id, q4, p4);
        //            //InsertNewTierPrice(Product.Id, q5, p5);
        //            //InsertNewTierPrice(Product.Id, q6, p6);
        //            //}
        //            endRow++;
        //        }
        //    }
        //}


        

        #endregion


    }
}
