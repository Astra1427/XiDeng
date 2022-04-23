using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XiDeng.Models;
using XiDeng.Models.Collections;
using XiDeng.Models.ExerciseLogs;
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

            var exerciseLogs = await App.Database.GetAllAsync<ExerciseLogDTO>(x=>x.AccountId == Utility.LoggedAccount.Id);

            var response = await ActionNames.Synchronization.LocalToCloud.PostAsync(new SynchronizationDTO
            {
                //Account = Utility.LoggedAccount,
                ExercisePlans = plans,
                Skill = skill,
                RunningPlans = runningPlans,
                ExerciseLogs = exerciseLogs
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
                        await "获取数据失败，请重试！".Message();
                        return false;
                    }

                    Utility.LoggedAccount = model.Account;
                    FileHelper.WriteFile(FileHelper.LoginInfoFile, model.Account.ToJson());

                    #region Owner Skills
                    model.Skill.Updated = true;
                    model.Skill.IsRemoved = false;
                    int rows = await App.Database.SaveAsync(model.Skill);
                    StringBuilder RecordCountInfo = new StringBuilder($"Skill:{rows}\n");
                    rows = 0;
                    if (model.Skill.SkillStyles != null)
                    {
                        foreach (var item in model.Skill.SkillStyles)
                        {
                            item.Updated = true;
                            item.IsRemoved = false;
                        }

                        rows += await App.Database.SaveAllAsync(model.Skill.SkillStyles);
                    }
                    RecordCountInfo.Append($"Styles:{rows}");
                    #endregion

                    #region My ExercisePlans

                    model.ExercisePlans?.ForEach(x => { x.Updated = true; x.IsRemoved = false; });
                    rows = await App.Database.SaveAllAsync(model.ExercisePlans);
                    RecordCountInfo.AppendLine($"ExercisePlans:{rows}");
                    int deleteRows = rows = 0;
                    foreach (ExercisePlanDTO item in model.ExercisePlans)
                    {
                        item.PlanEachDays.ForEach(x => { x.Updated = true; x.IsRemoved = false; });

                        IEnumerable<PlanEachDayDTO> deletedPlanDays = await App.Database.GetAllAsync<PlanEachDayDTO>(x => x.PlanId == item.Id);

                        deleteRows += await App.Database.DeleteAllAsync(deletedPlanDays);

                        rows += await App.Database.InsertAllAsync(item.PlanEachDays);
                    }
                    RecordCountInfo.AppendLine($"deletedPlanDays:{rows}___Insert:{rows}");
                    #endregion

                    #region Running Plans
                    model.RunningPlans?.ForEach(x => { x.Updated = true; x.IsRemoved = false; });
                    rows = await App.Database.SaveAllAsync(model.RunningPlans);
                    RecordCountInfo.AppendLine($"RunningPlans:{rows}");
                    #endregion

                    #region CollectionFolders
                    deleteRows = rows = 0;
                    await model.CollectionFolders?.ForEachAsync(async x =>
                    {
                        x.Updated = true;
                        x.IsRemoved = false;
                        await x.ExercisePlanCollections?.ForEachAsync(async e =>
                        {
                            e.Updated = true;
                            e.IsRemoved = false;
                            // 对比数据， 如果本地存在就先清空旧数据再插入,如果不存在就只执行插入.
                            var checkData = await App.Database.GetAllAsync<ExercisePlanCollectionDTO>(epcd => epcd.ExercisePlanId == e.ExercisePlanId && epcd.CollectionFolderId == e.CollectionFolderId);
                            if (checkData != null && checkData.Count() != 0)
                            {
                                //清空旧数据
                                deleteRows += await App.Database.DeleteAllAsync(checkData);
                            }

                        });

                        //插入
                        rows += await App.Database.InsertAllAsync(x.ExercisePlanCollections);
                    });
                    RecordCountInfo.AppendLine($"ExercisePlanCollection: Delete:{deleteRows}___Insert:{rows}");
                    rows = await App.Database.SaveAllAsync(model.CollectionFolders);
                    RecordCountInfo.AppendLine($"CollectionFolders:{rows}");

                    #endregion

                    #region PlansOfCollectionFolders
                    deleteRows = rows = 0;
                    await model.PlansOfCollectionFolders?.ForEachAsync(async x => {
                        x.Updated = true;
                        x.IsRemoved = false;
                        x.PlanEachDays.ForEach(p => {
                            p.Updated = true;
                            p.IsRemoved = false;
                        });
                        var deletedPlanDays = await App.Database.GetAllAsync<PlanEachDayDTO>(a => a.PlanId == x.Id);
                        deleteRows += await App.Database.DeleteAllAsync(deletedPlanDays);
                        rows += await App.Database.SaveAllAsync(x.PlanEachDays);
                    });
                    RecordCountInfo.AppendLine($"PlansOfCollectionFolders: Delete:{deleteRows}___Insert:{rows}");
                    await App.Database.SaveAllAsync(model.PlansOfCollectionFolders?.Where(pocf => !model.ExercisePlans.Any(ep => ep.Id == pocf.Id)));
                    #endregion

                    #region Exercise Logs
                    rows = await App.Database.SaveAllAsync(model.ExerciseLogs);
                    #endregion

                    await "同步成功".Message();
#if DEBUG
                    await RecordCountInfo.ToString().Message();
#endif

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
