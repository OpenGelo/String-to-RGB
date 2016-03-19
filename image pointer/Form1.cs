using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Windows.Forms;
using System.Globalization;
using System.IO;
using imageprocess;
using System.Windows.Forms;


namespace image_pointer
{
    public partial class Form1 : Form
    {
        Imageprocessing p;
        public Form1()
        {
            InitializeComponent();
            p = new Imageprocessing();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            pictureBox1.ImageLocation = openFileDialog1.FileName;
            Bitmap a = new Bitmap(pictureBox1.ImageLocation);
           // get(a);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //p.Image = pictureBox1.Image;
            //p.GrayscaleM();
            //p._displayWindow = richTextBox1;
            double nilai = 0;
            /*try
            {
                nilai = Convert.ToDouble(textBox1.Text); //range 0.5 - 25
                if (textBox1.Text == "")
                {
                    MessageBox.Show("Nilai kosong!!", "Important message", MessageBoxButtons.OK);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Nilai kosong!!", "Important message", MessageBoxButtons.OK);
            }*/
            Imageprocessing pkt = new Imageprocessing();
            // Mengambil citra dari pictureBox1
            Bitmap resizegmbr = new Bitmap (resizeImage(pictureBox1.Image, new Size (160,120)));
            Bitmap gambar = new Bitmap(resizegmbr);
            
            // Memanggil method CitraPangkat dari object pkt
            pkt.CitraPangkat(gambar, nilai);
            // menampilkan hasil pada pictureBox2
            pictureBox2.Image = gambar;

            try
            {
                //byte[] x = imageToByteArray(pictureBox1.Image);
                byte[] x = BmpToArray(new Bitmap(pictureBox1.Image));
                //byte[] x = BmpToArray(gambar, 200, 200);
                /*string value = ASCIIEncoding.ASCII.GetString(x);
                richTextBox1.AppendText(value);*/
                string hex = BitConverter.ToString(x).Replace("-", string.Empty);
                string l = Convert.ToString(hex.Length);
                //string hex = getstring(x);
                //string hex = BitConverter.ToString(x);
                //string hex = Encoding.Default.GetString(x);
                richTextBox1.AppendText(hex);
                label1.Text = l;
            }
            catch
            {
                MessageBox.Show("gagal");
            }

        }

        public static Image resizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }

       /* public bool getandsend(Bitmap a, int frame)
        {
            int idframe;
            byte* linedata = a.lo;
            for (idframe = 0; (a.Width - 1) * 3 ; idframe++)
            {
                richTextBox1.AppendText(char(linedata[idframe]));
            }
        }*/

       /* public bool get(Bitmap b)
        {
            BitmapData data = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, b.PixelFormat);
            byte* ptr = (byte*)data.Scan0;
            for (int j = 0; j < 200; j++)
            {
                byte* scanPtr = ptr + (j * data.Stride);
                for(int i = 0; i < data.Width; int++, scanPtr+= 
                
            return true;
            
        }*/

