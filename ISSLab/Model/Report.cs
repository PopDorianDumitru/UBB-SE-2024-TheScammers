using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSLab.Model
{
    public class Report
    {
        private Guid _reportId;
        private Guid _userId;
        private Guid _postId;
        private string _reasonForReporting;
        private DateTime _dateOfReport;

        public Report(Guid userId, Guid postId, string reasonForReporting)
        {
            this._reportId = Guid.NewGuid(); ;
            this._userId = userId;
            this._postId = postId;
            this._reasonForReporting = reasonForReporting;
            this._dateOfReport = DateTime.Now;
        }

        public Report(Guid id, Guid userId, Guid postId, string reasonForReporting, DateTime dateOfReport)
        {
            this._reportId = id;
            this._userId = userId;
            this._postId = postId;
            this._reasonForReporting = reasonForReporting;
            this._dateOfReport = dateOfReport;
        }

        public Report()
        {
            this._reportId = Guid.NewGuid();
            this._userId = Guid.NewGuid();
            this._postId = Guid.NewGuid();
            this._reasonForReporting = Constants.EMPTY_STRING;
            this._dateOfReport = DateTime.Now;
        }

        public Guid ReportId { get => _reportId; }
        public Guid UserId { get => _userId; }
        public Guid PostId { get => _postId; }
        public string ReasonForReporting { get => _reasonForReporting; set => _reasonForReporting = value; }
        public DateTime DateOfReport { get => _dateOfReport; set => _dateOfReport = value; }
    }
}
