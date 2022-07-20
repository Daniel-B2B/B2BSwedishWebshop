using Nop.Data.Mapping;
using Nop.PLugin.Misc.ImportProducts.Domain;

namespace Nop.PLugin.Misc.ImportProducts.Data
{
    public class CustomProductMap : NopEntityTypeConfiguration<CustomProduct>
    {
        public CustomProductMap()
        {
            this.ToTable("clipper_products");  //clipper_products_Local
            this.HasKey(t => t.Id);

            this.Property(t => t.CountryId);
            this.Property(t => t.ParentSKU);
            this.Property(t => t.SKU);
            this.Property(t => t.ColorName);
            this.Property(t => t.InternetName);
            this.Property(t => t.InternetTxt);
            this.Property(t => t.Q1);
            this.Property(t => t.Q2);
            this.Property(t => t.Q3);
            this.Property(t => t.Q4);
            this.Property(t => t.Q5);
            this.Property(t => t.Q6);
            this.Property(t => t.P1);
            this.Property(t => t.P2);
            this.Property(t => t.P3);
            this.Property(t => t.P4);
            this.Property(t => t.P5);
            this.Property(t => t.P6);
            this.Property(t => t.GrossNett);

            this.Property(t => t.ImprintType_IL1_T1);
            this.Property(t => t.ImprintLocation_IL1_T1);
            this.Property(t => t.ImprintImage_IL1_T1);           
            this.Property(t => t.PriceClass_IL1_T1);
            this.Property(t => t.PriceClass_IL1_CN);
            this.Property(t => t.SetupFee_IL1_T1);
            this.Property(t => t.SetupFee_IL1_NextColor);
            this.Property(t => t.SetupFeeStructure);
            this.Property(t => t.ExcludeFreeSetupCosts);
            this.Property(t => t.ImprintSize_IL1_T1);
            this.Property(t => t.MaxColors_IL1_T1);
            this.Property(t => t.Handling_IL1_T1);

            this.Property(t => t.ImprintType_IL1_T2);
            this.Property(t => t.ImprintLocation_IL1_T2);
            this.Property(t => t.ImprintImage_IL1_T2);
            this.Property(t => t.PriceClass_IL1_T2);
            this.Property(t => t.PriceClass_IL1_CN2);
            this.Property(t => t.SetupFee_IL1_T2);
            this.Property(t => t.SetupFee_IL1_NextColor2);
            this.Property(t => t.SetupFeeStructure2);
            this.Property(t => t.ExcludeFreeSetupCosts2);
            this.Property(t => t.ImprintSize_IL1_T2);
            this.Property(t => t.MaxColors_IL1_T2);
            this.Property(t => t.Handling_IL1_T2);

            this.Property(t => t.ImprintType_IL1_T3);
            this.Property(t => t.ImprintLocation_IL1_T3);
            this.Property(t => t.ImprintImage_IL1_T3);
            this.Property(t => t.PriceClass_IL1_T3);
            this.Property(t => t.PriceClass_IL1_CN3);
            this.Property(t => t.SetupFee_IL1_T3);
            this.Property(t => t.SetupFee_IL1_NextColor3);
            this.Property(t => t.SetupFeeStructure3);
            this.Property(t => t.ExcludeFreeSetupCosts3);
            this.Property(t => t.ImprintSize_IL1_T3);
            this.Property(t => t.MaxColors_IL1_T3);
            this.Property(t => t.Handling_IL1_T3);

            this.Property(t => t.ImprintType_IL1_T4);
            this.Property(t => t.ImprintLocation_IL1_T4);
            this.Property(t => t.ImprintImage_IL1_T4);
            this.Property(t => t.PriceClass_IL1_T4);
            this.Property(t => t.PriceClass_IL1_CN4);
            this.Property(t => t.SetupFee_IL1_T4);
            this.Property(t => t.SetupFee_IL1_NextColor4);
            this.Property(t => t.SetupFeeStructure4);
            this.Property(t => t.ExcludeFreeSetupCosts4);
            this.Property(t => t.ImprintSize_IL1_T4);
            this.Property(t => t.MaxColors_IL1_T4);
            this.Property(t => t.Handling_IL1_T4);

            this.Property(t => t.ImprintType_IL1_T5);
            this.Property(t => t.ImprintLocation_IL1_T5);
            this.Property(t => t.ImprintImage_IL1_T5);
            this.Property(t => t.PriceClass_IL1_T5);
            this.Property(t => t.PriceClass_IL1_CN5);
            this.Property(t => t.SetupFee_IL1_T5);
            this.Property(t => t.SetupFee_IL1_NextColor5);
            this.Property(t => t.SetupFeeStructure5);
            this.Property(t => t.ExcludeFreeSetupCosts5);
            this.Property(t => t.ImprintSize_IL1_T5);
            this.Property(t => t.MaxColors_IL1_T5);
            this.Property(t => t.Handling_IL1_T5);

            this.Property(t => t.ImprintType_IL1_T6);
            this.Property(t => t.ImprintLocation_IL1_T6);
            this.Property(t => t.ImprintImage_IL1_T6);
            this.Property(t => t.PriceClass_IL1_T6);
            this.Property(t => t.PriceClass_IL1_CN6);
            this.Property(t => t.SetupFee_IL1_T6);
            this.Property(t => t.SetupFee_IL1_NextColor6);
            this.Property(t => t.SetupFeeStructure6);
            this.Property(t => t.ExcludeFreeSetupCosts6);
            this.Property(t => t.ImprintSize_IL1_T6);
            this.Property(t => t.MaxColors_IL1_T6);
            this.Property(t => t.Handling_IL1_T6);

            this.Property(t => t.IntraCode);
            this.Property(t => t.CountryOfOrigin);
            this.Property(t => t.OuterCartonHeight);
            this.Property(t => t.OuterCartonLength);
            this.Property(t => t.OuterCartonPieces);
            this.Property(t => t.OuterCartonWidth);
            this.Property(t => t.CommercialItemHeight);
            this.Property(t => t.CommercialItemLength);
            this.Property(t => t.CommercialItemWeight);
            this.Property(t => t.CommercialItemWidth);
            this.Property(t => t.Flavours);
            this.Property(t => t.Sizes);
            this.Property(t => t.WritingColor);
            this.Property(t => t.CapacityTxt);
            this.Property(t => t.OrderUnit);
            this.Property(t => t.MainGroup);
            this.Property(t => t.Material);
            this.Property(t => t.BatteryType);
            this.Property(t => t.NumberOfBatteries);
            this.Property(t => t.ProductImageURL);
            this.Property(t => t.DeliveryTimeMT_IL1_T1);
            this.Property(t => t.Q4_Q_OnPallet);
        }
    }
}
