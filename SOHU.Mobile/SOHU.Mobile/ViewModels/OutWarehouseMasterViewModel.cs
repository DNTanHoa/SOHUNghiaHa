using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SOHU.Mobile.ViewModels
{
    public class OutWarehouseMasterViewModel : ViewModelBase
    {
        public OutWarehouseMasterViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Phiếu xuất kho";
        }
    }
}
