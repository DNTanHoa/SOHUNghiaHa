using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SOHU.Mobile.ViewModels
{
    public class SupplierMasterViewModel : ViewModelBase
    {
        public SupplierMasterViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Nhà cung cấp";
        }
    }
}
