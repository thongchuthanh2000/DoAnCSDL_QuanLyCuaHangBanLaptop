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
    public class NSXViewModel : BaseViewModel
    {
        private ObservableCollection<Model.NSX> _List;
        public ObservableCollection<Model.NSX> List { get => _List; set { _List = value; OnPropertyChanged(); } }


        private NSX _SelectedItem;
        public NSX SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    MaNSX = SelectedItem.IdNSX;
                    TenNSX = SelectedItem.TenNSX;
                    DiaChi = SelectedItem.DiaChi;
                }
            }
        }


        private int _MaNSX;
        public int MaNSX { get => _MaNSX; set { _MaNSX = value; OnPropertyChanged(); } }

        private string _TenNSX;
        public string TenNSX { get => _TenNSX; set { _TenNSX = value; OnPropertyChanged(); } }


        private string _DiaChi;
        public string DiaChi { get => _DiaChi; set { _DiaChi = value; OnPropertyChanged(); } }


        //public ICommand DeleteCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand AddCommand { get; set; }

       

        private void LoadListNSX()
        {
            List = new ObservableCollection<NSX>();

            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM dbo.NSX");

            foreach (DataRow item in data.Rows)
            {
                NSX nsx = new NSX(item);
                List.Add(nsx);
            }
            OnPropertyChanged();

        }

        public NSXViewModel()
        {
            LoadListNSX();

            AddCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                string query = string.Format("Exec AddNSX @idNSX = {0}, @TenNSX =N'{1}', @DiaChi =N'{2}'",MaNSX,TenNSX.Trim(),DiaChi.Trim());

                var Object = DataProvider.Instance.ExecuteNonQuery(query);
                LoadListNSX();
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;

                string query = string.Format("Select * from NSX where  idNSX = {0}", SelectedItem.IdNSX);
                var displayList = DataProvider.Instance.ExecuteQuery(query);
                if (displayList != null)
                    return true;

                return false;

            }, (p) =>
            {
                string query = string.Format("Exec ChangeNSX @idNSX = {0}, @TenNSX = N'{1}', @DiaChi = N'{2}'",MaNSX,TenNSX.Trim(),DiaChi.Trim());

                var Object = DataProvider.Instance.ExecuteNonQuery(query);
                
                LoadListNSX();

            });
        }

    }
}