        public unsafe byte[] tes(Bitmap c)
        {
            BitmapData data = c.LockBits(new Rectangle(0, 0, c.Width, c.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            int bytecount = data.Stride * c.Height;
            byte[] bmpbyte = new byte[bytecount];
            System.Runtime.InteropServices.Marshal.Copy(data.Scan0, bmpbyte, 0, bytecount);
            c.UnlockBits(data);

            return bmpbyte;
        }

        public static Byte[] BmpToArray(Bitmap value, int awidth, int aheight)
        {
            //value;

            //value.Width = awidth;
            BitmapData data = value.LockBits(new Rectangle(0, 0, awidth, aheight), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);

            try
            {
                IntPtr ptr = data.Scan0;
                int bytes = Math.Abs(data.Stride) * value.Height;
                byte[] rgbValues = new byte[bytes];
                Marshal.Copy(ptr, rgbValues, 0, bytes);
                return rgbValues;
            }
            finally
            {
                value.UnlockBits(data);
            }
        }

        public static Byte[] BmpToArray(Bitmap value)
        {
            BitmapData data = value.LockBits(new Rectangle(0, 0, value.Width, value.Height), ImageLockMode.ReadOnly, value.PixelFormat);

            try
            {
                IntPtr ptr = data.Scan0;
                int bytes = Math.Abs(data.Stride) * value.Height;
                byte[] rgbValues = new byte[bytes];
                Marshal.Copy(ptr, rgbValues, 0, bytes);
                return rgbValues;
            }
            finally
            {
                value.UnlockBits(data);
            }
        }
        public unsafe Image Arraytobmp(byte[] tes)
        {
            Bitmap z = new Bitmap(pictureBox1.Image);
            BitmapData data = z.LockBits(new Rectangle(0, 0, z.Width, z.Height), ImageLockMode.ReadWrite, z.PixelFormat);

            try
            {
                IntPtr ptr = data.Scan0;
                int bytes = Math.Abs(data.Stride) * z.Height;
                byte[] rgbValues = new byte[bytes];
                //Marshal.Copy(ptr, rgbValues, 0, bytes);
                Marshal.Copy(tes, 0, ptr, bytes);
                return z;
            }
            finally
            {
                z.UnlockBits(data);
            }
        }

        public byte[] imageToByteArray(System.Drawing.Image imageIn)
		{
			MemoryStream ms = new MemoryStream();
			imageIn.Save(ms,System.Drawing.Imaging.ImageFormat.Jpeg);
			return  ms.ToArray();
		}

        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        string datagmbr;
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string g = richTextBox1.Text;
                /*string[] dataloop = Regex.Split(g, @"\t|\n|\r");
                for( int i=0; i<=216;i++)
                {
                     string[] tes = Regex.Split(dataloop[i], @"\s");
                     string gambar = tes[0];
                     if (gambar.Length == 117)
                     {
                        //datagmbr = gambar.Substring(5, gambar.Length - 5);
                        datagmbr = gambar.Substring(0, gambar.Length);
                        richTextBox2.AppendText(datagmbr);
                     }
                     else
                     {
                        datagmbr = gambar.Substring(5, gambar.Length - 5);
                        richTextBox2.AppendText(datagmbr);
                     }
                }
                
                //byte[] v = stringtobyte(richTextBox2.Text); 
                */
                string[] tes = Regex.Split(g, @"\s");
                string replacement = Regex.Replace(g, @"\t|\n|\r|\s", "");
                byte[] v = stringtobyte(replacement);
               // string p = BitConverter.ToString(v).Replace("-", string.Empty);
                richTextBox2.AppendText(replacement);
                
                //richTextBox2.AppendText(datagmbr);
                //string replacement = g.Substring(0,5);
                //string replacement = Regex.Replace(richTextBox2.Text, @"\t|\n|\r", "");
                
                //byte[] v = stringtobyte(richTextBox2.Text); 
                //byte[] v = stringtobyte(tes);
                //uint r = uint.Parse(g, System.Globalization.NumberStyles.HexNumber);
                //byte[] n = getbytes(richTextBox1.Text);
                //byte[] t = BitConverter.GetBytes(r); 
                //byte[] v = Encoding.Default.GetBytes(richTextBox1.Text);
                //byte[] n = Convert.ToByte( 
                //string p = Encoding.Default.GetString(n);
                //richTextBox2.AppendText(tes);
                //string l = Convert.ToString(replacement.Length);
                //label2.Text = l;
                pictureBox3.Image = Arraytobmp(v);
                //pictureBox3.Image = byteArrayToImage(v);
                
            }
            catch
            {
                MessageBox.Show("gagal");
            }
        }

        static byte[] getbytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        static string getstring(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }

        private byte[] stringtobyte(string strinput)
        {
            int i = 0;
            int x = 0;
            byte[] bytes = new byte[(strinput.Length) / 2];
            while (strinput.Length > i + 1)
            {
                long lgndecimal = Convert.ToInt32(strinput.Substring(i, 2), 16);
                bytes[x] = Convert.ToByte(lgndecimal);
                i=i + 2;
                ++x;
            }
            return bytes;
        }
        /*
        public static void getRGB(this Bitmap image, int startX, int startY, int w, int h, int[] rgbArray, int offset, int scansize)
        {
            const int PixelWidth = 3;
            const PixelFormat PixelFormat = PixelFormat.Format24bppRgb;

            // En garde!
            if (image == null) throw new ArgumentNullException("image");
            if (rgbArray == null) throw new ArgumentNullException("rgbArray");
            if (startX < 0 || startX + w > image.Width) throw new ArgumentOutOfRangeException("startX");
            if (startY < 0 || startY + h > image.Height) throw new ArgumentOutOfRangeException("startY");
            if (w < 0 || w > scansize || w > image.Width) throw new ArgumentOutOfRangeException("w");
            if (h < 0 || (rgbArray.Length < offset + h * scansize) || h > image.Height) throw new ArgumentOutOfRangeException("h");

            BitmapData data = image.LockBits(new Rectangle(startX, startY, w, h), System.Drawing.Imaging.ImageLockMode.ReadOnly, PixelFormat);
            try
            {
                byte[] pixelData = new Byte[data.Stride];
                for (int scanline = 0; scanline < data.Height; scanline++)
                {
                    Marshal.Copy(data.Scan0 + (scanline * data.Stride), pixelData, 0, data.Stride);
                    for (int pixeloffset = 0; pixeloffset < data.Width; pixeloffset++)
                    {
                        // PixelFormat.Format32bppRgb means the data is stored
                        // in memory as BGR. We want RGB, so we must do some 
                        // bit-shuffling.
                        rgbArray[offset + (scanline * scansize) + pixeloffset] =
                            (pixelData[pixeloffset * PixelWidth + 2] << 16) +   // R 
                            (pixelData[pixeloffset * PixelWidth + 1] << 8) +    // G
                            pixelData[pixeloffset * PixelWidth];                // B
                    }
                }
            }
            finally
            {
                image.UnlockBits(data);
            }
        }
        */
        private void button4_Click(object sender, EventArgs e)
        {
            label1.Text = Convert.ToString(richTextBox1.TextLength);
            textBox1.Text = richTextBox1.Text.Substring(5, 112);
        }
    }
}
