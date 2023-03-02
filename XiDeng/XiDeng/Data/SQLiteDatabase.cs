using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XiDeng.Models;
using XiDeng.Models.ExercisePlanModels;
using System.Linq;
using Xamarin.Essentials;
using System.IO;
using XiDeng.Models.SkillModels;
using XiDeng.Models.Collections;
using XiDeng.Common;
using XiDeng.Models.ExerciseLogs;

namespace XiDeng.Data
{
    public class SQLiteDatabase
    {
        public readonly SQLiteAsyncConnection database;

        public SQLiteDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<CollectionFolderDTO>().Wait();
            database.CreateTableAsync<ExercisePlanDTO>().Wait();
            database.CreateTableAsync<PlanEachDayDTO>().Wait();
            database.CreateTableAsync<SkillDTO>().Wait();
            database.CreateTableAsync<SkillStyleDTO>().Wait();
            database.CreateTableAsync<StandardDTO>().Wait();
            database.CreateTableAsync<AccountRunningPlanDTO>().Wait();
            database.CreateTableAsync<ExercisePlanCollectionDTO>().Wait();
            database.CreateTableAsync<ExerciseLogDTO>().Wait();
            database.CreateTableAsync<CustomTheme>().Wait();
        }


        /// <summary>
        /// Get all of T async
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetAllAsync<T>(System.Linq.Expressions.Expression<Func<T, bool>> predExpr = null) where T : ModelBase, new()
        {
            if (predExpr != null)
            {
                return await database.Table<T>().Where(predExpr).ToListAsync();
            }
            return await database.Table<T>().ToListAsync();

        }

        /// <summary>
        /// Get a specific of T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predExpr"></param>
        /// <returns></returns>
        public async Task<T> GetAsync<T>(System.Linq.Expressions.Expression<Func<T, bool>> predExpr) where T : ModelBase, new()
        {
            return await database.Table<T>()?.FirstOrDefaultAsync(predExpr);
        }

        /// <summary>
        /// update or insert a model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> SaveAsync<T>(T model) where T : ModelBase, new()
        {
            if (model == null)
            {
                return 0;
            }
            if (await GetAsync<T>(x=>x.Id == model.Id) != null)
            {
                //update an existing model.
                
                return await database.UpdateAsync(model);
            }
            else
            {
                //save a new model.
                return await database.InsertAsync(model);
            }
        }

        public async Task<int> SaveAllAsync<T>(IEnumerable<T> models) where T : ModelBase, new()
        {
            if (models == null || models.Count() == 0)
            {
                return 0;
            }
            int rows = 0;
            foreach (T model in models)
            {
                rows += await SaveAsync(model);
            }
            return rows;
        }

        public async Task<int> UpdateAsync<T>(T model) where T : ModelBase
        {
            return await database.UpdateAsync(model);
        }

        public async Task<int> UpdateAllAsync<T>(IEnumerable<T> models) where T : ModelBase
        {

            return await database.UpdateAllAsync(models);
        }

        public async Task<int> InsertAsync<T>(T model) where T : ModelBase
        {
            return await database.InsertAsync(model);
        }

        public async Task<int> InsertAllAsync<T>(IEnumerable<T> models) where T : ModelBase
        {
            if (models == null || models.Count() == 0)
            {
                return 0;
            }
            return await database.InsertAllAsync(models);
        }


        /// <summary>
        /// Delete a model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns>The number of rows deleted.</returns>
        public async Task<int> DeleteAsync<T>(T model) where T : ModelBase
        {
            return await database.DeleteAsync(model);
        }
        public async Task<int> DeleteAllAsync<T>(IEnumerable<T> models) where T : ModelBase, new()
        {
            int rows = 0;
            foreach (var item in models)
            {
                rows += await database.Table<T>().DeleteAsync(x => x.Id == item.Id);
            }
            return rows;
        }
        public async Task<int> DeleteAllAsync<T>(System.Linq.Expressions.Expression<Func<T, bool>> predExpr) where T : ModelBase, new()
        {

            return await database.Table<T>().DeleteAsync(predExpr);

        }

        public async Task BackupAsync()
        {
            await Share.RequestAsync(new ShareFileRequest(new ShareFile(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "xdDB.db3"))));
            //await database.BackupAsync("data","xdDB");
        }

        public async Task<bool> CheckConflictAsync<T>(T t) where T : ModelBase, new()
        {
            if (t == null)
            {
                return false;
            }

            return (await GetAsync<T>(x => x.Id == t.Id && !x.Updated || x.Id == t.Id && x.IsRemoved)) != null;
        }


        public async Task SetUpdateTable<T>() where T : ModelBase, new()
        {
            var ts = await this.GetAllAsync<T>();
            foreach (var item in ts)
            {
                item.Updated = true;
            }
            await UpdateAllAsync(ts);

        }
    }
}
