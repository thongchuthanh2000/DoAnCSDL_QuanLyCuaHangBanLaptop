using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCSDL_QuanLyCuaHangBanLaptop.Model
{
    public class PhuongThucThanhToan
    {
        public int MaPTTT { get; set; }

        public string TenPTTT { get; set; }

        public PhuongThucThanhToan(int maPT_ThanhToan, string tenPT_ThanhToan)
        {
            this.MaPTTT = maPT_ThanhToan;
            this.TenPTTT = tenPT_ThanhToan;
        }
        public PhuongThucThanhToan(DataRow row)
        {
            this.MaPTTT = (int)row["MaPT_ThanhToan"];
            this.TenPTTT = row["TenPT_ThanhToan"].ToString();
        }
    }
}
