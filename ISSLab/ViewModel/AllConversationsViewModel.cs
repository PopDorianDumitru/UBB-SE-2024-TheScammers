using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSLab.ViewModel
{
    public class AllConversationsViewModel
    {
        private ObservableCollection<Model.User> AllProfiles { get; set; }

        public AllConversationsViewModel()
        {
            AllProfiles = new ObservableCollection<ISSLab.Model.User>();

            AddHardcodedProfiles();
        }

        private void AddHardcodedProfiles()
        {
            AllProfiles.Add(new ISSLab.Model.User { Username = "John Doe", ProfilePicture = @"" });
            AllProfiles.Add(new ISSLab.Model.User { Username = "Jane Smith", ProfilePicture = @"" });
        }
    }
}
