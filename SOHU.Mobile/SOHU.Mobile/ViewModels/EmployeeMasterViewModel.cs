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
    public class EmployeeMasterViewModel : ViewModelBase
    {
        private readonly IMembershipService membershipService;

        public EmployeeMasterViewModel(INavigationService navigationService,
                                       IMembershipService membershipService) : base(navigationService)
        {
            ItemTappedCommand = new DelegateCommand<string>(ItemTappedCommandExecute);
            SearchCommand = new DelegateCommand(SearchCommandExecute);
            Title = "Danh sách nhân viên";
            this.membershipService = membershipService;

            this.Employees = membershipService.GetEmployees().ToObservableCollection();
        }

        private string _searchContent;
        public string SearchContent
        {
            get => _searchContent;
            set
            {
                _searchContent = value;
                RaisePropertyChanged(nameof(SearchContent));
            }
        }


        private ObservableCollection<Employee> _employees;

        public ObservableCollection<Employee> Employees 
        {
            get => _employees;
            set
            {
                _employees = value;
                RaisePropertyChanged(nameof(Employees));
            }
        }

        public Employee SelectedEmployee { get; set; }

        public DelegateCommand<string> ItemTappedCommand { get; private set; }
        
        public DelegateCommand SearchCommand { get; private set; }

        public void ItemTappedCommandExecute(string commandType)
        {
            NavigationParameters parameters = new NavigationParameters();
            if (commandType.Equals("Add"))
            {
                parameters.Add("EmployeeId", 0);
            }
            else
            {
                parameters.Add("EmployeeId", SelectedEmployee?.Id);
            }
            NavigationService.NavigateAsync("EmployeeDetail", parameters);
        }

        public void SearchCommandExecute()
        {
            if(string.IsNullOrEmpty(this.SearchContent))
            {
                this.Employees = membershipService.GetEmployees().ToObservableCollection();
            }
            else
            {
                this.Employees = this.Employees?.Where(item => item.FullName.Contains(SearchContent)
                                                               || item.Phone.Contains(SearchContent)).ToObservableCollection();
            }
        }
    }
}
