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
using PerfumeApp.LoginPages;

namespace PerfumeApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для PerfumePage.xaml
    /// </summary>
    public partial class PerfumePage : Page
    {
        List<Perfume> perfumes = new List<Perfume>();
        List<PerfumeType> perfumeTypes = new List<PerfumeType>();
        User localUser;

        public PerfumePage(User user)
        {
            InitializeComponent();
            perfumes = MainWindow.ent.Perfume.ToList();

            perfumeTypes=MainWindow.ent.PerfumeType.ToList();
            perfumeTypes.Insert(0, new PerfumeType() { TypeName = "Все типы" });
            PerfumeList.ItemsSource = perfumes;
            FilterCB.ItemsSource = perfumeTypes;
            localUser = user;
            if (localUser != null)
            {
                if (user.RoleID == 1)
                {
                    AddPerfumeBtn.Height = 0;
                }
                else
                {
                    AddPerfumeBtn.Height = 30;
                }
            }
            else
            {
                AddPerfumeBtn.Height = 0;
            }
        }

        private void PerfumeList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (localUser != null&& localUser.RoleID!=1)
            {
                var perfume =PerfumeList.SelectedItem as Perfume;
                if (perfume != null)
                {
                    NavigationService.Navigate(new AddEditPerfumePage(perfume, true));
                }
            }
            
        }

        private void SearchNameTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private void SortCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SortCB.SelectedIndex != -1)
            {
                string sortText = (SortCB.SelectedItem as ComboBoxItem).Content as string;
                if(sortText != null && sortText != "")
                {
                    if(sortText =="От А до Я")
                    {
                        PerfumeList.ItemsSource = perfumes.OrderBy(c => c.PerfumeName).ToList();
                    }
                    if(sortText=="От Я до А")
                    {
                        PerfumeList.ItemsSource = perfumes.OrderByDescending(c => c.PerfumeName).ToList();
                    }
                }
            }
        }

        private void FilterCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void AuthorizationBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthorizationPage());
        }
        private void Filter()
        {
            List<Perfume> perfumes = MainWindow.ent.Perfume.ToList();
            if (SearchNameTB.Text != "")
            {
                perfumes = perfumes.Where(c => c.PerfumeName.StartsWith(SearchNameTB.Text)).ToList();
            }
            if (FilterCB.SelectedIndex != -1)
            {
                var perfumeType=FilterCB.SelectedItem as PerfumeType;
                if(perfumeType.TypeName!="Все типы")
                {
                    perfumes = perfumes.Where(c => c.PerfumeType == perfumeType).ToList();
                }
            }
            PerfumeList.ItemsSource = perfumes;
        }
    }
}
