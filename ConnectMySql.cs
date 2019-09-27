using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Drawing;
using System.Data;

using nsStockManage;
using nsMainWindow;

namespace nsDBConnection
{
    public class DBConnection
    {
        public MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        public DBConnection()
        {
            InitializeMySql();
        }

        //Initialize MySQL connection
        private void InitializeMySql()
        {
            server = "localhost";
            database = "toolmanage";
            uid = "jiang";
            password = "1234";

            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            connection = new MySqlConnection(connectionString);
        }

        //open connection to database
        public bool Open()
        {
            try
            {
                connection.Open();
                MainWindow.mySqlConnectionState = true;
                Program.mw.statusStripStatusLabel3.BackColor = Color.GreenYellow;
                //MessageBox.Show("数据库连接成功");
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                Program.mw.statusStripStatusLabel3.BackColor = Color.Red;
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again");
                        break;

                    default:
                        MessageBox.Show(ex.Message);
                        break;
                }
                return false;
            }
        }

        //Close connection
        public bool Close()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        //Insert statement
        public bool Insert(String str)
        {
            string query = str;// "INSERT INTO tableinfo (id,name, age) VALUES('11','John Smith', '33')";
            //open connection
            if (this.Open() == true)
            {
                try
                {
                    //create command and assign the query and connection from the constructor
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    //Execute command
                    cmd.ExecuteNonQuery();
                    //close connection
                    this.Close();
                    return true;
                }
                catch(MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                    return false;
                }
                
            }
            else
            {
                Program.mw.statusStripStatusLabel3.BackColor = Color.Red;
                MessageBox.Show("数据库连接失败!");
                return false;
            }
        }

        //Update statement
        public void Update(String str)
        {
            string query = str;// "UPDATE tableinfo SET id='22', name='Joe', age='22' WHERE name='John Smith'";

            //Open connection
            if (this.Open() == true)
            {
                //create mysql command
                MySqlCommand cmd = new MySqlCommand();
                //Assign the query using CommandText
                cmd.CommandText = query;
                //Assign the connection using Connection
                cmd.Connection = connection;

                //Execute query
                cmd.ExecuteNonQuery();

                //close connection
                this.Close();
            }
            else
            {
                Program.mw.statusStripStatusLabel3.BackColor = Color.Red;
                MessageBox.Show("数据库连接失败!");
            }
        }

        //Delete statement
        public void Delete(String str)
        {
            string query = str;// "DELETE FROM tableinfo WHERE id=" + id;

            if (this.Open() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.Close();
            }
            else
            {
                Program.mw.statusStripStatusLabel3.BackColor = Color.Red;
                MessageBox.Show("数据库连接失败!");
            }
        }

        //Select statement
        public DataSet Select(String str)
        {
            string query = str;
            MySqlDataAdapter mda = null;
            DataSet ds = new DataSet();

            //Open connection
            if (this.Open() == true)
            {
                //Create Command
                mda = new MySqlDataAdapter(query, connection);
                mda.Fill(ds);
                
                //close Connection
                this.Close();

                //return list to be displayed
                return ds;
            }
            else
            {
                Program.mw.statusStripStatusLabel3.BackColor = Color.Red;
                MessageBox.Show("数据库连接失败!");
                return ds;
            }
        }

        //Count statement
        public int Count(String str)
        {
            string query = str;// "SELECT Count(*) FROM tableinfo";
            int count = -1;

            //Open Connection
            if (this.Open() == true)
            {
                //Create Mysql Command
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //ExecuteScalar will return one value
                count = int.Parse(cmd.ExecuteScalar() + "");

                //close Connection
                this.Close();

                return count;
            }
            else
            {
                Program.mw.statusStripStatusLabel3.BackColor = Color.Red;
                MessageBox.Show("数据库连接失败!");
                return count;
            }
        }

    }
}

