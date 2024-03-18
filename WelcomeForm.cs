using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace MiSPIC
{
    public partial class WelcomeForm : Form
    {
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\FrogmoneyCasinoDatabase.mdf;Integrated Security=True";
        string checkUserAndPasswordPresenceCommand = "SELECT* FROM UserList WHERE Name = 'admin' AND Password = 'admin';";
        string addIdNamePassordAccessInTableCommand = "INSERT INTO UserList (Id, Name, Password, AccessLevel) VALUES (@Id, @Name, @Password, @AccessLevel)";

        /// 

        public WelcomeForm()
        {
            InitializeComponent();
        }

        private void AdminPanelButton_Click(object sender, EventArgs e)
        {
            AdminUsersPanelForm adminPanelForm = new AdminUsersPanelForm();
            adminPanelForm.Show();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            
            MessageBox.Show(CheckUserPresence().ToString());
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(addIdNamePassordAccessInTableCommand, connection))
                {
                    command.Parameters.AddWithValue("@Id", int.Parse(ReturnLastIdOfTable("UserList")) + 1).ToString();
                    command.Parameters.AddWithValue("@Name", "Dralis");
                    command.Parameters.AddWithValue("@Password", "paroldota");
                    command.Parameters.AddWithValue("@AccessLevel", "ludik");
                    command.ExecuteNonQuery();
                }
            }
        }

        /// 

        public int CheckUserPresence()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(checkUserAndPasswordPresenceCommand, connection))
                {
                    command.Parameters.AddWithValue("@Name", "admin");
                    command.Parameters.AddWithValue("@Password", "admin");

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            return 1;
                        }
                        else
                        {
                            return 2;
                        }
                    }
                    command.ExecuteNonQuery();
                }
            }
        }

        public string ReturnLastIdOfTable(string tableName)
        {
            string lastIndexCommand = $"SELECT TOP 1 id FROM {tableName} ORDER BY id DESC;";
            string commandResult;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(lastIndexCommand, connection))
                {
                    commandResult = command.ExecuteScalar().ToString();
                }
            }
            return commandResult;
        }
    }
}
