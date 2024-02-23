using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLTT.Areas.Admin.Models
{
    public class KhachHangKH
    {
        public String hotenKH { get; set; }
        public String diaChiKH { get; set; }
        public String emailKH { get; set; }
        public String sdtKH { get; set; }
        public KhachHangKH()
        {
        }
        public KhachHangKH(String hotenKH, String emailKH)
        {
            this.hotenKH = hotenKH;
            this.emailKH = emailKH;
        }
        public KhachHangKH(String hotenKH, String diaChiKH, String emailKH, String sdtKH)
        {
            this.hotenKH = hotenKH;
            this.diaChiKH = diaChiKH;
            this.emailKH = emailKH;
            this.sdtKH = sdtKH;
        }
    }
}