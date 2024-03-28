using QLTT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLTT.Areas.Admin.Strategy
{
    public interface IHoaDon
    {
        double CalculateTotal(HoaDonT hoaDon);
    }
}
