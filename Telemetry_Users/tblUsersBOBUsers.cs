//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Telemetry_Users
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblUsersBOBUsers
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Solt { get; set; }
        public string Mail { get; set; }
        public string Phone { get; set; }
        public int id_Company { get; set; }
        public string Password1 { get; set; }
        public string GUID { get; set; }
        public bool isAdmin { get; set; }
        public bool isSubmit { get; set; }
        public Nullable<bool> isCompanyAdmin { get; set; }
    
        public virtual tblCompanyBOBCompany tblCompanyBOBCompany { get; set; }
    }
}
