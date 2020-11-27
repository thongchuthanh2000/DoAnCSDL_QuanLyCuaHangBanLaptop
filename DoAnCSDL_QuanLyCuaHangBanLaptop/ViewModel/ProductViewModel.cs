using DoAnCSDL_QuanLyCuaHangBanLaptop.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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

        private ObservableCollection<Model.SanPham> _List;
        public ObservableCollection<Model.SanPham> List { get => _List; set { _List = value; OnPropertyChanged(); } }

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


        private Model.Image _SelectedImage;
        public Model.Image SelectedImage
        {
            get => _SelectedImage;
            set
            {
                _SelectedImage = value;
                OnPropertyChanged();
            }
        }
        public ICommand ImageCommand { get; set; }

      
private SanPham _SelectedItem;
        public SanPham SelectedItem
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
                    ChangeImage(SelectedItem.MaSP);
                }
            }
        }
        public void ChangeImage(int id)
        {
            string query = string.Format("Select * from HangHoaImage where  MaSP = {0}", id);
            DataTable table = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in table.Rows)
            {
                Image image = new Image(item);
                SelectedImage = image;
                
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



        //public ICommand DeleteCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand AddCommand { get; set; }

        public string RAM { get => _rAM; set { _rAM = value; OnPropertyChanged(); } }
        public string ManHinh { get => _manHinh; set { _manHinh = value; OnPropertyChanged(); } }
        public string CPU { get => _cPU; set { _cPU = value; OnPropertyChanged(); } }
        public string PIN { get => _pIN; set { _pIN = value; OnPropertyChanged(); } }

        public BitmapImage Image { get => _Image; set => _Image = value; }

        private BitmapImage _Image;

        private void LoadListSanPham()
        {
            List = new ObservableCollection<SanPham>();
            
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM dbo.HangHoa");

            foreach (DataRow item in data.Rows)
            {
                foreach (var temp in NSX)
                {
                    if (temp.MaNSX == (int)item["MaNSX"])
                    {
                        SanPham sanpham = new SanPham(item,temp);
                        List.Add(sanpham);
                    }
                }
                
            }
            OnPropertyChanged();

        }

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
     
        public ProductViewModel()
        {
             LoadListNSX();
            LoadListSanPham();

            AddCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedNSX == null)
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                string query = string.Format("Exec AddHangHoa @MaNSX ={0}, @TenSP =N'{1}',@giaGoc= {2},@giaBan={3},"
                   + "@SoLuong ={4}, @CPU =N'{5}', @RAM =N'{6}', @ManHinh = N'{7}', @PIN=N'{8}'",
                   SelectedNSX.MaNSX, TenSanPham, GiaGoc, GiaBan, SoLuong, CPU.Trim(), RAM.Trim(), ManHinh.Trim(), PIN.Trim());
                //AddImage();
                var Object = DataProvider.Instance.ExecuteNonQuery(query);
                LoadListSanPham();
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;

                string query = string.Format("Select * from HangHoa where  MaHang = {0}", SelectedItem.MaSP);
                var displayList = DataProvider.Instance.ExecuteQuery(query);
                if (displayList != null)
                    return true;

                return false;

            }, (p) =>
            {
                string query = string.Format("Exec ChangeHangHoa @MaSP = {0}, @MaNSX ={1}, @TenSP =N'{2}',@giaGoc= {3},@giaBan={4},"
                    +"@SoLuong ={5}, @CPU =N'{6}', @RAM =N'{7}', @ManHinh = N'{8}', @PIN=N'{9}'",
                    MaHang, SelectedNSX.MaNSX, TenSanPham,GiaGoc,GiaBan,SoLuong,CPU.Trim(), RAM.Trim(),ManHinh.Trim(), PIN.Trim());

                var Object = DataProvider.Instance.ExecuteNonQuery(query);

                LoadListSanPham();


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


                    string query = string.Format("INSERT INTO HangHoaImage(MaSP, Hinh) VALUES( @MaSP , @image )");
                    int x = DataProvider.Instance.ExecuteNonQuery(query, new Object[2] { SelectedItem.MaSP, ImageData });

                }


            });

        }

    }
}
