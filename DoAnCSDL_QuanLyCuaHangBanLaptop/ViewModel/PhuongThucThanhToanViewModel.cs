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
    public class PhuongThucThanhToanViewModel:BaseViewModel
    {
     
        
            private ObservableCollection<Model.PhuongThucThanhToan> _List;
            public ObservableCollection<Model.PhuongThucThanhToan> List { get => _List; set { _List = value; OnPropertyChanged(); } }


            private PhuongThucThanhToan _SelectedItem;
            public PhuongThucThanhToan SelectedItem
            {
                get => _SelectedItem;
                set
                {
                    _SelectedItem = value;
                    OnPropertyChanged();
                    if (SelectedItem != null)
                    {
                        MaPhuongThucThanhToan = SelectedItem.MaPTTT;
                        TenPhuongThucThanhToan = SelectedItem.TenPTTT;
                    }
                }
            }


            private int _MaPhuongThucThanhToan;
            public int MaPhuongThucThanhToan { get => _MaPhuongThucThanhToan; set { _MaPhuongThucThanhToan = value; OnPropertyChanged(); } }

            private string _TenPhuongThucThanhToan;
            public string TenPhuongThucThanhToan { get => _TenPhuongThucThanhToan; set { _TenPhuongThucThanhToan = value; OnPropertyChanged(); } }

            //public ICommand DeleteCommand { get; set; }
            public ICommand EditCommand { get; set; }
            public ICommand AddCommand { get; set; }



            private void LoadListPTThanhToan()
            {
                List = new ObservableCollection<PhuongThucThanhToan>();

                DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM dbo.PT_ThanhToan" +
                    "");

                foreach (DataRow item in data.Rows)
                {
                    PhuongThucThanhToan ptThanhToan = new PhuongThucThanhToan(item);
                    List.Add(ptThanhToan);
                }
                OnPropertyChanged();

            }

            public PhuongThucThanhToanViewModel()
            {
                LoadListPTThanhToan();

                AddCommand = new RelayCommand<object>((p) =>
                {
                    return true;
                }, (p) =>
                {
                    string query = string.Format("Exec AddKhachHang @MaPTTT = {0}, @TenPTTT =N'{1}'",
                    MaPhuongThucThanhToan, TenPhuongThucThanhToan);

                    var Object = DataProvider.Instance.ExecuteNonQuery(query);
                    LoadListPTThanhToan();
                });

                EditCommand = new RelayCommand<object>((p) =>
                {
                    if (SelectedItem == null)
                        return false;

                    string query = string.Format("Select * from PT_ThanhToan where  MaPTTT = {0}", SelectedItem.MaPTTT);
                    var displayList = DataProvider.Instance.ExecuteQuery(query);
                    if (displayList != null)
                        return true;

                    return false;

                }, (p) =>
                {
                    string query = string.Format("");

                    var Object = DataProvider.Instance.ExecuteNonQuery(query);

                    LoadListPTThanhToan();

                });
            }

        }
    }

