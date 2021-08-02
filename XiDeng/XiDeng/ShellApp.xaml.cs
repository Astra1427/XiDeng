using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XiDeng.Common;
using XiDeng.Data;
using XiDeng.Views;

namespace XiDeng
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShellApp : Shell
    {
        public ShellApp()
        {
            InitializeComponent();
            RegisterRout();
        }

        public void RegisterRout()
        {
            //Init routing
            Routing.RegisterRoute("StylePage", typeof(StylePage));
            Routing.RegisterRoute("SkillStyleDetailPage", typeof(SkillStyleDetailPage));
            Routing.RegisterRoute("TraningPage",typeof(TraningPage));
            Routing.RegisterRoute("SettingPage",typeof(SettingPage));
            Routing.RegisterRoute("ThanksPage",typeof(ThanksPage));
            Routing.RegisterRoute("AboutPage", typeof(AboutPage));
            Routing.RegisterRoute("ExerciseLogPage", typeof(ExerciseLogPage));
            Routing.RegisterRoute("ExerciseProjectDetailPage",typeof(ExerciseProjectDetailPage));
            Routing.RegisterRoute("ExerciseDateDetailPage",typeof(ExerciseDateDetailPage));
            Routing.RegisterRoute("StatisticsPage",typeof(StatisticsPage));
            Routing.RegisterRoute("StretchGuidancePage", typeof(StretchGuidancePage));
            Routing.RegisterRoute("DonationPage", typeof(DonationPage)) ;
        }

    }
}