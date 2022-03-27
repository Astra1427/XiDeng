using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XiDeng.Data;
using XiDeng.ViewModel;

namespace XiDeng.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile),QueryProperty("ID","id")]
    public partial class StylePage : ContentPage
    {
        public string ID { 
            set { 
            BindingContext = new SkillStylePageViewModel(Guid.Parse(value));
            }
        }
        public StylePage()
        {
            InitializeComponent();
        }

       
    }
}