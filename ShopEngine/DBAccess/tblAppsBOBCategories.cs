//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ShopEngine.DBAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblAppsBOBCategories
    {
        public tblAppsBOBCategories()
        {
            this.tblAppsBOBApps = new HashSet<tblAppsBOBApps>();
            this.tblAppsBOBApps1 = new HashSet<tblAppsBOBApps>();
        }
    
        public int id { get; set; }
        public string Name { get; set; }
    
        public virtual ICollection<tblAppsBOBApps> tblAppsBOBApps { get; set; }
        public virtual ICollection<tblAppsBOBApps> tblAppsBOBApps1 { get; set; }
    }
}