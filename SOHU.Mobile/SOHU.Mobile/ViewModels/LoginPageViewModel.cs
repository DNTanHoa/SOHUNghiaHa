using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using SOHU.Mobile.Services.Membership;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SOHU.Mobile.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        public LoginPageViewModel(INavigationService navigationService,
            IPageDialogService pageDialogService,
            IMembershipService membershipService) : base(navigationService) 
        {
            LoginCommand = new DelegateCommand(LoginCommandExecute);
            this.pageDialogService = pageDialogService;
            this.membershipService = membershipService;
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
        private readonly IPageDialogService pageDialogService;
        private readonly IMembershipService membershipService;

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
            bool loginResult = membershipService.Login(this.userName, this.passWord, out string message);
            if(loginResult)
            {
                NavigationService.NavigateAsync("MainPage");
            }
            else
            {
                pageDialogService.DisplayAlertAsync("Thông báo", message, "OK");
            }    
        }
    }
}
