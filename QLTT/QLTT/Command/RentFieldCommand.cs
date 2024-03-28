using QLTT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLTT.Command
{
    public class RentFieldCommand : ICommand
    {
        private readonly QlyTheThaoEntities _db;
        private readonly List<int> _maSan; 
        private readonly string _maKH;
        private readonly DateTime _ngayThue;
        private readonly int _soGioThue;
        private readonly DateTime _ngayDat;
        public RentFieldCommand(QlyTheThaoEntities db, List<int> maSan, string maKH, DateTime ngayThue,DateTime ngayDat, int soGioThue)
        {
            _db = db;
            _maSan = maSan;
            _maKH = maKH;
            _ngayDat = ngayDat;
            _ngayThue = ngayThue;
            _soGioThue = soGioThue;
        }
        public void Execute()
        {
            foreach (var fieldId in _maSan)
            {
                var rental = new PhieuThueSan()
                {
                    maSan = fieldId, 
                    maKH = _maKH,
                    NgayDat = _ngayDat,
                    NgayThue = _ngayThue,
                    soGioThue = _soGioThue,
                    maTinhTrang = 1
                };
                _db.PhieuThueSans.Add(rental); 
            }

            _db.SaveChanges();
           
        }
     
    }

}
