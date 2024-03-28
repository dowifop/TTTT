using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLTT.Template
{
    public abstract class Crawler
    {
        public abstract Blog Request(string url);
    }
}