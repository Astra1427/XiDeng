using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using XiDeng.Models.ExercisePlanModels;
using XiDeng.Models.SkillModels;
using System.Linq;
using XiDeng.Common;
using XiDeng.Common.Converts;

namespace XiDeng.ViewModel.PlanViewModels
{
    public class AddActionPageViewModel:BaseViewModel
    {
        public ImageSource RemoveIcon => Utility.GetImage("minus_5_240");


        public static ObservableCollection<PlanEachDayDTO> planActions;
        public ObservableCollection<PlanEachDayDTO> PlanActions
        {
            get { return planActions; }
            set
            {
                planActions = value;
                this.RaisePropertyChanged(nameof(PlanActions));
            }
        }

        public static IEnumerable<IGrouping<int,PlanEachDayDTO>> groupPlanActions;
        public IEnumerable<IGrouping<int,PlanEachDayDTO>> GroupPlanActions
        {
            get { return groupPlanActions; }
            set
            {
                groupPlanActions = value;
                this.RaisePropertyChanged(nameof(GroupPlanActions));
            }
        }



        private int selectedDayNumber;
        public int SelectedDayNumber
        {
            get { return selectedDayNumber; }
            set
            {
                selectedDayNumber = value;
                this.RaisePropertyChanged(nameof(SelectedDayNumber));
            }
        }

        private SkillDTO selectedSkill;
        public SkillDTO SelectedSkill
        {
            get { return selectedSkill; }
            set
            {
                selectedSkill = value;
                this.RaisePropertyChanged(nameof(SelectedSkill));
            }
        }

        private SkillStyleDTO selectedStyle;
        public SkillStyleDTO SelectedStyle
        {
            get { return selectedStyle; }
            set
            {
                selectedStyle = value;
                if (value == null)
                {
                    this.RaisePropertyChanged(nameof(SelectedStyle));
                    return;
                }
                if(selectedStyle.Standards.Count == 3)
                {
                    selectedStyle.Standards.Add(new StandardDTO() { 
                        Style = SelectedStyle,
                        StyleId = SelectedStyle.Id,
                        Grade = 4 ,
                        GroupNumber = 1,
                        Number = 1
                    });
                }
                this.RaisePropertyChanged(nameof(SelectedStyle));
            }
        }

        private StandardDTO selectedStandard;
        public StandardDTO SelectedStandard
        {
            get { return selectedStandard; }
            set
            {
                selectedStandard = value;
                this.RaisePropertyChanged(nameof(SelectedStandard));

                if (value == null)
                {
                    IsReadOnlyStandard = true;
                    return;
                }
                IsReadOnlyStandard = value.Grade <= 3;

            }
        }

        private bool isReadOnlyStandard;
        public bool IsReadOnlyStandard
        {
            get { return isReadOnlyStandard; }
            set
            {
                isReadOnlyStandard = value;
                this.RaisePropertyChanged(nameof(IsReadOnlyStandard));
            }
        }



        private ObservableCollection<SkillDTO> skills;
        public ObservableCollection<SkillDTO> Skills
        {
            get { return skills; }
            set
            {
                skills = value;
                this.RaisePropertyChanged(nameof(Skills));
            }
        }

        private static List<int> dayList;
        public List<int> DayList
        {
            get { return dayList; }
            set
            {
                dayList = value;
                this.RaisePropertyChanged(nameof(DayList));
            }
        }
        public Binding DayListItemDisplayBinding { get; set; }

