using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCSDL_QuanLyCuaHangBanLaptop.Model
{
    public class TaiKhoan
    {
        public int idTK { get; set; }

        public string users { get; set; }

        public string pass { get; set; }

        public string Chan { get; set; }

        public TaiKhoan(int idTK, string users, string pass, string chan)
        {
            this.idTK = idTK;
            this.users = users;
            this.pass = pass;
            this.Chan = chan;
        }
        public TaiKhoan(DataRow row)
        {
            this.idTK = (int)row["idTK"];
            this.users = row["users"].ToString();
            this.pass = row["pass"].ToString();
            this.Chan = row["Chan"].ToString();
        }
    }
}
