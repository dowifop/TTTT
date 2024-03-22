using QLTT.Areas.Facade;
using QLTT.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace QLTT.Facade
{
    public class SanFacade : ISanFacade
    {
        private QlyTheThaoEntities _db;
        public SanFacade(QlyTheThaoEntities db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }
        public IEnumerable<San> GetAllSans()
        {
            return _db.Sans.Where(s => s.maTinhTrang < 5).ToList();
        }
        public San GetSanById(int id)
        {
            return _db.Sans.Find(id);
        }
        public void AddSan(San san)
        {
            _db.Sans.Add(san);
            _db.SaveChanges();
        }
        public void UpdateSan(San san)
        {
            _db.Entry(san).State = EntityState.Modified;
            _db.SaveChanges();
        }
        public void DeleteSan(int id)
        {
            var san = GetSanById(id);
            if (san != null)
            {
                _db.Sans.Remove(san);
                _db.SaveChanges();
            }
        }
        public IEnumerable<LoaiSan> GetLoaiSans()
        {
            return _db.LoaiSans.ToList();
        }
        public IEnumerable<TinhTrangSan> GetTinhTrangSans()
        {
            return _db.TinhTrangSans.ToList();
        }
        public void Dispose()
        {
            _db.Dispose();
        }
        public void RemoveSan(San san)
        {
            throw new NotImplementedException();
        }
    }
}