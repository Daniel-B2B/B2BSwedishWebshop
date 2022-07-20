using Nop.Core;

namespace Nop.PLugin.Misc.ImportProducts.Domain
{
    public class CustomProduct : BaseEntity
    {        
        public int CountryId { get; set; }
        public string ParentSKU { get; set; }
        public string SKU { get; set; }
        public string ColorName { get; set; }
        public string InternetName { get; set; }
        public string InternetTxt { get; set; }       
        public   double? Q1 { get; set; }
        public   double? Q2 { get; set; }
        public  double? Q3 { get; set; }
        public  double? Q4 { get; set; }
        public  double? Q5 { get; set; }
        public  double? Q6 { get; set; }
        public  double? P1 { get; set; }
        public  double? P2 { get; set; }
        public  double? P3 { get; set; }
        public  double? P4 { get; set; }
        public  double? P5 { get; set; }
        public  double? P6 { get; set; }
        public string GrossNett { get; set; }

        public string ImprintType_IL1_T1 { get; set; }
        public string ImprintLocation_IL1_T1 { get; set; }
        public string ImprintImage_IL1_T1 { get; set; }
        public string PriceClass_IL1_T1 { get; set; }
        public string PriceClass_IL1_CN { get; set; }
        public  double? SetupFee_IL1_T1 { get; set; }
        public  double? SetupFee_IL1_NextColor { get; set; }
        public string SetupFeeStructure { get; set; }
        public string ExcludeFreeSetupCosts { get; set; }
        public string ImprintSize_IL1_T1 { get; set; }
        public  double? MaxColors_IL1_T1 { get; set; }
        public string Handling_IL1_T1 { get; set; }

        public string ImprintType_IL1_T2 { get; set; }
        public string ImprintLocation_IL1_T2 { get; set; }
        public string ImprintImage_IL1_T2 { get; set; }
        public string PriceClass_IL1_T2 { get; set; }
        public string PriceClass_IL1_CN2 { get; set; }
        public  double? SetupFee_IL1_T2 { get; set; }
        public  double? SetupFee_IL1_NextColor2 { get; set; }
        public string SetupFeeStructure2 { get; set; }
        public string ExcludeFreeSetupCosts2 { get; set; }
        public string ImprintSize_IL1_T2 { get; set; }
        public  double? MaxColors_IL1_T2 { get; set; }
        public string Handling_IL1_T2 { get; set; }

        public string ImprintType_IL1_T3 { get; set; }
        public string ImprintLocation_IL1_T3 { get; set; }
        public string ImprintImage_IL1_T3 { get; set; }
        public string PriceClass_IL1_T3 { get; set; }
        public string PriceClass_IL1_CN3 { get; set; }
        public  double? SetupFee_IL1_T3 { get; set; }
        public  double? SetupFee_IL1_NextColor3 { get; set; }
        public string SetupFeeStructure3 { get; set; }
        public string ExcludeFreeSetupCosts3 { get; set; }
        public string ImprintSize_IL1_T3 { get; set; }
        public  double? MaxColors_IL1_T3 { get; set; }
        public string Handling_IL1_T3 { get; set; }

        public string ImprintType_IL1_T4 { get; set; }
        public string ImprintLocation_IL1_T4 { get; set; }
        public string ImprintImage_IL1_T4 { get; set; }
        public string PriceClass_IL1_T4 { get; set; }
        public string PriceClass_IL1_CN4 { get; set; }
        public  double? SetupFee_IL1_T4 { get; set; }
        public  double? SetupFee_IL1_NextColor4 { get; set; }
        public string SetupFeeStructure4 { get; set; }
        public string ExcludeFreeSetupCosts4 { get; set; }
        public string ImprintSize_IL1_T4 { get; set; }
        public  double? MaxColors_IL1_T4 { get; set; }
        public string Handling_IL1_T4 { get; set; }

        public string ImprintType_IL1_T5 { get; set; }
        public string ImprintLocation_IL1_T5 { get; set; }
        public string ImprintImage_IL1_T5 { get; set; }
        public string PriceClass_IL1_T5 { get; set; }
        public string PriceClass_IL1_CN5 { get; set; }
        public  double? SetupFee_IL1_T5 { get; set; }
        public  double? SetupFee_IL1_NextColor5 { get; set; }
        public string SetupFeeStructure5 { get; set; }
        public string ExcludeFreeSetupCosts5 { get; set; }
        public string ImprintSize_IL1_T5 { get; set; }
        public  double? MaxColors_IL1_T5 { get; set; }
        public string Handling_IL1_T5 { get; set; }



        public string ImprintType_IL1_T6 { get; set; }
        public string ImprintLocation_IL1_T6 { get; set; }
        public string ImprintImage_IL1_T6 { get; set; }
        public string PriceClass_IL1_T6 { get; set; }
        public string PriceClass_IL1_CN6 { get; set; }
        public  double? SetupFee_IL1_T6 { get; set; }
        public  double? SetupFee_IL1_NextColor6 { get; set; }
        public string SetupFeeStructure6 { get; set; }
        public string ExcludeFreeSetupCosts6 { get; set; }
        public string ImprintSize_IL1_T6 { get; set; }
        public  double? MaxColors_IL1_T6 { get; set; }
        public string Handling_IL1_T6 { get; set; }

        public string IntraCode { get; set; }
        public string CountryOfOrigin { get; set; }
        public  double? OuterCartonPieces { get; set; }
        public  double? OuterCartonLength { get; set; }
        public  double? OuterCartonWidth { get; set; }
        public  double? OuterCartonHeight { get; set; }
        public  double? CommercialItemLength { get; set; }
        public  double? CommercialItemWidth { get; set; }
        public  double? CommercialItemHeight { get; set; }
        public  double? CommercialItemWeight { get; set; }
        public string Flavours { get; set; }
        public string Sizes { get; set; }
        public string WritingColor { get; set; }
        public string CapacityTxt { get; set; }
        public string OrderUnit { get; set; }
        public string MainGroup { get; set; }
        public string Material { get; set; }
        public string BatteryType { get; set; }
        public  double? NumberOfBatteries { get; set; }
        public string ProductImageURL { get; set; }
        public string DeliveryTimeMT_IL1_T1 { get; set; }
        //public string 07_12_SearchTerms { get; set; }
        public string Q4_Q_OnPallet { get; set; }
        //public string 04_Brandnames {get;set;}
    }
}
