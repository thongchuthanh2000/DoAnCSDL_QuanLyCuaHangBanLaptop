using DoAnCSDL_QuanLyCuaHangBanLaptop.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCSDL_QuanLyCuaHangBanLaptop.Model
{
    public class BillInfo: BaseViewModel
    {
        private int _MaBill;
        private int _MaSP;
        private int _SoLuong;
        private string _TenSP;


        public int MaBill { get => _MaBill; set => _MaBill = value; }
        public int MaSP { get => _MaSP; set => _MaSP = value; }
        public int SoLuong { get => _SoLuong; set => _SoLuong = value; }
       public string TenSP { get => _TenSP; set =>  _TenSP = value; }

        public BillInfo( int maBill, int maSP, int soLuong, string tenSP)
        {
            
            this.MaBill = maBill;
            this.MaSP = maSP;
            this.SoLuong = soLuong;
            this.TenSP = tenSP;

        }
        public BillInfo(DataRow row)
        {
           
            this.MaBill = (int)row["MaBill"];
            this.MaSP = (int)row["MaSP"];
            this.SoLuong = (int)row["SoLuong"];
            this.TenSP = row["TenSP"].ToString();
        }
      
    }
}
