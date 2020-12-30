using DoAnCSDL_QuanLyCuaHangBanLaptop.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCSDL_QuanLyCuaHangBanLaptop.Model
{
    public class DonNhapInfo:BaseViewModel
    {
        public int MaDonNhap { get; set; }

        public int MaSP { get; set; }

        public int SoLuong  { get; set; }
        public string TenSP { get; set; }

        public DonNhapInfo(int maDonNhap, int maSP, int soLuong, string tenSP)
        {
            this.MaDonNhap = maDonNhap;
            this.MaSP = maSP;
            this.SoLuong = soLuong;
            this.TenSP = tenSP;
        }
        public DonNhapInfo(DataRow row)
        {
            this.MaDonNhap = (int)row["MaDonNhap"];
            this.MaSP = (int)row["MaSP"];
            this.SoLuong = (int)row["SoLuong"];
            this.TenSP = row["TenSP"].ToString();
        }
      
    }
}
