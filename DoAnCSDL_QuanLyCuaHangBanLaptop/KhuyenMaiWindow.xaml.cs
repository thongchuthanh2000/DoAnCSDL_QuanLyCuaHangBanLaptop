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
    /// Interaction logic for KhuyenMaiWindow.xaml
    /// </summary>
    public partial class KhuyenMaiWindow : Window
    {
        public KhuyenMaiWindow()
        {
            InitializeComponent();
        }

        private void ngBatDauDate_Loaded(object sender, RoutedEventArgs e)
        {
            ngBatDauDate.SelectedDate = DateTime.Now.AddDays(0);
        }

        private void ngKetThucDate_Loaded(object sender, RoutedEventArgs e)
        {
            ngKetThucDate.SelectedDate = DateTime.Now.AddDays(0);

        }
    }
}
