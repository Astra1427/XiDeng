using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Xamarin.Forms;
using XiDeng.Command;

namespace XiDeng.Models.SkillModels
{
    public class SkillDTO : ModelBase
    {
        //[PrimaryKey]
        //public new Guid Id { get; set; }
        public string Name { get; set; } 
        public string Description { get; set; }
        public string ImgUrl { get; set; }
        public Guid OwnerId { get; set; }
        public long OrderNumber { get; set; }
        public new DateTime CreateTime { get; set; }
        public IEnumerable<SkillStyleDTO> SkillStyles { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        [NotMapped]
        public DelegateCommand SkillCommand { get { return new DelegateCommand() { ExecuteAction = new Action<object>(SkillFunc) }; } }

        private async void SkillFunc(object obj)
        {
            await Shell.Current.GoToAsync($"StylePage?id={this.Id}");
        }
    }
}
