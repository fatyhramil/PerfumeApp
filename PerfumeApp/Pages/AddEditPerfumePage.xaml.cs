using Microsoft.Win32;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using PerfumeApp.Models;
using PerfumeApp.Pages;

namespace PerfumeApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditPerfumePage.xaml
    /// </summary>
    public partial class AddEditPerfumePage : Page
    {
        Perfume localPerfume;
        bool isEdit;
        public AddEditPerfumePage(Perfume perfume, bool IsEdit)
        {
            InitializeComponent();
            isEdit = IsEdit;
            localPerfume = perfume;
            DataContext = localPerfume;
            PerfumeTypeTB.ItemsSource = MainWindow.ent.PerfumeType.ToList();
        }

        private void AddImageBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if(dialog.ShowDialog() == true)
            {
                PerfumeImage.Source=new BitmapImage(new Uri (dialog.FileName));
                localPerfume.PhotoName = dialog.FileName;
            }
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Deletebtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
