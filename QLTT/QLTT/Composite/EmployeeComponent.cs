using QLTT.Composite;
using QLTT.Models;

namespace QLTT.Composite
{
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
}


