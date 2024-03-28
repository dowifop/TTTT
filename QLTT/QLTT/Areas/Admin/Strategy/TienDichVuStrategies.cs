using QLTT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLTT.Areas.Admin.Strategy
{
    public class TienDichVuStrategies : IHoaDon
    {
        public double CalculateTotal(HoaDonT hoaDon)
        {
            double tongtiendv = 0;
            foreach (var item in hoaDon.DichVuDaDats)
            {
                double giaValue = item.DichVu.gia.HasValue ? (double)item.DichVu.gia.Value : 0.0;
                int soLuong = item.so_luong.HasValue ? item.so_luong.Value : 0;
                tongtiendv += soLuong * giaValue;
            }
            return tongtiendv;
        }
    }
}