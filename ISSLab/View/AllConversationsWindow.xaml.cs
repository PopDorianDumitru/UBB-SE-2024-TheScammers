using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows;
using ISSLab.Model;
using ISSLab.ViewModel;

namespace ISSLab.View
{
    public partial class AllConversationsWindow : UserControl
    {
        private ObservableCollection<Model.User> AllProfiles { get; set; }


        public AllConversationsWindow()
        {
            InitializeComponent();
            DataContext = this;

            AllProfiles = new ObservableCollection<ISSLab.Model.User>();
            AllProfiles.Add(new ISSLab.Model.User { Username = "John Doe", ProfilePicture = @"" });
            AllProfiles.Add(new ISSLab.Model.User { Username = "Jane Smith", ProfilePicture = @"" });
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ListView listView && listView.SelectedItem is ISSLab.Model.User selectedUser)
            {
                Window parentWindow = Window.GetWindow(this);
                parentWindow?.Close();

                var viewModel = DataContext as PostContentViewModel;
                Post post = viewModel.getPost();

                Chat chat = new Chat(selectedUser,post, new ChatViewModel());
                parentWindow.Content = chat;
            }
        }

    }
}
