using Prism;
using Prism.Ioc;
using SOHU.Mobile.ViewModels;
using SOHU.Mobile.Views;
using Xamarin.Essentials.Interfaces;
using Xamarin.Essentials.Implementation;
using Xamarin.Forms;

namespace SOHU.Mobile
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/LoginPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<HomePage, HomePageViewModel>();
            containerRegistry.RegisterForNavigation<CustomerMaster, CustomerMasterViewModel>();
            containerRegistry.RegisterForNavigation<CustomerDetail, CustomerDetailViewModel>();
            containerRegistry.RegisterForNavigation<InvoiceMaster, InvoiceMasterViewModel>();
            containerRegistry.RegisterForNavigation<InvoiceDetail, InvoiceDetailViewModel>();
            containerRegistry.RegisterForNavigation<InWareHouseMaster, InWareHouseMasterViewModel>();
            containerRegistry.RegisterForNavigation<OutWarehouseMaster, OutWarehouseMasterViewModel>();
            containerRegistry.RegisterForNavigation<SupplierMaster, SupplierMasterViewModel>();
            containerRegistry.RegisterForNavigation<ProjectPage, ProjectPageViewModel>();
            containerRegistry.RegisterForNavigation<AboutPage, AboutPageViewModel>();
        }
    }
}
