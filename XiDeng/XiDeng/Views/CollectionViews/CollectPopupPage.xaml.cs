using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XiDeng.ViewModel.CollectionViewModels;

namespace XiDeng.Views.CollectionViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CollectPopupPage : PopupPage
    {
        private TaskCompletionSource<bool?> _taskCompletionSource;
        public Task<bool?> PopupClosedTask => _taskCompletionSource.Task;
        private CollectPopupPageViewModel vm { get; set; }
        Guid PlanId { get; set; }
        public CollectPopupPage(Guid planId)
        {
            InitializeComponent();
            this.PlanId = planId;
            vm = new CollectPopupPageViewModel(PlanId);

        }
        public bool? IsCollect { get; set; }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await vm.Init(PlanId);
            this.BindingContext = vm;
            _taskCompletionSource = new TaskCompletionSource<bool?>();
        }
        protected override void OnDisappearing()
        { 
            base.OnDisappearing();

            if (vm.IsSubmitted)
            {
                IsCollect = vm.CollectionFolders?.Count(x => x.IsSelected) > 0;
            }
            _taskCompletionSource.SetResult(IsCollect);

        }

        public enum EnumInputType { Ok, YesNo, OkCancel, OkCancelWithInput }
        public enum EnumOutputType { Ok, Yes, No, Cancel }
    }
}