
using System.Data;
using System.Windows;
using System;

namespace DataBaseClient
{
    class View_model
    {
        DataTable datatableUser;
        DataTable datatableActivity;
        Model model;

        public DataTable DatatableUser { get => datatableUser; set => datatableUser = value; }
        public DataTable DatatableActivity { get => datatableActivity; set => datatableActivity = value; }

        public View_model()
        {
            datatableUser = new DataTable();
            datatableActivity = new DataTable();
            model = new Model(DatatableUser, DatatableActivity);
        }
        public void AddUser()
        {
            DataRow newRow = datatableUser.NewRow();
            AddUser adduser = new AddUser(newRow);
            adduser.ShowDialog();

            if (adduser.DialogResult.Value)
            {
                try
                {
                    datatableUser.Rows.Add(adduser.resultRow);
                    model.UpdateUsers(DatatableUser);
                }
                catch (System.Data.SqlClient.SqlException e)
                {
                    Console.WriteLine(e.Message);
                    MessageBox.Show("Невозможно добавить запись, проверьте корректность заполнения полей");
                }
            }
        }

        public void ChangeUser(DataRowView datarow)
        {
            DataRowView newRow = datarow;
            newRow.BeginEdit();
            AddUser changeuser = new AddUser(newRow.Row);
            changeuser.ShowDialog();
            if (changeuser.DialogResult.HasValue && changeuser.DialogResult.Value)
            {
                newRow.EndEdit();
                try
                {
                    model.UpdateUsers(DatatableUser);
                }
                catch (System.Data.SqlClient.SqlException e)
                {
                    Console.WriteLine(e.Message);
                    MessageBox.Show("Невозможно добавить запись, проверьте корректность заполнения полей");
                }
            }
            else
            {
                newRow.CancelEdit();
            }           
        }

        internal void DeleteActivity(DataRowView drv)
        {
            drv.Row.Delete();
            model.UpdateActivity(DatatableActivity);
        }

        public void DeleteUser(DataRowView drv)
        {
            model.DeleteReferences(drv, DatatableActivity);
            drv.Row.Delete();
            model.UpdateUsers(datatableUser);
        }

        public void ChangedtgA(DataRowView drv)//обновляем вид dtgA для каждого пользователя
        {
            model.UpdatedtgA(drv, datatableActivity);
        }

        public void PrintAllIndgA()
        {
            model.PrintAllIndgA(datatableActivity);
        }

        public void AddActivity(int selectedUser)
        {

            DataRow newRow = datatableActivity.NewRow();
            AddActivity addactivity = new AddActivity(newRow);
            addactivity.ShowDialog();
            if (addactivity.DialogResult.Value)
            {
                try
                {
                    datatableActivity.Rows.Add(addactivity.resultRow);
                    model.UpdateActivity(DatatableActivity, selectedUser);
                }
                catch (System.Data.SqlClient.SqlException e)
                {
                    Console.WriteLine(e.Message);
                    MessageBox.Show("Невозможно добавить запись, проверьте корректность заполнения полей");
                }
            }
        }
    }
}
