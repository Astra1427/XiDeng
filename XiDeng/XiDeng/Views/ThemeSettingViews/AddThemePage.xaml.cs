using Rg.Plugins.Popup.Extensions;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XiDeng.Views.ThemeSettingViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [DesignTimeVisible(false)]
    public partial class AddThemePage : ContentPage
    {
        public AddThemePage()
        {
            InitializeComponent();
            this.BindingContext = new ViewModel.BaseViewModel();
            
        }

        private void SKCanvasView_PaintSurface(object sender, SkiaSharp.Views.Forms.SKPaintSurfaceEventArgs e)
        {
            SKCanvas canvas = e.Surface.Canvas;
            
            var scale = 21F;
            SKPath path = new SKPath();
            path.MoveTo(-1 * scale, -1 * scale);
            path.LineTo(0 * scale, -1 * scale);
            path.LineTo(0 * scale, 0 * scale);
            path.LineTo(1 * scale, 0 * scale);
            path.LineTo(1 * scale, 1 * scale);
            path.LineTo(0 * scale, 1 * scale);
            path.LineTo(0 * scale, 0 * scale);
            path.LineTo(-1 * scale, 0 * scale);
            path.LineTo(-1 * scale, -1 * scale);

            SKMatrix matrix = SKMatrix.MakeScale(2 * scale, 2 * scale);
            SKPaint paint = new SKPaint
            {
                PathEffect = SKPathEffect.Create2DPath(matrix, path),
                Color = Color.LightGray.ToSKColor(),
                IsAntialias = true
            };
            var patternRect = new SKRect(0, 0, ((SKCanvasView)sender).CanvasSize.Width, ((SKCanvasView)sender).CanvasSize.Height);
            canvas.Save();
            canvas.DrawRect(patternRect, paint);
            canvas.Restore();
        }

        private void ColorWheel1_SelectedColorChanged(object sender, ColorPicker.BaseClasses.ColorPickerEventArgs.ColorChangedEventArgs e)
        {
            //Shell.SetTabBarBackgroundColor(Shell.Current, ColorWheel1.SelectedColor);
            //Shell.SetBackgroundColor(Shell.Current, ColorWheel2.SelectedColor);
            if (this.BindingContext is ViewModel.BaseViewModel bm)
            {
                //ViewModel.BaseViewModel.CurrentTheme = new Models.CustomTheme() {
                //    TopBarHexColor = ColorWheel1.SelectedColor.ToHex(),
                //    BottomBarHexColor = ColorWheel2.SelectedColor.ToHex(),
                //};
                //ViewModel.BaseViewModel.TabColor = ColorWheel1.SelectedColor;
            }
        }

        private async void Popup_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.Navigation.PushPopupAsync(new ColorWheelPopupPage());
        }
    }
}