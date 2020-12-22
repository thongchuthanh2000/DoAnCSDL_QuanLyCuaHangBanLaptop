using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System;
using System.Drawing;

namespace DoAnCSDL_QuanLyCuaHangBanLaptop.ViewModel
{
    public class ImageViewModel:BaseViewModel
    {
        public bool Isloaded = false;
        public ICommand ImageCommand { get; set; }

        public  ImageViewModel()
        {

            ImageCommand = new RelayCommand<object>((p) => { return true; }, (p) => {

             
                OpenFileDialog dialog1 = new OpenFileDialog();
           
                if (dialog1.ShowDialog()== true)
                {
                    dialog1.Filter = "txt files (*.txt)|*.txt| DOC files (*.doc)|*.doc";
                    FileStream fs = new FileStream(dialog1.FileName, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);
                    byte[] image = null;
                    image = br.ReadBytes((int)fs.Length);
                    string query = " INSERT INTO HangHoaImage(Image) VALUES(@Imgg)";
                    //int x = DataProvider.Instance.ExecuteNonQuery(query);
                }


            });
       
        }
    }
}
