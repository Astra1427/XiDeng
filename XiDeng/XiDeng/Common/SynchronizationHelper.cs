using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
            var plans = await App.Database.ExercisePlans.ToListAsync() ;

            if (plans != null)
            {
                foreach (ExercisePlanDTO item in plans)
                {
                    
                    item.PlanEachDays = await App.Database.PlanEachDays.Where(x => x.ExercisePlanDTOId == item.Id).ToListAsync();
                }
            }


            var skill = await App.Database.Skills.FirstOrDefaultAsync(x => x.OwnerId == Utility.LoggedAccount.Id && x.OrderNumber > 6);
            if (skill != null)
            {
                skill.SkillStyles = await App.Database.SkillStyles.Where(x => x.SkillId == skill.Id).ToListAsync();
            }

            var runningPlans = await App.Database.AccountRunningPlans.Where(x => x.AccountId == Utility.LoggedAccount.Id).ToListAsync();
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
                    int rows = 0;
                    #region Owner Skills
                    model.Skill.Updated = true;
                    model.Skill.IsRemoved = false;
                    rows = await App.Database.Skills.AddOrUpdateAsync(model.Skill);

                    if (model.Skill.SkillStyles != null)
                    {
                        foreach (var item in model.Skill.SkillStyles)
                        {
                            item.Updated = true;
                            item.IsRemoved = false;
                        }

                        rows = await App.Database.SkillStyles.AddOrUpdateRangeAsync(model.Skill.SkillStyles);
                    }
                    #endregion
                    rows = await App.Database.SaveChangesAsync();


                    #region My ExercisePlans

                    model.ExercisePlans.ForEach(x => { x.Updated = true; x.IsRemoved = false; });
                    rows = await App.Database.ExercisePlans.AddOrUpdateRangeAsync(model.ExercisePlans);
                    foreach (ExercisePlanDTO item in model.ExercisePlans)
                    {
                        item.PlanEachDays.ForEach(x => { x.Updated = true; x.IsRemoved = false; });

                        IEnumerable<PlanEachDayDTO> deletedPlanDays = await App.Database.PlanEachDays.Where(x => x.ExercisePlanDTOId == item.Id).ToListAsync();

                        App.Database.PlanEachDays.RemoveRange(deletedPlanDays);
                        rows = await App.Database.SaveChangesAsync();
                        App.Database.PlanEachDays.AddRange(item.PlanEachDays);
                        rows = await App.Database.SaveChangesAsync();
                    }
                    #endregion

                    #region Running Plans
                    model.RunningPlans.ForEach(x => { x.Updated = true; x.IsRemoved = false; });
                    rows = await App.Database.AccountRunningPlans.AddOrUpdateRangeAsync(model.RunningPlans);
                    #endregion
                    rows = await App.Database.SaveChangesAsync();

                    #region CollectionFolders
                    model.CollectionFolders.ForEach(async x => {
                        x.Updated = true;
                        x.IsRemoved = false;
                        x.ExercisePlanCollections.ForEach(e => {
                            e.Updated = true;
                            e.IsRemoved = false;
                        });
                        await App.Database.ExercisePlanCollections.AddOrUpdateRangeAsync(x.ExercisePlanCollections);
                    });
                    await App.Database.CollectionFolders.AddRangeAsync(model.CollectionFolders);
                    #endregion
                    rows = await App.Database.SaveChangesAsync();

                    #region PlansOfCollectionFolders

                    model.PlansOfCollectionFolders.ForEach(async x => {
                        x.Updated = true;
                        x.IsRemoved = false;
                        x.PlanEachDays.ForEach(p => {
                            p.Updated = true;
                            p.IsRemoved = false;
                        });
                        var deletedPlanDays = await App.Database.PlanEachDays.Where(a => a.ExercisePlanDTOId == x.Id).ToListAsync();
                        App.Database.PlanEachDays.RemoveRange(deletedPlanDays);
                        await App.Database.SaveChangesAsync();
                        await App.Database.PlanEachDays.AddRangeAsync(x.PlanEachDays);
                        rows = await App.Database.SaveChangesAsync();
                    });
                    await App.Database.ExercisePlans.AddRangeAsync(model.PlansOfCollectionFolders);
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
