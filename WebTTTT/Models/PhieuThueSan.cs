//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebTTTT.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PhieuThueSan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PhieuThueSan()
        {
            this.HoaDons = new HashSet<HoaDon>();
            this.LapPhieux = new HashSet<LapPhieu>();
        }
    
        public string maPT { get; set; }
        public System.DateTime ngayLapPhieu { get; set; }
        public string maSan { get; set; }
        public int maKH { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoaDon> HoaDons { get; set; }
        public virtual KhachHang KhachHang { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LapPhieu> LapPhieux { get; set; }
        public virtual San San { get; set; }
    }
}
