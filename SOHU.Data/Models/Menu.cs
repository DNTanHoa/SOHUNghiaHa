using System;
using System.Collections.Generic;
using System.Text;

namespace SOHU.Data.Models
{
    public class Menu : BaseModel
    {
        public string DisplayName { get; set; }

        public string Icon { get; set; }

        public string Action { get; set; }

        public string Controller { get; set; }

        public long? SortOrder { get; set; }

        public bool MenuLeft { get; set; }

        public bool MenuBasic { get; set; }
    }
}
