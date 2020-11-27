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
    public class KhachHangViewModel : BaseViewModel
    {
        private ObservableCollection<Model.KhachHang> _List;
        public ObservableCollection<Model.KhachHang> List { get => _List; set { _List = value; OnPropertyChanged(); } }


        private KhachHang _SelectedItem;
        public KhachHang SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    MaKhachHang = SelectedItem.MaKH;
                    HoTen = SelectedItem.HoTen;
                    GioiTinh = SelectedItem.GioiTinh;
                    DiaChi = SelectedItem.DiaChi;
                    SDT = SelectedItem.SDT;

                }
            }
        }


        private int _MaKhachHang;
        public int MaKhachHang { get => _MaKhachHang; set { _MaKhachHang = value; OnPropertyChanged(); } }

        private string _HoTen;
        public string HoTen { get => _HoTen; set { _HoTen = value; OnPropertyChanged(); } }


        private string _GioiTinh;
        public string GioiTinh { get => _GioiTinh; set { _GioiTinh = value; OnPropertyChanged(); } }

        private string _DiaChi;
        public string DiaChi { get => _DiaChi; set { _DiaChi = value; OnPropertyChanged(); } }

        private string _SDT;
        public string SDT { get => _SDT; set { _SDT = value; OnPropertyChanged(); } }
   


        //public ICommand DeleteCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand AddCommand { get; set; }

        private void LoadListKhachHang()
        {
            List = new ObservableCollection<KhachHang>();

            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM dbo.KhachHang");

            foreach (DataRow item in data.Rows)
            {
                KhachHang khachhang = new KhachHang(item);
                List.Add(khachhang);
            }
            OnPropertyChanged();

        }

        public KhachHangViewModel()
        {
            LoadListKhachHang();

            AddCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                string query = string.Format("Exec AddKhachHang @MaKH = {0}, @HoTen =N'{1}', @GioiTinh =N'{2}',@DiaChi= N'{3}',@SDT={4}",
                MaKhachHang, HoTen, GioiTinh, DiaChi, SDT);

                var Object = DataProvider.Instance.ExecuteNonQuery(query);
                LoadListKhachHang();
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;

                string query = string.Format("Select * from KhachHang where  MaKH = {0}", SelectedItem.MaKH);
                var displayList = DataProvider.Instance.ExecuteQuery(query);
                if (displayList != null)
                    return true;

                return false;

            }, (p) =>
            {
                string query = string.Format("Exec ChangeKhachHang @MaKH = {0}, @HoTen =N'{1}', @GioiTinh =N'{2}',@DiaChi= N'{3}',@SDT={4}",
                MaKhachHang, HoTen, GioiTinh, DiaChi, SDT);

                var Object = DataProvider.Instance.ExecuteNonQuery(query);

                LoadListKhachHang();

            });
        }

    }
}
