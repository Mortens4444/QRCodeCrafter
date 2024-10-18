namespace QuickResponseCodeCrafter
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        
        private void GenerateQRCodeBtn_Clicked(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(TextInput.Text))
            {
                var destinationFile = Path.Combine(AppContext.BaseDirectory, "QRCode.png");
                CodeCrafter.CreateQuickResponseCode(TextInput.Text, destinationFile, QRCodeImage);
            }
        }

        private void GenerateWiFiQRCodeBtn_Clicked(object sender, EventArgs e)
        {
            TextInput.Text = "WIFI:T:WPA2;S:NetworkName;P:NetworkPassword;;";
        }
    }
}