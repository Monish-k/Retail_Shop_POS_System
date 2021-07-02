using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CSharpCourseProjectPOS
{

    public partial class add_new_user : Form
    {
        SqlConnection sqlConnection = new SqlConnection(@"Data Source=DESKTOP-J7SFGQV; Initial Catalog=POSDB; User ID=monish_db;Password=monimkd072000");

        public add_new_user()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i = 0;

            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.CommandText = "select * from Registration where UserName='" + textBox3.Text + "'";
            sqlCommand.ExecuteNonQuery();
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            sqlDataAdapter.Fill(dataTable);

            i = Convert.ToInt32(dataTable.Rows.Count.ToString());

            if (i == 0)
            {
                if(textBox1.Text.Length==0||textBox2.Text.Length==0||textBox3.Text.Length==0|| textBox4.Text.Length == 0 || textBox5.Text.Length == 0 || textBox6.Text.Length == 0)
                {
                    MessageBox.Show("Please Fill All The Fields");
                }

                else
                {
                    if (textBox4.Text.Length < 8)
                    {
                        MessageBox.Show("Password is not Strong enough");
                    }

                    else
                    {
                        SqlCommand sqlCommand1 = sqlConnection.CreateCommand();
                        sqlCommand1.CommandType = CommandType.Text;
                        sqlCommand1.CommandText = "insert into Registration values('"+textBox1.Text+"','"+ textBox2.Text + "','"+ textBox3.Text + "','"+ textBox4.Text + "','"+ textBox5.Text + "','"+ textBox6.Text + "')";
                        sqlCommand1.ExecuteNonQuery();

                        MessageBox.Show("New User added successfully");

                        textBox1.Text = "";
                        textBox2.Text = "";
                        textBox3.Text = "";
                        textBox4.Text = "";
                        textBox5.Text = "";
                        textBox6.Text = "";

                        display();
                    }
                    
                }
                
            }

            else
            {
                MessageBox.Show("UserName already exist");
            }
        }

        private void add_new_user_Load(object sender, EventArgs e)
        {
            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }

            sqlConnection.Open();
            display();
        }

        public void display()
        {
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.CommandText = "select ID,FirstName,LastName,Username,Email,ContactNo from Registration";
            sqlCommand.ExecuteNonQuery();
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            sqlDataAdapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int id;
            id = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.CommandText = "delete from registration where id = "+id+"";
            sqlCommand.ExecuteNonQuery();

            display();
        }
    }
}
