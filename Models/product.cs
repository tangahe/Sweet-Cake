//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Projects.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class product
    {
        public int pid { get; set; }
        public string pname { get; set; }
        public Nullable<int> pclassify { get; set; }
        public Nullable<decimal> pprice { get; set; }
        public string pimg { get; set; }
        public string pdsc { get; set; }
    
        public virtual classify classify { get; set; }
    }
}
