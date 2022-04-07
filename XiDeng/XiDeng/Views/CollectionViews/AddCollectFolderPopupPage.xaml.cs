﻿using Rg.Plugins.Popup.Pages;
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
    public partial class AddCollectFolderPopupPage : PopupPage
    {
        private TaskCompletionSource<bool> _taskCompletionSource;
        public Task<bool> PopupClosedTask => _taskCompletionSource.Task;
        private AddCollectFolderPopupPageViewModel vm;
        public AddCollectFolderPopupPage()
        {
            InitializeComponent();
            vm = new AddCollectFolderPopupPageViewModel();
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