using QLTT.Composite;
using QLTT.Models;
using System;

namespace QLTT.Composite
{
    public class CustomerComponent : ICatalogComponent
    {
        public KhachHang Customer { get; set; }
        public CustomerComponent(KhachHang customer)
        {
            Customer = customer;
        }
        public void Display()
        {
            Console.WriteLine(GetHtml());
        }
        public string GetHtml()
        {
            return $"<div><h3>Thông tin khách hàng</h3><p>Tên: {Customer.hoTenKH}</p><p>Email: {Customer.emailKH}</p></div>";
        }
    }
}
