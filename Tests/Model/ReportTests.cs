using ISSLab.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Model
{
    internal class ReportTests
    {
        public Report reportToTest1;
        public Report reportToTest2;
        public Report reportToTest3;


        [SetUp]
        public void SettingUp()
        {
            reportToTest1 = new Report(Guid.NewGuid(), Guid.NewGuid(), "violence");
            reportToTest2 = new Report(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), "rated18+", DateTime.Parse("Jan 11,2024"));
            reportToTest3 = new Report();
        }

        [Test]
        public void ReportIdGet_ReportFirstConstructor_ShouldBeNotEmpty()
        {
            Assert.That(reportToTest1.ReportId, Is.Not.EqualTo(Guid.Empty));
        }

        [Test]
        public void ReportIdGet_ReportSecondConstructor_ShouldBeNotEmpty()
        {
            Assert.That(reportToTest2.ReportId, Is.Not.EqualTo(Guid.Empty));
        }

        [Test]
        public void ReportIdGet_ReportThirdConstructor_ShouldBeNotEmpty()
        {
            Assert.That(reportToTest3.ReportId, Is.Not.EqualTo(Guid.Empty));
        }

        [Test]
        public void UserIdGet_ReportFirstConstructor_ShouldBeNotEmpty()
        {
            Assert.That(reportToTest1.UserId, Is.Not.EqualTo(Guid.Empty));
        }

        [Test]
        public void UserIdGet_ReportSecondConstructor_ShouldBeNotEmpty()
        {
            Assert.That(reportToTest2.UserId, Is.Not.EqualTo(Guid.Empty));
        }

        [Test]
        public void UserIdGet_ReportThirdConstructor_ShouldBeNotEmpty()
        {
            Assert.That(reportToTest3.UserId, Is.Not.EqualTo(Guid.Empty));
        }

        [Test]
        public void PostIdGet_ReportFirstConstructor_ShouldBeNotEmpty()
        {
            Assert.That(reportToTest1.PostId, Is.Not.EqualTo(Guid.Empty));
        }

        [Test]
        public void PostIdGet_ReportSecondConstructor_ShouldBeNotEmpty()
        {
            Assert.That(reportToTest2.PostId, Is.Not.EqualTo(Guid.Empty));
        }

        [Test]
        public void PostIdGet_ReportThirdConstructor_ShouldBeNotEmpty()
        {
            Assert.That(reportToTest3.PostId, Is.Not.EqualTo(Guid.Empty));
        }

        [Test]
        public void ReasonForReportingGet_ReportFirstConstructor_ShouldBeEqualWithViolence()
        {
            Assert.True(reportToTest1.ReasonForReporting == "violence");
        }

        [Test]
        public void ReasonForReportingSet_ReportSecondConstructor_ReasonForReportingShouldBeEqualWithHate()
        {
            reportToTest2.ReasonForReporting = "hate";
            Assert.True(reportToTest2.ReasonForReporting == "hate");
        }

        [Test]
        public void DateOfReportGet_GetDateOfReportForReportSecondConstructor_ShouldBeJan112024()
        {
            Assert.True(reportToTest2.DateOfReport == DateTime.Parse("Jan 11,2024"));
        }

        [Test]
        public void DateOfReportSet_GetReasonForReportingForReportFirstConstructor_ShouldBeJan112023()
        {
            reportToTest1.DateOfReport = DateTime.Parse("Jan 11,2023");
            Assert.True(reportToTest1.DateOfReport == DateTime.Parse("Jan 11,2023"));
        }

    }
}

