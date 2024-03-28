using QLTT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLTT.Areas.Admin.Strategy
{
    public class TienSanStrategies : IHoaDon
    {
        public double CalculateTotal(HoaDonT hoaDon)
        {
            double giaThueSanMoiGio = hoaDon.PhieuThueSan.San.LoaiSan.giaThue ?? 0;
            // Tính tổng tiền sân dựa trên số giờ thuê và giá thuê mỗi giờ
            double tienSan = (hoaDon.PhieuThueSan.soGioThue ?? 0) * giaThueSanMoiGio;

            return tienSan;
        }   
    }
}