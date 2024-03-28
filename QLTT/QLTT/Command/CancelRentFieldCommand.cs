using QLTT.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
namespace QLTT.Command
{
    public class CancelRentFieldCommand : ICommand
    {
        private readonly QlyTheThaoEntities _db;
        private readonly int _maPhieuThue;

        public CancelRentFieldCommand(QlyTheThaoEntities db, int rentalId)
        {
            _db = db;
            _maPhieuThue = rentalId;
        }

        public void Execute()
        {
            var rental = _db.PhieuThueSans.Find(_maPhieuThue);
            if (rental != null)
            {
                rental.maTinhTrang = 3; 
                _db.Entry(rental).State = EntityState.Modified;
                _db.SaveChanges();
            }
        }
    }

}