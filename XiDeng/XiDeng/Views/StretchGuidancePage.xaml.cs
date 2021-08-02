using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XiDeng.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile),QueryProperty("SKillID","SkillID")]

    public partial class StretchGuidancePage : ContentPage
    {
        public string SkillID { set {
                switch (value)
                {
                    case "1":
                        
                        break;
                }
            } }
        public StretchGuidancePage()
        {
            InitializeComponent();
        }
    }
}