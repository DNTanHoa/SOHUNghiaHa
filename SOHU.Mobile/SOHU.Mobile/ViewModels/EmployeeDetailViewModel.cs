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
    public class EmployeeDetailViewModel : ViewModelBase
    {
        private readonly IPageDialogService pageDialogService;
        private readonly IMembershipService membershipService;

        public EmployeeDetailViewModel(INavigationService navigationService,
            IPageDialogService pageDialogService,
            IMembershipService membershipService) : base(navigationService)
        {
            this.pageDialogService = pageDialogService;
            this.membershipService = membershipService;
            Title = "Thông tin nhân viên";
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
    }
}
