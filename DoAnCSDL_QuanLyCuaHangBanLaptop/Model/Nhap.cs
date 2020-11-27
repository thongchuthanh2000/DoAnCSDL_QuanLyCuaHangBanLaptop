using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCSDL_QuanLyCuaHangBanLaptop.Model
{
    public class Nhap
    {
        public int MaGiaoDich { get; set; }

        public int MaSP { get; set; }

        public int SoLuong  { get; set; }

        public Nhap(int maGiaoDich, int maSP, int soLuong)
        {
            this.MaGiaoDich = maGiaoDich;
            this.MaSP = maSP;
            this.SoLuong = soLuong;
        }
        public Nhap(DataRow row)
        {
            this.MaGiaoDich = (int)row["MaGiaoDich"];
            this.MaSP = (int)row["MaSP"];
            this.SoLuong = (int)row["SoLuong"];
        }
    }
}
