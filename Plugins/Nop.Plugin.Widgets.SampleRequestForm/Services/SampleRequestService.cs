using System;
using Nop.Core;
using Nop.Plugin.Widgets.SampleRequestForm.Domain;
using Nop.Core.Data;
using System.Linq;

namespace Nop.Plugin.Widgets.SampleRequestForm.Services
{
    public class SampleRequestService : ISampleRequestService
    {
        #region Fields

        private readonly IRepository<SampleRequestFormRecord> _sampleRequestFormRepository;

        #endregion

        #region Constructor

        public SampleRequestService(IRepository<SampleRequestFormRecord> sampleRequestFormRepository)
        {
            this._sampleRequestFormRepository = sampleRequestFormRepository;
        }

        #endregion

        #region Methods

        public void AddSampleRequestFormRecord(SampleRequestFormRecord sampleRequestFormRecord)
        {
            if (sampleRequestFormRecord == null)
                throw new ArgumentNullException("sampleRequestFormRecord");
            _sampleRequestFormRepository.Insert(sampleRequestFormRecord);
        }

        public void DeleteSampleRequestFormRecord(SampleRequestFormRecord sampleRequestFormRecord)
        {
            if (sampleRequestFormRecord == null)
                throw new ArgumentNullException("registerRecord");
            _sampleRequestFormRepository.Delete(sampleRequestFormRecord);
        }

        public IPagedList<SampleRequestFormRecord> GetAllSampleRequestFormRecord(int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _sampleRequestFormRepository.Table;
            query = query.OrderByDescending(c => c.Id);
            var unsortedCategories = query.ToList();
            return new PagedList<SampleRequestFormRecord>(unsortedCategories, pageIndex, pageSize);
        }

        public SampleRequestFormRecord GetSampleRequestFormRecordByEmail(string email)
        {
            if (!string.IsNullOrEmpty(email))
                return null;
            var query = _sampleRequestFormRepository.Table;

            query = query.Where(c => c.Email == email);

            return query.FirstOrDefault();
        }

        public SampleRequestFormRecord GetSampleRequestFormRecordById(int id)
        {
            if (id == 0)
                return null;
            return _sampleRequestFormRepository.GetById(id);
        }

        public void UpdateSampleRequestFormRecord(SampleRequestFormRecord sampleRequestFormRecord)
        {
            if (sampleRequestFormRecord == null)
                throw new ArgumentNullException("sampleRequestFormRecord");

            _sampleRequestFormRepository.Update(sampleRequestFormRecord);
        }

        #endregion
    }
}
