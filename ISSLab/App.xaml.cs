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

            IPostRepository postRepository = new PostRepository();
            IUserRepository userRepository = new UserRepository();
            IChatFactory chatFactory = new ChatFactory();

            User connectedUser = new User(userId, "Soundboard1", "Dorian", DateOnly.Parse("11.12.2003"), "../Resources/Images/Dorian.jpeg", "fsdgfd", DateTime.Parse("10.04.2024"), new List<Guid>(), new List<Guid>(), new List<SellingUserScore>(), new List<Cart>(), new List<UsersFavoritePosts>(), new List<Guid>(), new List<Review>(), 0);
            User userOne = new User("Vini", "Vinicius Junior", DateOnly.Parse("11.12.2003"), "../Resources/Images/Vini.png", "fdsfsdfds");
            User userTwo = new User("DDoorian", "Pop Dorian", DateOnly.Parse("12.12.2003"), "../Resources/Images/Dorian.jpeg", "bcvbc");

            AddHardcodedUsers(userRepository, connectedUser, userOne, userTwo);
            AddHardcodedPosts(postRepository, userOne, userTwo, groupId);

            IPostService postService = new PostService(postRepository);
            IUserService userService = new UserService(userRepository, postRepository);

            IMainWindowViewModel mainWindowViewModel = new MainWindowViewModel(postService, userService, userId, groupId, chatFactory);
            MainWindow mainWindow = new MainWindow(mainWindowViewModel);
            mainWindow.Show();
        }

        private void AddHardcodedUsers(IUserRepository userRepo, User connectedUser, User userOne, User userTwo)
        {
            userRepo.AddUser(connectedUser);
            userRepo.AddUser(userOne);
            userRepo.AddUser(userTwo);
        }

        private void AddHardcodedPosts(IPostRepository postRepository, User userOne, User userTwo, Guid groupId)
        {
            DonationPost donationPost = new DonationPost("../Resources/Images/catei.jpeg", userOne.Id, groupId, "Oradea", "A bunch of great dogssdaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", "Dogs", "077333999", "https://www.unicef.org/romania/ro", Constants.DONATION_POST_TYPE, true);
            AuctionPost auctionPost = new AuctionPost("../Resources/Images/catei.jpeg", userOne.Id, groupId, "Oradea", "A bunch of great dogssdaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", "Dogs", "077333999", 300, DateTime.Now.AddSeconds(80), "InPerson", new List<Review>(), 4, Guid.Empty, Guid.Empty, 100, 105, true);
            postRepository.AddPost(new FixedPricePost("../Resources/Images/catei.jpeg", userOne.Id, groupId, "Oradea", "A bunch of great dogssdaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", "Dogs", "077333999", 300, DateTime.Now.AddMonths(2), "InPerson", new List<Review>(), 4, Guid.Empty, Constants.FIXED_PRICE_POST_TYPE, true));

            FixedPricePost post1 = new FixedPricePost("../Resources/Images/catei.jpeg", userOne.Id, groupId, "Oradea", "A bunch of great dogssdaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", "Dogs", "077333999", 300, DateTime.Now.AddMonths(2), "InPerson", new List<Review>(), 4, Guid.Empty, Constants.FIXED_PRICE_POST_TYPE, true);
            post1.ReviewScore = 4;
            postRepository.AddPost(post1);

            postRepository.AddPost(new FixedPricePost("../Resources/Images/catei.jpeg", userTwo.Id, groupId, "Bistrita", "Some great dogs", "Something else", "0222111333", 350, DateTime.Now.AddDays(6), "shipping", new List<Review>(), 4, Guid.Empty, Constants.FIXED_PRICE_POST_TYPE, true));
            postRepository.AddPost(donationPost);
            postRepository.AddPost(auctionPost);
        }
    }
}
