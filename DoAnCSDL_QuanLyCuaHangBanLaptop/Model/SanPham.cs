using DoAnCSDL_QuanLyCuaHangBanLaptop.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCSDL_QuanLyCuaHangBanLaptop.Model
{
    public class SanPham: BaseViewModel
    {
        private int _idSP;
        private int _idNSX;
        private string _tenSP;
        private int _giaGoc;
        private int _giaBan;
        private int _soLuong;
        private string _cPU;
        private string _rAM;
        private string _manHinh;
        private string _pIN;


        public int IdSP { get => _idSP; set => _idSP = value; }
        public int IdNSX { get => _idNSX; set => _idNSX = value; }
        public string TenSP { get => _tenSP; set => _tenSP = value; }
        public int GiaGoc { get => _giaGoc; set => _giaGoc = value; }
        public int SoLuong { get => _soLuong; set => _soLuong = value; }
        public int GiaBan { get => _giaBan; set => _giaBan = value; }
        public string CPU { get => _cPU; set => _cPU = value; }
        public string RAM { get => _rAM; set => _rAM = value; }
        public string ManHinh { get => _manHinh; set => _manHinh = value; }
        public string PIN { get => _pIN; set => _pIN = value; }

        public SanPham(int idSP,string tenSP,int giaBan,int  soLuong, int giaGoc, int idNSX, string cpu, string ram, string manhinh, string pin,NSX nsx)
        {
            this.IdNSX = idNSX;
            this.IdSP = idSP;
            this.TenSP = (string)tenSP;
            this.GiaGoc = giaGoc;
            this.SoLuong = soLuong;
            this.GiaBan = giaBan;
            this.CPU = cpu;
            this.RAM = ram;
            this.ManHinh = manhinh;
            this.PIN = pin;
            this.NSX = nsx;

        }
        public SanPham(DataRow row, NSX nsx)
        {
            this.IdNSX = (int)row["idNSX"];
            this.IdSP = (int)row["idSP"];
            this.TenSP = row["TenSP"].ToString();
            this.GiaGoc = (int)row["giagoc"];
            this.SoLuong = (int)row["SoLuong"];
            this.SoLuong = (int)row["soluong"]; 
            this.GiaBan =   (int)row["giaban"]; 
            this.CPU  = row["cpu"].ToString();
            this.RAM  = row["ram"].ToString();
            this.ManHinh = row["manhinh"].ToString();
            this.PIN  = row["pin"].ToString();
            this.NSX = nsx;
        }



        private NSX _NSX;
        public  NSX NSX { get => _NSX; set { _NSX = value; OnPropertyChanged(); } }

    }
}
