using QLTT.Composite;
using QLTT.Models;
using System;


namespace QLTT.Composite
{
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
}