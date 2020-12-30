using DoAnCSDL_QuanLyCuaHangBanLaptop.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace DoAnCSDL_QuanLyCuaHangBanLaptop.ViewModel
{
  
    public class ProductViewModel:BaseViewModel
    {

        private ObservableCollection<Model.HangHoa> _List;
        public ObservableCollection<Model.HangHoa> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.NSX> _NSX;
        public ObservableCollection<Model.NSX> NSX { get => _NSX; set { _NSX = value; OnPropertyChanged(); } }
       

        private Model.NSX _SelectedNSX;
        public Model.NSX SelectedNSX
        {
            get => _SelectedNSX;
            set
            {
                _SelectedNSX = value;
                OnPropertyChanged();
            }
        }

        private int _Con;
        public int Con { get => _Con; set { _Con = value; OnPropertyChanged(); } }

        private int _Ban;
        public int Ban { get => _Ban; set { _Ban = value; OnPropertyChanged(); } }

        public ICommand ImageCommand { get; set; }
        public ICommand HangConCommand { get; set; }
        public ICommand HetHangCommand { get; set; }
        public ICommand TonKhoCommand { get; set; }
        public ICommand BanChayCommand { get; set; }

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
                    SoLuong = SelectedItem.SoLuong;
                    GiaGoc = SelectedItem.GiaGoc;
                    GiaBan = SelectedItem.GiaBan;
                    MaNSX = SelectedItem.MaNSX;
                    RAM = SelectedItem.RAM;
                    CPU = SelectedItem.CPU;
                    PIN = SelectedItem.PIN;
                    ManHinh = SelectedItem.ManHinh;
                    SelectedNSX = SelectedItem.NSX;
                    BitmapImage = SelectedItem.BitmapImage;
                    TongBan = GiaTriTongBan(SelectedItem.MaSP);
                }
            }
        }
    

        private int _MaNSX;
        public int MaNSX { get => _MaNSX; set { _MaNSX = value; OnPropertyChanged(); } }

        private int _MaHang;
        public int MaHang { get => _MaHang; set { _MaHang = value; OnPropertyChanged(); } }


        private string _TenSanPham;
        public string TenSanPham { get => _TenSanPham; set { _TenSanPham = value; OnPropertyChanged(); } }


        private int _SoLuong;
        public int SoLuong { get => _SoLuong; set { _SoLuong = value; OnPropertyChanged(); } }


        private int _GiaGoc;
        public int GiaGoc { get => _GiaGoc; set { _GiaGoc = value; OnPropertyChanged(); } }


        private int _GiaBan;
        public int GiaBan { get => _GiaBan; set { _GiaBan = value; OnPropertyChanged(); } }

        private string _rAM;
        private string _manHinh;
        private string _cPU;
        private string _pIN;
        private string _TenSPTimKiem;
        private int _TongBan;
        public int TongBan { get => _TongBan; set { _TongBan = value; OnPropertyChanged(); } }

        private string _TenCommand;
        public string TenCommand { get => _TenCommand; set { _TenCommand = value; OnPropertyChanged(); } }



        private string _TenBanCommand;
        public string TenBanCommand { get => _TenBanCommand; set { _TenBanCommand = value; OnPropertyChanged(); } }


        public ICommand SearchCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand LoadCommand { get; set; }
        public ICommand SapXepCommand { get; set; }
        public ICommand AllHangCommand { get; set; }
        public ICommand AllBanCommand { get; set; }
        public string RAM { get => _rAM; set { _rAM = value; OnPropertyChanged(); } }
        public string ManHinh { get => _manHinh; set { _manHinh = value; OnPropertyChanged(); } }
        public string CPU { get => _cPU; set { _cPU = value; OnPropertyChanged(); } }
        public string PIN { get => _pIN; set { _pIN = value; OnPropertyChanged(); } }
        public string TenSPTimKiem { get => _TenSPTimKiem; set { _TenSPTimKiem = value; OnPropertyChanged(); } }
        public byte[] BitmapImage { get => _BitmapImage; set { _BitmapImage = value; OnPropertyChanged(); }  }

        private byte[] _BitmapImage;

        private void LoadListSanPham()
        {
            try
            {
                List = new ObservableCollection<HangHoa>();

                DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM V_LIST_Laptop");

                foreach (DataRow item in data.Rows)
                {
                    string qu = string.Format("Select * From  dbo.fn_TimKiemNSXByMaNSX ({0})", (int)item["MaNSX"]);


                    DataTable dataNSX = DataProvider.Instance.ExecuteQuery(qu);

                    NSX nsx = new NSX(dataNSX.Rows[0]);


                    HangHoa sanpham = new HangHoa(item, nsx);
                    List.Add(sanpham);



                }
                OnPropertyChanged();
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show(sqlEx.Message);
            }

        }
        private int GiaTriTongBan(int ID)
        {
            try
            {
                string query = string.Format("Select dbo.fn_SoLuongBan({0})", ID);
                DataTable data = DataProvider.Instance.ExecuteQuery(query);

                return (int)data.Rows[0][0];
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show(sqlEx.Message);
            }
            return 0;
        }
        private void LoadListSXSanPham()
        {
            try
            {
                List = new ObservableCollection<HangHoa>();
                string query = string.Format("EXEC  dbo.sp_ListSXSP @Ban = {0},  @Con = {1}", Ban, Con);


                DataTable data = DataProvider.Instance.ExecuteQuery(query);

                foreach (DataRow item in data.Rows)
                {
                    string qu = string.Format("Select * From  dbo.fn_TimKiemNSXByMaNSX ({0})", (int)item["MaNSX"]);

                    DataTable dataNSX = DataProvider.Instance.ExecuteQuery(qu);

                    NSX nsx = new NSX(dataNSX.Rows[0]);


                    HangHoa sanpham = new HangHoa(item, nsx);
                    List.Add(sanpham);
                }
                OnPropertyChanged();
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show(sqlEx.Message);
            }
        }
        private void LoadListSearchSP()
        {
            try
            {
                List = new ObservableCollection<HangHoa>();
                string query = string.Format("Select * From dbo.fn_GetLapByTenSP(N'{0}')", TenSPTimKiem);


                DataTable data = DataProvider.Instance.ExecuteQuery(query);

                foreach (DataRow item in data.Rows)
                {
                    string qu = string.Format("Select * From  dbo.fn_TimKiemNSXByMaNSX ({0})", (int)item["MaNSX"]);
                    DataTable dataNSX = DataProvider.Instance.ExecuteQuery(qu);

                    NSX nsx = new NSX(dataNSX.Rows[0]);


                    HangHoa sanpham = new HangHoa(item, nsx);
                    List.Add(sanpham);
                }
                OnPropertyChanged();
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show(sqlEx.Message);
            }
        }

        private void LoadListNSX()
        {
            try
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
            catch (SqlException sqlEx)
            {
                MessageBox.Show(sqlEx.Message);
            }
            OnPropertyChanged();
        }
     
        public ProductViewModel()
        {
            LoadListNSX();
            LoadListSanPham();
            Con = -1;
            Ban = -1;
            TenCommand = "All";
            TenBanCommand = "All";
            AddCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                try
                {
                    string query = string.Format("Exec dbo.sp_AddHangHoa @MaSP= {0}, @MaNSX ={1}, @TenSP =N'{2}', @CPU =N'{3}', @RAM =N'{4}', @ManHinh = N'{5}', @PIN=N'{6}',@giaGoc= {7},@giaBan={8},@SoLuong ={9}",
                       MaHang, SelectedNSX.MaNSX, TenSanPham, CPU.Trim(), RAM.Trim(), ManHinh.Trim(), PIN.Trim(), GiaGoc, GiaBan, SoLuong);

                    var Object = DataProvider.Instance.ExecuteNonQuery(query);
                    LoadListSanPham();
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
                    string query = string.Format("Exec dbo.sp_ChangeHangHoa @MaSP = {0}, @MaNSX ={1}, @TenSP =N'{2}',@CPU =N'{3}', @RAM =N'{4}', @ManHinh = N'{5}', @PIN=N'{6}', @giaGoc= {7},@giaBan={8},@SoLuong ={9}",
                        MaHang, SelectedNSX.MaNSX, TenSanPham, CPU.Trim(), RAM.Trim(), ManHinh.Trim(), PIN.Trim(), GiaGoc, GiaBan, SoLuong);

                    var Object = DataProvider.Instance.ExecuteNonQuery(query);

                    LoadListSanPham();
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
                    string query = string.Format("Exec dbo.sp_DeleteHangHoa @MaSP = {0}", MaHang);

                    var Object = DataProvider.Instance.ExecuteNonQuery(query);

                    LoadListSanPham();
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show(sqlEx.Message);
                }

            }); LoadCommand = new RelayCommand<object>((p) =>

            {
                return true;
            }, (p) =>
            {
                LoadListSanPham();

            });
            SearchCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                LoadListSearchSP();
            });
            ImageCommand = new RelayCommand<object>((p) => { return true; }, (p) => {


                OpenFileDialog dialog1 = new OpenFileDialog();
                FileStream fs;
                BinaryReader br;
                byte[] ImageData;

                if (dialog1.ShowDialog() == true)
                {
                    MessageBox.Show(dialog1.FileName);
                    fs = new FileStream(dialog1.FileName, FileMode.Open, FileAccess.Read);
                    br = new BinaryReader(fs);
                    ImageData = br.ReadBytes((int)fs.Length);
                    br.Close();
                    fs.Close();


                    DataProvider.Instance.ExecuteNonQuery(" UPDATE dbo.HangHoa_Image_VIEW SET Hinh = @Hinh WHERE MaSP = @MaSP", new object[2] { ImageData ,MaHang});

                }


            });
            HangConCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                Con++;
                if (Con == 2)
                {
                    Con = -1;
                }
                if (Con == -1)
                {
                    TenCommand = "All";
                }
                if (Con == 0)
                {
                    TenCommand = "HetHang";
                }
                if (Con == 1)
                    TenCommand = "ConHang";
            });
  
            BanChayCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {

                Ban++;
                if (Ban == 2)
                {
                    Ban = -1;
                }
                if (Ban == -1)
                {
                    TenBanCommand = "All";
                }
                if (Ban == 0)
                {
                    TenBanCommand = "TonKho";
                }
                if (Ban == 1)
                    TenBanCommand = "BanChay";
            });
            SapXepCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                LoadListSXSanPham();
            });
        }

    }
}
