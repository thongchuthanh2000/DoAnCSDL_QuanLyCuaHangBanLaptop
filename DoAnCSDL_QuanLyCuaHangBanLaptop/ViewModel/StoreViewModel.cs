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
    public class StoreViewModel:BaseViewModel
    {
        private ObservableCollection<Model.DonNhap> _List;
        public ObservableCollection<Model.DonNhap> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.Nhap> _Nhap;
        public ObservableCollection<Model.Nhap> Nhap { get => _Nhap; set { _Nhap = value; OnPropertyChanged(); } }



        public int MaSP { get; set; }
        public int SoLuong { get; set; }
        private Model.Nhap _SelectedNhap;
        public Model.Nhap SelectedNhap
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
                }
            }
        }

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
                    MaGiaoDich = SelectedItem.MaGiaoDich;
                    TongTien = SelectedItem.TongTien;
                    NgayGiaoDich = SelectedItem.NgayGiaoDich;
                    ViTriKho = SelectedItem.ViTriKho;
                    LoadNhap(MaGiaoDich);
                }
            }
        }

       


        //public ICommand DeleteCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand EditNhapCommand { get; set; }
        public ICommand AddNhapCommand { get; set; }


        private void LoadNhap(int id)
        {
            Nhap = new ObservableCollection<Nhap>();
            string query = string.Format("SELECT * FROM dbo.Nhap where MaGiaoDich = {0}", id);
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Nhap nhap = new Nhap(item);
                Nhap.Add(nhap);
            }
            OnPropertyChanged();

        }

        private void LoadListDonNhap()
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

        public StoreViewModel()
        {
            LoadListDonNhap();

            AddCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                string query = string.Format("Exec AddDonNhap @MaGiaoDich = {0}, @NgayGiaoDich =N'{1}', @TongTien={2}",
                MaGiaoDich, NgayGiaoDich, TongTien);
                var Object = DataProvider.Instance.ExecuteNonQuery(query);
                LoadListDonNhap();
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;

                string query = string.Format("Select * from DonNhap where  idGiaoDich = {0}", SelectedItem.MaGiaoDich);
                var displayList = DataProvider.Instance.ExecuteQuery(query);
                if (displayList != null)
                    return true;

                return false;

            }, (p) =>
            {
                string query = string.Format("Exec ChangeDonNhap @MaGiaoDich = {0}, @NgayGiaoDich =N'{1}', @TongTien={2}",
                MaGiaoDich, NgayGiaoDich, TongTien);

                var Object = DataProvider.Instance.ExecuteNonQuery(query);

                LoadListDonNhap();


            });

            AddNhapCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedNhap == null)
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                string query = string.Format("Exec AddDonNhap @idGiaoDich = {0}, @idSP ={1}, @SoLuong={2}",
                MaGiaoDich, MaSP, SoLuong);
                var Object = DataProvider.Instance.ExecuteNonQuery(query);
                LoadNhap(SelectedItem.MaGiaoDich);
            });

            EditNhapCommand
                = new RelayCommand<object>((p) =>
            {
                if (SelectedNhap == null)
                    return false;

                string query = string.Format("Select * from Nhap where  idGiaoDich = {0}", SelectedItem.MaGiaoDich);
                var displayList = DataProvider.Instance.ExecuteQuery(query);
                if (displayList != null)
                    return true;

                return false;

            }, (p) =>
            {
                string query = string.Format("Exec ChangeDonNhap @idGiaoDich = {0}, @idSP ={1}, @SoLuong={2}",
                MaGiaoDich, MaSP, SoLuong);

                var Object = DataProvider.Instance.ExecuteNonQuery(query);

                LoadNhap(SelectedItem.MaGiaoDich);
            });



        }

    }
}

