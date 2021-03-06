﻿using DoAnCSDL_QuanLyCuaHangBanLaptop.Model;
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
                    MaNSX = SelectedItem.MaNSX;
                    TenNSX = SelectedItem.TenNSX;
                    DiaChi = SelectedItem.DiaChi;
                    LoadSoLuong(SelectedItem.MaNSX);
                    LoadGiaTriThuongHieu(SelectedItem.MaNSX);
                }
            }
        }
        public void LoadGiaTriThuongHieu(int maNSX)
        {
            String query = string.Format("Select dbo.fn_GiaTriThuongHieuCuaNSX({0})", maNSX );
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            try { GiaTriThuongHieu = (int)data.Rows[0][0]; }
            catch { GiaTriThuongHieu = 0; }
        }
        public void LoadSoLuong(int maNSX)
        {
            try
            {
                String query = string.Format("Select dbo.fn_TongHangHoaCuaNSX({0})", maNSX);
                DataTable data = DataProvider.Instance.ExecuteQuery(query);

            
                TongSP = (int)data.Rows[0][0];
            }
            catch
            {
                TongSP = 0;
            }
           }
        
        private int _TangDan;
        public int TangDan { get => _TangDan; set { _TangDan = value; OnPropertyChanged(); } }
       
        private int _Theo;
        public int Theo { get => _Theo; set { _Theo = value; OnPropertyChanged(); } }


        private int _GiaTriThuongHieu;
        public int GiaTriThuongHieu{ get => _GiaTriThuongHieu; set { _GiaTriThuongHieu = value; OnPropertyChanged(); } }
                
        private int _TongSP;
        public int TongSP { get => _TongSP; set { _TongSP = value; OnPropertyChanged(); } }


        private int _MaNSX;
        public int MaNSX { get => _MaNSX; set { _MaNSX = value; OnPropertyChanged(); } }

        private string _TenNSX;
        public string TenNSX { get => _TenNSX; set { _TenNSX = value; OnPropertyChanged(); } }

        private string _DiaChi;
        public string DiaChi { get => _DiaChi; set { _DiaChi = value; OnPropertyChanged(); } }


        public ICommand DeleteCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand TongHangCommand { get; set; }
        public ICommand CheckedCommand { get; set; }
        public ICommand LoadCommand { get; set; }
        public ICommand UncheckedCommand { get; set; }
        public ICommand TheoCommand { get; set; }
        private string _TenCommand;
        public string TenCommand { get => _TenCommand; set { _TenCommand = value; OnPropertyChanged(); } }
        public ICommand SapXepCommand { get; set; }

        private void LoadListNSX()
        {
            try { 
            List = new ObservableCollection<NSX>();

            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM dbo.V_List_NSX");


            foreach (DataRow item in data.Rows)
            {
                NSX nsx = new NSX(item);

                List.Add(nsx);
            }
            OnPropertyChanged();
            }
             catch (SqlException sqlEx)
            {
                MessageBox.Show(sqlEx.Message);
            }
        }
        private void LoadListTheoTongHang()
        {
            try
            {
                List = new ObservableCollection<NSX>();
                string query = string.Format("EXEC dbo.sp_SapXepHangHoaTongHangCuaNSX @Tang ={0}", TangDan);
                DataTable data = DataProvider.Instance.ExecuteQuery(query);


                foreach (DataRow item in data.Rows)
                {
                    NSX nsx = new NSX(item);

                    List.Add(nsx);
                }
                OnPropertyChanged();

            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show(sqlEx.Message);
            }

        }
        private void LoadListGiaTriThuongHieu()
        {
            try
            {
                List = new ObservableCollection<NSX>();
                string query = string.Format("EXEC dbo.sp_GiaTriThuongHieu @Tang ={0}", TangDan);
                DataTable data = DataProvider.Instance.ExecuteQuery(query);


                foreach (DataRow item in data.Rows)
                {
                    NSX nsx = new NSX(item);

                    List.Add(nsx);
                }
                OnPropertyChanged();
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show(sqlEx.Message);
            }
        }
        public NSXViewModel()
        {
            LoadListNSX();
            TangDan = 0;
            Theo = 0;
            TenCommand = "GiaTriThuongHieu";
            AddCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                try
                {
                    string query = string.Format("Exec dbo.sp_AddNSX @MaNSX = {0}, @TenNSX =N'{1}', @DiaChi =N'{2}'", MaNSX, TenNSX.Trim(), DiaChi.Trim());

                    var Object = DataProvider.Instance.ExecuteNonQuery(query);
                    LoadListNSX();
                }
                catch(SqlException sqlEx)
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
                    string query = string.Format("Exec dbo.sp_DeleteNSX @MaNSX = {0}", MaNSX);

                    var Object = DataProvider.Instance.ExecuteNonQuery(query);

                    LoadListNSX();
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
                    string query = string.Format("Exec dbo.sp_ChangeNSX @MaNSX = {0}, @TenNSX = N'{1}', @DiaChi = N'{2}'", MaNSX, TenNSX.Trim(), DiaChi.Trim());

                    var Object = DataProvider.Instance.ExecuteNonQuery(query);

                    LoadListNSX();
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show(sqlEx.Message);
                }

            });

            LoadCommand = new RelayCommand<object>((p) =>
            {

                return true;

            }, (p) =>
            {
                try
                {
                    LoadListNSX();
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show(sqlEx.Message);
                }
            });

            TheoCommand = new RelayCommand<object>((p) =>
            {

                return true;

            }, (p) =>
            {
                Theo = 1 - Theo;
                if (Theo == 1)
                {
                    TenCommand = "Tong SP";

                }
                else
                {
                    TenCommand = "GiaTriThuongHieu";
                }

            });
            SapXepCommand = new RelayCommand<object>((p) =>
            {

                return true;

            }, (p) =>
            {
                if (Theo==0)
                {
                    LoadListTheoTongHang();
                }
                else
                {
                    LoadListGiaTriThuongHieu();
                }
            });

            UncheckedCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                TangDan = 0;
            }); 
            CheckedCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                TangDan = 1;
            });

        }
    }
}
