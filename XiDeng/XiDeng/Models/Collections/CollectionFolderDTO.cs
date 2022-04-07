using System;
using System.Collections.Generic;
using System.Text;

namespace XiDeng.Models.Collections
{
    public class CollectionFolderDTO : ModelBase
    {
        public Guid AccountId { get; set; }
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                this.RaisePropertyChanged(nameof(Name));
            }
        }
        
        private bool isPublic;
        public bool IsPublic
        {
            get { return isPublic; }
            set
            {
                isPublic = value;
                this.RaisePropertyChanged(nameof(IsPublic));
            }
        }

        [SQLite.Ignore]
        public IEnumerable<ExercisePlanCollectionDTO> ExercisePlanCollections { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        [SQLite.Ignore]
        public bool isSelected { get; set; }
        /// <summary>
        /// Use for CollectionFolderPopupPage
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [SQLite.Ignore]
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                if (isSelected && !value)
                {
                    if (IsAdded)
                    {
                        IsAdded = false;
                    }
                    else
                    {
                        IsDeleted = true;
                    }
                }
                else if (isDeleted && value)
                {
                    IsDeleted = false;
                }
                else if (!isSelected && value)
                {
                    IsAdded = true;
                }
                isSelected = value;
                this.RaisePropertyChanged(nameof(IsSelected));
            }
        }

        private bool isAdded;
        [Newtonsoft.Json.JsonIgnore]
        [SQLite.Ignore]
        public bool IsAdded
        {
            get { return isAdded; }
            set { isAdded = value; }
        }


        private bool isDeleted;
        [Newtonsoft.Json.JsonIgnore]
        [SQLite.Ignore]
        public bool IsDeleted
        {
            get { return isDeleted; }
            set { isDeleted = value; }
        }


    }
}
