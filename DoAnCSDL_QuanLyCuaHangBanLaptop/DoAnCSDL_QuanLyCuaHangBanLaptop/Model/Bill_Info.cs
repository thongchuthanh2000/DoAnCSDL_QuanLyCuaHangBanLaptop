using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCSDL_QuanLyCuaHangBanLaptop.Model
{
    public class Bill_Info
    {
        public int idBill { get; set; }

        public int idSP { get; set; }

        public int SoLuong { get; set; }


        public Bill_Info(int idBill, int idSP, int soLuong)
        {
            this.idBill = idBill;
            this.idSP = idSP;
            this.SoLuong = soLuong;
        }
        public Bill_Info(DataRow row)
        {
            this.idBill = (int)row["idBill"];
            this.idSP = (int)row["idSP"];
            this.SoLuong = (int)row["SoLuong"];
        }
    }
}
