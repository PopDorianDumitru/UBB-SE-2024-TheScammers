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
        public Report reportToTest1,reportToTest2,reportToTest3;
        

        [SetUp]
        public void SettingUp()
        {
            reportToTest1 = new Report(Guid.NewGuid(),Guid.NewGuid(),"violence");
            reportToTest2= new Report(Guid.NewGuid(),Guid.NewGuid(), Guid.NewGuid(), "rated18+", DateTime.Parse("Jan 11,2024"));
            reportToTest3 = new Report();
        }
        [Test]
        public void IdGet_GetTheIdReportFirstConstructor_ShouldBeNotEmpty()
        {
            Assert.That(reportToTest1.Id, Is.Not.EqualTo(Guid.Empty));

        }
        [Test]
        public void IdGet_GetTheIdReportSecondConstructor_ShouldBeNotEmpty()
        {
            Assert.That(reportToTest2.Id, Is.Not.EqualTo(Guid.Empty));

        }
        [Test]
        public void IdGet_GetTheIdReportThirdConstructor_ShouldBeNotEmpty()
        {
            Assert.That(reportToTest3.Id, Is.Not.EqualTo(Guid.Empty));

        }
        [Test]
        public void UserIdGet_GetTheUserIdReportFirstConstructor_ShouldBeNotEmpty()
        {
            Assert.That(reportToTest1.UserId, Is.Not.EqualTo(Guid.Empty));

        }
        [Test]
        public void UserIdGet_GetTheUserIdReportSecondConstructor_ShouldBeNotEmpty()
        {
            Assert.That(reportToTest2.UserId, Is.Not.EqualTo(Guid.Empty));

        }
        [Test]
        public void UserIdGet_GetTheUserIdReportThirdConstructor_ShouldBeNotEmpty()
        {
            Assert.That(reportToTest3.UserId, Is.Not.EqualTo(Guid.Empty));

        }
        [Test]
        public void PostIdGet_GetThePostIdReportFirstConstructor_ShouldBeNotEmpty()
        {
            Assert.That(reportToTest1.PostId, Is.Not.EqualTo(Guid.Empty));

        }
        [Test]
        public void PostIdGet_GetThePostIdReportSecondConstructor_ShouldBeNotEmpty()
        {
            Assert.That(reportToTest2.PostId, Is.Not.EqualTo(Guid.Empty));

        }
        [Test]
        public void PostIdGet_GetThePostIdReportThirdConstructor_ShouldBeNotEmpty()
        {
            Assert.That(reportToTest3.PostId, Is.Not.EqualTo(Guid.Empty));

        }
        [Test]
        public void ReasonForReportingGet_GetReasonForReportingForReportFirstConstructor_ShouldBeEqualWithViolence()
        {
            Assert.True(reportToTest1.ReasonForReporting=="violence");

        }
        [Test]
        public void ReasonForReportingSet_SetReasonForReportingForReportSecondConstructor_ShouldBeEqualWithHate()
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

