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
        public int MaGiaoDich { get; set; }

        public int TongTien { get; set; }

        public DateTime? NgayGiaoDich { get; set; }

        public string ViTriKho { get; set; }

        public DonNhap(int maGiaoDich, int tongTien, DateTime ngayGiaoDich, string viTriKho)
        {
            this.MaGiaoDich = maGiaoDich;
            this.TongTien = tongTien;
            this.NgayGiaoDich = ngayGiaoDich;
            this.ViTriKho = viTriKho;
        }
        public DonNhap(DataRow row)
        {
            this.MaGiaoDich = (int)row["MaGiaoDich"];
            this.TongTien = (int)row["TongTien"];
            this.NgayGiaoDich = (DateTime)row["NgayGiaoDich"];
            this.ViTriKho = row["ViTriKho"].ToString();
        }
    }
}
