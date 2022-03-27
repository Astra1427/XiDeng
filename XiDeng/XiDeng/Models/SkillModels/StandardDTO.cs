using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;


namespace XiDeng.Models.SkillModels
{
    public class StandardDTO : ModelBase
    {
        //[PrimaryKey]
        //public new Guid Id { get; set; }
        public Guid StyleId { get; set; }
        public int GroupNumber { get; set; }
        public int Number { get; set; }
        public int Grade { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        private SkillStyleDTO style;

        [Newtonsoft.Json.JsonIgnore]
        [NotMapped]
        public SkillStyleDTO Style
        {
            get { 
                if(style == null)
                    style = SkillDataCommon.Skills.FirstOrDefault(x => x.SkillStyles.Any(s => s.Id == StyleId)).SkillStyles.FirstOrDefault(x => x.Id == StyleId);
                return style;
            }
            set {

                style = value; 
            }
        }

        public override string ToString()
        {
            if (!Style.TraningType)
            {
                return $"{GroupNumber} 组 {Number}次";
            }
            else
            {
                return $"{GroupNumber} 组 {Number}秒";
            }

        }


        public StandardDTO(StandardDTO standard)
        {
            this.Id = standard.Id;
            this.StyleId = standard.StyleId;
            this.GroupNumber = standard.GroupNumber;
            this.Number = standard.Number;
            this.Grade = standard.Grade;
            this.style = standard.style;
            

        }

        public StandardDTO()
        {

        }
    }
}
