using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XiDeng.Views.ThemeSettingViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ColorWheelPopupPage : PopupPage
    {
        private TaskCompletionSource<string> _taskCompletionSource;
        public Task<string> PopupClosedTask => _taskCompletionSource.Task;
        public ColorWheelPopupPage()
        {
            InitializeComponent();
        }

        

        private void ColorWheel1_SelectedColorChanged(object sender, ColorPicker.BaseClasses.ColorPickerEventArgs.ColorChangedEventArgs e)
        {
            txtColorCode.Text = ColorWheel1.SelectedColor.ToHex().Substring(1);
            
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.Navigation.PopPopupAsync();
        }

        private async void Confirm_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.Navigation.PopPopupAsync();
            _taskCompletionSource.SetResult(ColorWheel1.SelectedColor.ToHex());
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            _taskCompletionSource = new TaskCompletionSource<string>();
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        private void txtColorCode_Unfocused(object sender, FocusEventArgs e)
        {
            ColorWheel1.SelectedColor = Color.FromHex("#" + txtColorCode.Text);
        }
    }
}