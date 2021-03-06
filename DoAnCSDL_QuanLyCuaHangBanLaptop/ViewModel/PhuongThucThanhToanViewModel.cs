﻿using DoAnCSDL_QuanLyCuaHangBanLaptop.Model;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;

namespace DoAnCSDL_QuanLyCuaHangBanLaptop.ViewModel
{
    public class PhuongThucThanhToanViewModel : BaseViewModel
    {


        private ObservableCollection<Model.PT_ThanhToan> _List;
        public ObservableCollection<Model.PT_ThanhToan> List { get => _List; set { _List = value; OnPropertyChanged(); } }


        private PT_ThanhToan _SelectedItem;
        public PT_ThanhToan SelectedItem
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
                    LoadTongPTTT();
                }
            }
        }
        public void LoadTongPTTT()
        {
            string query = string.Format(" Select dbo.fn_TongGiaTriThanhToanBangPTTT ({0}) ", MaPhuongThucThanhToan);
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            try
            {
                TongPTTT = (int)data.Rows[0][0];
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show(sqlEx.Message);

            }
            catch
            {
                TongPTTT = 0;
            }
        }

        private int _TongPTTT;
        public int TongPTTT { get => _TongPTTT; set { _TongPTTT = value; OnPropertyChanged(); } }



        private int _MaPhuongThucThanhToan;
        public int MaPhuongThucThanhToan { get => _MaPhuongThucThanhToan; set { _MaPhuongThucThanhToan = value; OnPropertyChanged(); } }

        private string _TenPhuongThucThanhToan;
        public string TenPhuongThucThanhToan { get => _TenPhuongThucThanhToan; set { _TenPhuongThucThanhToan = value; OnPropertyChanged(); } }

        public ICommand DeleteCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand AddCommand { get; set; }



        private void LoadListPTThanhToan()
        {
            try
            {
                List = new ObservableCollection<PT_ThanhToan>();

                DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM V_LIST_PhuongThucThanhToan");

                foreach (DataRow item in data.Rows)
                {
                    PT_ThanhToan ptThanhToan = new PT_ThanhToan(item);
                    List.Add(ptThanhToan);
                }
                OnPropertyChanged();
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show(sqlEx.Message);
            }
        }

        public PhuongThucThanhToanViewModel()
        {
            LoadListPTThanhToan();

            AddCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                try
                {
                    string query = string.Format("Exec dbo.sp_AddPT_ThanhToan @MaPTTT = {0}, @TenPTTT =N'{1}'",
                    MaPhuongThucThanhToan, TenPhuongThucThanhToan);

                    var Object = DataProvider.Instance.ExecuteNonQuery(query);
                    LoadListPTThanhToan();
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
                    string query = string.Format("Exec dbo.sp_DeletePT_ThanhToan @MaPTTT={0}", MaPhuongThucThanhToan);
                    var Object = DataProvider.Instance.ExecuteNonQuery(query);

                    LoadListPTThanhToan();
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show(sqlEx.Message);
                }
            }
            );
            EditCommand = new RelayCommand<object>((p) =>
                {
                    return true;
                }, (p) =>
                {
                    try
                    {
                        string query = string.Format("Exec dbo.sp_ChangePT_ThanhToan @MaPTTT={0}, @TenPTTT =N'{1}'", MaPhuongThucThanhToan, TenPhuongThucThanhToan);


                        var Object = DataProvider.Instance.ExecuteNonQuery(query);

                        LoadListPTThanhToan();
                    }
                    catch (SqlException sqlEx)
                    {
                        MessageBox.Show(sqlEx.Message);
                    }
                });
        }

    }
}

