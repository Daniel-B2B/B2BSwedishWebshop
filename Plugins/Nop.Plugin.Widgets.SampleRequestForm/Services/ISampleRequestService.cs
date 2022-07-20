using Nop.Core;
using Nop.Plugin.Widgets.SampleRequestForm.Domain;

namespace Nop.Plugin.Widgets.SampleRequestForm.Services
{
   public interface ISampleRequestService
    {
        void AddSampleRequestFormRecord(SampleRequestFormRecord sampleRequestFormRecord);    

        IPagedList<SampleRequestFormRecord> GetAllSampleRequestFormRecord(int pageIndex = 0, int pageSize = int.MaxValue);

        SampleRequestFormRecord GetSampleRequestFormRecordById(int id);

        SampleRequestFormRecord GetSampleRequestFormRecordByEmail(string email);

        void UpdateSampleRequestFormRecord(SampleRequestFormRecord sampleRequestFormRecord);

        void DeleteSampleRequestFormRecord(SampleRequestFormRecord sampleRequestFormRecord);
    }
}
