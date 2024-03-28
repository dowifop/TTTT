using QLTT.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace QLTT.Command
{
    public class EditRentFeildCommand : ICommand
    {
        private readonly QlyTheThaoEntities _db;
        private readonly PhieuThueSan _phieuThueSan;

        public EditRentFeildCommand(QlyTheThaoEntities db, PhieuThueSan phieuThueSan)
        {
            _db = db;
            _phieuThueSan = phieuThueSan;
        }

        public void Execute()
        {
            _db.Entry(_phieuThueSan).State = EntityState.Modified;
            _db.SaveChanges();
        }
    }
}