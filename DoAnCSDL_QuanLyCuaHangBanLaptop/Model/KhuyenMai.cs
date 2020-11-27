using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCSDL_QuanLyCuaHangBanLaptop.Model
{
    public class KhuyenMai
    {
        private int _maKhuyenMai;

        public int GiaTriKhuyenMai { get; set; }

        public DateTime NgayBatDau { get; set; }

        public DateTime NgayKetThuc { get; set; }
        public int MaKhuyenMai { get => _maKhuyenMai; set => _maKhuyenMai = value; }

        public KhuyenMai(int maKhuyenMai, int giaTriKhuyenMai, DateTime ngayBatDau, DateTime ngayKetThuc)
        {
            this.MaKhuyenMai = maKhuyenMai;
            this.GiaTriKhuyenMai = giaTriKhuyenMai;
            this.NgayBatDau = ngayBatDau;
            this.NgayKetThuc = ngayKetThuc;
        }
        public KhuyenMai(DataRow row)
        {
            this.MaKhuyenMai = (int)row["MaKhuyenMai"];
            this.GiaTriKhuyenMai = (int)row["GiaTriKhuyenMai"];
            this.NgayBatDau = (DateTime)row["NgayBatDau"];
            this.NgayKetThuc = (DateTime)row["NgayKetThuc"];
        }
    }
}
