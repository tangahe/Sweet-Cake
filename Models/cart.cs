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
    
    public partial class cart
    {
        public int cid { get; set; }
        public Nullable<int> uid { get; set; }
        public Nullable<int> pid { get; set; }
        public Nullable<int> quantity { get; set; }
        public Nullable<int> size { get; set; }
    
        public virtual size size1 { get; set; }
        public virtual users users { get; set; }
    }
}
