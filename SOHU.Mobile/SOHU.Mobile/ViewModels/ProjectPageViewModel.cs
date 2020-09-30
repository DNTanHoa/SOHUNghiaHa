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
    public class ProjectPageViewModel : ViewModelBase
    {
        public ProjectPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Danh sách dự án";
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
