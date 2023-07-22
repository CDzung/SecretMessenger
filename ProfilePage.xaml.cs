using Firebase.Auth;
using Firebase.Auth.UI;
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

namespace SecretMessage
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        public ProfilePage()
        {
            InitializeComponent();

            FirebaseUI.Instance.Client.AuthStateChanged += AuthStateChanged;
        }

        private void AuthStateChanged(object sender, UserEventArgs e)
        {
            var user = e.User;

            Application.Current.Dispatcher.Invoke(() =>
            {
                this.UidTextBlock.Text = user.Uid;
                this.NameTextBlock.Text = user.Info.DisplayName;
                this.EmailTextBlock.Text = user.Info.Email;
                this.ProviderTextBlock.Text = user.Credential.ProviderType.ToString();

                if (!string.IsNullOrWhiteSpace(user.Info.PhotoUrl))
                {
                    this.ProfileImage.Source = new BitmapImage(new Uri(user.Info.PhotoUrl));
                }
            });
        }

        private void SignOutClick(object sender, RoutedEventArgs e)
        {
            FirebaseUI.Instance.Client.AuthStateChanged -= this.AuthStateChanged;
            FirebaseUI.Instance.Client.SignOut();
            
        }
    }
}
