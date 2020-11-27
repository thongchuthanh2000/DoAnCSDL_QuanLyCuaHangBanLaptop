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
        private ObservableCollection<Model.Bill> _List;
        public ObservableCollection<Model.Bill> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.BillInfo> _BillInfo;
        public ObservableCollection<Model.BillInfo> BillInfo { get => _BillInfo; set { _BillInfo = value; OnPropertyChanged(); } }

        private Bill _SelectedItem;
        public Bill SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    MaBill = SelectedItem.MaBill;
                    TongTien = SelectedItem.TongTien;
                    ThoiGian = SelectedItem.ThoiGian;
                    MaKH = SelectedItem.MaKH;
                    MaPTTT = SelectedItem.MaPTTT;
                    MaKhuyenMai = SelectedItem.MaKhuyenMai;
                    DiaChi = SelectedItem.DiaChi;
                    LoadBillInfo(MaBill);
                }
            }
        }

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

        private string _DiaChi;
        public string DiaChi { get => _DiaChi; set { _DiaChi = value; OnPropertyChanged(); } }



        //public ICommand DeleteCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand EditBillInfoCommand { get; set; }
        public ICommand AddBillInfoCommand { get; set; }

        private void LoadListBill()
        {
            List = new ObservableCollection<Bill>();

            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM dbo.Bill");

            foreach (DataRow item in data.Rows)
            {
                Bill Bill = new Bill(item);
                List.Add(Bill);
            }
            OnPropertyChanged();

        }
        private void LoadBillInfo(int id)
        {
            BillInfo = new ObservableCollection<BillInfo>();
            string query = string.Format("SELECT * FROM dbo.Bill_info where MaBll = {0}", id);
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
            LoadListBill();

            AddCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                string query = string.Format("Exec AddBill  @TongTien ={0}, @ThoiGian ={1},@MaKH= {2},@MaPTTT={3}, @MaKhuyenMai={4}, @DiaChi=N'{5}'",
                TongTien, ThoiGian, MaKH, MaPTTT, MaKhuyenMai, DiaChi);

                var Object = DataProvider.Instance.ExecuteNonQuery(query);
                LoadListBill();
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;

                string query = string.Format("Select * from Bill where  MaBill= {0}", SelectedItem.MaBill);
                var displayList = DataProvider.Instance.ExecuteQuery(query);
                if (displayList != null)
                    return true;

                return false;

            }, (p) =>
            {
                string query = string.Format("Exec AddBill  @TongTien ={0}, @ThoiGian ={1},@MaKH= {2},@MaPTTT={3}, @MaKhuyenMai={4}, @DiaChi=N'{5}",
                MaBill, TongTien, ThoiGian, MaKH, MaPTTT, MaKhuyenMai, DiaChi);

                var Object = DataProvider.Instance.ExecuteNonQuery(query);

                LoadListBill();

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
                string query = string.Format("Exec AddBillInfo @MaBill={0},@MaSP ={1}, @SoLuong={2}",
                MaBill, MaSP, SoLuong);
                var Object = DataProvider.Instance.ExecuteNonQuery(query);
                LoadBillInfo(SelectedItem.MaBill);
            });

            EditBillInfoCommand
                = new RelayCommand<object>((p) =>
                {
                    if (SelectedBillInfo == null)
                        return false;

                    string query = string.Format("Select * from BillInfo where  MaBill = {0}", SelectedItem.MaBill);
                    var displayList = DataProvider.Instance.ExecuteQuery(query);
                    if (displayList != null)
                        return true;

                    return false;

                }, (p) =>
                {
                    string query = string.Format("Exec ChangeBillInfo @MaBill = {0}, @MaSP ={1}, @SoLuong={2}",
                    MaBill, MaSP, SoLuong);

                    var Object = DataProvider.Instance.ExecuteNonQuery(query);

                    LoadBillInfo(SelectedItem.MaBill);
                });
        }
    

    }
}
    

