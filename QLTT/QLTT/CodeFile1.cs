using System.Web;

namespace Test
{

    public interface ITinhTien
    {
        double TinhTien();
    }


    public class TienSan : ITinhTien
    {
        private int soGioThue;
        private int gia;

        public TienSan(int soGioThue, int gia)
        {
            this.soGioThue = soGioThue;
            this.gia = gia;
        }

        public double TinhTien()
        {
            return soGioThue * gia;
        }
    }

    public class TienDichVu : ITinhTien
    {
        private double tongTienDV;

        public TienDichVu(double tongTienDV)
        {
            this.tongTienDV = tongTienDV;
        }

        public double TinhTien()
        {
            return tongTienDV;
        }
    }

    // Buoc 3 taoj Decorator
    public abstract class TinhTienDecorator : ITinhTien
    {
        protected ITinhTien tinhTienComponent;

        public TinhTienDecorator(ITinhTien tinhTienComponent)
        {
            this.tinhTienComponent = tinhTienComponent;
        }

        public virtual double TinhTien()
        {
            return tinhTienComponent.TinhTien();
        }
    }


    // Buoc 4 tao Decorator cu the 

    public class VATDecorator : TinhTienDecorator
    {
        public VATDecorator(ITinhTien tinhTienComponent) : base(tinhTienComponent) { }

        public override double TinhTien()
        {
            return base.TinhTien() * 1.1; // Giả sử VAT là 10%
        }
    }

    public class DiscountDecorator : TinhTienDecorator
    {
        private double discount;

        public DiscountDecorator(ITinhTien tinhTienComponent, double discount) : base(tinhTienComponent)
        {
            this.discount = discount;
        }

        public override double TinhTien()
        {
            return base.TinhTien() * (1 - discount);
        }
    }


        
}