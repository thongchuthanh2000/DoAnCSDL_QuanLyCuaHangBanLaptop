using DoAnCSDL_QuanLyCuaHangBanLaptop.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCSDL_QuanLyCuaHangBanLaptop.Model
{
    public class Bill: BaseViewModel
    {
        public int MaBill { get; set; }

        public int TongTien { get; set; }

        public DateTime? ThoiGian { get; set; }

        public int MaKH { get; set; }

        public int MaPTTT { get; set; }

        public int MaKhuyenMai { get; set; }

        public string DiaChi { get; set; }

        public Bill(int maBill, int tongTien, DateTime thoiGian, int maKH, int maPTTT, int maKhuyenMai, string diaChi )
        {
            this.MaBill = maBill;
            this.TongTien = tongTien;
            this.ThoiGian = thoiGian;
            this.MaKH = maKH;
            this.MaPTTT = MaPTTT;
            this.MaKhuyenMai = maKhuyenMai;
            this.DiaChi = diaChi;
        }
        public Bill(DataRow row)
        {
            this.MaBill = (int)row["MaBill"];
            this.TongTien = (int)row["TongTien"];
            this.ThoiGian = (DateTime)row["ThoiGian"];
            this.MaKH = (int)row["MaKH"];
            this.MaPTTT = (int)row["MaPTTT"];
            this.MaKhuyenMai = (int)row["MaKhuyenMai"];
            this.DiaChi = row["DiaChi"].ToString();
        }
    }
}
