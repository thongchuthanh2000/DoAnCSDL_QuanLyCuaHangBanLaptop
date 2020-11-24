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
        private int _idKhuyenMai;

        public int GiaTriKhuyenMai { get; set; }

        public DateTime NgayBatDau { get; set; }

        public DateTime NgayKetThuc { get; set; }
        public int IdKhuyenMai { get => _idKhuyenMai; set => _idKhuyenMai = value; }

        public KhuyenMai(int idKhuyenMai, int giaTriKhuyenMai, DateTime ngayBatDau, DateTime ngayKetThuc)
        {
            this.IdKhuyenMai = idKhuyenMai;
            this.GiaTriKhuyenMai = giaTriKhuyenMai;
            this.NgayBatDau = ngayBatDau;
            this.NgayKetThuc = ngayKetThuc;
        }
        public KhuyenMai(DataRow row)
        {
            this.IdKhuyenMai = (int)row["idKhuyenMai"];
            this.GiaTriKhuyenMai = (int)row["GiaTriKhuyenMai"];
            this.NgayBatDau = (DateTime)row["NgayBatDau"];
            this.NgayKetThuc = (DateTime)row["NgayKetThuc"];
        }
    }
}
