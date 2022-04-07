using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;
using XiDeng.Common;
using XiDeng.Models.ExercisePlanModels;
using XiDeng.Views.PlanViews;

namespace XiDeng.ViewModel.PlanViewModels
{
    public class AddPlanPageViewModel:PlanViewModel
    {

        //private string planName;
        //public string PlanName
        //{
        //    get { return planName; }
        //    set
        //    {
        //        planName = value;
        //        this.RaisePropertyChanged(nameof(PlanName));
        //    }
        //}
        //private string introduce;
        //public string Introduce
        //{
        //    get { return introduce; }
        //    set
        //    {
        //        introduce = value;
        //        this.RaisePropertyChanged(nameof(Introduce));
        //    }
        //}



        //private bool byDay = true;
        //public bool ByDay
        //{
        //    get { return byDay; }
        //    set
        //    {
        //        byDay = value;
        //        this.Plan.Cycle = 1;
        //        this.RaisePropertyChanged(nameof(ByDay));
        //    }
        //}
        //private bool byWeek = false;
        //public bool ByWeek
        //{
        //    get { return byWeek; }
        //    set
        //    {
        //        byWeek = value;
        //        this.Plan.Cycle = 0;
        //        DayNumber = 7;
        //        this.RaisePropertyChanged(nameof(ByWeek));
        //    }
        //}
        //private int dayNumber = 1;
        //public int DayNumber
        //{
        //    get { return dayNumber; }
        //    set
        //    {
        //        dayNumber = value;
        //        this.RaisePropertyChanged(nameof(DayNumber));
        //    }
        //}

        //private bool isPublic;
        //public bool IsPublic
        //{
        //    get { return isPublic; }
        //    set
        //    {
        //        isPublic = value;
        //        this.RaisePropertyChanged(nameof(IsPublic));
        //    }
        //}
        //private bool isLoop;
        //public bool IsLoop
        //{
        //    get { return isLoop; }
        //    set
        //    {
        //        isLoop = value;
        //        this.RaisePropertyChanged(nameof(IsLoop));
        //    }
        //}

        //private IEnumerable<IGrouping<int, PlanEachDayDTO>> groupPlanActions;
        //public IEnumerable<IGrouping<int, PlanEachDayDTO>> GroupPlanActions
        //{
        //    get { return groupPlanActions; }
        //    set
        //    {
        //        groupPlanActions = value;
        //        this.RaisePropertyChanged(nameof(GroupPlanActions));
        //    }
        //}

        public AddPlanPageViewModel() : base(null)
        {


            SelectCoverCommand = new Command<object>(async delegate {
                var fileResult = await FilePicker.PickAsync(PickOptions.Images);
                if (fileResult != null)
                {
                    var stream = await fileResult.OpenReadAsync();
                    Cover = ImageSource.FromStream(() => stream);
                }
            });
            CancelCommand = new Command<object>(async delegate {
                await this.GoAsync("../");
            });


            #region old

            //GotoAddActionPageCommand = new Command<object>(async delegate {
            //    if (DayNumber <= 0)
            //    {
            //        await this.Message("天数必须大于0!");
            //        return;
            //    }
            //    await Shell.Current.GoAsync(nameof(AddActionPage)+$"?PlanID={Plan.Id}&DayNumber={DayNumber}&ByWeek={ByWeek}");

            //});
            /*SubmitCommand = new Command<object>(async obj=> {
                //submit 

                if (PlanName.IsEmpty())
                {
                    await this.Message("计划名不能为空!");
                    return;
                }

                if (Introduce.IsEmpty())
                {
                    await this.Message("描述不能为空!");
                    return;
                }

                //await this.Message(AddActionPageViewModel.planActions.Count.ToString());

                if (AddActionPageViewModel.planActions == null || AddActionPageViewModel.planActions.Count == 0)
                {
                    await this.Message("请设置动作!");
                    return;
                }

                await this.Try(async o=>
                {
                    if (AddActionPageViewModel.groupPlanActions.Count() != DayNumber)
                    {
                        //get no action day

                        string noActionDays = "";
                        int index = 0;
                        var dayList = AddActionPageViewModel.planActions.Select(x => x.DayNumber).OrderBy(x => x).Distinct().ToList();
                        for (int i = 0; i < DayNumber; i++)
                        {
                            if (!dayList.Any(x => x == (i + 1)))
                            {
                                noActionDays += $"\n第{(i + 1)}天";
                            }
                        }

                        await this.Message("以下天数还未设置动作:" + noActionDays);

                        return;
                    }

                    ExercisePlanDTO newPlan = new ExercisePlanDTO()
                    {
                        Id = Plan.Id,
                        AccountId = Utility.LoggedAccount.Id,
                        Cycle = ByDay ? 1 : 0,
                        CollectionCount = 0,
                        IsPublic = IsPublic,
                        IsLoop = IsLoop,
                        Name = PlanName,
                        Description = Introduce,
                        CoverUrl = null,
                        PlanEachDays = AddActionPageViewModel.planActions.Where(x => x.DayNumber <= DayNumber).ToObservableCollection()
                    };

                    //if online
                    //submit to datbase and save to native
                    bool Updated = false;
                    var response = await ActionNames.ExercisePlan.AddPlan.PostAsync(newPlan.ToJson());
                    if (response.IsSuccessStatusCode)
                    {
                        //update plan to local data
                        await this.Message("提交成功!");
                        Updated = true;
                    }
                    else
                    {
                        await this.Message(response.Message);
                    }


                    //save to native
                    newPlan.Updated = Updated;
                    int rows = await App.Database.SaveAsync(newPlan);
                    await this.Message("Save new plan rows:" + rows);
                    newPlan.PlanEachDays.ForEach(x=>x.Updated = Updated);
                    rows = await App.Database.database.InsertAllAsync(newPlan.PlanEachDays);
                    await this.Message("Save actions rows:"+ rows);
                },obj,true);


            });*/
            #endregion


        }

        //public Command<object> GotoAddActionPageCommand { get; set; }
        //public Command<object> SubmitCommand { get; set; }
        public Command<object> CancelCommand { get; set; }
        public Command<object> SelectCoverCommand { get; set; }


    }
}
