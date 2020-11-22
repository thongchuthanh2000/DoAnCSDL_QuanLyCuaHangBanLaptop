using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCSDL_QuanLyCuaHangBanLaptop.Model
{
    public class NhanVien
    {
        public int idNV { get; set; }

        public string HoTen { get; set; }

        public DateTime? NgaySinh { get; set; }

        public string GioiTinh { get; set; }

        public string DiaChi { get; set; }

        public string SDT { get; set; }

        public string ChucVu { get; set; }

        public NhanVien(int idNV, string hoTen, DateTime ngaySinh, string gioiTinh, string diaChi, string SDT, string chucVu)
        {
            
        }
        public NhanVien(DataRow row)
        {
          

        }
    }
}
}
