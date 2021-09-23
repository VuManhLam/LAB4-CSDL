using System;
using System.Collections.Generic;
using System.Text;

namespace Quan_ly_sinh_vien
{

    public class SinhVien
    {
        public string MaSo { get; set; }
        public string HovaTen { get; set; }
        public bool Phai { get; set; }
        public DateTime NgaySinh { get; set; }
        public string Lop { get; set; }
        public string SoDienThoai { get; set; }
        public string Email { get; set; }
        public string DiaChi { get; set; } 
        public string Hinh { get; set; }
        
       

        public SinhVien(string ms, string ht, bool gt, DateTime ngay,string lop,string sdt,string email, string dc, string hinh )
        {
            this.MaSo = ms;
            this.HovaTen = ht;
            this.Phai = gt;
            this.NgaySinh = ngay;
            this.Lop = lop;
            this.SoDienThoai = sdt;
            this.Email = email;
            this.DiaChi = dc;
            this.Hinh = hinh;
           
           
        }

        public SinhVien()
        {
        }
    }
}
