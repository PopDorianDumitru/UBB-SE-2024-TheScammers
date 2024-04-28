using ISSLab.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.ViewModel
{
    public class AllConversationsViewModelTests
    {
        public AllConversationsViewModel _allConversationsViewModel;
        [SetUp]
        public void SetUp()
        {
            _allConversationsViewModel = new AllConversationsViewModel();
        }

        [Test]
        public void AddHardcodedProfiles_OnInstantiation_ProfilesAddedToCollection()
        {
            Assert.That(_allConversationsViewModel.AllProfiles.Count, Is.EqualTo(2));
        }
    }
}
