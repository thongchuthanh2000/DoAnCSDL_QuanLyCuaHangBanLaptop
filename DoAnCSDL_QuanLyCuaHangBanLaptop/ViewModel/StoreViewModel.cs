using DoAnCSDL_QuanLyCuaHangBanLaptop.Model;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;

namespace DoAnCSDL_QuanLyCuaHangBanLaptop.ViewModel
{
    public class StoreViewModel:BaseViewModel
    {
        private ObservableCollection<Model.DonNhap> _List;
        public ObservableCollection<Model.DonNhap> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.DonNhapInfo> _Nhap;
        public ObservableCollection<Model.DonNhapInfo> Nhap { get => _Nhap; set { _Nhap = value; OnPropertyChanged(); } }



       
        private Model.DonNhapInfo _SelectedNhap;
        public Model.DonNhapInfo SelectedNhap
        {
            get => _SelectedNhap;
            set
            {
                _SelectedNhap = value;
                OnPropertyChanged();

                if (_SelectedNhap != null)
                {
                    MaSP = _SelectedNhap.MaSP;
                    SoLuong = _SelectedNhap.SoLuong;
                    TenSP = _SelectedNhap.TenSP;
                }
            }
        }

        private string _TenSP;
        public string TenSP { get => _TenSP; set { _TenSP = value; OnPropertyChanged(); } }
        public int MaSP { get; set; }
        public int SoLuong { get; set; }

        public int MaGiaoDich { get; set; }
        public int TongTien { get; set; }
        public DateTime? NgayGiaoDich { get; set; }
        public string ViTriKho { get; set; }


        private DonNhap _SelectedItem;
        public DonNhap SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    MaGiaoDich = SelectedItem.MaDonNhap;
                    TongTien = SelectedItem.TongTien;
                    NgayGiaoDich = SelectedItem.ThoiGian;
                    ViTriKho = SelectedItem.DiaChi;
                    LoadDonNhap(MaGiaoDich);
                }
            }
        }

       


        public ICommand DeleteCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand AddCommand { get; set; }

        public ICommand EditNhapCommand { get; set; }
        public ICommand AddNhapCommand { get; set; }
        public ICommand DeleteNhapCommand { get; set; }


       
        private void LoadListDonNhap()
        {
            try
            {
                List = new ObservableCollection<DonNhap>();

                DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM dbo.DonNhap");

                foreach (DataRow item in data.Rows)
                {
                    DonNhap donnhap = new DonNhap(item);
                    List.Add(donnhap);
                }
                OnPropertyChanged();
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show(sqlEx.Message);
            }

        }
        private void LoadDonNhap(int id)
        {
            try{
                Nhap = new ObservableCollection<DonNhapInfo>();
                string query = string.Format("EXEC dbo.sp_DonNhapInfo @MaDonNhap = {0}", id);
                DataTable data = DataProvider.Instance.ExecuteQuery(query);

                foreach (DataRow item in data.Rows)
                {
                    DonNhapInfo nhap = new DonNhapInfo(item);
                    Nhap.Add(nhap);
                }
                OnPropertyChanged();
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show(sqlEx.Message);
            }
        }

        public StoreViewModel()
        {
            LoadListDonNhap();

            AddCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                try
                {
                    string query = string.Format("Exec dbo.sp_AddDonNhap @MaDonNhap = {0}, @ThoiGian ='{1}', @DiaChi=N'{2}'",
                    MaGiaoDich, NgayGiaoDich, ViTriKho);
                    var Object = DataProvider.Instance.ExecuteNonQuery(query);
                    LoadListDonNhap();
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
                    string query = string.Format("EXEC sp_ChangeDonNhap @MaDonNhap = {0}, @ThoiGian = '{1}', @DiaChi = N'{2}'", MaGiaoDich, NgayGiaoDich, ViTriKho);
                    var Object = DataProvider.Instance.ExecuteNonQuery(query);

                    LoadListDonNhap();
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show(sqlEx.Message);
                }
            });
            DeleteCommand = new RelayCommand<object>((p) =>
            {
                return true;

            }, (p) =>
            {
                try
                {
                    string query = string.Format("EXEC sp_DeleteDonNhap @MaDonNhap = {0}", MaGiaoDich);

                    var Object = DataProvider.Instance.ExecuteNonQuery(query);

                    LoadListDonNhap();
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show(sqlEx.Message);
                }
            });

            AddNhapCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                try
                {
                    string query = string.Format("Exec AddDonNhap @MaDonNhap = {0}, @MaSP ={1}, @SoLuong={2}",
                    MaGiaoDich, MaSP, SoLuong);
                    var Object = DataProvider.Instance.ExecuteNonQuery(query);
                    LoadDonNhap(SelectedItem.MaDonNhap);
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show(sqlEx.Message);
                }
            });

            EditNhapCommand = new RelayCommand<object>((p) =>
            {
                    return true;
            }, (p) =>
            {
                try
                {
                    string query = string.Format("Exec sp_ChangeDonNhapInfo @MaDonNhap = {0}, @MaSP = {1},  @SoLuong = {2}", MaGiaoDich, MaSP, SoLuong);

                    var Object = DataProvider.Instance.ExecuteNonQuery(query);

                    LoadDonNhap(SelectedItem.MaDonNhap);
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show(sqlEx.Message);
                }
            });

            DeleteNhapCommand = new RelayCommand<object>((p) =>
               {
                   return true;
               }, (p) =>
               {
                   try
                   {
                       string query = string.Format("Exec sp_DeleteDonNhapInfo @MaDonNhap = {0}, @MaSP = {1}", MaGiaoDich, MaSP);

                       var Object = DataProvider.Instance.ExecuteNonQuery(query);

                       LoadDonNhap(SelectedItem.MaDonNhap);
                   }
                   catch (SqlException sqlEx)
                   {
                       MessageBox.Show(sqlEx.Message);
                   }
               });

        }

    }
}

