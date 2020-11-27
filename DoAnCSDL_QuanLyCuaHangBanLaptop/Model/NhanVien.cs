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

        public string users { get; set; }

        public string pass { get; set; }

        public string Chan { get; set; }

        public NhanVien(int idNV, string hoTen, DateTime ngaySinh, string gioiTinh, string diaChi, string SDT, string chucVu, string users, string pass, string chan)
        {
            this.idNV = idNV;
            this.HoTen = hoTen;
            this.NgaySinh = ngaySinh;
            this.GioiTinh = gioiTinh;
            this.DiaChi = diaChi;
            this.SDT = SDT;
            this.ChucVu = chucVu;
            this.users = users;
            this.pass = pass;
            this.Chan = chan;
        }
        public NhanVien(DataRow row)
        {
            this.idNV = (int)row["idNV"];
            this.HoTen = row["HoTen"].ToString();
            this.NgaySinh = (DateTime)row["TenSP"];
            this.GioiTinh = row["GioiTinh"].ToString();
            this.DiaChi = row["DiaChi"].ToString();
            this.SDT = row["SDT"].ToString();
            this.ChucVu = row["ChucVu"].ToString();
            this.users = row["users"].ToString();
            this.pass = row["pass"].ToString();
            this.Chan = row["Chan"].ToString();
        }
    }
}
