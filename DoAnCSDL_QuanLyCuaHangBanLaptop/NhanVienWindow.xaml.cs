using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DoAnCSDL_QuanLyCuaHangBanLaptop
{
    /// <summary>
    /// Interaction logic for NhanVienWindow.xaml
    /// </summary>
    public partial class NhanVienWindow : Window
    {
        public NhanVienWindow()
        {
            InitializeComponent();
        }

        private void FutureDatePicker_Loaded(object sender, RoutedEventArgs e)
        {
            FutureDatePicker.SelectedDate = DateTime.Now.AddDays(0);
        }
    }
}
