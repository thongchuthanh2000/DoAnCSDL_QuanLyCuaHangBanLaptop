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
    public class KhuyenMaiViewModel:BaseViewModel
    {
        private ObservableCollection<Model.KhuyenMai> _List;
        public ObservableCollection<Model.KhuyenMai> List { get => _List; set { _List = value; OnPropertyChanged(); } }


        private KhuyenMai _SelectedItem;
        public KhuyenMai SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    MaKhuyenMai = SelectedItem.MaKhuyenMai;
                    GiaTriKhuyenMai = SelectedItem.GiaTriKhuyenMai;
                    NgBatDau = SelectedItem.NgayBatDau;
                    NgKetThuc = SelectedItem.NgayKetThuc;
                    SoLuongTheKhuyenMai(NgBatDau, NgKetThuc);
                    TinhLoiNhuan(NgBatDau, NgKetThuc);
                }
            }
        }

        
             private int _LoiNhuan;
        public int LoiNhuan { get => _LoiNhuan; set { _LoiNhuan = value; OnPropertyChanged(); } }
        private int _MaKhuyenMai;
        public int MaKhuyenMai { get => _MaKhuyenMai; set { _MaKhuyenMai = value; OnPropertyChanged(); } }

        private int _GiaTriKhuyenMai;
        public int GiaTriKhuyenMai { get => _GiaTriKhuyenMai; set { _GiaTriKhuyenMai = value; OnPropertyChanged(); } }

        private int _TongMaKM;
        public int TongMaKM { get => _TongMaKM; set { _TongMaKM = value; OnPropertyChanged(); } }

        private int _Loi;
        public int Loi { get => _Loi; set { _Loi = value; OnPropertyChanged(); } }

        private int _ChenhLech;
        public int ChenhLech { get => _ChenhLech; set { _ChenhLech = value; OnPropertyChanged(); } }

        private DateTime _NgBatDau;
        public DateTime NgBatDau { get => _NgBatDau; set { _NgBatDau = value; OnPropertyChanged(); } }

        private DateTime _NgKetThuc;
        public DateTime NgKetThuc { get => _NgKetThuc; set { _NgKetThuc = value; OnPropertyChanged(); } }


        private DateTime _NgBatDauXet;
        public DateTime NgBatDauXet { get => _NgBatDauXet; set { _NgBatDauXet = value; OnPropertyChanged(); } }

        private DateTime _NgKetThucXet;
        public DateTime NgKetThucXet { get => _NgKetThucXet; set { _NgKetThucXet = value; OnPropertyChanged(); } }


        public ICommand DeleteCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand AddCommand { get; set; }

        public ICommand LoadListCommand { get; set; }
        public ICommand LoadThongKeCommand { get; set; }
        

        private void LoadListKM()
        {
            List = new ObservableCollection<KhuyenMai>();

            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM dbo.KhuyenMai");

            foreach (DataRow item in data.Rows)
            {
                KhuyenMai khuyenmai = new KhuyenMai(item);
                List.Add(khuyenmai);
            }
            OnPropertyChanged();

        }
        private void LoadListKM(DateTime? dateBatDau = null, DateTime? dateKetThuc = null)
        {
            List = new ObservableCollection<KhuyenMai>();
            string query = string.Format("EXEC dbo.sp_TimKiemKhuyenMai @NgayBatDau = {0} ,@NgayKetThuc = {1}", dateBatDau, dateKetThuc);
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                KhuyenMai khuyenmai = new KhuyenMai(item);
                List.Add(khuyenmai);
            }
            OnPropertyChanged();

        }
        private void SoLuongTheKhuyenMai(DateTime? dateBatDau =null, DateTime? dateKetThuc=null){
            if (dateBatDau == null)
            {
                dateBatDau = new DateTime(0001, 01, 01);
            }
            if (dateKetThuc == null)
            {
                dateKetThuc = new DateTime(3000, 01, 01);
            }
            string query = string.Format(" Select * From fn_SoLuongTheKhuyenMai({0},{1})", dateBatDau, dateKetThuc);

            DataTable table = DataProvider.Instance.ExecuteQuery(query);

            TongMaKM = (int)table.Rows[0][0];
            return;

        }
        
        private void TinhLoiNhuan(DateTime? dateBatDau = null, DateTime? dateKetThuc = null)
        {
            if (dateBatDau == null)
            {
                dateBatDau = new DateTime(0001, 01, 01);
            }
            if (dateKetThuc == null)
            {
                dateKetThuc = new DateTime(3000, 01, 01);
            }

            string query = string.Format(" Select * From fn_LoiNhuanKhuyenMai({0},{1},{2})", MaKhuyenMai,dateBatDau, dateKetThuc);
            DataTable table = DataProvider.Instance.ExecuteQuery(query);

            ChenhLech = (int)table.Rows[0][0];
            Loi = (int)table.Rows[0][1];
            return;
        }
       
        public KhuyenMaiViewModel()
        {
            LoadListKM();

            LoadListCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {

                LoadListKM();
            });
            LoadThongKeCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {    
                LoadListKM(NgBatDauXet, NgKetThucXet);
            });


            AddCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                string query = string.Format("Exec AddKhuyenMai @MaKhuyenMai = {0}, @GiaTriKhuyenMai =N'{1}', @NgayBatDau =N'{2}',@NgayKetThuc= N'{3}'",
                MaKhuyenMai, GiaTriKhuyenMai, NgBatDau, NgKetThuc);

                var Object = DataProvider.Instance.ExecuteNonQuery(query);
                LoadListKM();
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;


                string query = string.Format("Select * from KhuyenMai where  MaKhuyenMai = {0}", SelectedItem.MaKhuyenMai);
                var displayList = DataProvider.Instance.ExecuteQuery(query);
                if (displayList != null)
                    return true;

                return false;

            }, (p) =>
            {
                string query = string.Format("Exec ChangeKhuyenMai @MaKhuyenMai = {0}, @GiaTriKhuyenMai =N'{1}', @NgayBatDau =N'{2}',@NgayKetThuc= N'{3}'",
                               MaKhuyenMai, GiaTriKhuyenMai, NgBatDau, NgKetThuc);

                var Object = DataProvider.Instance.ExecuteNonQuery(query);

                LoadListKM();


            });

            DeleteCommand = new RelayCommand<object>((p) =>
            {
               
                    return true;
            }, (p) =>
            {
                string query = string.Format("Exec sp_DeleteKhuyenMai  @MaKhuyenMai = {0}",MaKhuyenMai);

                var Object = DataProvider.Instance.ExecuteNonQuery(query);

                LoadListKM();


            });

         
        }


    }
}
