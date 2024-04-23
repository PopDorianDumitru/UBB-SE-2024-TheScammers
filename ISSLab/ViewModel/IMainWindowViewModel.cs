using System.Collections.ObjectModel;
using ISSLab.Model;

namespace ISSLab.ViewModel
{
    public interface IMainWindowViewModel
    {
        ViewModelBase CurrentViewModel { get; }
        ICreatePostViewModel PostCreationViewModel { get; set; }
        ObservableCollection<IPostContentViewModel> ShownPosts { get; set; }

        void ChangeToCart();
        void ChangeToFavorites();
        void ChangeToMarketPlace();
        void ChangeToMarketplacePost();
        void LoadPostsCommand(List<Post> posts);
    }
}