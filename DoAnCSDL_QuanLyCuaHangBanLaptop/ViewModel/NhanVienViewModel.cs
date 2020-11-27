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
    public class NhanVienViewModel:BaseViewModel
    {
        private ObservableCollection<Model.NhanVien> _List;
        public ObservableCollection<Model.NhanVien> List { get => _List; set { _List = value; OnPropertyChanged(); } }


        private NhanVien _SelectedItem;
        public NhanVien SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    MaNhanVien= SelectedItem.MaNV;
                    TenNhanVien = SelectedItem.HoTen;
                    NgaySinh = (DateTime)SelectedItem.NgaySinh;
                    GioiTinh = SelectedItem.GioiTinh;
                    
                    DiaChi = SelectedItem.DiaChi;
                    SDT = SelectedItem.SDT;
                    ChucVu = SelectedItem.ChucVu;
                    TenTK = SelectedItem.TenTK;
                    MK = SelectedItem.MK;
                    Chan = SelectedItem.Chan;
                }
            }
        }


        private int _MaNhanVien;
        public int MaNhanVien { get => _MaNhanVien; set { _MaNhanVien = value; OnPropertyChanged(); } }

        private string _TenNhanVien;
        public string TenNhanVien { get => _TenNhanVien; set { _TenNhanVien = value; OnPropertyChanged(); } }


        private string _GioiTinh;
        public string GioiTinh { get => _GioiTinh; set { _GioiTinh = value; OnPropertyChanged(); } }

        private DateTime _ngaySinh;
        public DateTime NgaySinh { get => _ngaySinh; set { _ngaySinh = value; OnPropertyChanged(); } }

        private string _DiaChi;
        public string DiaChi { get => _DiaChi; set { _DiaChi = value; OnPropertyChanged(); } }

        private string _SDT;
        public string SDT { get => _SDT; set { _SDT = value; OnPropertyChanged(); } }
        private string _ChucVu;
        public string ChucVu { get => _ChucVu; set { _ChucVu = value; OnPropertyChanged(); } }

        private string _tenTK;
        public string TenTK { get => _tenTK; set { _tenTK = value; OnPropertyChanged(); } }

        private string _mk;
        public string MK { get => _mk; set { _mk = value; OnPropertyChanged(); } }

        private string _Chan;
        public string Chan { get => _Chan; set { _Chan = value; OnPropertyChanged(); } }

        //public ICommand DeleteCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand AddCommand { get; set; }

        private void LoadListNhanVien()
        {
            List = new ObservableCollection<NhanVien>();

            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM dbo.NhanVien");

            foreach (DataRow item in data.Rows)
            {
                NhanVien nhanvien = new NhanVien(item);
                List.Add(nhanvien);
            }
            OnPropertyChanged();

        }

        public NhanVienViewModel()
        {
            LoadListNhanVien();

            AddCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                string query = string.Format("Exec AddNhanVien  @Hoten =N'{0}', @NgaySinh =N'{1}',@GioiTinh= N'{2}',@DiaChi=N'{3}',"
                   + "@SDT =N'{4}', @ChucVu =N'{5}', @TenTK=N'{6}', @MK=N'{7}', @Chan=N'{8}'",
                   TenNhanVien, NgaySinh, GioiTinh, DiaChi, SDT, ChucVu, TenTK, MK, Chan);

                var Object = DataProvider.Instance.ExecuteNonQuery(query);
                LoadListNhanVien();
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;

                string query = string.Format("Select * from NhanVien where  MaNV = {0}", SelectedItem.MaNV);
                var displayList = DataProvider.Instance.ExecuteQuery(query);
                if (displayList != null)
                    return true;

                return false;

            }, (p) =>
            {
                string query = string.Format("Exec ChangeNhanVien @MaNV = {0}, @Hoten ={1}, @NgaySinh =N'{2}',@GioiTinh= {3},@DiaChi={4},"
                   + "@SDT ={5}, @ChucVu =N'{6}', @user=N'{7}', @pass=N'{8}', @chan=N'{9}'");

                var Object = DataProvider.Instance.ExecuteNonQuery(query);
                LoadListNhanVien();

            });
        }

    }

}
