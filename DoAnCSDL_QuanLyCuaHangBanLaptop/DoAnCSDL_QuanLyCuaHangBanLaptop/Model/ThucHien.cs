using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCSDL_QuanLyCuaHangBanLaptop.Model
{
    public class ThucHien
    {
        public int idBill { get; set; }

        public int idNV { get; set; }

        public ThucHien(int idBill, int idNV)
        {
            this.idBill = idBill;
            this.idNV = idNV;
        }
        public ThucHien(DataRow row)
        {
            this.idBill = (int)row["idBill"];
            this.idNV = (int)row["idNV"];
        }
    }
}
