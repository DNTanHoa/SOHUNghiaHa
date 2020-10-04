using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using SOHU.Mobile.Helpers;
using SOHU.Mobile.Models;
using SOHU.Mobile.Services.Membership;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace SOHU.Mobile.ViewModels
{
    public class CustomerMasterViewModel : ViewModelBase
    {
        private readonly IMembershipService membershipService;

        public CustomerMasterViewModel(INavigationService navigationService,
                                       IMembershipService membershipService) : base(navigationService)
        {
            ItemTappedCommand = new DelegateCommand<string>(ItemTappedCommandExecute);
            Title = "Danh sách khách hàng";
            this.membershipService = membershipService;

            this.Customers = membershipService.GetCustomers().ToObservableCollection();
        }

        public ObservableCollection<Customer> Customers { get; set; }

        public Customer SelectedCustomer { get; set; }

        public DelegateCommand<string> ItemTappedCommand { get; private set; }

        public void ItemTappedCommandExecute(string commandType)
        {
            NavigationParameters parameters = new NavigationParameters();
            if (commandType.Equals("Add"))
            {
                parameters.Add("CustomerId",0);
            }
            else
            {
                parameters.Add("CustomerId", SelectedCustomer?.Id);
            }
            NavigationService.NavigateAsync("CustomerDetail", parameters);
        }
    }
}