        public static Guid PlanID { get; set; }
        public bool ByWeek { get; set; }
        public AddActionPageViewModel(Guid planId,int dayNumber,bool byWeek)
        {
            ByWeek = byWeek;
            //set daylist item display binding
            //DayListItemDisplayBinding = new Binding(".",converter:new DayWeekConverter(),converterParameter:ByWeek);

            if ( dayList?.Count != dayNumber)
            {
                this.PlanActions = this.PlanActions == null ? new ObservableCollection<PlanEachDayDTO>() : this.PlanActions.Where(x=>x.DayNumber <= dayNumber).ToObservableCollection();
                UpdateGroupList();
                this.DayList = new List<int>();
                

                for (int i = 0; i < dayNumber; i++)
                {
                    DayList.Add(i+1);
                    //this.PlanActions.Add(new PlanEachDayDTO() { 
                    //    DayNumber = i+1,
                    //    Id = Guid.NewGuid(),
                    //    PlanId = planId,
                    //});
                }
            }

            this.Skills = SkillDataCommon.Skills;

            OnAppearingCommand = new Command<object>(async obj =>
            {
                await this.Try(async o =>
                {
                    if (PlanID != planId)
                    {
                        PlanID = planId;
                        PlanActions = (await App.Database.GetAllAsync<PlanEachDayDTO>(x => x.PlanId == PlanID)).ToObservableCollection();
                        UpdateGroupList();
                    }
                }, obj, true);
            });


            AddDayActionCommand = new Command<object>(async obj=> {
                if (SelectedDayNumber == 0)
                {
                    await this.Message("请选择一天!");
                    return;
                }
                if (SelectedSkill == null)
                {
                    await this.Message("请选择一个动作组!");
                    return;
                }

                if (SelectedStyle == null)
                {
                    await this.Message("请选择一个动作!");
                    return;
                }

                if (SelectedStandard == null)
                {
                    await this.Message("请选择一个等级!");
                    return;
                }

                if (SelectedStandard.GroupNumber  <= 0 || SelectedStandard.Number <= 0)
                {
                    await this.Message("组数和动作数必须大于0!");
                    return;
                }

                int orderNumer = PlanActions == null || PlanActions.Count == 0 ? 1 : PlanActions.Max(x => x.OrderNumber);
                PlanEachDayDTO DayAction = new PlanEachDayDTO()
                {
                    PlanId = PlanID,
                    DayNumber = SelectedDayNumber,
                    Id = Guid.NewGuid(),
                    StyleID = SelectedStyle.Id,
                    Style = SelectedStyle,
                    GroupNumber = SelectedStandard.GroupNumber,
                    Number = SelectedStandard.Number,
                    IsRestDay = false,
                    OrderNumber = orderNumer
                    //Time
                };
                //remove reset day tag for current day number
                this.PlanActions.Remove(this.PlanActions.FirstOrDefault(x =>x.DayNumber == SelectedDayNumber && x.IsRestDay));
                //add day action
                this.PlanActions.Add(DayAction);

                UpdateGroupList();
            });

            SetRestDayCommand = new Command<object>(async obj=> {
                if (SelectedDayNumber == 0)
                {
                    await this.Message("请选择一天!");
                    return;
                }
                IEnumerable<PlanEachDayDTO> plans = PlanActions.Where(x => x.DayNumber == SelectedDayNumber).ToList();
                foreach (var item in plans)
                {
                    this.PlanActions.Remove(item);
                }
                this.PlanActions.Add(new PlanEachDayDTO
                {
                    Id = Guid.NewGuid(),
                    PlanId = PlanID,
                    DayNumber = SelectedDayNumber,
                    IsRestDay = true,
                    StyleID = null,
                });

                UpdateGroupList();

            });

            RemoveActionCommand = new Command<object>(id=> {
                if (id is Guid guid)
                {
                    this.PlanActions.Remove(this.PlanActions.FirstOrDefault(x=>x.Id == guid));

                    UpdateGroupList();
                }
            });

        }

        private void UpdateGroupList()
        {
            //need optimize
            this.GroupPlanActions = this.PlanActions.OrderBy(x=>x.DayNumber).ThenBy(x=>x.OrderNumber).GroupBy(x => x.DayNumber).OrderBy(x => x.Key);
            
        }

        public Command<object> AddCommand { get; set; }
        public Command<object> AddDayActionCommand { get; set; }
        public Command<object> SetRestDayCommand { get; set; }
        public Command<object> RemoveActionCommand { get; set; }
        public Command<object> OnAppearingCommand { get; set; }


    }
}
