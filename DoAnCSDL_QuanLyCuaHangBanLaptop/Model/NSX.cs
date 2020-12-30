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
        private int _maNSX;
        private string _tenNSX;
        private string _diaChi;
 

        public int MaNSX { get => _maNSX; set => _maNSX = value; }
        public string TenNSX { get => _tenNSX; set => _tenNSX = value; }
        public string DiaChi { get => _diaChi; set => _diaChi = value; }
     

        public NSX(int maNSX, string tenNSX, string diaChi)
        {
            this.MaNSX = maNSX;
            this.TenNSX = tenNSX;
            this.DiaChi = diaChi;
        }
        public NSX(DataRow row)
        {
            this.MaNSX = (int)row["maNSX"];
            this.TenNSX = row["TenNSX"].ToString();
            this.DiaChi = row["DiaChi"].ToString();

        }
    }
}
