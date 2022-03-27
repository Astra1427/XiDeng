using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using XiDeng.Common;
using XiDeng.Models.ExercisePlanModels;
using XiDeng.Views.PlanViews;

namespace XiDeng.ViewModel.PlanViewModels
{
    public class MyPlanPageViewModel : BaseViewModel
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
        public int DayNumber { get; set; }
        public ImageSource AddIcon => Utility.GetImage("plus_5_240");
        public MyPlanPageViewModel()
        {
            LoadPlansCommand = new Command<object>(async obj=> {

                await this.Try(async o =>
                {

                    var response = await ActionNames.ExercisePlan.GetAllPlans.GetStringAsync();
                    ObservableCollection<ExercisePlanDTO> ps = null;

                    if (response.IsSuccessStatusCode)
                    {

                        ps = response.Content.To<ObservableCollection<ExercisePlanDTO>>();
                        bool IsConflict = false;
                        //save database data to sqlite
                        
                        //await App.Database.SaveAllAsync(ps);
                        foreach (var item in ps)
                        {
                            if (await App.Database.CheckConflictAsync(item))
                            {
                                IsConflict = true;
                                continue;
                            }

                            item.Updated = true;
                            await App.Database.SaveAsync(item);

                            foreach (var day in item.PlanEachDays)
                            {
                                if (await App.Database.CheckConflictAsync(day))
                                {
                                    IsConflict = true;
                                    continue;
                                }
                                day.Updated = true;
                                await App.Database.SaveAsync(day);
                            }
                        }

                        if (IsConflict)
                        {
                            await this.Message("本地数据与云端数据有冲突，请返回个人菜单界面进行同步后再尝试此操作！");
                            await Shell.Current.GoToAsync("../");
                            return;
                        }
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.SeeOther)
                    {
                        await this.Message(response.Message);
                        //load offline data
                        ps = new ObservableCollection<ExercisePlanDTO>((await App.Database.GetAllAsync<ExercisePlanDTO>(x => !x.IsRemoved && x.AccountId == Utility.LoggedAccount.Id)).OrderBy(x => x.CreateTime));

                        foreach (ExercisePlanDTO plan in ps)
                        {
                            plan.PlanEachDays = (await App.Database.GetAllAsync<PlanEachDayDTO>(x => x.PlanId == plan.Id && !x.IsRemoved)).ToObservableCollection() ;
                        }
                        this.Plans = ps;
                    }
                    else
                    {
                        await this.Message(response.Message);
                        
                    }

                    this.Plans = ps;


                }, obj, true);
            });


            GotoAddPlanPageCommand = new Command<object>(async obj=> {
                await Shell.Current.GoToAsync(nameof(AddPlanPage));
            });

            GotoPlanDetailCommand = new Command<object>(async obj=> {
                if (obj is Guid planId)
                {
                    var plan = await App.Database.GetAsync<ExercisePlanDTO>(x=>x.Id == planId);
                    if (plan == null)
                    {
                        await this.Message("该计划不存在!");
                        return;
                    }
                    await Shell.Current.GoToAsync(nameof(PlanDetailPage)+$"?PlanId={planId}&ByWeek={plan.Cycle == 0}");
                }
            });


            LoadPlansCommand?.Execute(null);
        }

        public Command<object> LoadPlansCommand { get; set; }
        public Command<object> GotoAddPlanPageCommand { get; set; }
        public Command<object> GotoPlanDetailCommand { get; set; }


    }
}
