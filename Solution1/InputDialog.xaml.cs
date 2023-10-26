using System.Windows;

namespace Solution1
{
    public partial class InputDialog : Window
    {
        public string EnteredText { get; private set; }

        public InputDialog(string sType_)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            //сохраняем введенный текст
            EnteredText = textBox.Text;
            DialogResult = true;//устанавливаем DialogResult как true
            Close();
        }
    }
}
