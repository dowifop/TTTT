using QLTT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLTT.Command
{
    public class DeleteVoteRentCommand : ICommand
    {
        private readonly QlyTheThaoEntities _db;
        private readonly int _id;

        public DeleteVoteRentCommand(QlyTheThaoEntities db, int id)
        {
            _db = db;
            _id = id;
        }

        public void Execute()
        {
            var rental = _db.PhieuThueSans.Find(_id);
            if (rental != null)
            {
                _db.PhieuThueSans.Remove(rental);
                _db.SaveChanges();
            }
        }
    }
}