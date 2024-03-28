using QLTT.Areas.Admin.Controllers;
using QLTT.Areas.Admin.Factory;
using QLTT.Areas.Admin.Proxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using QLTT.Areas.Admin.Controllers.Admin;

namespace QLTT.App_Start
{
    public class AutofacConfig
    {
        public static void RegisterContainer()
        {
            var builder = new ContainerBuilder();

            // Đăng ký các controllers của bạn.
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // Đăng ký các dependencies cụ thể.
            builder.RegisterType<DichVuController>().As<IDichVuController>();
            builder.RegisterType<NhanVienController>().As<INhanVienController>();
            builder.RegisterType<KhachHangController>().As<IKhachHangController>();
            builder.RegisterType<LoaiSanController>().As<ILoaiSanController>();
            builder.RegisterType<SanController>().As<ISanController>();
            builder.RegisterType<NhanVienFactory>().AsSelf();
            builder.RegisterType<DichVuProtectionProxy>().AsSelf();
            builder.RegisterType<NhanVienProtectionProxy>().AsSelf();
            builder.RegisterType<LoaiSanProtectionProxy>().AsSelf();
            builder.RegisterType<SanProtectionProxy>().AsSelf();
            builder.RegisterType<KhachHangProtectionProxy>().AsSelf();

            // Đây là phần quan trọng: đăng ký proxy với interface tương ứng.
            builder.RegisterType<DichVuProtectionProxy>().As<IDichVuController>();
            builder.RegisterType<NhanVienProtectionProxy>().As<INhanVienController>();
            builder.RegisterType<LoaiSanProtectionProxy>().As<ILoaiSanController>();
            builder.RegisterType<SanProtectionProxy>().As<ISanController>();
            builder.RegisterType<KhachHangProtectionProxy>().As<IKhachHangController>();

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}