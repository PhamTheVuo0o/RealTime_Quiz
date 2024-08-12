using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Infrastructure.Persistence.Models
{
    public class SortByInfo
    {
        #region Properties

        [Required]
        public string FieldName { get; set; }

        public bool Ascending { get; set; }

        #endregion Properties
        #region Constructors

        public SortByInfo() { }

        public SortByInfo(string fieldName, bool ascending)
        {
            FieldName = fieldName;
            Ascending = ascending;
        }

        #endregion Constructors
    }
}
