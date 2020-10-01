
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SOHU.Mobile.ViewModels
{
    public class CustomerDetailViewModel : ViewModelBase
    {
        private readonly IPageDialogService dialogService;

        public CustomerDetailViewModel(INavigationService navigationService,
                                       IPageDialogService dialogService) : base(navigationService)
        {
            Title = "Chi tiết khách hàng";
            this.dialogService = dialogService;

            SaveCommand = new DelegateCommand(async () => await SaveCommandExecute());
            BackCommand = new DelegateCommand(async () => await BackCommandExecute());
        }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                this._name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }

        private string _phone;
        public string Phone
        {
            get => _phone;
            set
            {
                this._phone = value;
                RaisePropertyChanged(nameof(Phone));
            }
        }

        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                this._email = value;
                RaisePropertyChanged(nameof(Email));
            }
        }


        private string _taxCode;
        public string TaxCode
        {
            get => _taxCode;
            set
            {
                this._taxCode = value;
                RaisePropertyChanged(nameof(TaxCode));
            }
        }

        private string _address;
        public string Address
        {
            get => _address;
            set
            {
                this._address = value;
                RaisePropertyChanged(nameof(Address));
            }
        }

        public DelegateCommand SaveCommand { get; private set; }
        
        public DelegateCommand BackCommand { get; private set; }


        public async Task SaveCommandExecute()
        {
            await dialogService.DisplayAlertAsync("Thông báo", "Ứng dụng chưa được kết nối dữ liệu", "OK");
        }

        public async Task BackCommandExecute()
        {
            await NavigationService.NavigateAsync("CustomerMaster");
        }
    }
}
