using Windows.Storage;
using Windows.UI.Xaml.Controls;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238
using HarvestApp.Core;

namespace HarvestApp.WinStore
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public static ApplicationDataContainer SettingsContainer { get; set; }

        public static string Subdomain
        {
            get { return SettingsContainer.Values["Subdomain"] as string; }
            set { SettingsContainer.Values["Subdomain"] = value; }
        }

        public static string Username
        {
            get { return SettingsContainer.Values["Username"] as string; }
            set { SettingsContainer.Values["Username"] = value; }
        }
        public static string Password
        {
            get { return SettingsContainer.Values["Password"] as string; }
            set { SettingsContainer.Values["Password"] = value; }
        }

        static MainPage()
        {
            SettingsContainer = ApplicationData.Current.LocalSettings;
        }

        public MainPage()
        {
            this.InitializeComponent();
            SubdomainBox.Text = Subdomain ?? string.Empty;
            UsernameBox.Text = Username ?? string.Empty;
            PasswordBox.Password = Password ?? string.Empty;
        }

        private async void Get_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Subdomain = SubdomainBox.Text;
            Username = UsernameBox.Text;
            Password = PasswordBox.Password;

            var response = await HarvestApi.Get<Hash>("https://"+SubdomainBox.Text+".harvestapp.com/account/who_am_i", UsernameBox.Text, PasswordBox.Password);
            OutputBox.DataContext = response;
        }
    }
}
