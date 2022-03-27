﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

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

        [Newtonsoft.Json.JsonIgnore]
        public IEnumerable<ExercisePlanCollectionDTO> ExercisePlanCollections { get; set; }

        private bool isSelected;
        /// <summary>
        /// Use for CollectionFolderPopupPage
        /// </summary>
        [NotMapped]
        [Newtonsoft.Json.JsonIgnore]
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                this.RaisePropertyChanged(nameof(IsSelected));
            }
        }

    }
}
