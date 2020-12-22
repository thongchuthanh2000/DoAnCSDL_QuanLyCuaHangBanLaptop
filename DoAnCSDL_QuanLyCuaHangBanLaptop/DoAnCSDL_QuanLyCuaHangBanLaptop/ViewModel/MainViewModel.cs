using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DoAnCSDL_QuanLyCuaHangBanLaptop.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public bool Isloaded = false;
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand LogoutCommand { get; set; }
        public ICommand ProductCommand { get; set; }
        public ICommand SettingsCommand { get; set; }
        public ICommand EmployeeCommand { get; set; }
        public ICommand KhuyenMaiCommand { get; set; }
        public MainViewModel()
        {
            LoadedWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                Isloaded = true;
                if (p == null)
                    return;

                p.Hide();
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.ShowDialog();

                if (loginWindow.DataContext == null)
                    return;
                var loginVM = loginWindow.DataContext as LoginViewModel;

                if (loginVM.IsLogin)
                {
                    p.Show();
                    //LoadTonKhoData();
                }
                else
                {
                    p.Close();
                }
            }
              );

            LogoutCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                System.Windows.Application.Current.Shutdown();
            });
            ProductCommand = new RelayCommand<object>((p) => { return true; }, (p) => { ProductDetailPage wd = new ProductDetailPage(); wd.ShowDialog(); });
            SettingsCommand = new RelayCommand<object>((p) => { return true; }, (p) => { SettingsWindow wd = new SettingsWindow(); wd.ShowDialog(); });
            EmployeeCommand = new RelayCommand<object>((p) => { return true; }, (p) => { NhanVienWindow wd = new NhanVienWindow(); wd.ShowDialog(); });
            KhuyenMaiCommand = new RelayCommand<object>((p) => { return true; }, (p) => { NhanVienWindow wd = new NhanVienWindow(); wd.ShowDialog(); });
        }


    }

}
