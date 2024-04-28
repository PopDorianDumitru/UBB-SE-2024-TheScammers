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
        private IPostService postService;
        private IUserService userService;
        private ObservableCollection<IPostContentViewModel> shownPosts;
        private Guid userId;
        private Guid groupId;
        private ICreatePostViewModel postCreationViewModel;
        private IChatFactory chatFactory;

        public IViewModelBase CurrentViewModel { get; }
        public MainWindowViewModel(IPostService givenPostService, IUserService givenUserService, Guid userId, Guid groupId, IChatFactory chatFactory)
        {
            this.postService = givenPostService;
            this.userService = givenUserService;
            this.userId = userId;
            this.groupId = groupId;
            this.chatFactory = chatFactory;

            shownPosts = new ObservableCollection<IPostContentViewModel>();

            postCreationViewModel = new CreatePostViewModel(userId, groupId, postService);

            LoadPostsCommand(postService.GetPosts());
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
            get { return shownPosts; }
            set
            {
                ShownPosts = value;
                OnPropertyChanged(nameof(ShownPosts));
            }
        }
        public void ChangeToFavorites()
        {
            List<Post> favoritedPosts = userService.GetFavoritePosts(groupId, userId);
            LoadPostsCommand(favoritedPosts);
        }

        public void ChangeToMarketPlace()
        {
            List<Post> posts = postService.GetPosts();
            LoadPostsCommand(posts);
        }

        public void ChangeToCart()
        {
            List<Post> cart = userService.GetPostsFromCart(userId, groupId);
            LoadPostsCommand(cart);

        }
        public void ChangeToMarketplacePost()
        {
            return;
        }

        public void LoadPostsCommand(List<Post> postsToLoad)
        {
            shownPosts.Clear();
            foreach (Post onePostToLoad in postsToLoad)
            {
                User originalPoster = userService.GetUserById(onePostToLoad.AuthorId);
                //shownPosts.Add(p);
                shownPosts.Add(new PostContentViewModel(onePostToLoad, originalPoster, this.userId, this.groupId, this.userService, this.chatFactory));
            }

            OnPropertyChanged(nameof(ShownPosts));
        }
    }
}
