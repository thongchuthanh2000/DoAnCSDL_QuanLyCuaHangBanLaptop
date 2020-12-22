using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCSDL_QuanLyCuaHangBanLaptop.Model
{
    public class PhanQuyen
    {
        public int idPQ { get; set; }

        public int LoaiQuyen { get; set; }

        public string TenPQ { get; set; }

        public PhanQuyen(int idPQ, int loaiQuyen, string tenPQ)
        {
            this.idPQ = idPQ;
            this.LoaiQuyen = loaiQuyen;
            this.TenPQ = tenPQ;
        }
        public PhanQuyen(DataRow row)
        {
            this.idPQ = (int)row["idPQ"];
            this.LoaiQuyen = (int)row["LoaiQuyen"];
            this.TenPQ = row["TenPQ"].ToString();
        }
    }
}
