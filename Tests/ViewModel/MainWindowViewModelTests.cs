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
        private MainWindowViewModel _mainWindowViewModel;
        private FakePostService _fakePostService;
        private FakeUserService _fakeUserService;
        [SetUp]
        public void SetUp()
        {
            _fakePostService = new FakePostService();
            _fakeUserService = new FakeUserService();
            _mainWindowViewModel = new MainWindowViewModel(_fakePostService, _fakeUserService, Guid.NewGuid(), Guid.NewGuid());
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
        public void ChangeToMarketPlace_AnyPost_CallsGetFavoritePosts()
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


    }
}
