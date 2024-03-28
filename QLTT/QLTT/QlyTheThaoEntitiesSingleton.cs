using QLTT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLTT
{
    public class QlyTheThaoEntitiesSingleton
    {
        private static QlyTheThaoEntities _instance;
        private static readonly object _lock = new object();

        private QlyTheThaoEntitiesSingleton() { }

        public static QlyTheThaoEntities GetInstance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new QlyTheThaoEntities();
                    }
                }
            }
            return _instance;
        }
    }
}