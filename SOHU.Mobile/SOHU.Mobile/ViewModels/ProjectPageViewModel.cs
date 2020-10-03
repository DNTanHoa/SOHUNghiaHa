using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using SOHU.Mobile.Helpers;
using SOHU.Mobile.Models;
using SOHU.Mobile.Services.Invoice;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace SOHU.Mobile.ViewModels
{
    public class ProjectPageViewModel : ViewModelBase
    {
        private readonly IInvoiceService invoiceService;
        private readonly IPageDialogService pageDialogService;

        public ProjectPageViewModel(INavigationService navigationService,
                                    IInvoiceService invoiceService,
                                    IPageDialogService pageDialogService) : base(navigationService)
        {
            Title = "Danh sách dự án";
            this.invoiceService = invoiceService;
            this.pageDialogService = pageDialogService;

            this.Projects = invoiceService.GetProjects(2020,10).ToObservableCollection();
        }

        public ObservableCollection<Project> Projects { get; set; }

        public Project SelectedProject { get; set; }

        public DelegateCommand ItemTappedCommand { get; private set; }

        public async Task ItemTappedCommandExecute()
        {
            NavigationParameters parameters = new NavigationParameters();
            parameters.Add("ProjectId", SelectedProject.Id);
            await NavigationService.NavigateAsync("ProjectPageDetail", parameters);
        }
    }
}
