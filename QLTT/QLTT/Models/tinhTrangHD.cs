//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QLTT.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tinhTrangHD
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tinhTrangHD()
        {
            this.HoaDonTS = new HashSet<HoaDonT>();
        }
    
        public int maTinhTrang { get; set; }
        public string mo_ta { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoaDonT> HoaDonTS { get; set; }
    }
}
