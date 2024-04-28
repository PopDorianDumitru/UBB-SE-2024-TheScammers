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
using System.Text.RegularExpressions;

namespace Tests.ViewModel
{
    internal class MainWindowViewModelTests
    {
        private MainWindowViewModel _mainWindowViewModel;
        private FakePostService _fakePostService;
        private FakeUserService _fakeUserService;
        Guid expectedUserId;
        Guid expectedGroupId;

        [SetUp]
        public void SetUp()
        {
            _fakePostService = new FakePostService();
            _fakeUserService = new FakeUserService();
            expectedUserId = Guid.NewGuid();
            expectedGroupId = Guid.NewGuid();
            _mainWindowViewModel = new MainWindowViewModel(_fakePostService, _fakeUserService, expectedUserId, expectedGroupId, new ChatFactory());
        }

        [Test]
        public void ShownPosts_GetValue_ReturnsCorrectValue()
        {
            Assert.That(_mainWindowViewModel.ShownPosts, Has.Count.EqualTo(1));
        }
        [Test]
        public void ShownPosts_SetValue_SetsCorrectValue()
        {
            var expectedResult = new ObservableCollection<IPostContentViewModel>();

            _mainWindowViewModel.ShownPosts = expectedResult;
            Assert.That(_mainWindowViewModel.ShownPosts, Has.Count.EqualTo(0));
        }

        [Test]
        public void ChangeToFavorites_AnyPost_CallsGetFavoritePosts()
        {
            _mainWindowViewModel.ChangeToFavorites();
            Assert.That(_fakeUserService.getFavoritePostsCalled, Is.EqualTo(true));
        }

        [Test]
        public void ChangeToMarketPlace_AnyPost_CallsGetPosts()
        {
            _mainWindowViewModel.ChangeToMarketPlace();
            Assert.That(_fakePostService.getPostsCalled, Is.EqualTo(true));
        }

        [Test]
        public void ChangeToCart_AnyPost_CallsGetFavoritePosts()
        {
            _mainWindowViewModel.ChangeToCart();
            Assert.That(_fakeUserService.getItemsFromCartCalled, Is.EqualTo(true));
        }

        [Test]
        public void PostCreationViewModel_SetGetValue_ReturnsCorrectValue()
        {
            var expectedPostCreationViewModel = new CreatePostViewModel(expectedUserId, expectedGroupId, _fakePostService);
            var actualPostCreationViewModel = _mainWindowViewModel.PostCreationViewModel;
            _mainWindowViewModel.PostCreationViewModel = expectedPostCreationViewModel;

            Assert.That(actualPostCreationViewModel.AccountId, Is.EqualTo(expectedUserId));
            Assert.That(actualPostCreationViewModel.GroupId, Is.EqualTo(expectedGroupId));
        }
    }
}
