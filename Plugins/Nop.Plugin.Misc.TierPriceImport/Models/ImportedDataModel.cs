namespace Nop.Plugin.Misc.TierPriceImport.Models
{
    public class ImportedDataModel
    {
        public string ParentSKU { get; set; }      
        public int Q1 { get; set; }
        public int Q2 { get; set; }
        public int Q3 { get; set; }
        public int Q4 { get; set; }
        public int Q5 { get; set; }
        public int Q6 { get; set; }      
        public decimal P1 { get; set; }
        public decimal P2 { get; set; }
        public decimal P3 { get; set; }
        public decimal P4 { get; set; }
        public decimal P5 { get; set; }
        public decimal P6 { get; set; }
    }
}
