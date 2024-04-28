using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSLab.Model
{
    public class Report
    {
        private Guid reportId;
        private Guid userId;
        private Guid postId;
        private string reasonForReporting;
        private DateTime dateOfReport;

        public Report(Guid userId, Guid postId, string reasonForReporting)
        {
            this.reportId = Guid.NewGuid();
            this.userId = userId;
            this.postId = postId;
            this.reasonForReporting = reasonForReporting;
            this.dateOfReport = DateTime.Now;
        }

        public Report(Guid id, Guid userId, Guid postId, string reasonForReporting, DateTime dateOfReport)
        {
            this.reportId = id;
            this.userId = userId;
            this.postId = postId;
            this.reasonForReporting = reasonForReporting;
            this.dateOfReport = dateOfReport;
        }

        public Report()
        {
            this.reportId = Guid.NewGuid();
            this.userId = Guid.NewGuid();
            this.postId = Guid.NewGuid();
            this.reasonForReporting = Constants.EMPTY_STRING;
            this.dateOfReport = DateTime.Now;
        }

        public Guid ReportId { get => reportId; }
        public Guid UserId { get => userId; }
        public Guid PostId { get => postId; }
        public string ReasonForReporting { get => reasonForReporting; set => reasonForReporting = value; }
        public DateTime DateOfReport { get => dateOfReport; set => dateOfReport = value; }
    }
}
