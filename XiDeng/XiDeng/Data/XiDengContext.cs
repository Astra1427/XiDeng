using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XiDeng.Models;
using XiDeng.Models.Collections;
using XiDeng.Models.ExercisePlanModels;
using XiDeng.Models.SkillModels;

namespace XiDeng.Data
{
    public class XiDengContext:DbContext
    {
        public DbSet<ExercisePlanDTO> ExercisePlans { get; set; }
        public DbSet<PlanEachDayDTO> PlanEachDays { get; set; }
        public DbSet<SkillDTO> Skills { get; set; }
        public DbSet<SkillStyleDTO> SkillStyles { get; set; }
        public DbSet<StandardDTO> Standards { get; set; }
        public DbSet<AccountRunningPlanDTO> AccountRunningPlans { get; set; }
        public DbSet<CollectionFolderDTO> CollectionFolders { get; set; }
        public DbSet<ExercisePlanCollectionDTO> ExercisePlanCollections { get; set; }

        public XiDengContext()
        {
            SQLitePCL.Batteries_V2.Init();

            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "xdDB.db3");
            optionsBuilder
                .UseSqlite($"Filename={dbPath}")
                .EnableSensitiveDataLogging();
            
        }

        public void DetachAllEntities()
        {
            var changedEntriesCopy = this.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added ||
                            e.State == EntityState.Modified ||
                            e.State == EntityState.Deleted)
                .ToList();

            foreach (var entry in changedEntriesCopy)
                entry.State = EntityState.Detached;
        }


    }
}
