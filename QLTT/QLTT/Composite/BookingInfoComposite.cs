using QLTT.Composite;
using System.Collections.Generic;
using System.Text;


namespace Test.Composite
{
    public class BookingInfoComposite : ICatalogComponent
    {
        public List<ICatalogComponent> components { get; private set; } = new List<ICatalogComponent>();
        public int maPT { get; set; } // Thêm thuộc tính này

        public int maNV { get; set; } // chi tiết nhân viên lấy theo id 

        public string maKH { get; set; } // chi tiet khach hang
        public void Add(ICatalogComponent component)
        {
            components.Add(component);
        }
        public void Remove(ICatalogComponent component)
        {
            components.Remove(component);
        }

        public string GetHtml()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var component in components)
            {
                sb.Append(component.GetHtml());
            }
            return sb.ToString();
        }
    }
}

