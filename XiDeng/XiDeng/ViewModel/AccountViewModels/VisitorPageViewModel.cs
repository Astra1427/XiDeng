using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XiDeng.Common;
using XiDeng.Models.AccountModels;
using XiDeng.Models.Collections;
using XiDeng.Models.ExercisePlanModels;
using XiDeng.Views.CollectionViews;
using XiDeng.Views.PlanViews;

namespace XiDeng.ViewModel.AccountViewModels
{
    public class VisitorPageViewModel : BaseViewModel
    {
        private string title;
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                this.RaisePropertyChanged(nameof(Title));
            }
        }


        private VisitAccountInfoDTO visitInfo;
        public VisitAccountInfoDTO VisitInfo
        {
            get { return visitInfo; }
            set
            {
                visitInfo = value;
                this.RaisePropertyChanged(nameof(VisitInfo));
            }
        }
        private IEnumerable<ExercisePlanDTO> disPlans;
        public IEnumerable<ExercisePlanDTO> DisPlans
        {
            get { return disPlans; }
            set
            {
                disPlans = value;
                this.RaisePropertyChanged(nameof(DisPlans));
            }
        }
        private IEnumerable<CollectionFolderDTO> disFolders;
        public IEnumerable<CollectionFolderDTO> DisFolders
        {
            get { return disFolders; }
            set
            {
                disFolders = value;
                this.RaisePropertyChanged(nameof(DisFolders));
            }
        }


        public ImageSource CollectIcon => Utility.GetImage("star_5_240");
        public ImageSource FolderIcon => Utility.GetImage("layer_21_240");
        
        public bool IsAppearing { get; set; }
        public VisitorPageViewModel(Guid authorId)
        {
            AppearingCommand = new Command<object>(async obj =>
            {
                if (VisitInfo != null)
                {
                    return;
                }

                IsAppearing = true;
                base.Appearing(obj);
                await this.Try(async o =>
                {
                    await LoadData(authorId);
                }, obj, true);
                IsAppearing = false;
            });

            RefreshPageCommand = new Command<object>(async obj=> {
                if (IsAppearing)
                {
                    return;
                }
                await this.Try(async o =>
                {
                    await LoadData(authorId);
                }, obj, true);
            });

            GotoFolderDetailCommand = new Command<object>(async obj => {
                await this.GoAsync(nameof(CollectFolderDetailPage) + $"?FolderId={obj}");
            });
            GotoPlanDetailCommand = new Command<object>(async obj =>
            {
                if (obj is ExercisePlanDTO plan)
                {

                    await this.GoAsync(nameof(PlanDetailPage) + $"?PlanId={plan.Id}&ByWeek={plan.Cycle == 0}");

                }
            });
        }

        private async Task LoadData(Guid authorId)
        {
            var response = await ActionNames.Account.GetVisitAccountInfoById.GetStringAsync(paras: authorId.ToString());
            if (response.IsSuccessStatusCode)
            {
                VisitInfo = response.Content.To<VisitAccountInfoDTO>();
                if (VisitInfo == null || VisitInfo.Account == null)
                {
                    await this.Message("加载数据失败！");
                    await this.GoAsync("..");
                    return;
                }

                Title = $"{VisitInfo.Account.Name} 的主页";

                DisPlans = VisitInfo.PublishPlans.OrderByDescending(x=>x.CollectionCount);
                DisFolders = VisitInfo.PublicFolders;
            }
            else
            {
                await this.Message("加载数据失败!\n" + response.Message);
                await this.GoAsync("..");
                return;
            }
        }

        public new Command<object> AppearingCommand { get; set; }
        public Command<object> RefreshPageCommand { get; set; }
        public Command<object> GotoPlanDetailCommand { get; set; }
        public Command<object> GotoFolderDetailCommand { get; set; }
    }
}
