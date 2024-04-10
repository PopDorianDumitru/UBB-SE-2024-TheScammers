using ISSLab.Model;
using ISSLab.Model.Repositories;
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
    class MainWindowViewModel : ViewModelBase
    {
        PostService postService;
        UserService userService;
        GroupRepository groupRepository;
        ObservableCollection<Post> shownPosts;
        Guid userId;
        public ViewModelBase CurrentViewModel { get; }
        public MainWindowViewModel() 
        {
            userId = Guid.NewGuid();
            DataSet dataSet = new DataSet();
            PostRepository postRepo = new PostRepository(dataSet, Guid.Parse("e21f4b3b-f80b-48ea-a309-a25948fd150e"));
            UserRepository userRepo = new UserRepository(dataSet);
            postRepo.addPost(new FixedPricePost("../Resources/Images/vini.jpeg", new Guid(), new Guid(), "fsd", "fds", "Macarena", "fds", 34, DateTime.Now, "", new List<Review>(), 4, Guid.Empty, "FixedPrice", true));
            postRepo.addPost(new FixedPricePost("../Resources/Images/vini.jpeg", new Guid(), new Guid(), "fsd", "fds", "Macarena", "fds", 34, DateTime.Now, "", new List<Review>(), 4, Guid.Empty, "FixedPrice", true));

            shownPosts = [.. postRepo.getAll()];
            groupRepository = new GroupRepository(dataSet);
            postService = new PostService(postRepo,userRepo,groupRepository);
            userService = new UserService(userRepo,postRepo,groupRepository);

        }



        public ObservableCollection<Post> ShownPosts { get { return shownPosts; } set
            {
                ShownPosts = value;
                OnPropertyChanged(nameof(ShownPosts));
            }
        }

        public void LoadPostsCommand()
        {
            List<Post> posts = postService.GetPosts();
            shownPosts.Clear();
            foreach(Post p in posts)
            {
                shownPosts.Add(p);
            }

            OnPropertyChanged(nameof(ShownPosts));
        }

       

    }
}
