using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Moq;
using ISSLab.Model;
using ISSLab.Services;
using ISSLab.ViewModel;

namespace Tests.ViewModel
{
    internal class MainWindowViewModelTests
    {
        private MainWindowViewModel mainWindowViewModel;
        private FakePostService fakePostService;
        private FakeUserService fakeUserService;
        private Guid expectedUserId;
        private Guid expectedGroupId;

        [SetUp]
        public void SetUp()
        {
            fakePostService = new FakePostService();
            fakeUserService = new FakeUserService();
            expectedUserId = Guid.NewGuid();
            expectedGroupId = Guid.NewGuid();
            mainWindowViewModel = new MainWindowViewModel(fakePostService, fakeUserService, expectedUserId, expectedGroupId, new ChatFactory());
        }

        [Test]
        public void ShownPosts_GetValue_ReturnsCorrectValue()
        {
            Assert.That(mainWindowViewModel.ShownPosts, Has.Count.EqualTo(1));
        }
        [Test]
        public void ShownPosts_SetValue_SetsCorrectValue()
        {
            var expectedResult = new ObservableCollection<IPostContentViewModel>();

            mainWindowViewModel.ShownPosts = expectedResult;
            Assert.That(mainWindowViewModel.ShownPosts, Has.Count.EqualTo(0));
        }

        [Test]
        public void ChangeToFavorites_AnyPost_CallsGetFavoritePosts()
        {
            mainWindowViewModel.ChangeToFavorites();
            Assert.That(fakeUserService.GetFavoritePostsCalled, Is.EqualTo(true));
        }

        [Test]
        public void ChangeToMarketPlace_AnyPost_CallsGetPosts()
        {
            mainWindowViewModel.ChangeToMarketPlace();
            Assert.That(fakePostService.GetPostsCalled, Is.EqualTo(true));
        }

        [Test]
        public void ChangeToCart_AnyPost_CallsGetFavoritePosts()
        {
            mainWindowViewModel.ChangeToCart();
            Assert.That(fakeUserService.GetItemsFromCartCalled, Is.EqualTo(true));
        }

        [Test]
        public void PostCreationViewModel_SetGetValue_ReturnsCorrectValue()
        {
            var expectedPostCreationViewModel = new CreatePostViewModel(expectedUserId, expectedGroupId, fakePostService);
            var actualPostCreationViewModel = mainWindowViewModel.PostCreationViewModel;
            mainWindowViewModel.PostCreationViewModel = expectedPostCreationViewModel;

            Assert.That(actualPostCreationViewModel.AccountId, Is.EqualTo(expectedUserId));
            Assert.That(actualPostCreationViewModel.GroupId, Is.EqualTo(expectedGroupId));
        }
    }
}
