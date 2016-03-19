using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;

namespace imageprocess
{

    public class Imageprocessing
    {
        protected Image _image;
        public RichTextBox _displayWindow;

        public Image Image
        {
            get
            {
                return _image;
            }
            set
            {
                _image = value;
            }
        }

        #region DisplayData
        /// <summary>
        /// method to display the data to & from the port
        /// on the screen
        /// </summary>
        /// <param name="type">MessageType of the message</param>
        /// <param name="msg">Message to display</param>
        [STAThread]
        public void DisplayData(string msg)
        {
            _displayWindow.Invoke(new EventHandler(delegate
            {
                _displayWindow.SelectedText = string.Empty;
                //_displayWindow.SelectionFont = new Font(_displayWindow.SelectionFont, FontStyle.Bold);
                //_displayWindow.SelectionColor = MessageColor[(int)type];          
                _displayWindow.AppendText(msg);
                _displayWindow.ScrollToCaret();
            }));
        }
        #endregion

        public bool CitraPangkat(Bitmap b, double nilai)

        {

            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height),

                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int stride = bmData.Stride; // bytes in a row 3*b.Width

            System.IntPtr Scan0 = bmData.Scan0;

            unsafe

            {

                byte* p = (byte*)(void*)Scan0;

                double sRed, sGreen, sBlue;

                double aRed, aGreen, aBlue;

                byte red, green, blue;

                int nOffset = stride - b.Width * 3;

                for (int y = 0; y < b.Height; ++y)

                {

                    for (int x = 0; x < b.Width; ++x)

                    {

                        red = p[0];

                        green = p[1];

                        blue = p[2];



                        sRed = (double)(red) / 255;

                        sGreen = (double)(green) / 255;

                        sBlue = (double)(blue) / 255;



                        aRed = (double)(Math.Pow(sRed, nilai));

                        aGreen = (double)(Math.Pow(sGreen, nilai));

                        aBlue = (double)(Math.Pow(sBlue, nilai));



                        p[0] = Convert.ToByte(aRed * 255);

                        p[1] = Convert.ToByte(aGreen * 255);

                        p[2] = Convert.ToByte(aBlue * 255);


                         
                        p += 3;

                    }

                    p += nOffset;

                }

            }

            b.UnlockBits(bmData);

            return true;

        }

        //#endregion


        public unsafe Image GrayscaleM()
        {
            Bitmap a = new Bitmap(_image);

            BitmapData adata = a.LockBits(new Rectangle(0, 0, _image.Width, _image.Height), ImageLockMode.ReadWrite, a.PixelFormat);

            
            int r = 0;
            int g = 0;
            int b = 0;

            byte* imgPtr = (byte*)(adata.Scan0);

            a.UnlockBits(adata);
            for (int x = 0; x < a.Width; x++)
            {
                for (int y = 0; y < a.Height; y++)
                {

                    r = (int)*imgPtr;
                    imgPtr++;
                    g = (int)*imgPtr;
                    imgPtr++;
                    b = (int)*imgPtr;
                    imgPtr++;
                    DisplayData(Convert.ToString(r & g & b));
                }
                imgPtr += adata.Stride - a.Height * 3;
            }
            return a;
        }
    }
}
