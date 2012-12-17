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
            Attempt2();            
        }

        private void Attempt2()
        {
            const int width = 200;
            WriteableBitmap bitmap = BitmapFactory.New(width, 2 << 8);
            imageControl.Source = bitmap;

            using (bitmap.GetBitmapContext())
            {
                bitmap.Clear(Colors.White);

                for (int i = 0; i < width; i++)
                {
                    int x = i;
                    int y = 10;

                    bitmap.SetPixel(x, y, Colors.Black);
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
