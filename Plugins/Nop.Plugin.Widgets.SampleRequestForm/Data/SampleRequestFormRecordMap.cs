using Nop.Data.Mapping;
using Nop.Plugin.Widgets.SampleRequestForm.Domain;

namespace Nop.Plugin.Widgets.SampleRequestForm.Data
{
    public class SampleRequestFormRecordMap : NopEntityTypeConfiguration<SampleRequestFormRecord>
    {
        public SampleRequestFormRecordMap()
        {
            this.ToTable("Akiko_SampleRequestForms");
            this.HasKey(a => a.Id);
            this.Property(a => a.Name).HasMaxLength(500);
            this.Property(a => a.Company).HasMaxLength(500);
            this.Property(a => a.Email).HasMaxLength(500);
            this.Property(a => a.PhoneNumber).HasMaxLength(500);
            this.Property(a => a.Product).HasMaxLength(500);
            this.Property(a => a.PotentialOrderQty);
            this.Property(a => a.Comments).HasMaxLength(1000);
            this.Property(a => a.Address);
            this.Property(a => a.City);
            this.Property(a => a.ZipCode);
        }
    }
}
