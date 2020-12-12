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
        public int MaGiaoDich { get; set; }

        public int MaSP { get; set; }

        public int SoLuong  { get; set; }

        public DonNhapInfo(int maGiaoDich, int maSP, int soLuong, HangHoa hangHoa)
        {
            this.MaGiaoDich = maGiaoDich;
            this.MaSP = maSP;
            this.SoLuong = soLuong;
            this.HangHoa = hangHoa;
        }
        public DonNhapInfo(DataRow row)
        {
            this.MaGiaoDich = (int)row["MaGiaoDich"];
            this.MaSP = (int)row["MaSP"];
            this.SoLuong = (int)row["SoLuong"];
        }
        private HangHoa _HangHoa;
        public HangHoa HangHoa { get => _HangHoa; set { _HangHoa = value; OnPropertyChanged(); } }
    }
}
