using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCSDL_QuanLyCuaHangBanLaptop.Model
{
    public class Bill
    {
        public int idBill { get; set; }

        public int TongTien { get; set; }

        public DateTime NgayDatHang { get; set; }

        public int idKH { get; set; }

        public int idPT_ThanhToan { get; set; }

        public int idKhuyenMai { get; set; }

        public string DiaChiGiaoHang { get; set; }

        public string TinhTrangGiaoHang { get; set; }

        public Bill(int idBill, int tongTien, DateTime ngayDatHang, int idKH, int idPT_ThanhToan, int idKhuyenMai, string diaChiGiaoHang, string tinhTrangGiaoHang)
        {
            this.idBill = idBill;
            this.TongTien = tongTien;
            this.NgayDatHang = ngayDatHang;
            this.idKH = idKH;
            this.idPT_ThanhToan = idPT_ThanhToan;
            this.idKhuyenMai = idKhuyenMai;
            this.DiaChiGiaoHang = diaChiGiaoHang;
            this.TinhTrangGiaoHang = tinhTrangGiaoHang;
        }
        public Bill(DataRow row)
        {
            this.idBill = (int)row["idBill"];
            this.TongTien = (int)row["TongTien"];
            this.NgayDatHang = (DateTime)row["NgayDatHang"];
            this.idKH = (int)row["idKH"];
            this.idPT_ThanhToan = (int)row["idPT_ThanhToan"];
            this.idKhuyenMai = (int)row["idKhuyenMai"];
            this.DiaChiGiaoHang = row["DiaChiGiaoHang"].ToString();
            this.TinhTrangGiaoHang = row["TinhTrangGiaoHang"].ToString();
        }
    }
}
