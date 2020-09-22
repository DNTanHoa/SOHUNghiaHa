using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using SOHU.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace SOHU.Mobile.ViewModels
{
    public class CustomerMasterViewModel : ViewModelBase
    {
        public CustomerMasterViewModel(INavigationService navigationService) : base(navigationService)
        {
            ItemTappedCommand = new DelegateCommand(async () => await ItemTappedCommandExecute());
            Title = "Danh sách khách hàng";
        }

        public ObservableCollection<Customer> Customers { get; set; }

        public Customer SelectedCustomer { get; set; }

        public DelegateCommand ItemTappedCommand { get; private set; }

        public async Task ItemTappedCommandExecute()
        {
            NavigationParameters parameters = new NavigationParameters();
            parameters.Add("CustomerId", SelectedCustomer.Id);
            await NavigationService.NavigateAsync("CustomerDetail", parameters);
        }
    }
}
