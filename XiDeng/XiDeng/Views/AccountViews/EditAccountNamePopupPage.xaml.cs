using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XiDeng.ViewModel.AccountViewModels;

namespace XiDeng.Views.AccountViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditAccountNamePopupPage : PopupPage
    {
        private TaskCompletionSource<bool> _taskCompletionSource;
        public Task<bool> PopupClosedTask => _taskCompletionSource.Task;
        EditAccountNamePopupPageViewModel vm { get; set; }
        public EditAccountNamePopupPage()
        {
            InitializeComponent();
            vm = new EditAccountNamePopupPageViewModel();
            this.BindingContext = vm;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _taskCompletionSource = new TaskCompletionSource<bool>();

        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            _taskCompletionSource.SetResult(vm.IsSubmitted);
        }
    }
}