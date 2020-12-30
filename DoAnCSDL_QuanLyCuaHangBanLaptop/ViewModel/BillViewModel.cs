using DoAnCSDL_QuanLyCuaHangBanLaptop.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DoAnCSDL_QuanLyCuaHangBanLaptop.ViewModel
{
    public class BillViewModel : BaseViewModel
    {
        private ObservableCollection<Model.Bill> _List;
        public ObservableCollection<Model.Bill> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private Model.Bill _SelectedBill;
        public Model.Bill SelectedBill
        {
            get => _SelectedBill;
            set
            {
                _SelectedBill = value;
                OnPropertyChanged();

                if (_SelectedBill != null)
                {
                    MaBill = _SelectedBill.MaBill;
                    TongTien = _SelectedBill.TongTien;
                    ThoiGian = _SelectedBill.ThoiGian;
                    MaKH = _SelectedBill.MaKH;
                    MaPTTT = _SelectedBill.MaPTTT;
                    MaNV = _SelectedBill.MaNV;
                    ChonPTTT();
                    ChonKhuyenMai();
                    MaKhuyenMai = _SelectedBill.MaKhuyenMai;
                    LoadBillInfo(MaBill);
                }
            }
        }
        private int _MaBill;
        public int MaBill { get => _MaBill; set { _MaBill = value; OnPropertyChanged(); } }

        private int _TongTien;
        public int TongTien { get => _TongTien; set { _TongTien = value; OnPropertyChanged(); } }

        private DateTime? _ThoiGian;
        public DateTime? ThoiGian { get => _ThoiGian; set { _ThoiGian = value; OnPropertyChanged(); } }

        private int _MaKH;
        public int MaKH { get => _MaKH; set { _MaKH = value; OnPropertyChanged(); } }

        private int _MaPTTT;
        public int MaPTTT { get => _MaPTTT; set { _MaPTTT = value; OnPropertyChanged(); } }

        private int _MaKhuyenMai;
        public int MaKhuyenMai { get => _MaKhuyenMai; set { _MaKhuyenMai = value; OnPropertyChanged(); } }
        private int _MaNV;
        public int MaNV { get => _MaNV; set { _MaNV = value; OnPropertyChanged(); } }



        private ObservableCollection<Model.BillInfo> _BillInfo;
        public ObservableCollection<Model.BillInfo> BillInfo { get => _BillInfo; set { _BillInfo = value; OnPropertyChanged(); } }
        private int _MaSP;
        public int MaSP { get => _MaSP; set {_MaSP= value; OnPropertyChanged(); } }

        private int _SL;
        public int SL { get => _SL; set { _SL = value; OnPropertyChanged(); } }

        private string _TenSP;
        public string TenSP { get => _TenSP; set { _TenSP = value; OnPropertyChanged(); } }

        private Model.BillInfo _SelectedBillInfo;
        public Model.BillInfo SelectedBillInfo
        {
            get => _SelectedBillInfo;
            set
            {
                _SelectedBillInfo = value;
                OnPropertyChanged();

                if (_SelectedBillInfo != null)
                {
                    MaSP = _SelectedBillInfo.MaSP;
                    SL = _SelectedBillInfo.SoLuong;
                    TenSP = _SelectedBillInfo.TenSP;
                }
            }
        }


       

       
        private void ChonPTTT()
        {
            foreach (PT_ThanhToan pT_ThanhToans in ListPTThanhToan)
            {
                if (pT_ThanhToans.MaPTTT == MaPTTT)
                {
                    SelectedPT_ThanhToan = pT_ThanhToans;
                }
            }
        }
        private void ChonKhuyenMai()
        {
            foreach (KhuyenMai khuyenMai in ListKhuyenMai)
            {
                if (khuyenMai.MaKhuyenMai == MaKhuyenMai)
                {
                    SelectedKhuyenMai = khuyenMai;
                }
            }
        }

        private ObservableCollection<Model.KhuyenMai> _KhuyenMai;
        public ObservableCollection<Model.KhuyenMai> KhuyenMai { get => _KhuyenMai; set { _KhuyenMai = value; OnPropertyChanged(); } }
        private Model.KhuyenMai _SelectedKhuyenMai;
        public Model.KhuyenMai SelectedKhuyenMai
        {
            get => _SelectedKhuyenMai;
            set
            {
                _SelectedKhuyenMai = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Model.PT_ThanhToan> _PT_ThanhToan;
        public ObservableCollection<Model.PT_ThanhToan> PT_ThanhToan { get => _PT_ThanhToan; set { _PT_ThanhToan = value; OnPropertyChanged(); } }
        private Model.PT_ThanhToan _SelectedPT_ThanhToan;
        public Model.PT_ThanhToan SelectedPT_ThanhToan
        {
            get => _SelectedPT_ThanhToan;
            set
            {
                _SelectedPT_ThanhToan = value;
                OnPropertyChanged();
            }
        }

        public ICommand DeleteCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand AddCommand { get; set; }

        public ICommand EditBillInfoCommand { get; set; }
        public ICommand AddBillInfoCommand { get; set; }


        private void LoadBillInfo(int Ma)
        {
            try
            {
                BillInfo = new ObservableCollection<BillInfo>();
                string query = string.Format("Exec dbo.sp_GetBillInfoByMaBill @MaBill = {0}", Ma);
                DataTable data = DataProvider.Instance.ExecuteQuery(query);

                foreach (DataRow item in data.Rows)
                {
                    BillInfo billInfo = new BillInfo(item);
                    BillInfo.Add(billInfo);
                }
                OnPropertyChanged();
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void LoadListBill()
        {
            try
            {
                List = new ObservableCollection<Bill>();

                DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM dbo.Bill");

                foreach (DataRow item in data.Rows)
                {
                    Bill bill = new Bill(item);
                    List.Add(bill);
                }
                OnPropertyChanged();
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show(sqlEx.Message);
            }
        }
        public BillViewModel()
        {
            LoadListBill();

            LoadListKM();
            LoadListPTThanhToan();

            AddCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                try
                {
                    string query = string.Format("Exec dbo.sp_AddBill  @MaBill ={0},@ThoiGian = '{1}', @MaKH ={2},@MaPTTT= {3},@MaKhuyenMai={4}, @MaNV={5}",
                    MaBill, ThoiGian, MaKH, SelectedPT_ThanhToan.MaPTTT, SelectedKhuyenMai.MaKhuyenMai, MaNV);

                    var Object = DataProvider.Instance.ExecuteNonQuery(query);
                    LoadListBill();
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show(sqlEx.Message);
                }
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                    return true;
            }, (p) =>
            {
                try
                {
                    string query = string.Format("Exec dbo.sp_ChangeBill  @MaBill = {0}, @ThoiGian ={1},@MaKH= {2},@MaPTTT={3}, @MaKhuyenMai={4}, @MaNV=N{5}",
                    MaBill, TongTien, ThoiGian, MaKH, MaPTTT, MaKhuyenMai, MaNV);

                    var Object = DataProvider.Instance.ExecuteNonQuery(query);

                    LoadListBill();
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show(sqlEx.Message);
                }
            });



            AddBillInfoCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                try
                {
                    string query = string.Format("Exec dbo.sp_AddKiemTraBillInfo @MaBill={0},@MaSP ={1}, @SoLuong={2}",
                    MaBill, MaSP, SL);
                    var Object = DataProvider.Instance.ExecuteNonQuery(query);
                    LoadBillInfo(MaBill);
                    LoadListBill();

                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show(sqlEx.Message);
                }
            });

            EditBillInfoCommand = new RelayCommand<object>((p) =>
                {
                        return true;
                }, (p) =>
                {
                    try
                    {
                        string query = string.Format("Exec dbo.sp_ChangeBillInfo @MaBill = {0}, @MaSP ={1}, @SoLuong={2}",
                        MaBill, MaSP, SL);

                        var Object = DataProvider.Instance.ExecuteNonQuery(query);

                        LoadBillInfo(MaBill);
                        LoadListBill();

                    }
                    catch (SqlException sqlEx)
                    {
                        MessageBox.Show(sqlEx.Message);
                    }
                });
        }


        private ObservableCollection<Model.PT_ThanhToan> _ListPTThanhToan;
        public ObservableCollection<Model.PT_ThanhToan> ListPTThanhToan { get => _ListPTThanhToan; set { _ListPTThanhToan = value; OnPropertyChanged(); } }

        private void LoadListPTThanhToan()
        {
            ListPTThanhToan = new ObservableCollection<PT_ThanhToan>();

            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM dbo.V_LIST_PhuongThucThanhToan");

            foreach (DataRow item in data.Rows)
            {
                PT_ThanhToan ptThanhToan = new PT_ThanhToan(item);
                ListPTThanhToan.Add(ptThanhToan);
            }
            OnPropertyChanged();
        }

        private ObservableCollection<Model.KhuyenMai> _ListKhuyenMai;
        public ObservableCollection<Model.KhuyenMai> ListKhuyenMai { get => _ListKhuyenMai; set { _ListKhuyenMai = value; OnPropertyChanged(); } }
        private void LoadListKM()
        {
            ListKhuyenMai = new ObservableCollection<KhuyenMai>();

            DataTable data = DataProvider.Instance.ExecuteQuery("Exec dbo.sp_KhuyenMaiHienTai");

            foreach (DataRow item in data.Rows)
            {
                KhuyenMai khuyenmai = new KhuyenMai(item);
                ListKhuyenMai.Add(khuyenmai);
            }
            OnPropertyChanged();
        }
    }
}
    

