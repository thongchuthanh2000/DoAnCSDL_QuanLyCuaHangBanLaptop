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
        private int _maBill;
        private int _maSP;
        private int _soLuong;
        private string _TenSP;


        public int MaBill { get => _maBill; set => _maBill = value; }
        public int MaSP { get => _maSP; set => _maSP = value; }
        public int SoLuong { get => _soLuong; set => _soLuong = value; }
       public string TenSP { get; set; }

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
