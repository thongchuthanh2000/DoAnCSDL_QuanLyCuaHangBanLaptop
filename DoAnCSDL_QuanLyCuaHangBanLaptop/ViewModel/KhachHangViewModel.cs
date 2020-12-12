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
                    TongGiaTri = SelectedItem.TongTien;
                }
            }
        }
        private int _TongGiaTri;
        public int TongGiaTri { get => _TongGiaTri; set { _TongGiaTri = value; OnPropertyChanged(); } }
        private bool _TangDan;
        public bool TangDan { get => _TangDan; set { _TangDan = value; OnPropertyChanged(); } }
        private int _MaKhachHang;
        public int MaKhachHang { get => _MaKhachHang; set { _MaKhachHang = value; OnPropertyChanged(); } }

        private string _HoTen;
        public string HoTen { get => _HoTen; set { _HoTen = value; OnPropertyChanged(); } }


        private string _GioiTinh;
        public string GioiTinh { get => _GioiTinh; set { _GioiTinh = value; OnPropertyChanged(); } }
        private string _TenKhachHang;
        public string TenKhachHang { get => _TenKhachHang; set { _TenKhachHang = value; OnPropertyChanged(); } }
        private string _DiaChi;
        public string DiaChi { get => _DiaChi; set { _DiaChi = value; OnPropertyChanged(); } }

        private string _SDT;
        public string SDT { get => _SDT; set { _SDT = value; OnPropertyChanged(); } }

        public ICommand CheckedCommand { get; private set; }
        
        public ICommand UncheckedCommand { get; private set; }
        public ICommand SearchCommand { get; private set; }


        public ICommand DeleteCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand LoadCommand { get; set; }
        public ICommand SapXepCommand { get; set; }
        public ICommand TongGiaTriCommand { get; set; }
        private void LoadListKhachHang()
        {
            List = new ObservableCollection<KhachHang>();

            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM V_LIST_KhachHang");

            foreach (DataRow item in data.Rows)
            {
                KhachHang khachhang = new KhachHang(item);
                List.Add(khachhang);
            }
            OnPropertyChanged();

        }
        private void SapXepKhachHang()
        {
            List = new ObservableCollection<KhachHang>();
            string query = "";
            if (TangDan)
            {
                query = string.Format("EXEC dbo.sp_KhachHangChiNhieuNhatvsItNhat @Tang = 0 ");
            }
            else
            {
                query = string.Format("EXEC dbo.sp_KhachHangChiNhieuNhatvsItNhat @Tang = 1 " );
            }
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                KhachHang khachhang = new KhachHang(item);
                List.Add(khachhang);
            }
            OnPropertyChanged();
        }
        private void LoadListKhachHangTheoTen(String tenKhachHang)
        {
            List = new ObservableCollection<KhachHang>();
            string query = string.Format("SELECT * FROM Fn_GetKhachHangtByHoTen (N'{0}') ", tenKhachHang);
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

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
            TangDan = true;
            DeleteCommand = new RelayCommand<object>((p) =>
                {
                    return true;
                }, (p) =>
                {
                    string query = string.Format("Exec DeleteKhachHang @MaKH = {0}",  MaKhachHang);

                    var Object = DataProvider.Instance.ExecuteNonQuery(query);
                    LoadListKhachHang();
                });
            SapXepCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                SapXepKhachHang();
            });
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
            SearchCommand = new  RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                LoadListKhachHangTheoTen(TenKhachHang);
            });
            TongGiaTriCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                
            });
          
            LoadCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                LoadListKhachHang();
            });
            UncheckedCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                TangDan = false;
            }); CheckedCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                TangDan = true;
            });

        }
    }
}
