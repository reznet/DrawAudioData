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
            Attempt3();         
        }

        private void Attempt3()
        {
            byte[] data = File.ReadAllBytes(@"C:\Users\Jeff\Documents\audio files\sine 1hz 1sec signed 16bit pcm.wav");

            var shorts = GetShorts(data, 44);

            WriteableBitmap bitmap = BitmapFactory.New(shorts.Length, 1024);
            imageControl.Source = bitmap;

            using (var context = bitmap.GetBitmapContext())
            {
                bitmap.Clear(Colors.White);

                int maxY = 0;

                for (int i = 0; i < shorts.Length; i++)
                {
                    int x = i;
                    int y = shorts[i] >> 6;

                    maxY = (int)Math.Max(y, maxY);

                    bitmap.SetPixel(x, y, Colors.Black);
                }

                unsafe
                {
                    for (int i = 0; i < shorts.Length; i++)
                    {
                        int x = i;
                        int y = i % 1024;
                        //bitmap.SetPixel(x, x % 1024, Colors.Red);

                        Color color = Colors.Red;

                        var a = color.A + 1;
                        var col = (color.A << 24)
                                 | ((byte)((color.R * a) >> 8) << 16)
                                 | ((byte)((color.G * a) >> 8) << 8)
                                 | ((byte)((color.B * a) >> 8));

                        context.Pixels[y * context.Width + i] = col;
                    }
                }

                Console.WriteLine("max Y:{0}", maxY);
            }

            
        }

        UInt16[] GetShorts(byte[] bytes, int offset)
        {
            int numberBytesToCopy = (bytes.Length - offset) / 2;

            UInt16[] shorts = new UInt16[numberBytesToCopy];
            for (int i = 0; i < numberBytesToCopy; i++)
            {
                byte low = bytes[offset + (i * 2)];
                byte high = bytes[offset + ((i * 2) + 1)];

                shorts[i] = (UInt16)((high << 8) + low);
            }

            return shorts;
        }

        private void Attempt2()
        {
            const int width = 2 << 16;
            WriteableBitmap bitmap = BitmapFactory.New(width, 2 << 8);
            imageControl.Source = bitmap;

            byte[] data = File.ReadAllBytes(@"C:\Users\Jeff\Documents\audio files\sine 1hz 1sec signed 16bit pcm.wav");

            using (bitmap.GetBitmapContext())
            {
                bitmap.Clear(Colors.White);

                int i = 0;
                int x = 0;
                while(45 + i < data.Length && i < width)
                {
                    //Console.WriteLine("x:{0}", x);
                    int d = data[44 + i];
                    //Console.WriteLine("d:{0}", d);
                    //int y = data[44 + i] << 8 + data[45 + i];
                    int y = data[45 + i];

                    i++;
                    i++;

                    bitmap.SetPixel(x, y, Colors.Black);

                    x++;
                }
            }
        }

        private void Attempt1()
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

                    int y = (high << 8) + low;

                    Console.WriteLine("y:{0}", y);

                    bitmap.SetPixel(x++, y, Colors.Black);
                }

                imageControl.Source = bitmap;
            }
        }
    }
}
