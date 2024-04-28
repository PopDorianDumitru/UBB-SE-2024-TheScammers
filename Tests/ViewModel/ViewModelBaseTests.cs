using Moq;
using NUnit.Framework;
using ISSLab.ViewModel;
using System.ComponentModel;

namespace ISSLab.ViewModel.Tests
{
    [TestFixture]
    public class ViewModelBaseTests
    {
        [Test]
        public void OnPropertyChanged_Raises_PropertyChanged_Event()
        {
            var mock = new Mock<TestViewModel>();
            bool eventRaised = false;
            mock.Object.PropertyChanged += (sender, args) => eventRaised = true;

            mock.Object.RaisePropertyChanged();

            Assert.IsTrue(eventRaised, "PropertyChanged event was not raised.");
        }
    }

    public class TestViewModel : ViewModelBase
    {
        public void RaisePropertyChanged()
        {
            OnPropertyChanged(nameof(TestProperty));
        }

        public string? TestProperty { get; set; }
    }
}
