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


        private KhachHang _KhachHang;
        public KhachHang khachHang { get => _KhachHang; set { _KhachHang = value; OnPropertyChanged(); } }

        private PT_ThanhToan _PT_ThanhToan;
        public PT_ThanhToan PT_ThanhToan { get => _PT_ThanhToan; set { _PT_ThanhToan = value; OnPropertyChanged(); } }

        private KhuyenMai _KhuyenMai;
        public KhuyenMai KhuyenMai { get => _KhuyenMai; set { _KhuyenMai = value; OnPropertyChanged(); } }
    }
}
