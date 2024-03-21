using QLTT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLTT.Areas.Facade
{
    public interface ISanFacade : IDisposable
    {
        IEnumerable<San> GetAllSans();
        San GetSanById(int id);
        void AddSan(San san);
        void UpdateSan(San san);
        void DeleteSan(int id);
        IEnumerable<LoaiSan> GetLoaiSans();
        IEnumerable<TinhTrangSan> GetTinhTrangSans();
    }
}
