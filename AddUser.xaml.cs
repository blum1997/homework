
using System.Windows;
using System.Data;

namespace DataBaseClient
{
    /// <summary>
    /// Логика взаимодействия для AddUser.xaml
    /// </summary>
    public partial class AddUser : Window
    {
        public DataRow resultRow;
        public AddUser(DataRow dr)
        {
            InitializeComponent();
            resultRow = dr;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Name.Text = resultRow["Name"].ToString();
            SecondName.Text = resultRow["SecondName"].ToString();
            LastName.Text = resultRow["LastName"].ToString();
        }
        private void cancelbtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void addbtn_Click(object sender, RoutedEventArgs e)
        {
            resultRow["Name"] = Name.Text;
            resultRow["SecondName"] = SecondName.Text;
            resultRow["LastName"] = LastName.Text;
            DialogResult = true;
        }
    }
}
