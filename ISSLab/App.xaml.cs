using System.Configuration;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using ISSLab.Model;
using ISSLab.Model.Repositories;
using ISSLab.Services;
using ISSLab.ViewModel;

namespace ISSLab
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Guid userId = Guid.NewGuid();
            Guid groupId = Guid.NewGuid();

            DataSet dataSet = new DataSet();
            IPostRepository postRepo = new PostRepository(dataSet, groupId);
            IUserRepository userRepo = new UserRepository(dataSet);

            User connectedUser = new User(userId, "Soundboard1", "Dorian", DateOnly.Parse("11.12.2003"), "../Resources/Images/Dorian.jpeg", "fsdgfd", DateTime.Parse("10.04.2024"), new List<Guid>(), new List<Guid>(), new List<SellingUserScore>(), new List<Cart>(), new List<Favorites>(), new List<Guid>(), new List<Review>(), 0);
            User tempUser1 = new User("Vini", "Vinicius Junior", DateOnly.Parse("11.12.2003"), "../Resources/Images/Vini.png", "fdsfsdfds");
            User tempUser2 = new User("DDoorian", "Pop Dorian", DateOnly.Parse("12.12.2003"), "../Resources/Images/Dorian.jpeg", "bcvbc");

            AddHardcodedUsers(userRepo, connectedUser, tempUser1, tempUser2);
            AddHardcodedPosts(postRepo, tempUser1, tempUser2, groupId);

            IGroupRepository groupRepository = new GroupRepository(dataSet);
            IPostService postService = new PostService(postRepo, userRepo, groupRepository);
            IUserService userService = new UserService(userRepo, postRepo, groupRepository);

            IMainWindowViewModel mainWindowViewModel = new MainWindowViewModel(postService, userService, userId, groupId);
            MainWindow mainWindow = new MainWindow(mainWindowViewModel);
            mainWindow.Show();
        }

        private void AddHardcodedUsers(IUserRepository userRepo, User connectedUser, User tempUser1, User tempUser2)
        {
            userRepo.AddUser(connectedUser);
            userRepo.AddUser(tempUser1);
            userRepo.AddUser(tempUser2);
        }

        private void AddHardcodedPosts(IPostRepository postRepo, User tempUser1, User tempUser2, Guid groupId)
        {
            DonationPost donationPost = new DonationPost("../Resources/Images/catei.jpeg", tempUser1.Id, groupId, "Oradea", "A bunch of great dogssdaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", "Dogs", "077333999", "https://www.unicef.org/romania/ro", "Donation", true);
            AuctionPost auctionPost = new AuctionPost("../Resources/Images/catei.jpeg", tempUser1.Id, groupId, "Oradea", "A bunch of great dogssdaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", "Dogs", "077333999", 300, DateTime.Now.AddSeconds(80), "InPerson", new List<Review>(), 4, Guid.Empty, Guid.Empty, 100, 105, "Auction", true);
            postRepo.addPost(new FixedPricePost("../Resources/Images/catei.jpeg", tempUser1.Id, groupId, "Oradea", "A bunch of great dogssdaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", "Dogs", "077333999", 300, DateTime.Now.AddMonths(2), "InPerson", new List<Review>(), 4, Guid.Empty, "FixedPrice", true));

            FixedPricePost post1 = new FixedPricePost("../Resources/Images/catei.jpeg", tempUser1.Id, groupId, "Oradea", "A bunch of great dogssdaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", "Dogs", "077333999", 300, DateTime.Now.AddMonths(2), "InPerson", new List<Review>(), 4, Guid.Empty, "FixedPrice", true);
            post1.ReviewScore = 4;
            postRepo.addPost(post1);

            postRepo.addPost(new FixedPricePost("../Resources/Images/catei.jpeg", tempUser2.Id, groupId, "Bistrita", "Some great dogs", "Something else", "0222111333", 350, DateTime.Now.AddDays(6), "shipping", new List<Review>(), 4, Guid.Empty, "FixedPrice", true));
            postRepo.addPost(donationPost);
            postRepo.addPost(auctionPost);
        }
    }
}
