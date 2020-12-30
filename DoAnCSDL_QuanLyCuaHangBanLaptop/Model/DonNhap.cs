using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCSDL_QuanLyCuaHangBanLaptop.Model
{
    public class DonNhap
    {
        public int MaDonNhap { get; set; }

        public int TongTien { get; set; }

        public DateTime? ThoiGian { get; set; }

        public string DiaChi { get; set; }

        public DonNhap(int maGiaoDich, int tongTien, DateTime thoiGian, string diaChi)
        {
            this.MaDonNhap = maGiaoDich;
            this.TongTien = tongTien;
            this.ThoiGian = thoiGian;
            this.DiaChi = diaChi;
        }
        public DonNhap(DataRow row)
        {
            this.MaDonNhap = (int)row["MaDonNhap"];
            this.TongTien = (int)row["TongTien"];
            this.ThoiGian = (DateTime)row["ThoiGian"];
            this.DiaChi = row["DiaChi"].ToString();
        }
    }
}
