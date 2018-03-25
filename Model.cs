using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
namespace DataBaseClient
{
    class Model
    {       
        SqlDataAdapter adapterUsers;
        SqlDataAdapter adapterActivity;
        SqlConnection connection;
        string connectionString;


        public Model(DataTable dtU, DataTable dtA)
        {

            #region настройка адаптера для Users

            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            connection = new SqlConnection(connectionString);
            adapterUsers = new SqlDataAdapter();

            
            // select

            SqlCommand command = new SqlCommand(@" SELECT * FROM Users", connection);        
            adapterUsers.SelectCommand = command;

            //insert
            //
            command = new SqlCommand(@"INSERT INTO Users (Name, SecondName, LastName) 
                          VALUES (@Name, @SecondName, @LastName); SET @ID = @@IDENTITY;",
                          connection);

            command.Parameters.Add("@Name", SqlDbType.NVarChar, -1, "Name");
            command.Parameters.Add("@SecondName", SqlDbType.NVarChar, -1, "SecondName");
            command.Parameters.Add("@LastName", SqlDbType.NVarChar, 100, "LastName");
         // command.Parameters.Add("@ID", SqlDbType.Int, 5, "ID");
            SqlParameter param = command.Parameters.Add("@ID", SqlDbType.Int, 0, "ID");
            param.Direction = ParameterDirection.Output;

            adapterUsers.InsertCommand = command;

            // update
            command = new SqlCommand(@"UPDATE Users SET Name = @Name,
SecondName = @SecondName, LastName = @LastName ID = @ID WHERE ID = @ID", connection);

            command.Parameters.Add("@Name", SqlDbType.NVarChar, -1, "Name");
            command.Parameters.Add("@SecondName", SqlDbType.NVarChar, -1, "SecondName");
            command.Parameters.Add("@LastName", SqlDbType.NVarChar, 100, "LastName");
            param = command.Parameters.Add("@ID", SqlDbType.Int, 5, "ID");
            param.SourceVersion = DataRowVersion.Original;

            adapterUsers.UpdateCommand = command;

            //delete
            command = new SqlCommand("DELETE FROM Users WHERE ID = @ID", connection);
            param = command.Parameters.Add("@ID", SqlDbType.Int, 0, "ID");
            param.SourceVersion = DataRowVersion.Original;
            adapterUsers.DeleteCommand = command;

            adapterUsers.Fill(dtU);


            #endregion

            #region настройка адаптера для Activity

            connection = new SqlConnection(connectionString);
            adapterActivity = new SqlDataAdapter();


            // select

            command = new SqlCommand(@" SELECT ID, Address, Type, Description FROM Activity", connection);
            adapterActivity.SelectCommand = command;

            //insert
            command = new SqlCommand(@"INSERT INTO Activity (Address, Type, Description, UserID) 
                          VALUES (@Address, @Type, @Description, @UserID); SET @ID = @@IDENTITY;",
                          connection);

            command.Parameters.Add("@Address", SqlDbType.NVarChar, -1, "Address");
            command.Parameters.Add("@Type", SqlDbType.NVarChar, -1, "Type");
            command.Parameters.Add("@Description", SqlDbType.NVarChar, 100, "Description");
            command.Parameters.Add("@UserID", SqlDbType.Int, 5, "UserID");
            param = command.Parameters.Add("@ID", SqlDbType.Int, 0, "ID");
            param.Direction = ParameterDirection.Output;

            adapterActivity.InsertCommand = command;

            // update
            command = new SqlCommand(@"UPDATE Activity SET Address = @Address,
Type = @Type, Description = @Description ID = @ID WHERE ID = @ID", connection);

            command.Parameters.Add("@Address", SqlDbType.NVarChar, -1, "Address");
            command.Parameters.Add("@Type", SqlDbType.NVarChar, -1, "Type");
            command.Parameters.Add("@Description", SqlDbType.NVarChar, 100, "Description");
            param = command.Parameters.Add("@ID", SqlDbType.Int, 5, "ID");
            param.SourceVersion = DataRowVersion.Original;

            adapterActivity.UpdateCommand = command;

            //delete
            command = new SqlCommand("DELETE FROM Activity WHERE ID = @ID", connection);
            param = command.Parameters.Add("@ID", SqlDbType.Int, 0, "ID");
            param.SourceVersion = DataRowVersion.Original;
            adapterActivity.DeleteCommand = command;

            adapterActivity.Fill(dtA);

            #endregion
        }

        

        internal void DeleteReferences(DataRowView drv, DataTable dtA)
        {
            SqlCommand command = new SqlCommand($@"DELETE FROM Activity Where UserID={drv["ID"]}", connection);
            connection.Open();
            command.ExecuteNonQuery();
            adapterActivity.Fill(dtA);
            connection.Close();
            command = new SqlCommand($@"DELETE FROM Users Where ID={drv["ID"]}", connection);
            adapterUsers.DeleteCommand = command;
        }

        public SqlDataAdapter AdapterActivity { get => adapterActivity; set => adapterActivity = value; }

        public void UpdateActivity(DataTable dt)
        {
            adapterActivity.Update(dt);
        }

        public void UpdateActivity(DataTable dt, int selectedUser)
        {
            SqlCommand command = new SqlCommand($@"INSERT INTO Activity (Address, Type, Description, UserID) 
                          VALUES (@Address, @Type, @Description, {selectedUser}); SET @ID = @@IDENTITY;",
                          connection);

            command.Parameters.Add("@Address", SqlDbType.NVarChar, -1, "Address");
            command.Parameters.Add("@Type", SqlDbType.NVarChar, -1, "Type");
            command.Parameters.Add("@Description", SqlDbType.NVarChar, 100, "Description");
            SqlParameter param = command.Parameters.Add("@ID", SqlDbType.Int, 0, "ID");
            param.Direction = ParameterDirection.Output;

            adapterActivity.InsertCommand = command;

            adapterActivity.Update(dt);
        }

        public void UpdateUsers(DataTable dt)
        {         
            adapterUsers.Update(dt);
        }

        public void UpdatedtgA(DataRowView drv, DataTable dta)
        {
            SqlCommand command = new SqlCommand($@" SELECT ID, Address, Type, Description FROM Activity Where UserID={drv["ID"]}", connection);
            adapterActivity.SelectCommand = command;
            dta.Clear();
            adapterActivity.Fill(dta);
        }

        public void PrintAllIndgA(DataTable dta)
        {
            SqlCommand command = new SqlCommand($@" SELECT ID, Address, Type, Description FROM Activity", connection);
            adapterActivity.SelectCommand = command;
            dta.Clear();
            adapterActivity.Fill(dta);
        }
    }
}
