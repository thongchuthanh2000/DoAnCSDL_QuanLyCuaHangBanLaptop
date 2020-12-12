using DoAnCSDL_QuanLyCuaHangBanLaptop.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCSDL_QuanLyCuaHangBanLaptop.Model
{
    public class TaiKhoan: BaseViewModel
    {
        public int MaTK { get; set; }

        public string User { get; set; }

        public string Pass { get; set; }

        public string Chan { get; set; }

        public TaiKhoan(int maTK, string users, string pass, string chan)
        {
            this.MaTK = maTK;
            this.User = users;
            this.Pass = pass;
            this.Chan = chan;
        }
        public TaiKhoan(DataRow row)
        {
            this.MaTK = (int)row["idTK"];
            this.User = row["users"].ToString();
            this.Pass = row["pass"].ToString();
            this.Chan = row["Chan"].ToString();
        }

        private PhanQuyen _phanQuyen;
        public PhanQuyen PhanQuyen { get => _phanQuyen; set { _phanQuyen = value; OnPropertyChanged(); } }


    }
}
