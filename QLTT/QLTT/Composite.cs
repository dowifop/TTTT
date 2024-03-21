using QLTT.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
namespace Composite
{

    //Tính linh hoạt: Dễ dàng thêm hoặc loại bỏ thông tin cần hiển thị mà không cần sửa đổi cấu trúc tổng thể của view.
    //Tái sử dụng: Các component có thể được tái sử dụng trong nhiều tình huống khác nhau, giúp giảm bớt việc lặp code.
    //Quản lý phức tạp: Composite Pattern giúp quản lý dễ dàng các cấu trúc dữ liệu phức tạp, như cây hoặc

    public interface ICatalogComponent
    {
        void Display();
        string GetHtml(); // Thêm phương thức này để trả về HTML.
    }

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

    public class FieldComponent : ICatalogComponent
    {
        public San Field { get; set; }
        public FieldComponent(San field)
        {
            Field = field;
        }
        public void Display()
        {
            Console.WriteLine(GetHtml()); 
        }

        public string GetHtml()
        {
            return $"<div><h3>Thông tin sân</h3><p>Mã sân: {Field.maSoSan}</p></div>";
        }
    }


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
        public void Display()
        {
            foreach (var component in components)
            {
                component.Display();
            }
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

    /// thông tin chi tiết phiếu đặt sân 
    /// 


    /// thông tin nhân viên 
    public class EmployeeComponent : ICatalogComponent
    {
        public NhanVien Employee { get; set; }

        public EmployeeComponent(NhanVien employee)
        {
            Employee = employee;
        }

        public void Display()
        {
            // Logic hiển thị hoặc trả về HTML
        }

        public string GetHtml()
        {
            // Trả về HTML dựa trên thông tin nhân viên
            return $"<div><h3>Thông tin nhân viên</h3><p>Họ tên: {Employee.hotenNV}</p><p>Địa chỉ: {Employee.diaChiNV}</p></div>";
        }
    }


    /// thông tin khách hàng
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
    /// thông tin sân 
    /// thông tin hóa đơn
    /// thông tin dịch vụ

}