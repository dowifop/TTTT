using QLTT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLTT.Areas.Admin.Strategy
{
    public class HoaDonContext
    {
        private IHoaDon _strategy;
        public void SetStrategy(IHoaDon strategy) 
        {
            _strategy = strategy;
        }
        public double ExecuteStrategy(HoaDonT hoaDon)
        {
            return _strategy.CalculateTotal(hoaDon);
        }
    }
}