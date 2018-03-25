
using System.Windows;
using System.Data;

namespace DataBaseClient
{
    /// <summary>
    /// Логика взаимодействия для AddActivity.xaml
    /// </summary>
    public partial class AddActivity : Window
    {
        public DataRow resultRow;

        public AddActivity(DataRow dr)
        {
            InitializeComponent();
            resultRow = dr;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {          
            Address.Text = resultRow["Address"].ToString();
            Type.Text = resultRow["Type"].ToString();
            Description.Text = resultRow["Description"].ToString();
        }
        private void cancelbtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void addbtn_Click(object sender, RoutedEventArgs e)
        {
            resultRow["Address"] = Address.Text;
            resultRow["Type"] = Type.Text;
            resultRow["Description"] = Description.Text;
            DialogResult = true;
        }

    }
}
