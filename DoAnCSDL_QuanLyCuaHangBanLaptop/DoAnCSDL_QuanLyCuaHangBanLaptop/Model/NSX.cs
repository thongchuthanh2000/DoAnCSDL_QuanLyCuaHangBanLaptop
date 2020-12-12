using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCSDL_QuanLyCuaHangBanLaptop.Model
{
    public class NSX
    {
        private int _idNSX;
        private string _tenNSX;
        private string _diaChi;

        public int IdNSX { get => _idNSX; set => _idNSX = value; }
        public string TenNSX { get => _tenNSX; set => _tenNSX = value; }
        public string DiaChi { get => _diaChi; set => _diaChi = value; }

        public NSX(int idNSX, string tenNSX, string diaChi)
        {
            this.IdNSX = idNSX;
            this.TenNSX = (string)tenNSX;
            this.DiaChi = (string)diaChi;
        }
        public NSX(DataRow row)
        {
            this.IdNSX = (int)row["idNSX"];
            this.TenNSX = row["TenNSX"].ToString();
            this.DiaChi = row["DiaChi"].ToString();

        }
    }
}
