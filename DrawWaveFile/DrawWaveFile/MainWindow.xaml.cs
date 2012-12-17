using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DrawWaveFile
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {

            const int width = 110;

            WriteableBitmap bitmap = BitmapFactory.New(width, 2 << 8);


            using (bitmap.GetBitmapContext())
            {

                bitmap.Clear(Colors.White);

                int x = 0;

                byte[] data = File.ReadAllBytes(@"C:\Users\Jeff\Documents\audio files\sine 1hz 1sec signed 16bit pcm.wav");

                int i = 44;
                while (i + 1 < data.Length && x < width)
                {
                    byte low = data[i++];
                    byte high = data[i++];

                    //Console.WriteLine("low: {0}, high:{1}", low, high);

                    int l = Convert.ToInt32(low);
                    int h = Convert.ToInt32(high);

                    //Console.WriteLine("l: {0}, h:{1}", l, h);

                    int y = (h << 8) + l;

                    Console.WriteLine("y:{0}", y);

                    //y = y >> 8;

                    //y = h;

                    Console.WriteLine(y);

                    bitmap.SetPixel(x++, y, Colors.Black);
                }

                //for (int i = 0; i < data.Length && x < 100; i++)
                //{
                //    byte b = data[i];
                //    bitmap.SetPixel(x++, Convert.ToInt32(b), Colors.Black);
                //}

                imageControl.Source = bitmap;
                //bitmap.AddDirtyRect(new Int32Rect(0, 0, bitmap.PixelWidth, bitmap.PixelHeight));
            }
        }
    }
}
