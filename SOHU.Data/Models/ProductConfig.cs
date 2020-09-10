using System;
using System.Collections.Generic;

namespace SOHU.Data.Models
{
    public partial class ProductConfig : BaseModel
    {
        public int? ProductId { get; set; }
        public int? ConfigId { get; set; }
        public decimal? Value { get; set; }
        public string Title { get; set; }
        public string FileName { get; set; }
    }
}
