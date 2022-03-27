using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace XiDeng.Common.Controls
{
    public class CustomCheckBox : ContentView
    {


        private View customContent;

        public View CustomContent
        {
            get { return customContent; }
            set
            {
                customContent = value;
                this.CustomView.Content = value;
            }
        }


        /*
        <StackLayout Orientation="Horizontal" >
            <CheckBox IsChecked="{Binding IsChecked}"/>
            <ContentView Content="{Binding CustomContent}">
                <ContentView.GestureRecognizers>
                    <TapGestureRecognizer Tapped="TapGestureRecognizer_Tapped"/>
                </ContentView.GestureRecognizers>
            </ContentView>
        </StackLayout>
        */

        public static readonly BindableProperty IsCheckedProperty =
           BindableProperty.Create(nameof(IsChecked),
                                   typeof(bool),
                                   typeof(CustomCheckBox),
                                   true,
                                   BindingMode.TwoWay
                                   );
        public bool IsChecked
        {
            get
            {
                return (bool)GetValue(IsCheckedProperty);
            }
            set
            {
                SetValue(IsCheckedProperty, value);
                this.Check.IsChecked = value;
                this.OnPropertyChanged(nameof(IsChecked));
            }
        }

        private CheckBox check = new CheckBox();

        public CheckBox Check
        {
            get { return check; }
            set { check = value; }
        }

        
        private ContentView CustomView = new ContentView();

        public CustomCheckBox()
        {
            Content = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children = {
                    Check,
                    CustomView
                }
            };
            var tap = new TapGestureRecognizer();
            tap.Tapped += TapGestureRecognizer_Tapped;
            CustomView.GestureRecognizers.Add(tap);


        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            IsChecked = !IsChecked;
            Check.IsChecked = IsChecked;
        }
    }
}