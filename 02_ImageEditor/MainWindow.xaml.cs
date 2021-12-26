using Microsoft.Win32;
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

namespace _02_ImageEditor
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (inkCanvas != null)
            {
                inkCanvas.EditingMode = InkCanvasEditingMode.Ink;
            }
        }
        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            if (inkCanvas != null)
            {
                inkCanvas.EditingMode = InkCanvasEditingMode.EraseByPoint;
            }
        }
        private void RadioButton_Checked_6(object sender, RoutedEventArgs e)
        {
            inkCanvas.EditingMode = InkCanvasEditingMode.Select;
        }

        private void RadioButton_Checked_2(object sender, RoutedEventArgs e)
        {
            if (inkCanvas != null)
            {
                inkCanvas.DefaultDrawingAttributes.Color = Colors.Black;
            }
        }

        private void RadioButton_Checked_3(object sender, RoutedEventArgs e)
        {
            if (inkCanvas != null)
            {
                inkCanvas.DefaultDrawingAttributes.Color = Colors.Red;
            }
        }

        private void RadioButton_Checked_4(object sender, RoutedEventArgs e)
        {
            if (inkCanvas != null)
            {
                inkCanvas.DefaultDrawingAttributes.Color = Colors.Green;
            }
        }

        private void RadioButton_Checked_5(object sender, RoutedEventArgs e)
        {
            if (inkCanvas != null)
            {
                inkCanvas.DefaultDrawingAttributes.Color = Colors.Blue;
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int stylusWeigth = Convert.ToInt32(((sender as ComboBox).SelectedItem as TextBlock).Text);
            if (inkCanvas != null)
            {
                inkCanvas.DefaultDrawingAttributes.Width = stylusWeigth;
                inkCanvas.DefaultDrawingAttributes.Height = stylusWeigth;
            }
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            inkCanvas.Strokes.Clear();
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            TextBox textBox = new TextBox
            {
                Width = 100,
                Height = 50,
                BorderBrush = new SolidColorBrush(Colors.Transparent)
            };
            inkCanvas.Children.Add(textBox);
            textBox.Focus();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Файлы изображений(*.jpg;*.jpeg,*.png,*.bmp)|*.jpg;*.jpeg,*.png,*.bmp|Все файлы(*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {

            }
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Файлы изображений(*.bmp)|*.bmp|Все файлы(*.*)|*.*";
            if (saveFileDialog.ShowDialog() == true)
            {
                int margin = (int)inkCanvas.Margin.Left;
                int width = (int)inkCanvas.ActualWidth - margin;
                int height = (int)inkCanvas.ActualHeight - margin;
                RenderTargetBitmap bitmap = new RenderTargetBitmap(width, height, 96d, 96d, PixelFormats.Default);
                bitmap.Render(inkCanvas);
                string filename = saveFileDialog.FileName;
                FileStream fileStream = new FileStream(filename, FileMode.Create);
                using (fileStream)
                {
                    BmpBitmapEncoder encoder = new BmpBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(bitmap));
                    encoder.Save(fileStream);
                }
            }
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
