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
        public int MaNV { get; set; }

        public string HoTen { get; set; }

        public DateTime? NgaySinh { get; set; }

        public string GioiTinh { get; set; }

        public string DiaChi { get; set; }

        public string SDT { get; set; }

        public string ChucVu { get; set; }

        public string TenTK { get; set; }

        public string MK { get; set; }

        public string Chan { get; set; }

        public NhanVien(int maNV, string hoTen, DateTime ngaySinh, string gioiTinh, string diaChi, string SDT, string chucVu, string tenTK, string mk, string chan)
        {
            this.MaNV = maNV;
            this.HoTen = hoTen;
            this.NgaySinh = ngaySinh;
            this.GioiTinh = gioiTinh;
            this.DiaChi = diaChi;
            this.SDT = SDT;
            this.ChucVu = chucVu;
            this.TenTK = tenTK;
            this.MK = mk;
            this.Chan = chan;
        }
        public NhanVien(DataRow row)
        {
            this.MaNV = (int)row["MaNV"];
            this.HoTen = row["HoTen"].ToString();
            this.NgaySinh = (DateTime)row["NgaySinh"];
            this.GioiTinh = row["GioiTinh"].ToString();
            this.DiaChi = row["DiaChi"].ToString();
            this.SDT = row["SDT"].ToString();
            this.ChucVu = row["ChucVu"].ToString();
            this.TenTK = row["TenTK"].ToString();
            this.MK = row["MK"].ToString();
            this.Chan = row["Chan"].ToString();
        }
    }
}
