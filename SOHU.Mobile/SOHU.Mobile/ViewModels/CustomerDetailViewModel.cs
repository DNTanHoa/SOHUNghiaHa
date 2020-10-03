
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using SOHU.Mobile.Models;
using SOHU.Mobile.Services.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SOHU.Mobile.ViewModels
{
    public class CustomerDetailViewModel : ViewModelBase
    {
        private readonly IPageDialogService dialogService;
        private readonly IMembershipService membershipService;

        public CustomerDetailViewModel(INavigationService navigationService,
                                       IPageDialogService dialogService,
                                       IMembershipService membershipService) : base(navigationService)
        {
            Title = "Chi tiết khách hàng";
            this.dialogService = dialogService;
            this.membershipService = membershipService;
            SaveCommand = new DelegateCommand(async () => await SaveCommandExecute());
            BackCommand = new DelegateCommand(async () => await BackCommandExecute());
        }

        private int _Id;
        public int Id
        {
            get => _Id;
            set
            {
                _Id = value;
                RaisePropertyChanged(nameof(Id));
            }
        }

        private string _fullName;
        public string FullName
        {
            get => _fullName;
            set
            {
                this._fullName = value;
                RaisePropertyChanged(nameof(FullName));
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

        private string _bankName;
        public string BankName
        {
            get => _bankName;
            set
            {
                this._bankName = value;
                RaisePropertyChanged(nameof(BankName));
            }
        }

        private string _bankAccount;
        public string BankAccount
        {
            get => _bankAccount;
            set
            {
                this._bankAccount = value;
                RaisePropertyChanged(nameof(BankAccount));
            }
        }

        private string _contactFullName;
        public string ContactFullName
        {
            get => _contactFullName;
            set
            {
                this._contactFullName = value;
                RaisePropertyChanged(nameof(ContactFullName));
            }
        }


        private string _contactPosition;
        public string ContactPosition
        {
            get => _contactPosition;
            set
            {
                this._contactPosition = value;
                RaisePropertyChanged(nameof(ContactPosition));
            }
        }

        public DelegateCommand SaveCommand { get; private set; }
        
        public DelegateCommand BackCommand { get; private set; }


        public async Task SaveCommandExecute()
        {
            var customer = new Customer()
            {
                FullName = this.FullName,
                Code = string.Empty,
                Id = this.Id,
                Address = this.Address,
                TaxCode = this.TaxCode,
                Email = this.Email,
                Phone = this.Phone,
                ContactFullName = this.ContactFullName,
                ContactPosition = this.ContactPosition,
                ParentID = 46,
                BankAccount = this.BankAccount,
                BankName  = this.BankName
            };
            var result = membershipService.SaveCustomer(customer, out string message);
            if (result)
            {
                await dialogService.DisplayAlertAsync("Thông báo", "Lưu thành công", "OK");
                await NavigationService.NavigateAsync("CustomerMaster");
            }
            else
            {
                await dialogService.DisplayAlertAsync("Thông báo", message, "OK");
            }
        }

        public async Task BackCommandExecute()
        {
            await NavigationService.NavigateAsync("CustomerMaster");
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            var customerId = parameters.GetValue<int>("CustomerId");
            if(customerId > 0)
            {
                var customer = membershipService.GetByID(customerId);
                this.Id = customer.Id;
                this.FullName = customer.FullName;
                this.Phone = customer.Phone;
                this.TaxCode = customer.TaxCode;
                this.Address = customer.Address;
                this.BankAccount = customer.BankAccount;
                this.BankName = customer.BankName;
                this.Email = customer.Email;
                this.ContactFullName = customer.ContactFullName;
                this.ContactPosition = customer.ContactPosition;
            }    
            base.OnNavigatedTo(parameters);
        }
    }
}
