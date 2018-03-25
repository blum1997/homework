using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DataBaseClient
{
    /// <summary>
    /// Логика взаимодействия для AdminPanel.xaml
    /// </summary>
    public partial class AdminPanel : Window
    {
        View_model viewmodel;
        public AdminPanel()
        {
            InitializeComponent();
            viewmodel = new View_model();
            dgUsers.DataContext = viewmodel.DatatableUser.DefaultView;
            dgUserActivity.DataContext = viewmodel.DatatableActivity.DefaultView;
        }

        private void Addbtn_Click(object sender, RoutedEventArgs e)
        {
           viewmodel.AddUser();
        }

        private void Deletebtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                viewmodel.DeleteUser((DataRowView)dgUsers.SelectedItem);
            }   
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        private void Changebtn_Click(object sender, RoutedEventArgs e)
        {
            DataRowView dr = (DataRowView)dgUsers.SelectedItem;
            viewmodel.ChangeUser(dr);
        }

        private void dgUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((DataRowView)dgUsers.SelectedItem != null)
            {
                AddActivity_btn.IsEnabled = true;
                viewmodel.ChangedtgA((DataRowView)dgUsers.SelectedItem);
            }
        }

        /// <summary>
        /// Возвращает все записи из таблицы Activity в dtgActivity
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Allbtn_Click(object sender, RoutedEventArgs e)
        {
            viewmodel.PrintAllIndgA();
        }

        private void DeleteActivity_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                viewmodel.DeleteActivity((DataRowView)dgUserActivity.SelectedItem);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        private void AddActivity_btn_Click(object sender, RoutedEventArgs e)
        {
            if ((DataRowView)dgUsers.SelectedItem != null)
            {
                DataRowView selectedUser = (DataRowView)dgUsers.SelectedItem;
                viewmodel.AddActivity(Convert.ToInt32(selectedUser["ID"]));
            }
        }

    }
}
