using ISSLab.Model;
using ISSLab.Services;
using ISSLab.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ISSLab.ViewModel
{
    public class MainWindowViewModel : ViewModelBase, IMainWindowViewModel
    {
        private IPostService _postService;
        private IUserService _userService;
        private ObservableCollection<IPostContentViewModel> _shownPosts;
        private Guid _userId;
        private Guid _groupId;
        private ICreatePostViewModel postCreationViewModel;
        private IChatFactory chatFactory;

        public IViewModelBase CurrentViewModel { get; }
        public MainWindowViewModel(IPostService givenPostService, IUserService givenUserService, Guid userId, Guid groupId, IChatFactory chatFactory)
        {
            this._postService = givenPostService;
            this._userService = givenUserService;
            this._userId = userId;
            this._groupId = groupId;
            this.chatFactory = chatFactory;

            _shownPosts = new ObservableCollection<IPostContentViewModel>();

            postCreationViewModel = new CreatePostViewModel(userId, groupId, _postService);

            LoadPostsCommand(_postService.GetPosts());
        }

        public ICreatePostViewModel PostCreationViewModel
        {
            get { return postCreationViewModel; }
            set
            {
                postCreationViewModel = value;
                OnPropertyChanged(nameof(PostCreationViewModel));
            }
        }

        public ObservableCollection<IPostContentViewModel> ShownPosts
        {
            get { return _shownPosts; }
            set
            {
                _shownPosts = value;
                OnPropertyChanged(nameof(ShownPosts));
            }
        }
        public void ChangeToFavorites()
        {
            List<Post> favoritedPosts = _userService.GetFavoritePosts(_groupId, _userId);
            LoadPostsCommand(favoritedPosts);
        }

        public void ChangeToMarketPlace()
        {
            List<Post> posts = _postService.GetPosts();
            LoadPostsCommand(posts);
        }

        public void ChangeToCart()
        {
            List<Post> cart = _userService.GetPostsFromCart(_userId, _groupId);
            LoadPostsCommand(cart);

        }

        public void LoadPostsCommand(List<Post> postsToLoad)
        {
            _shownPosts.Clear();
            foreach (Post currentPostToLoad in postsToLoad)
            {
                User originalPoster = _userService.GetUserById(currentPostToLoad.AuthorId);
                //shownPosts.Add(p);
                _shownPosts.Add(new PostContentViewModel(currentPostToLoad, originalPoster, this._userId, this._groupId, this._userService, this.chatFactory));
            }

            OnPropertyChanged(nameof(ShownPosts));
        }
    }
}
