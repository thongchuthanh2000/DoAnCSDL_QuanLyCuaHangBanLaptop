using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCSDL_QuanLyCuaHangBanLaptop.Model
{
    public class PT_ThanhToan
    {
        public int MaPTTT { get; set; }

        public string TenPTTT { get; set; }

        public PT_ThanhToan(int maPTTT, string tenPTTT)
        {
            this.MaPTTT = maPTTT;
            this.TenPTTT = tenPTTT;
        }
        public PT_ThanhToan(DataRow row)
        {
            this.MaPTTT = (int)row["MaPTTT"];
            this.TenPTTT = row["TenPTTT"].ToString();
        }
    }
}
