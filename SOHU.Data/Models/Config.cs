using System;
using System.Collections.Generic;

namespace SOHU.Data.Models
{
    public partial class Config : BaseModel
    {      
        public string GroupName { get; set; }
        public string Code { get; set; }
        public string CodeName { get; set; }
        public string CodenameSub { get; set; }
        public int? SortOrder { get; set; }
    }
}
