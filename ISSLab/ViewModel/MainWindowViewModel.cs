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
        IPostService postService;
        IUserService userService;
        ObservableCollection<IPostContentViewModel> shownPosts;
        Guid userId;
        Guid groupId;
        ICreatePostViewModel postCreationViewModel;

        public ViewModelBase CurrentViewModel { get; }
        public MainWindowViewModel(IPostService givenPostService, IUserService givenUserService, Guid userId, Guid groupId)
        {
            this.postService = givenPostService;
            this.userService = givenUserService;
            this.userId = userId;
            this.groupId = groupId;

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
            List<Post> cart = userService.GetItemsFromCart(userId, groupId);
            LoadPostsCommand(cart);

        }

        public void ChangeToMarketplacePost()
        {

        }

        public void LoadPostsCommand(List<Post> posts)
        {

            shownPosts.Clear();
            foreach (Post p in posts)
            {
                User originalPoster = userService.GetUserById(p.AuthorId);
                //shownPosts.Add(p);
                shownPosts.Add(new PostContentViewModel(p, originalPoster, this.userId, this.groupId, this.userService));
            }

            OnPropertyChanged(nameof(ShownPosts));
        }
    }
}
