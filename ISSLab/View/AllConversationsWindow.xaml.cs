using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows;
using ISSLab.Model;
using ISSLab.ViewModel;

namespace ISSLab.View
{
    public partial class AllConversationsWindow : UserControl
    {
        private AllConversationsViewModel _viewModel;


        public AllConversationsWindow(AllConversationsViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
            this._viewModel = viewModel;
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ListView listView && listView.SelectedItem is ISSLab.Model.User selectedUser)
            {
                Window parentWindow = Window.GetWindow(this);
                parentWindow?.Close();

                var postContentViewModel = DataContext as PostContentViewModel;
                Post post = postContentViewModel.getPost();

                Chat chat = new Chat(new ChatViewModel(selectedUser, post));
                parentWindow.Content = chat;
            }
        }

    }
}
