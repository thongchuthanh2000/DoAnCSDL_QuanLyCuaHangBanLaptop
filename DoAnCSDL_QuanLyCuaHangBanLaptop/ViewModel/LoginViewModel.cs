using DoAnCSDL_QuanLyCuaHangBanLaptop.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DoAnCSDL_QuanLyCuaHangBanLaptop.ViewModel
{
    class LoginViewModel : BaseViewModel
    {
        public bool IsLogin { get; set; }
        private string _UserName;
        public string UserName { get => _UserName; set { _UserName = value; OnPropertyChanged(); } }

        private string _Password;
        public string Password { get => _Password; set { _Password = value; OnPropertyChanged(); } }

        public ICommand CloseCommand { get; set; }
        public ICommand LoginCommand { get; set; }


        public LoginViewModel()
        {
            IsLogin = false;
            Password = "";
            UserName = "";
            LoginCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { Login(p); });
            
 
        }

        private void Login(Window p)
        {
            if (p == null)
                return;




            NhanVien nhanVien = new NhanVien();
            try
            {
                string query = string.Format(" SELECT * FROM dbo.fn_DangNhap(N'{0}',N'{1}')", UserName,Password);

                DataTable data = DataProvider.Instance.ExecuteQuery(query);
                
                foreach (DataRow item in data.Rows)
                {
                    nhanVien = new NhanVien(item);
                    break;
                }
                
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show(sqlEx.Message);
            }


            if (nhanVien.TenTK != null)
            {
                if (nhanVien.TenPQ == "Admin")
                {
                    DataProvider.connectionSTR = "Data Source=(local);Initial Catalog=CSDL;User ID=QuanLyBanHang;Password=123";
                }
                else
                {
                    DataProvider.connectionSTR = "Data Source=(local);Initial Catalog=CSDL;User ID=NhanVienBanHang;Password=1234";
                }
                IsLogin = true;
                p.Close();
            }
            else
            {
                IsLogin = false;
                MessageBox.Show("Sai tài khoản hoặc mật khẩu!");
            }
        }

       

       
    }
}
