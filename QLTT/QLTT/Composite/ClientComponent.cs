using QLTT.Composite;
using QLTT.Models;

namespace QLTT.Composite
{
    public class ClientComponent : ICatalogComponent
    {
        public KhachHang client { get; set; }
        public ClientComponent(KhachHang client1)
        {
            client = client1;
        }

        public void Display()
        {
            // Logic hiển thị hoặc trả về HTML
        }

        public string GetHtml()
        {
            // Trả về HTML dựa trên thông tin nhân viên
            return $"<div><h3>Thông tin nhân viên</h3><p>Họ tên: {client.hoTenKH}</p><p>Địa chỉ: {client.diaChiKH}</p></div>";
        }
    }
}
