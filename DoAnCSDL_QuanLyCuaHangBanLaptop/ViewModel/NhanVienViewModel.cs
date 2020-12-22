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
                    TenPQ = SelectedItem.TenPQ;
                    TenTK = SelectedItem.TenTK;
                    Chan = SelectedItem.Chan;
                }
            }
        }


        private int _MaNhanVien;
        public int MaNhanVien { get => _MaNhanVien; set { _MaNhanVien = value; OnPropertyChanged(); } }

        private int _Quyen;
        public int Quyen { get => _Quyen; set { _Quyen = value; OnPropertyChanged(); } }

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
        private string _TenPQ;
        public string TenPQ { get => _TenPQ; set { _TenPQ = value; OnPropertyChanged(); } }

        private string _tenTK;
        public string TenTK { get => _tenTK; set { _tenTK = value; OnPropertyChanged(); } }
        
        private string _Chan;
        public string Chan { get => _Chan; set { _Chan = value; OnPropertyChanged(); } }


        private int _SXKhoa;
        public int SXKhoa { get => _SXKhoa; set { _SXKhoa = value; OnPropertyChanged(); } }

        private string _SXQuyen;
        public string SXQuyen { get => _SXQuyen; set { _SXQuyen = value; OnPropertyChanged(); } }

        private string _TenNhanVienTimKiem;
        public string TenNhanVienTimKiem { get => _TenNhanVienTimKiem; set { _TenNhanVienTimKiem = value; OnPropertyChanged(); } }

        
        private string _MK;
        public string MK { get => _MK; set { _MK = value; OnPropertyChanged(); } }

        private string _MKMoi;
        public string MKMoi { get => _MKMoi; set { _MKMoi = value; OnPropertyChanged(); } }

        private string _MKMoiXacNhan;
        public string MKMoiXacNhan { get => _MKMoiXacNhan; set { _MKMoiXacNhan = value; OnPropertyChanged(); } }

        public ICommand DeleteCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand ChanCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand LoadCommand { get; set; }
        public ICommand QuyenCommand { get; set; }
        public ICommand MKCommand { get; set; }
        public ICommand AdminCommand { get; set; }
        public ICommand NhanVienCommand { get; set; }
        public ICommand AllQuyenCommand { get; set; }
        public ICommand Khoa { get; set; }
        public ICommand KhongKhoa { get; set; }
        public ICommand AllKhoaCommand { get; set; }

        private void LoadListNhanVien()
        {
            try
            {
                List = new ObservableCollection<NhanVien>();
                string query = string.Format("SELECT * FROM  fn_DanhSachNV @TenNV =N'{0}', @Khoa ={1}, @Quyen =N'{2}'", "", SXKhoa, SXQuyen);
                DataTable data = DataProvider.Instance.ExecuteQuery(query);

                foreach (DataRow item in data.Rows)
                {
                    NhanVien nhanvien = new NhanVien(item);
                    List.Add(nhanvien);
                }
                OnPropertyChanged();
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Khong co quyen truy cap Hoac loi du lieu");
            }
        }

        public NhanVienViewModel()
        {
            LoadListNhanVien();
            SXKhoa = -1;
            SXQuyen = "All";

            AddCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                try
                {
                    string query = string.Format("Exec sp_AddNhanVien @MaNV ={0}, @Hoten =N'{1}', @NgaySinh ='{2}',@GioiTinh= N'{3}',@DiaChi=N'{4}',"
                       + "@SDT =N'{5}', @TenTK=N'{6}'",
                       MaNhanVien, TenNhanVien, NgaySinh, GioiTinh, DiaChi, SDT, TenTK);

                    var Object = DataProvider.Instance.ExecuteNonQuery(query);
                    LoadListNhanVien();
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show("Khong co quyen truy cap Hoac loi du lieu");
                }
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                try
                {
                    string query = string.Format("Exec dbo.sp_ChangeThongTinNhanVien @MaNV = {0}, @Hoten =N'{1}', @NgaySinh ='{2}',@GioiTinh='{3}',@DiaChi=N{4}',"
                       + "@SDT ='{5}', @TenTK = N'{6}'", MaNhanVien, TenNhanVien, NgaySinh, GioiTinh, DiaChi, SDT, TenTK);

                    var Object = DataProvider.Instance.ExecuteNonQuery(query);
                    LoadListNhanVien();
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show("Khong co quyen truy cap Hoac loi du lieu");
                }
            });
            DeleteCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
             {
                 try
                 {
                     string query = string.Format("Exec sp_DeleteNhanVien @MaNV = {0}", MaNhanVien);

                     var Object = DataProvider.Instance.ExecuteNonQuery(query);
                     LoadListNhanVien();
                 }
                 catch (SqlException sqlEx)
                 {
                     MessageBox.Show("Khong co quyen truy cap Hoac loi du lieu");
                 }
             }
            );

            ChanCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                try
                {
                    string query = string.Format("Exec sp_ChangeChanNhanVien @MaNV = {0}", MaNhanVien);

                    var Object = DataProvider.Instance.ExecuteNonQuery(query);
                    LoadListNhanVien();
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show("Khong co quyen truy cap Hoac loi du lieu");
                }

            }
            );
            SearchCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                try
                {
                    List = new ObservableCollection<NhanVien>();
                    string query = string.Format("SELECT * FROM  fn_DanhSachNV @TenNV =N'{0}', @Khoa ={1}, @Quyen =N'{2}'", TenNhanVienTimKiem, SXKhoa, SXQuyen);
                    DataTable data = DataProvider.Instance.ExecuteQuery(query);

                    foreach (DataRow item in data.Rows)
                    {
                        NhanVien nhanvien = new NhanVien(item);
                        List.Add(nhanvien);
                    }
                    OnPropertyChanged();
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show("Khong co quyen truy cap Hoac loi du lieu");
                }
            }
           );

            LoadCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                LoadListNhanVien();
            }
           );

            QuyenCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                try
                {
                    string query = string.Format("Exec sp_ChangeQuyenNhanVien  @MaNV = {0}, @TenPQ = {1}", MaNhanVien, TenPQ);

                    var Object = DataProvider.Instance.ExecuteNonQuery(query);
                    LoadListNhanVien();
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show("Khong co quyen truy cap Hoac loi du lieu");
                }
            }
           );

            MKCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                try
                {
                    string query = string.Format("Exec sp_ChangeMatKhauNhanVien  @TenTK = N'{0}', @MK = N'{1}', @MKMoi = N'{2}', @MKMoiXacNhan=N'{3}'", TenTK, MK, MKMoi, MKMoiXacNhan);

                    var Object = DataProvider.Instance.ExecuteNonQuery(query);
                    LoadListNhanVien();
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show("Khong co quyen truy cap Hoac loi du lieu");
                }
            }
           );

            AdminCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                SXQuyen = "Admin";
            }
            );
            NhanVienCommand =
            AdminCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                SXQuyen = "NhanVien";

            });
            AllQuyenCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                SXQuyen = "All";

            });
            
            
            AllKhoaCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                SXKhoa = -1;

            });
            Khoa = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                SXKhoa = 1;

            });
            KhongKhoa = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                SXKhoa = 0;

            });
        }

    }

}
