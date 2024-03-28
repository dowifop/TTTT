using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLTT.Models
{
    public class GoiDichVuModel
    {
        public IEnumerable<DichVu> dsDichVu { get; set; }
        public IEnumerable<DichVuDaDat> dsDvDaDat { get; set; }
    }
}