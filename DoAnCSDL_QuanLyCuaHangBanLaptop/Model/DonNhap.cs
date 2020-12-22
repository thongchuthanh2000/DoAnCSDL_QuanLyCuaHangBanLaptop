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

        public DateTime? NgayGiaoDich { get; set; }

        public string ViTriKho { get; set; }

        public DonNhap(int maGiaoDich, int tongTien, DateTime ngayGiaoDich, string viTriKho)
        {
            this.MaDonNhap = maGiaoDich;
            this.TongTien = tongTien;
            this.NgayGiaoDich = ngayGiaoDich;
            this.ViTriKho = viTriKho;
        }
        public DonNhap(DataRow row)
        {
            this.MaDonNhap = (int)row["MaDonNhap"];
            this.TongTien = (int)row["TongTien"];
            this.NgayGiaoDich = (DateTime)row["ThoiGian"];
            this.ViTriKho = row["DiaChi"].ToString();
        }
    }
}
