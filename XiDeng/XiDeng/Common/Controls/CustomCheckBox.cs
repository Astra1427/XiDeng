using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using static Xamarin.Forms.BindableProperty;

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

        public static readonly BindableProperty IsCustomCheckedProperty =
           BindableProperty.Create(nameof(IsCustomChecked),
                                   typeof(bool),
                                   typeof(CustomCheckBox),
                                   false,
                                   BindingMode.TwoWay,
                                   propertyChanged: IsCustomCheckedChanged
                                   );
        public bool IsCustomChecked
        {
            get
            {
                return (bool)GetValue(IsCustomCheckedProperty);
            }
            set
            {
                SetValue(IsCustomCheckedProperty, value);
                this.Check.IsChecked = value;
                this.OnPropertyChanged(nameof(IsCustomChecked));
            }
        }

        private CheckBox check = new CheckBox();

        public CheckBox Check
        {
            get { return check; }
            set { check = value; }
        }

        
        private ContentView CustomView = new ContentView();
        private static BindingPropertyChangedDelegate IsCustomCheckedChanged = new BindingPropertyChangedDelegate((source,oldValue,newValue)=> {
            
        });
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
            IsCustomChecked = !IsCustomChecked;
            Check.IsChecked = IsCustomChecked;
        }
    }
}