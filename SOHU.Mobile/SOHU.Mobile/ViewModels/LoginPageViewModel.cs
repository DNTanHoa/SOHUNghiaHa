using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SOHU.Mobile.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        public LoginPageViewModel(INavigationService navigationService) : base(navigationService) 
        {
            LoginCommand = new DelegateCommand(LoginCommandExecute);
        }

        private string _userName;

        public string userName
        {
            get => _userName;
            set
            {
                this._userName = value;
                RaisePropertyChanged(nameof(userName));
            }
        }

        private string _password;

        public string passWord
        {
            get => _password;
            set
            {
                this._password = value;
                RaisePropertyChanged(nameof(passWord));
            }
        }

        public DelegateCommand LoginCommand { get; private set; }

        public void LoginCommandExecute()
        {
            NavigationService.NavigateAsync("MainPage");
        }
    }
}
