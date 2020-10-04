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
    public class InvoiceMasterViewModel : ViewModelBase
    {
        public InvoiceMasterViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Hóa đơn bán";
        }

        public ObservableCollection<Invoice> Invoices { get; set; }

        public Invoice SelectedInvoice { get; set; }

        public DelegateCommand ItemTappedCommand { get; private set; }

        public async Task ItemTappedCommandExecute()
        {
            NavigationParameters parameters = new NavigationParameters();
            parameters.Add("InvoiceId", SelectedInvoice.Id);
            await NavigationService.NavigateAsync("InvoiceDetail", parameters);
        }
    }
}
