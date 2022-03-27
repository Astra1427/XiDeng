using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using XiDeng.Models;
using XiDeng.Models.Collections;
using XiDeng.Models.ExercisePlanModels;
using XiDeng.Models.SkillModels;

namespace XiDeng.Common
{
    public class SynchronizationHelper
    {

        public static async Task LocalToCloud()
        {
            var plans = await App.Database.GetAllAsync<ExercisePlanDTO>();

            if (plans != null)
            {
                foreach (ExercisePlanDTO item in plans)
                {
                    item.PlanEachDays = new ObservableCollection<PlanEachDayDTO>(await App.Database.GetAllAsync<PlanEachDayDTO>(x => x.PlanId == item.Id));
                }
            }


            var skill = await App.Database.GetAsync<SkillDTO>(x => x.OwnerId == Utility.LoggedAccount.Id && x.OrderNumber > 6);
            if (skill != null)
            {
                skill.SkillStyles = new ObservableCollection<SkillStyleDTO>(await App.Database.GetAllAsync<SkillStyleDTO>(x => x.SkillId == skill.Id));
            }

            var runningPlans = await App.Database.GetAllAsync<AccountRunningPlanDTO>(x => x.AccountId == Utility.LoggedAccount.Id);
            var response = await ActionNames.Synchronization.LocalToCloud.PostAsync(new SynchronizationDTO
            {
                Account = Utility.LoggedAccount,
                ExercisePlans = plans,
                Skill = skill,
                RunningPlans = runningPlans
            }.ToJson());

            if (response.IsSuccessStatusCode)
            {
                await "同步成功".Message();
            }
            else
            {
                await ("同步失败：\n" + response.Message).Message();
            }
        }

        public static async Task<bool> CloudToLocal()
        {
            try
            {
                var response = await ActionNames.Synchronization.CouldToLocal.GetStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    //save data
                    var model = response.Content.To<SynchronizationDTO>();
                    if (model == null)
                    {
                        await "数据丢失，请重试！".Message();
                        return false;
                    }

                    Utility.LoggedAccount = model.Account;
                    FileHelper.WriteFile(FileHelper.LoginInfoFile, model.Account.ToJson());

                    #region Owner Skills
                    model.Skill.Updated = true;
                    model.Skill.IsRemoved = false;
                    await App.Database.SaveAsync(model.Skill);

                    if (model.Skill.SkillStyles != null)
                    {
                        foreach (var item in model.Skill.SkillStyles)
                        {
                            item.Updated = true;
                            item.IsRemoved = false;
                        }

                        await App.Database.SaveAllAsync(model.Skill.SkillStyles);
                    }
                    #endregion

                    #region My ExercisePlans

                    model.ExercisePlans.ForEach(x => { x.Updated = true; x.IsRemoved = false; });
                    await App.Database.SaveAllAsync(model.ExercisePlans);

                    foreach (ExercisePlanDTO item in model.ExercisePlans)
                    {
                        item.PlanEachDays.ForEach(x => { x.Updated = true; x.IsRemoved = false; });

                        IEnumerable<PlanEachDayDTO> deletedPlanDays = await App.Database.GetAllAsync<PlanEachDayDTO>(x => x.PlanId == item.Id);

                        _ = await App.Database.DeleteAllAsync(deletedPlanDays);

                        _ = await App.Database.InsertAllAsync(item.PlanEachDays);
                    }
                    #endregion

                    #region Running Plans
                    model.RunningPlans.ForEach(x => { x.Updated = true; x.IsRemoved = false; });
                    await App.Database.SaveAllAsync(model.RunningPlans);
                    #endregion

                    #region CollectionFolders
                    await model.CollectionFolders.ForEachAsync(async x =>
                    {
                        x.Updated = true;
                        x.IsRemoved = false;
                        x.ExercisePlanCollections?.ForEach(e =>
                        {
                            e.Updated = true;
                            e.IsRemoved = false;
                        });

                        await App.Database.SaveAllAsync(x.ExercisePlanCollections);
                    });
                    await App.Database.SaveAllAsync(model.CollectionFolders);
                    #endregion

                    #region PlansOfCollectionFolders

                    await model.PlansOfCollectionFolders.ForEachAsync(async x => {
                        x.Updated = true;
                        x.IsRemoved = false;
                        x.PlanEachDays.ForEach(p => {
                            p.Updated = true;
                            p.IsRemoved = false;
                        });
                        var deletedPlanDays = await App.Database.GetAllAsync<PlanEachDayDTO>(a => a.PlanId == x.Id);
                        await App.Database.DeleteAllAsync(deletedPlanDays);
                        await App.Database.InsertAllAsync(x.PlanEachDays);
                    });
                    await App.Database.InsertAllAsync(model.PlansOfCollectionFolders);
                    #endregion

                    await "同步成功".Message();
                    return true;
                }
                else
                {
                    await ("同步失败：\n" + response.Message).Message();
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

    }
}
