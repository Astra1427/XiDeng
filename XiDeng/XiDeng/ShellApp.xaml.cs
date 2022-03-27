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
using XiDeng.Views.AccountViews;
using XiDeng.Views.CollectionViews;
using XiDeng.Views.PlanViews;

namespace XiDeng
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShellApp : Shell
    {
        public ImageSource IconHome { get; set; }
        public ImageSource IconProfile { get; set; }
        public ImageSource IconStar { get; set; }
        public ShellApp()
        {
            InitializeComponent();
            RegisterRout();
            IconHome = Utility.GetImage("home_6_240");
            IconStar = Utility.GetImage("weather_116_240");
            IconProfile = Utility.GetImage("user_5_240");
            this.BindingContext = this;
            
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

            #region Account Pages
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage)) ;
            Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage)) ;
            Routing.RegisterRoute(nameof(ForgotPasswordPage), typeof(ForgotPasswordPage)) ;
            Routing.RegisterRoute(nameof(PersonalInfoPage), typeof(PersonalInfoPage)) ;
            #endregion

            #region Plan Pages
            Routing.RegisterRoute(nameof(MyPlanPage), typeof(MyPlanPage));
            Routing.RegisterRoute(nameof(AddPlanPage), typeof(AddPlanPage));
            Routing.RegisterRoute(nameof(AddActionPage), typeof(AddActionPage));
            Routing.RegisterRoute(nameof(PlanDetailPage), typeof(PlanDetailPage));
            Routing.RegisterRoute(nameof(UpdatePlanPage), typeof(UpdatePlanPage));

            Routing.RegisterRoute(nameof(TraningPlanPage), typeof(TraningPlanPage));

            #endregion
            #region Collection Folder
            Routing.RegisterRoute(nameof(CollectFolderListPage), typeof(CollectFolderListPage));
            Routing.RegisterRoute(nameof(CollectFolderDetailPage), typeof(CollectFolderDetailPage));

            #endregion
        }

    }
}