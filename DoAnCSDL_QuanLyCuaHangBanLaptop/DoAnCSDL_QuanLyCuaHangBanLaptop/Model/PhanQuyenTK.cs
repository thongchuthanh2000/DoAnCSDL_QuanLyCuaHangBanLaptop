using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCSDL_QuanLyCuaHangBanLaptop.Model
{
    public class PhanQuyenTK
    {
        public int idTK { get; set; }

        public int idPQ { get; set; }

        public PhanQuyenTK(int idTK, int idPQ)
        {
            this.idTK = idTK;
            this.idPQ = idPQ;
        }
        public PhanQuyenTK(DataRow row)
        {
            this.idTK = (int)row["idTK"];
            this.idPQ = (int)row["idPQ"];
        }
    }
}
