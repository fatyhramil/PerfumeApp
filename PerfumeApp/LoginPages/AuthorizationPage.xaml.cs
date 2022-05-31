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

namespace PerfumeApp.LoginPages
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Page
    {
        string kaptcha = "";
        string symbols = "1234567890qwertyuiopasdfghjklzxcvbnm";
        Random random = new Random();
        public AuthorizationPage()
        {
            InitializeComponent();
            GenerateKaptcha();
        }

        private async void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            if(LoginTB.Text!="" && PasswordTB.Password != "" && KaptchaTB.Text != "")
            {
                if (KaptchaTB.Text == kaptcha)
                {
                    var user = MainWindow.ent.User.Where(c => c.Login == LoginTB.Text).FirstOrDefault();
                    if (user != null)
                    {
                        if (user.Password == PasswordTB.Password)
                        {
                            NavigationService.Navigate(new PerfumeApp.Pages.PerfumePage(user));
                        }
                        else
                        {
                            MessageBox.Show("Неправильный пароль");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Пользователя не существует");
                    }
                }
                else
                {
                    await KaptchaTimer();
                }
            }
            else
            {
                MessageBox.Show("Введены не все значения");
            }
        }
        private async Task KaptchaTimer()
        {
            MessageBox.Show("Неправильная капча");
            KaptchaTB.IsEnabled = false;
            LoginBtn.IsEnabled = false;
            RefreshKaptcha.IsEnabled = false;
            await Task.Delay(2000);
            KaptchaTB.IsEnabled = true;
            LoginBtn.IsEnabled = true;
            RefreshKaptcha.IsEnabled = true;
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PerfumeApp.Pages.PerfumePage(null));
        }
        private void GenerateKaptcha()
        {
            kaptcha="";
            Symbols.Children.Clear();
            for(int i = 0; i < 4; i++)
            {
                char symbol = symbols.ElementAt(random.Next(0, symbols.Length));
                kaptcha += symbol;
                TextBlock tb = new TextBlock();
                tb.FontSize = random.Next(20, 60);
                tb.Margin = new Thickness(random.Next(10, 20), 0, random.Next(10, 20), 0);
                tb.Text = symbol.ToString();
                tb.TextDecorations = TextDecorations.Strikethrough;
                Symbols.Children.Add(tb);
            }
        }

        private void RefreshKaptcha_Click(object sender, RoutedEventArgs e)
        {
            GenerateKaptcha();
        }
    }
}
