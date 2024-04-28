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

        [SetUp]
        public void SetUp()
        {
            _mainWindowViewModel = new MainWindowViewModel(new FakePostService(), new FakeUserService(), Guid.NewGuid(), Guid.NewGuid());
        }

        [Test]
        public void ShownPosts_GetValue_ReturnsCorrectValue()
        {
            var expectedResult = new ObservableCollection<IPostContentViewModel>();

            Assert.That(_mainWindowViewModel.ShownPosts, Has.Count.EqualTo(1));
           _mainWindowViewModel.ShownPosts = expectedResult;
            Assert.That(_mainWindowViewModel.ShownPosts, Has.Count.EqualTo(0));
        }
        
    }
}
