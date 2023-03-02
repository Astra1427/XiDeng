using SQLite;
using System;
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
        public string DisGrade { 
            get {
                switch (this.Grade)
                {
                    case 1:
                        return "初级标准";
                    case 2:
                        return "中级标准";
                    case 3:
                        return "高级标准";
                    case 4:
                        return "自由训练";
                    default: return "----";
                }
            } 
        }

    [Newtonsoft.Json.JsonIgnore]
        private SkillStyleDTO style;

        [Newtonsoft.Json.JsonIgnore]
        [SQLite.Ignore]
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
