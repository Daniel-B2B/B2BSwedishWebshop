using Nop.Core;
using Nop.Core.Data;
using Nop.PLugin.Misc.ImportProducts.Domain;
using Nop.PLugin.Misc.ImportProducts.Models;
using Nop.Services.Catalog;
using Nop.Services.ExportImport.Help;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Nop.PLugin.Misc.ImportProducts.Services
{
    public class ExcelImportService : IExcelImportService
    {
        #region Fields
        private readonly IClipperProductsService _clipperProductsService;
        private readonly IProductService _productService;
        #endregion

        #region Ctor
        public ExcelImportService( IProductService productService, IClipperProductsService clipperProductsService)
        {
            _clipperProductsService = clipperProductsService;
            _productService = productService;
        }
        #endregion
        
        #region Methods
        public virtual void ImportProductsFromXlscx(Stream stream, int countryId, int siteId, string database)
        {          
            using (var xlPackage = new ExcelPackage(stream))
            {
                // get the first worksheet in the workbook
                var worksheet = xlPackage.Workbook.Worksheets.FirstOrDefault();
                if (worksheet == null)
                    throw new NopException("No worksheet found");

                var properties = new[]
                   {
                       new PropertyByName<CustomProductModel>("ParentSKU"),
                       new PropertyByName<CustomProductModel>("SKU"),
                       new PropertyByName<CustomProductModel>("ColorName"),
                       new PropertyByName<CustomProductModel>("InternetName"),
                       new PropertyByName<CustomProductModel>("InternetTxt"),                       
                       new PropertyByName<CustomProductModel>("Q1"),
                       new PropertyByName<CustomProductModel>("Q2"),
                       new PropertyByName<CustomProductModel>("Q3"),
                       new PropertyByName<CustomProductModel>("Q4"),
                       new PropertyByName<CustomProductModel>("Q5"),
                       new PropertyByName<CustomProductModel>("Q6"),
                       new PropertyByName<CustomProductModel>("P1"),
                       new PropertyByName<CustomProductModel>("P2"),
                       new PropertyByName<CustomProductModel>("P3"),
                       new PropertyByName<CustomProductModel>("P4"),
                       new PropertyByName<CustomProductModel>("P5"),
                       new PropertyByName<CustomProductModel>("P6"),
                       new PropertyByName<CustomProductModel>("GrossNett"),

                       new PropertyByName<CustomProductModel>("ImprintType_IL1_T1"),   
                       new PropertyByName<CustomProductModel>("ImprintDescription_IL1_T1"),
                       new PropertyByName<CustomProductModel>("ImprintImage_IL1_T1"),
                       new PropertyByName<CustomProductModel>("PriceClass_IL1_T1"),
                       new PropertyByName<CustomProductModel>("PriceClass_IL1_CN"),
                       new PropertyByName<CustomProductModel>("SetupFee_IL1_T1"),
                       new PropertyByName<CustomProductModel>("SetupFee_IL1_T1_NextColor"),
                       new PropertyByName<CustomProductModel>("SetupFeeStructure_IL1_T1"),
                       new PropertyByName<CustomProductModel>("ExcludeFreeSetupCosts_IL1_T1"),
                       new PropertyByName<CustomProductModel>("ImprintSize_IL1_T1"),
                       new PropertyByName<CustomProductModel>("MaxColors_IL1_T1"),
                       new PropertyByName<CustomProductModel>("Handling_IL1_T1"),

                       new PropertyByName<CustomProductModel>("ImprintType_IL1_T2"),
                       new PropertyByName<CustomProductModel>("ImprintDescription_IL1_T2"),
                       new PropertyByName<CustomProductModel>("ImprintImage_IL1_T2"),
                       new PropertyByName<CustomProductModel>("PriceClass_IL1_T2"),
                       new PropertyByName<CustomProductModel>("PriceClass_IL1_CN2"), //Wrong
                       new PropertyByName<CustomProductModel>("SetupFee_IL1_T2"),
                       new PropertyByName<CustomProductModel>("SetupFee_IL1_T2_NextColor"),
                       new PropertyByName<CustomProductModel>("SetupFeeStructure_IL1_T2"),
                       new PropertyByName<CustomProductModel>("ExcludeFreeSetupCosts_IL1_T2"),
                       new PropertyByName<CustomProductModel>("ImprintSize_IL1_T2"),
                       new PropertyByName<CustomProductModel>("MaxColors_IL1_T2"),
                       new PropertyByName<CustomProductModel>("Handling_IL1_T2"),

                       new PropertyByName<CustomProductModel>("ImprintType_IL1_T3"),
                       new PropertyByName<CustomProductModel>("ImprintDescription_IL1_T3"),
                       new PropertyByName<CustomProductModel>("ImprintImage_IL1_T3"),
                       new PropertyByName<CustomProductModel>("PriceClass_IL1_T3"),
                       new PropertyByName<CustomProductModel>("PriceClass_IL1_CN3"), //Wrong
                       new PropertyByName<CustomProductModel>("SetupFee_IL1_T3"),
                       new PropertyByName<CustomProductModel>("SetupFee_IL1_T3_NextColor"),
                       new PropertyByName<CustomProductModel>("SetupFeeStructure_IL1_T3"),
                       new PropertyByName<CustomProductModel>("ExcludeFreeSetupCosts_IL1_T3"),
                       new PropertyByName<CustomProductModel>("ImprintSize_IL1_T3"),
                       new PropertyByName<CustomProductModel>("MaxColors_IL1_T3"),
                       new PropertyByName<CustomProductModel>("Handling_IL1_T3"),

                       new PropertyByName<CustomProductModel>("ImprintType_IL1_T4"),
                       new PropertyByName<CustomProductModel>("ImprintDescription_IL1_T4"),
                       new PropertyByName<CustomProductModel>("ImprintImage_IL1_T4"),
                       new PropertyByName<CustomProductModel>("PriceClass_IL1_T4"),
                       new PropertyByName<CustomProductModel>("PriceClass_IL1_CN4"), //Wrong
                       new PropertyByName<CustomProductModel>("SetupFee_IL1_T4"),
                       new PropertyByName<CustomProductModel>("SetupFee_IL1_T4_NextColor"),
                       new PropertyByName<CustomProductModel>("SetupFeeStructure_IL1_T4"),
                       new PropertyByName<CustomProductModel>("ExcludeFreeSetupCosts_IL1_T4"),
                       new PropertyByName<CustomProductModel>("ImprintSize_IL1_T4"),
                       new PropertyByName<CustomProductModel>("MaxColors_IL1_T4"),
                       new PropertyByName<CustomProductModel>("Handling_IL1_T4"),

                       new PropertyByName<CustomProductModel>("ImprintType_IL1_T5"),
                       new PropertyByName<CustomProductModel>("ImprintDescription_IL1_T5"),
                       new PropertyByName<CustomProductModel>("ImprintImage_IL1_T5"),
                       new PropertyByName<CustomProductModel>("PriceClass_IL1_T5"),
                       new PropertyByName<CustomProductModel>("PriceClass_IL1_CN5"), //Wrong
                       new PropertyByName<CustomProductModel>("SetupFee_IL1_T5"),
                       new PropertyByName<CustomProductModel>("SetupFee_IL1_T5_NextColor"),
                       new PropertyByName<CustomProductModel>("SetupFeeStructure_IL1_T5"),
                       new PropertyByName<CustomProductModel>("ExcludeFreeSetupCosts_IL1_T5"),
                       new PropertyByName<CustomProductModel>("ImprintSize_IL1_T5"),
                       new PropertyByName<CustomProductModel>("MaxColors_IL1_T5"),
                       new PropertyByName<CustomProductModel>("Handling_IL1_T5"),

                        new PropertyByName<CustomProductModel>("ImprintType_IL1_T6"),
                       new PropertyByName<CustomProductModel>("ImprintDescription_IL1_T6"),
                       new PropertyByName<CustomProductModel>("ImprintImage_IL1_T6"),
                       new PropertyByName<CustomProductModel>("PriceClass_IL1_T6"),
                       new PropertyByName<CustomProductModel>("PriceClass_IL1_CN6"), //Wrong
                       new PropertyByName<CustomProductModel>("SetupFee_IL1_T6"),
                       new PropertyByName<CustomProductModel>("SetupFee_IL1_T6_NextColor"),
                       new PropertyByName<CustomProductModel>("SetupFeeStructure_IL1_T6"),
                       new PropertyByName<CustomProductModel>("ExcludeFreeSetupCosts_IL1_T6"),
                       new PropertyByName<CustomProductModel>("ImprintSize_IL1_T6"),
                       new PropertyByName<CustomProductModel>("MaxColors_IL1_T6"),
                       new PropertyByName<CustomProductModel>("Handling_IL1_T6"),

                       new PropertyByName<CustomProductModel>("IntraCode"),
                       new PropertyByName<CustomProductModel>("CountryOfOrigin"),
                       new PropertyByName<CustomProductModel>("OuterCartonPieces"),
                       new PropertyByName<CustomProductModel>("OuterCartonLength"),
                       new PropertyByName<CustomProductModel>("OuterCartonWidth"),
                       new PropertyByName<CustomProductModel>("OuterCartonHeight"),
                       new PropertyByName<CustomProductModel>("CommercialItemLength"),
                       new PropertyByName<CustomProductModel>("CommercialItemWidth"),
                       new PropertyByName<CustomProductModel>("CommercialItemHeight"),
                       new PropertyByName<CustomProductModel>("CommercialItemWeight"),
                       new PropertyByName<CustomProductModel>("Flavours"),
                       new PropertyByName<CustomProductModel>("Sizes"),
                       new PropertyByName<CustomProductModel>("WritingColor"),
                       new PropertyByName<CustomProductModel>("CapacityTxt"),
                       new PropertyByName<CustomProductModel>("OrderUnit"),
                       new PropertyByName<CustomProductModel>("MainGroup"),
                       new PropertyByName<CustomProductModel>("Material"),
                       new PropertyByName<CustomProductModel>("BatteryType"),
                       new PropertyByName<CustomProductModel>("NumberOfBatteries"),
                       new PropertyByName<CustomProductModel>("ProductImageURL"),
                       new PropertyByName<CustomProductModel>("DeliveryTimeMT_IL1_T1"),
                       new PropertyByName<CustomProductModel>("07_12_SearchTerms"),
                       new PropertyByName<CustomProductModel>("Q_OnPallet"),
                       new PropertyByName<CustomProductModel>("Brandnames")
                };
                var manager = new PropertyManager<CustomProductModel>(properties);

                var endRow = 2;

                //Clear the table
                _clipperProductsService.ClearTable();

                while (true)
                {
                    var allColumnsAreEmpty = manager.GetProperties
                        .Select(property => worksheet.Cells[endRow, property.PropertyOrderPosition])
                        .All(cell => cell == null || cell.Value == null || String.IsNullOrEmpty(cell.Value.ToString()));

                    if (allColumnsAreEmpty)
                        break;
                    manager.ReadFromXlsx(worksheet, endRow);

                    //var CountryId = countryId;
                    //var ParentSKU = manager.GetProperty("ParentSKU").StringValue;
                    //var SKU = manager.GetProperty("SKU").StringValue;
                    //var InternetName = manager.GetProperty("InternetName").StringValue;
                    //var InternetTxt = manager.GetProperty("InternetTxt").StringValue;
                    //var ImprintLocation_IL1_T1 = manager.GetProperty("ImprintLocation_IL1_T1").StringValue;
                    ////var ImprintSize_IL1_T1 = manager.GetProperty("ImprintSize_IL1_T1").StringValue;
                    ////var ImprintType_IL1_T1 = manager.GetProperty("ImprintType_IL1_T1").StringValue;
                    //var Q1 = manager.GetProperty("Q1").IntValue;
                    //var Q2 = manager.GetProperty("Q2").IntValue;
                    //var Q3 = manager.GetProperty("Q3").IntValue;
                    //var Q4 = manager.GetProperty("Q4").IntValue;
                    //var Q5 = manager.GetProperty("Q5").IntValue;
                    //var Q6 = manager.GetProperty("Q6").IntValue;
                    //var P1 = manager.GetProperty("P1").DecimalValue;
                    //var P2 = manager.GetProperty("P2").DecimalValue;
                    //var P3 = manager.GetProperty("P3").DecimalValue;
                    //var P4 = manager.GetProperty("P4").DecimalValue;
                    //var P5 = manager.GetProperty("P5").DecimalValue;
                    //var P6 = manager.GetProperty("P6").DecimalValue;
                    //var GrossNett = manager.GetProperty("GrossNett").StringValue;
                   

                    //var ImprintType_IL1_T1 = manager.GetProperty("ImprintType_IL1_T1").StringValue;
                    //var ImprintDescription_IL1_T1 = manager.GetProperty("ImprintDescription_IL1_T1").StringValue;
                    //var ImprintImage_IL1_T1 = manager.GetProperty("ImprintImage_IL1_T1").DecimalValue;
                    //var PriceClass_IL1_CN = manager.GetProperty("PriceClass_IL1_CN").StringValue;
                    //var SetupFee_IL1_T1 = manager.GetProperty("SetupFee_IL1_T1").StringValue;
                    //var SetupFee_IL1_T1_NextColor = manager.GetProperty("SetupFee_IL1_T1_NextColor").StringValue;
                    //var SetupFeeStructure_IL1_T1 = manager.GetProperty("SetupFeeStructure_IL1_T1").StringValue;
                    //var ExcludeFreeSetupCosts_IL1_T1 = manager.GetProperty("ExcludeFreeSetupCosts_IL1_T1").StringValue;
                    //var ImprintSize_IL1_T1 = manager.GetProperty("ImprintSize_IL1_T1").StringValue;
                    //var MaxColors_IL1_T1 = manager.GetProperty("MaxColors_IL1_T1").StringValue;
                    //var Handling_IL1_T1 = manager.GetProperty("Handling_IL1_T1").StringValue;

                    //var ImprintType_IL1_T2 = manager.GetProperty("ImprintType_IL1_T2").StringValue;
                    //var ImprintDescription_IL1_T2 = manager.GetProperty("ImprintDescription_IL1_T2").StringValue;
                    //var ImprintImage_IL1_T2 = manager.GetProperty("ImprintImage_IL1_T2").DecimalValue;
                    //var PriceClass_IL1_CN2 = manager.GetProperty("PriceClass_IL1_CN2").StringValue;
                    //var SetupFee_IL1_T2 = manager.GetProperty("SetupFee_IL1_T2").StringValue;
                    //var SetupFee_IL1_T2_NextColor = manager.GetProperty("SetupFee_IL1_T2_NextColor").StringValue;
                    //var SetupFeeStructure_IL1_T2 = manager.GetProperty("SetupFeeStructure_IL1_T2").StringValue;
                    //var ExcludeFreeSetupCosts_IL1_T2 = manager.GetProperty("ExcludeFreeSetupCosts_IL1_T2").StringValue;
                    //var ImprintSize_IL1_T2 = manager.GetProperty("ImprintSize_IL1_T2").StringValue;
                    //var MaxColors_IL1_T2 = manager.GetProperty("MaxColors_IL1_T2").StringValue;
                    //var Handling_IL1_T2 = manager.GetProperty("Handling_IL1_T2").StringValue;


                    //var MainGroup = manager.GetProperty("MainGroup").StringValue;
                    //var Material = manager.GetProperty("Material").StringValue;
                    //var MaxColors_IL1_T1 = manager.GetProperty("MaxColors_IL1_T1").IntValue;

                    //var BatteryType = manager.GetProperty("BatteryType").StringValue;
                    //var CapacityTxt = manager.GetProperty("CapacityTxt").StringValue;
                    //var ColorName = manager.GetProperty("ColorName").StringValue;
                    //var CommercialItemHeight = manager.GetProperty("CommercialItemHeight").DecimalValue;
                    //var CommercialItemLength = manager.GetProperty("CommercialItemLength").DecimalValue;
                    //var CommercialItemWeight = manager.GetProperty("CommercialItemWeight").DecimalValue;
                    //var CommercialItemWidth = manager.GetProperty("CommercialItemWidth").DecimalValue;
                    //var CountryOfOrigin = manager.GetProperty("CountryOfOrigin").StringValue;
                    //var DeliveryTimeMT_IL1_T1 = manager.GetProperty("DeliveryTimeMT_IL1_T1").StringValue;
                    //var Flavours = manager.GetProperty("Flavours").StringValue;
                    //var Handling_IL1_T1 = manager.GetProperty("Handling_IL1_T1").StringValue;
                    //var IntraCode = manager.GetProperty("IntraCode").StringValue;
                    //var NumberOfBatteries = manager.GetProperty("NumberOfBatteries").IntValue;
                    //var OrderUnit = manager.GetProperty("OrderUnit").StringValue;
                    //var OuterCartonHeight = manager.GetProperty("OuterCartonHeight").DecimalValue;
                    //var OuterCartonLength = manager.GetProperty("OuterCartonLength").DecimalValue;
                    //var OuterCartonPieces = manager.GetProperty("OuterCartonPieces").DecimalValue;
                    //var OuterCartonWidth = manager.GetProperty("OuterCartonWidth").DecimalValue;
                    //var Sizes = manager.GetProperty("Sizes").StringValue;
                    //var WritingColor = manager.GetProperty("WritingColor").StringValue;
                    //var ProductImageURL = manager.GetProperty("ProductImageURL").StringValue;




                    var customProduct = new CustomProduct
                    {
                        CountryId = countryId,
                        ParentSKU = manager.GetProperty("ParentSKU").StringValue,
                        SKU = manager.GetProperty("SKU").StringValue,
                        ColorName = manager.GetProperty("ColorName").StringValue,
                        InternetName = manager.GetProperty("InternetName").StringValue,
                        InternetTxt = manager.GetProperty("InternetTxt").StringValue,                   
                        Q1 = manager.GetProperty("Q1").DoubleValue,
                        Q2 = manager.GetProperty("Q2").DoubleValue,
                        Q3 = manager.GetProperty("Q3").DoubleValue,
                        Q4 = manager.GetProperty("Q4").DoubleValue,
                        Q5 = manager.GetProperty("Q5").DoubleValue,
                        Q6 = manager.GetProperty("Q6").DoubleValue,
                        P1 = manager.GetProperty("P1").DoubleValue,
                        P2 = manager.GetProperty("P2").DoubleValue,
                        P3 = manager.GetProperty("P3").DoubleValue,
                        P4 = manager.GetProperty("P4").DoubleValue,
                        P5 = manager.GetProperty("P5").DoubleValue,
                        P6 = manager.GetProperty("P6").DoubleValue,
                        GrossNett = manager.GetProperty("GrossNett").StringValue,

                        ImprintType_IL1_T1 = manager.GetProperty("ImprintType_IL1_T1").StringValue,
                        ImprintLocation_IL1_T1 = manager.GetProperty("ImprintDescription_IL1_T1").StringValue,
                        ImprintImage_IL1_T1 = manager.GetProperty("ImprintImage_IL1_T1").StringValue,
                        PriceClass_IL1_T1 = manager.GetProperty("PriceClass_IL1_T1").StringValue,
                        PriceClass_IL1_CN = manager.GetProperty("PriceClass_IL1_CN").StringValue,
                        SetupFee_IL1_T1 = manager.GetProperty("SetupFee_IL1_T1").DoubleValue,
                        SetupFee_IL1_NextColor = manager.GetProperty("SetupFee_IL1_T1_NextColor").DoubleValue,
                        SetupFeeStructure = manager.GetProperty("SetupFeeStructure_IL1_T1").StringValue,
                        ExcludeFreeSetupCosts = manager.GetProperty("ExcludeFreeSetupCosts_IL1_T1").StringValue,
                        ImprintSize_IL1_T1 = manager.GetProperty("ImprintSize_IL1_T1").StringValue,
                        MaxColors_IL1_T1 = manager.GetProperty("MaxColors_IL1_T1").DoubleValue,
                        Handling_IL1_T1 = manager.GetProperty("Handling_IL1_T1").StringValue,

                        ImprintType_IL1_T2 = manager.GetProperty("ImprintType_IL1_T2").StringValue,
                        ImprintLocation_IL1_T2 = manager.GetProperty("ImprintDescription_IL1_T2").StringValue,
                        ImprintImage_IL1_T2 = manager.GetProperty("ImprintImage_IL1_T2").StringValue,
                        PriceClass_IL1_T2 = manager.GetProperty("PriceClass_IL1_T2").StringValue,
                        PriceClass_IL1_CN2 = manager.GetProperty("PriceClass_IL1_CN2").StringValue,
                        SetupFee_IL1_T2 = manager.GetProperty("SetupFee_IL1_T2").DoubleValue,
                        SetupFee_IL1_NextColor2 = manager.GetProperty("SetupFee_IL1_T2_NextColor").DoubleValue,
                        SetupFeeStructure2 = manager.GetProperty("SetupFeeStructure_IL1_T2").StringValue,
                        ExcludeFreeSetupCosts2 = manager.GetProperty("ExcludeFreeSetupCosts_IL1_T2").StringValue,
                        ImprintSize_IL1_T2 = manager.GetProperty("ImprintSize_IL1_T2").StringValue,
                        MaxColors_IL1_T2 = manager.GetProperty("MaxColors_IL1_T2").DoubleValue,
                        Handling_IL1_T2 = manager.GetProperty("Handling_IL1_T2").StringValue,

                        ImprintType_IL1_T3 = manager.GetProperty("ImprintType_IL1_T3").StringValue,
                        ImprintLocation_IL1_T3 = manager.GetProperty("ImprintDescription_IL1_T3").StringValue,
                        ImprintImage_IL1_T3 = manager.GetProperty("ImprintImage_IL1_T3").StringValue,
                        PriceClass_IL1_T3 = manager.GetProperty("PriceClass_IL1_T3").StringValue,
                        PriceClass_IL1_CN3 = manager.GetProperty("PriceClass_IL1_CN3").StringValue,
                        SetupFee_IL1_T3 = manager.GetProperty("SetupFee_IL1_T3").DoubleValue,
                        SetupFee_IL1_NextColor3 = manager.GetProperty("SetupFee_IL1_T3_NextColor").DoubleValue,
                        SetupFeeStructure3 = manager.GetProperty("SetupFeeStructure_IL1_T3").StringValue,
                        ExcludeFreeSetupCosts3 = manager.GetProperty("ExcludeFreeSetupCosts_IL1_T3").StringValue,
                        ImprintSize_IL1_T3 = manager.GetProperty("ImprintSize_IL1_T3").StringValue,
                        MaxColors_IL1_T3 = manager.GetProperty("MaxColors_IL1_T3").DoubleValue,
                        Handling_IL1_T3 = manager.GetProperty("Handling_IL1_T3").StringValue,

                        ImprintType_IL1_T4 = manager.GetProperty("ImprintType_IL1_T4").StringValue,
                        ImprintLocation_IL1_T4 = manager.GetProperty("ImprintDescription_IL1_T4").StringValue,
                        ImprintImage_IL1_T4 = manager.GetProperty("ImprintImage_IL1_T4").StringValue,
                        PriceClass_IL1_T4 = manager.GetProperty("PriceClass_IL1_T4").StringValue,
                        PriceClass_IL1_CN4 = manager.GetProperty("PriceClass_IL1_CN4").StringValue,
                        SetupFee_IL1_T4 = manager.GetProperty("SetupFee_IL1_T4").DoubleValue,
                        SetupFee_IL1_NextColor4 = manager.GetProperty("SetupFee_IL1_T4_NextColor").DoubleValue,
                        SetupFeeStructure4 = manager.GetProperty("SetupFeeStructure_IL1_T4").StringValue,
                        ExcludeFreeSetupCosts4 = manager.GetProperty("ExcludeFreeSetupCosts_IL1_T4").StringValue,
                        ImprintSize_IL1_T4 = manager.GetProperty("ImprintSize_IL1_T4").StringValue,
                        MaxColors_IL1_T4 = manager.GetProperty("MaxColors_IL1_T4").DoubleValue,
                        Handling_IL1_T4 = manager.GetProperty("Handling_IL1_T4").StringValue,

                        ImprintType_IL1_T5 = manager.GetProperty("ImprintType_IL1_T5").StringValue,
                        ImprintLocation_IL1_T5 = manager.GetProperty("ImprintDescription_IL1_T5").StringValue,
                        ImprintImage_IL1_T5 = manager.GetProperty("ImprintImage_IL1_T5").StringValue,
                        PriceClass_IL1_T5 = manager.GetProperty("PriceClass_IL1_T5").StringValue,
                        PriceClass_IL1_CN5 = manager.GetProperty("PriceClass_IL1_CN5").StringValue,
                        SetupFee_IL1_T5 = manager.GetProperty("SetupFee_IL1_T5").DoubleValue,
                        SetupFee_IL1_NextColor5 = manager.GetProperty("SetupFee_IL1_T5_NextColor").DoubleValue,
                        SetupFeeStructure5 = manager.GetProperty("SetupFeeStructure_IL1_T5").StringValue,
                        ExcludeFreeSetupCosts5 = manager.GetProperty("ExcludeFreeSetupCosts_IL1_T5").StringValue,
                        ImprintSize_IL1_T5 = manager.GetProperty("ImprintSize_IL1_T5").StringValue,
                        MaxColors_IL1_T5 = manager.GetProperty("MaxColors_IL1_T5").DoubleValue,
                        Handling_IL1_T5 = manager.GetProperty("Handling_IL1_T5").StringValue,

                        ImprintType_IL1_T6 = manager.GetProperty("ImprintType_IL1_T6").StringValue,
                        ImprintLocation_IL1_T6 = manager.GetProperty("ImprintDescription_IL1_T6").StringValue,
                        ImprintImage_IL1_T6 = manager.GetProperty("ImprintImage_IL1_T6").StringValue,
                        PriceClass_IL1_T6 = manager.GetProperty("PriceClass_IL1_T6").StringValue,
                        PriceClass_IL1_CN6 = manager.GetProperty("PriceClass_IL1_CN6").StringValue,
                        SetupFee_IL1_T6 = manager.GetProperty("SetupFee_IL1_T6").DoubleValue,
                        SetupFee_IL1_NextColor6 = manager.GetProperty("SetupFee_IL1_T6_NextColor").DoubleValue,
                        SetupFeeStructure6 = manager.GetProperty("SetupFeeStructure_IL1_T6").StringValue,
                        ExcludeFreeSetupCosts6 = manager.GetProperty("ExcludeFreeSetupCosts_IL1_T6").StringValue,
                        ImprintSize_IL1_T6 = manager.GetProperty("ImprintSize_IL1_T6").StringValue,
                        MaxColors_IL1_T6 = manager.GetProperty("MaxColors_IL1_T6").DoubleValue,
                        Handling_IL1_T6 = manager.GetProperty("Handling_IL1_T6").StringValue,

                        IntraCode = manager.GetProperty("IntraCode").StringValue,
                        CountryOfOrigin = manager.GetProperty("CountryOfOrigin").StringValue,
                        OuterCartonPieces = manager.GetProperty("OuterCartonPieces").DoubleValue,
                        OuterCartonLength = manager.GetProperty("OuterCartonLength").DoubleValue,
                        OuterCartonWidth = manager.GetProperty("OuterCartonWidth").DoubleValue,
                        OuterCartonHeight = manager.GetProperty("OuterCartonHeight").DoubleValue,
                        CommercialItemHeight = manager.GetProperty("CommercialItemHeight").DoubleValue,
                        CommercialItemLength = manager.GetProperty("CommercialItemLength").DoubleValue,
                        CommercialItemWeight = manager.GetProperty("CommercialItemWeight").DoubleValue,
                        CommercialItemWidth = manager.GetProperty("CommercialItemWidth").DoubleValue,
                        Flavours = manager.GetProperty("Flavours").StringValue,
                        Sizes = manager.GetProperty("Sizes").StringValue,
                        WritingColor = manager.GetProperty("WritingColor").StringValue,
                        CapacityTxt = manager.GetProperty("CapacityTxt").StringValue,
                        OrderUnit = manager.GetProperty("OrderUnit").StringValue,
                        MainGroup = manager.GetProperty("MainGroup").StringValue,
                        Material = manager.GetProperty("Material").StringValue,
                        BatteryType = manager.GetProperty("BatteryType").StringValue,
                        NumberOfBatteries = manager.GetProperty("NumberOfBatteries").IntValue,
                        ProductImageURL = manager.GetProperty("ProductImageURL").StringValue,
                        DeliveryTimeMT_IL1_T1 = manager.GetProperty("DeliveryTimeMT_IL1_T1").StringValue,
                        Q4_Q_OnPallet = manager.GetProperty("Q_OnPallet").StringValue
                    };

                    _clipperProductsService.InsertProduct(customProduct);
                    
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

        //public void InsertNewTierPrice(int ProductId, int Quantity, decimal Price)
        //{
        //    if (Quantity > 0 && Price > decimal.Zero)
        //    {
        //        TierPrice tierPrice = new TierPrice();
        //        tierPrice.ProductId = ProductId;
        //        tierPrice.StoreId = 0;
        //        tierPrice.CustomerRole = null;
        //        tierPrice.Quantity = Quantity; // Convert.ToInt32(Quantity);
        //        // tierPrice.Price = Convert.ToDecimal(Price, new System.Globalization.CultureInfo("en-GB"));
        //        tierPrice.Price = Price;// Convert.ToDecimal(Price, CultureInfo.InvariantCulture);

        //        _productService.InsertTierPrice(tierPrice);
        //    }
        //}
    }
}


