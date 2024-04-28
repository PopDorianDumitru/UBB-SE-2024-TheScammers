using ISSLab.Model;
using ISSLab.Services;
using ISSLab.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;

namespace Tests.ViewModel
{
    internal class MainWindowViewModelTests
    {
        //private ObservableCollection<IPostContentViewModel> shownPosts;
        private Guid userId;
        private Guid groupId;
        private Mock<IPostService> _postService;
        private Mock<IUserService> _userService;
        private IChatFactory _fakeChatFactory;
        private MainWindowViewModel _mainWindowViewModel;

        [SetUp]
        public void SetUp()
        {
            userId = Guid.NewGuid();
            groupId = Guid.NewGuid();
            _postService = new Mock<IPostService>();
            _userService = new Mock<IUserService>();
            _fakeChatFactory = new FakeChatFactory();
            _mainWindowViewModel = new MainWindowViewModel(_postService.Object, _userService.Object, userId, groupId, _fakeChatFactory);
        }

        [Test]
        public void ChangeToFavorites_Test()
        {
            List<Post> favoritedPosts = _userService.Object.GetFavoritePosts(groupId, userId);
            _mainWindowViewModel.LoadPostsCommand(favoritedPosts);
            Assert.Equals(favoritedPosts, _mainWindowViewModel.ShownPosts);
        }
    }
}
