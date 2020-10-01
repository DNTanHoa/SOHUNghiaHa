using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SOHU.Mobile.ViewModels
{
    public class InWareHouseMasterViewModel : ViewModelBase
    {
        public InWareHouseMasterViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Phiếu nhập kho";
        }
    }
}
