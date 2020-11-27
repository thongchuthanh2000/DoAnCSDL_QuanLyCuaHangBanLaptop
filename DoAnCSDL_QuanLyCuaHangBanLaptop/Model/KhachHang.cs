﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCSDL_QuanLyCuaHangBanLaptop.Model
{
    public class KhachHang
    {
        public int MaKH { get; set; }

        public string HoTen { get; set; }

        public string GioiTinh { get; set; }

        public string DiaChi { get; set; }

        public string SDT { get; set; }

        public KhachHang(int maKH, string hoTen, string gioiTinh, string diaChi, string sDT)
        {
            this.MaKH = maKH;
            this.HoTen = hoTen;
            this.GioiTinh = gioiTinh;
            this.DiaChi = diaChi;
            this.SDT = sDT;
        }
        public KhachHang(DataRow row)
        {
            this.MaKH = (int)row["idBill"];
            this.HoTen = row["HoTen"].ToString();
            this.GioiTinh = row["GioiTinh"].ToString();
            this.DiaChi = row["DiaChi"].ToString();
            this.SDT = row["SDT"].ToString();
        }
    }
}
