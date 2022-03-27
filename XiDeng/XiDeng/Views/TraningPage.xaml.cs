using Android.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XiDeng.ViewModel;

namespace XiDeng.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty("SkillID", "SkillID")]
    [QueryProperty("StyleID", "StyleID")]
    [QueryProperty("StandardJson", "StandardJson")]
    public partial class TraningPage : ContentPage
    {
        


        private string skillID;

        public string SkillID
        {
            get { return skillID; }
            set { 
                skillID = value; 
            }
        }

        public string StyleID { get; set; }
        private string standardJson;

        public string StandardJson
        {
            get { return standardJson; }
            set {
                standardJson = Uri.UnescapeDataString(value);
                this.BindingContext = new TraningPageViewModel2(SkillID, StyleID, StandardJson);
            }
        }

        public TraningPage()
        {
            InitializeComponent();
            
            
        }

        protected override bool OnBackButtonPressed()
        {
            (this.BindingContext as TraningPageViewModel2).ClearSound();
            (this.BindingContext as TraningPageViewModel2).IsBack = true;
            return base.OnBackButtonPressed();
        }
    }
}