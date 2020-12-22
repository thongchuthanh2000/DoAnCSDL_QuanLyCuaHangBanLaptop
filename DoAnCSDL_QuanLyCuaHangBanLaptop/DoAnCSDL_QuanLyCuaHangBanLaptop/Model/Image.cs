using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace DoAnCSDL_QuanLyCuaHangBanLaptop.Model
{
    public class Image
    {
        private int _idImage;
        private BitmapImage _bitmapImage;

        public int IdImage { get => _idImage; set => _idImage = value; }
        public BitmapImage BitmapImage { get => _bitmapImage; set => _bitmapImage = value; }

        public Image(int idImage, BitmapImage bitmapImage)
        {
            this.IdImage = idImage;
            this.BitmapImage = bitmapImage;
        }
        public Image(DataRow row)
        {
            this.IdImage = (int)row["idSP"];
            this.BitmapImage = (BitmapImage)row["Hinh"];
        }
    }
}
