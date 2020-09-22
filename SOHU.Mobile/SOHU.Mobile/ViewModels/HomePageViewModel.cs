using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SOHU.Mobile.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        public HomePageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Tổng quan ứng dụng";
            MenuTappedCommand = new DelegateCommand<string>(MenuTappedCommandExecute);
        }

        public string Page { get; set; }

        public DelegateCommand<string> MenuTappedCommand { get; private set; }

        public void MenuTappedCommandExecute(string Page)
        {
            NavigationService.NavigateAsync(Page);
        }
    }
}
