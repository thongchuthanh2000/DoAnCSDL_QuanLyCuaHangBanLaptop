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
        private int _maImage;
        private byte[] _bitmapImage;

        public int MaImage { get => _maImage; set => _maImage = value; }
        public byte[] BitmapImage { get => _bitmapImage; set => _bitmapImage = value; }


        public Image(int maImage, byte[] bitmapImage)
        {
            this.MaImage = maImage;
            this.BitmapImage = bitmapImage;
        }
       
        public Image(DataRow row)
        {
            this.MaImage = (int)row["MaSP"];
            this.BitmapImage = (byte[])row["Hinh"];
        }
    }
}
