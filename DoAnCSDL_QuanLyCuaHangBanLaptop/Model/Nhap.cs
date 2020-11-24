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
        public int idGiaoDich { get; set; }

        public int idSP { get; set; }

        public int SoLuong  { get; set; }

        public Nhap(int idGiaoDich, int idSP, int soLuong)
        {
            this.idGiaoDich = idGiaoDich;
            this.idSP = idSP;
            this.SoLuong = soLuong;
        }
        public Nhap(DataRow row)
        {
            this.idGiaoDich = (int)row["idGiaoDich"];
            this.idSP = (int)row["idSP"];
            this.SoLuong = (int)row["SoLuong"];
        }
    }
}
