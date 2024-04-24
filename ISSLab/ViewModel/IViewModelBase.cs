using System.ComponentModel;

namespace ISSLab.ViewModel
{
    public interface IViewModelBase
    {
        event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged(string propertyName);
    }
}