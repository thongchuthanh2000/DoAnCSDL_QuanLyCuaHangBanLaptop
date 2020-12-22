using DoAnCSDL_QuanLyCuaHangBanLaptop.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DoAnCSDL_QuanLyCuaHangBanLaptop.ViewModel
{
    public class BillViewModel : BaseViewModel
    {
        private ObservableCollection<Model.HangHoa> _List;
        public ObservableCollection<Model.HangHoa> List { get => _List; set { _List = value; OnPropertyChanged(); } }


        private HangHoa _SelectedItem;
        public HangHoa SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    MaHang = SelectedItem.MaSP;
                    TenSanPham = SelectedItem.TenSP;
                    SL = SelectedItem.SoLuong;
                    GiaGoc = SelectedItem.GiaGoc;
                    GiaBan = SelectedItem.GiaBan;
                    MaNSX = SelectedItem.MaNSX;
                    RAM = SelectedItem.RAM;
                    CPU = SelectedItem.CPU;
                    PIN = SelectedItem.PIN;
                    ManHinh = SelectedItem.ManHinh;
                }
            }
        }
       
        private void LoadListSanPham()
        {
            List = new ObservableCollection<HangHoa>();

            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM V_LIST_Laptop");

            foreach (DataRow item in data.Rows)
            {
                string qu = string.Format("Select * From  fn_TimKiemNSXByMaNSX ({0})", (int)item["MaNSX"]);

                DataTable dataNSX = DataProvider.Instance.ExecuteQuery(qu);
                NSX nsx = new NSX(dataNSX.Rows[0]);

                HangHoa sanpham = new HangHoa(item, nsx);
                List.Add(sanpham);
            }
            OnPropertyChanged();

        }
        private int _MaNSX;
        public int MaNSX { get => _MaNSX; set { _MaNSX = value; OnPropertyChanged(); } }

        private int _MaHang;
        public int MaHang { get => _MaHang; set { _MaHang = value; OnPropertyChanged(); } }


        private string _TenSanPham;
        public string TenSanPham { get => _TenSanPham; set { _TenSanPham = value; OnPropertyChanged(); } }


        private int _SL;
        public int SL { get => _SL; set { _SL = value; OnPropertyChanged(); } }


        private int _GiaGoc;
        public int GiaGoc { get => _GiaGoc; set { _GiaGoc = value; OnPropertyChanged(); } }


        private int _GiaBan;
        public int GiaBan { get => _GiaBan; set { _GiaBan = value; OnPropertyChanged(); } }

        private string _rAM;
        private string _manHinh;
        private string _cPU;
        private string _pIN;
        public string RAM { get => _rAM; set { _rAM = value; OnPropertyChanged(); } }
        public string ManHinh { get => _manHinh; set { _manHinh = value; OnPropertyChanged(); } }
        public string CPU { get => _cPU; set { _cPU = value; OnPropertyChanged(); } }
        public string PIN { get => _pIN; set { _pIN = value; OnPropertyChanged(); } }
        
        private ObservableCollection<Model.NSX> _NSX;
        public ObservableCollection<Model.NSX> NSX { get => _NSX; set { _NSX = value; OnPropertyChanged(); } }

        private void LoadListNSX()
        {
            NSX = new ObservableCollection<NSX>();

            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM dbo.NSX");

            foreach (DataRow item in data.Rows)
            {
                NSX nsx = new NSX(item);
                NSX.Add(nsx);
            }
            OnPropertyChanged();
        }

        private ObservableCollection<Model.BillInfo> _BillInfo;
        public ObservableCollection<Model.BillInfo> BillInfo { get => _BillInfo; set { _BillInfo = value; OnPropertyChanged(); } }

   
        public int MaSP { get; set; }
        public int SoLuong { get; set; }
   

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
                    SoLuong = _SelectedBillInfo.SoLuong;
                    TenSP = _SelectedBillInfo.TenSP;
                }
            }
        }

        private int _MaBill;
        public int MaBill { get => _MaBill; set { _MaBill = value; OnPropertyChanged(); } }

        private int _TongTien;
        public int TongTien { get => _TongTien; set { _TongTien = value; OnPropertyChanged(); } }
        private string _TenSP;
        public string TenSP { get => _TenSP; set { _TenSP = value; OnPropertyChanged(); } }


        private DateTime? _ThoiGian;
        public DateTime? ThoiGian { get => _ThoiGian; set { _ThoiGian = value; OnPropertyChanged(); } }

        private int _MaKH;
        public int MaKH { get => _MaKH; set { _MaKH = value; OnPropertyChanged(); } }

        private int _MaPTTT;
        public int MaPTTT { get => _MaPTTT; set { _MaPTTT = value; OnPropertyChanged(); } }

        private int _MaKhuyenMai;
        public int MaKhuyenMai { get => _MaKhuyenMai; set { _MaKhuyenMai = value; OnPropertyChanged(); } }



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
            BillInfo = new ObservableCollection<BillInfo>();
            string query = string.Format("EXec sp_BillInfo @Ma = {0}", Ma);
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                BillInfo billInfo = new BillInfo(item);
                BillInfo.Add(billInfo);
            }
            OnPropertyChanged();

        }
        public BillViewModel()
        {
            LoadListNSX();
            LoadListKM();
            LoadListPTThanhToan();
            LoadListSanPham();

            AddCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                string query = string.Format("Exec AddBill  @MaBill ={0},@MaKH ={1},@MaPTTT= {2},@MaKhuyenMai={3}, @MaNV=N{4}",
                MaBill, MaKH, MaPTTT, MaKhuyenMai, 0);

                var Object = DataProvider.Instance.ExecuteNonQuery(query);
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                    return true;
            }, (p) =>
            {
                string query = string.Format("Exec sp_ChangeBill  @MaBill = {0}, @ThoiGian ={1},@MaKH= {2},@MaPTTT={3}, @MaKhuyenMai={4}",
                MaBill, TongTien, ThoiGian, MaKH, MaPTTT, MaKhuyenMai);

                var Object = DataProvider.Instance.ExecuteNonQuery(query);

                //LoadListBill();

            });

            AddBillInfoCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedBillInfo == null)
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                string query = string.Format("Exec sp_AddKiemTraBillInfo @MaBill={0},@MaSP ={1}, @SoLuong={2}",
                MaBill, MaSP, SoLuong);
                var Object = DataProvider.Instance.ExecuteNonQuery(query);
                LoadBillInfo(MaBill);
            });

            EditBillInfoCommand = new RelayCommand<object>((p) =>
                {
                        return true;
                }, (p) =>
                {
                    string query = string.Format("Exec sp_ChangeBillInfo @MaBill = {0}, @MaSP ={1}, @SoLuong={2}",
                    MaBill, MaSP, SoLuong);

                    var Object = DataProvider.Instance.ExecuteNonQuery(query);

                    LoadBillInfo(MaBill);
                });
        }

        private void LoadListPTThanhToan()
        {
            ListPTThanhToan = new ObservableCollection<PT_ThanhToan>();

            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM V_LIST_PhuongThucThanhToan");

            foreach (DataRow item in data.Rows)
            {
                PT_ThanhToan ptThanhToan = new PT_ThanhToan(item);
                ListPTThanhToan.Add(ptThanhToan);
            }
            OnPropertyChanged();
        }
                private ObservableCollection<Model.PT_ThanhToan> _ListPTThanhToan;
        public ObservableCollection<Model.PT_ThanhToan> ListPTThanhToan { get => _ListPTThanhToan; set { _ListPTThanhToan = value; OnPropertyChanged(); } }



        private ObservableCollection<Model.KhuyenMai> _ListKhuyenMai;
        public ObservableCollection<Model.KhuyenMai> ListKhuyenMai { get => _ListKhuyenMai; set { _ListKhuyenMai = value; OnPropertyChanged(); } }
        private void LoadListKM()
        {
            ListKhuyenMai = new ObservableCollection<KhuyenMai>();

            DataTable data = DataProvider.Instance.ExecuteQuery("EXec sp_KhuyenMaiHienTai");

            foreach (DataRow item in data.Rows)
            {
                KhuyenMai khuyenmai = new KhuyenMai(item);
                ListKhuyenMai.Add(khuyenmai);
            }
            OnPropertyChanged();

        }

    }

    
}
    

