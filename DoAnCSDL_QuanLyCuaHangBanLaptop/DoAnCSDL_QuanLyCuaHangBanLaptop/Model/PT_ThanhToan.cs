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
        public int idPT_ThanhToan { get; set; }

        public string TenPT_ThanhToan { get; set; }

        public PT_ThanhToan(int idPT_ThanhToan, string tenPT_ThanhToan)
        {
            this.idPT_ThanhToan = idPT_ThanhToan;
            this.TenPT_ThanhToan = tenPT_ThanhToan;
        }
        public PT_ThanhToan(DataRow row)
        {
            this.idPT_ThanhToan = (int)row["idPT_ThanhToan"];
            this.TenPT_ThanhToan = row["TenPT_ThanhToan"].ToString();
        }
    }
}
