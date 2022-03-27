using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using XiDeng.Common;
using XiDeng.Models.ExercisePlanModels;
using XiDeng.Views.CollectionViews;
using XiDeng.Views.PlanViews;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Extensions;

namespace XiDeng.ViewModel.PlanViewModels
{
    public class PublicPlanPageViewModel:BaseViewModel
    {
        private ObservableCollection<ExercisePlanDTO> plans;
        public ObservableCollection<ExercisePlanDTO> Plans
        {
            get { return plans; }
            set
            {
                plans = value;
                this.RaisePropertyChanged(nameof(Plans));
            }
        }
        public int pageIndex = 0;
        public int pageSize = 10;
        private int orderPriority;
        public int OrderPriority
        {
            get { return orderPriority; }
            set
            {
                orderPriority = value;
                this.RaisePropertyChanged(nameof(OrderPriority));
            }
        }



        public PublicPlanPageViewModel()
        {
            Plans = new ObservableCollection<ExercisePlanDTO>();
            LoadPlansCommand = new Command<object>(async obj=> {
                await this.Try(async o=> {
                    var response = await (ActionNames.ExercisePlan.GetAllPublishPlansByPage+$"?pageIndex={pageIndex}&pageSize={pageSize}&orderPriority={OrderPriority}").GetStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        Plans.AddRange(response.Content.To<ObservableCollection<ExercisePlanDTO>>());
                    }
                    else
                    {
                        await this.Message(response.Message);
                    }
                },obj,true);
            });
            GotoPlanDetailCommand = new Command<object>(async obj=> {
                if (obj is ExercisePlanDTO plan)
                {
                    await Shell.Current.GoToAsync(nameof(PlanDetailPage)+$"?PlanId={plan.Id}&ByWeek={plan.Cycle==0}");
                }
            });
            GotoCollectionFolderPopupPageCommand = new Command<object>(async delegate {
                await Shell.Current.Navigation.PushPopupAsync(new CollectPopupPage());
            });
            LoadPlansCommand?.Execute(null);
        }

        public Command<object> LoadPlansCommand { get; set; }
        public Command<object> GotoPlanDetailCommand { get; set; }
        public Command<object> GotoCollectionFolderPopupPageCommand { get; set; }

    }

}
