using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Infrastructure.Models
{
    public class RestricItem
    {
        public int RestrictCycleTimeSeconds { get; set; }
        public string? RestrictRequestEndPoints { get; set; }
        public RestricItem()
        {
            RestrictCycleTimeSeconds = 0;
            RestrictRequestEndPoints = "";
        }
    }
}
