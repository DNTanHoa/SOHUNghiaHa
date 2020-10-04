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
    public class EmployeeDetailViewModel : ViewModelBase
    {
        private readonly IPageDialogService pageDialogService;
        private readonly IMembershipService membershipService;

        public DelegateCommand SaveCommand { get; private set; }

        public DelegateCommand BackCommand { get; private set; }

        public EmployeeDetailViewModel(INavigationService navigationService,
            IPageDialogService pageDialogService,
            IMembershipService membershipService) : base(navigationService)
        {
            this.pageDialogService = pageDialogService;
            this.membershipService = membershipService;
            Title = "Thông tin nhân viên";
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

        private string _citizenIdentification;
        public string CitizenIdentification
        {
            get => _citizenIdentification;
            set
            {
                this._citizenIdentification = value;
                RaisePropertyChanged(nameof(CitizenIdentification));
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

        public async Task SaveCommandExecute()
        {
            var employee = new Employee()
            {
                FullName = this.Name,
                Code = string.Empty,
                Id = this.Id,
                Address = this.Address,
                TaxCode = this.TaxCode,
                CitizenIdentification = this.CitizenIdentification,
                //Email = this.Email,
                Phone = this.Phone,
                //ContactFullName = this.ContactFullName,
                //ContactPosition = this.ContactPosition,
                ParentID = 48,
                //BankAccount = this.BankAccount,
                //BankName = this.BankName
            };
            var result = membershipService.SaveEmployee(employee, out string message);
            if (result)
            {
                await pageDialogService.DisplayAlertAsync("Thông báo", "Lưu thành công", "OK");
                await NavigationService.NavigateAsync("EmployeeMaster");
            }
            else
            {
                await pageDialogService.DisplayAlertAsync("Thông báo", message, "OK");
            }
        }

        public async Task BackCommandExecute()
        {
            await NavigationService.NavigateAsync("EmployeeMaster");
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            var employeeId = parameters.GetValue<int>("EmployeeId");
            if (employeeId > 0)
            {
                var employee = membershipService.GetEmployeeByID(employeeId);
                this.Id = employee.Id;
                this.Name = employee.FullName;
                this.Phone = employee.Phone;
                this.TaxCode = employee.TaxCode;
                this.Address = employee.Address;
                this.CitizenIdentification = employee.CitizenIdentification;
                //    this.BankAccount = employee.BankAccount;
                //    this.BankName = employee.BankName;
                //    this.Email = employee.Email;
                //    this.ContactFullName = employee.ContactFullName;
                //    this.ContactPosition = employee.ContactPosition;
                //}
                base.OnNavigatedTo(parameters);
            }
        }
    }
}
