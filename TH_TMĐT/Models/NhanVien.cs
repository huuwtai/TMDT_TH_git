//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TH_TMĐT.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class NhanVien
    {
        public string MaNV { get; set; }
        public string HoTen { get; set; }
        public Nullable<System.DateTime> NgaySinh { get; set; }
        public string DiaChi { get; set; }
        public string CMND { get; set; }
        public string EMAIL { get; set; }
        public string MaCV { get; set; }
        public Nullable<System.DateTime> NgayTao { get; set; }
        public string MaTK { get; set; }
    
        public virtual ChucVu ChucVu { get; set; }
        public virtual TaiKhoan TaiKhoan { get; set; }
    }
}
