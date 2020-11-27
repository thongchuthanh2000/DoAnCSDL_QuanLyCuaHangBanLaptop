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
        public int MaPQ { get; set; }

        public int LoaiQuyen { get; set; }

        public string TenPQ { get; set; }

        public PhanQuyen(int maPQ, int loaiQuyen, string tenPQ)
        {
            this.MaPQ = maPQ;
            this.LoaiQuyen = loaiQuyen;
            this.TenPQ = tenPQ;
        }
        public PhanQuyen(DataRow row)
        {
            this.MaPQ = (int)row["MaPQ"];
            this.LoaiQuyen = (int)row["LoaiQuyen"];
            this.TenPQ = row["TenPQ"].ToString();
        }
    }
}
