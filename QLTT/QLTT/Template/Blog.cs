using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;

namespace QLTT.Template
{
    public class Blog
    {
        public string Title { get; set; }
        public string Image { get; set; }
        public string Content { get; set; }

        public Blog(string title, string image, string content)
        {
            Title = title;
            Image = image;
            Content = content;
        }
    }
}