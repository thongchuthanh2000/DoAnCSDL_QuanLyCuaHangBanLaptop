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
                }
            }
        }


        private int _MaKhuyenMai;
        public int MaKhuyenMai { get => _MaKhuyenMai; set { _MaKhuyenMai = value; OnPropertyChanged(); } }

        private int _GiaTriKhuyenMai;
        public int GiaTriKhuyenMai { get => _GiaTriKhuyenMai; set { _GiaTriKhuyenMai = value; OnPropertyChanged(); } }


        private DateTime _NgBatDau;
        public DateTime NgBatDau { get => _NgBatDau; set { _NgBatDau = value; OnPropertyChanged(); } }

        private DateTime _NgKetThuc;
        public DateTime NgKetThuc { get => _NgKetThuc; set { _NgKetThuc = value; OnPropertyChanged(); } }

        //public ICommand DeleteCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand AddCommand { get; set; }



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

        public KhuyenMaiViewModel()
        {
            LoadListKM();

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
        }

    }
}
