using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISSLab.ViewModel;

namespace Tests.ViewModel
{
    public class AllConversationsViewModelTests
    {
        private AllConversationsViewModel allConversationsViewModel;

        [SetUp]
        public void SetUp()
        {
            allConversationsViewModel = new AllConversationsViewModel();
        }

        [Test]
        public void AddHardcodedProfiles_OnInstantiation_ProfilesAddedToCollection()
        {
            Assert.That(allConversationsViewModel.AllProfiles.Count, Is.EqualTo(2));
        }
    }
}
