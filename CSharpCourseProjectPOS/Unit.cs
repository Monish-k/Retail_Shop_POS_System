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
    public partial class Unit : Form
    {
        SqlConnection sqlConnection = new SqlConnection(@"Data Source=DESKTOP-J7SFGQV; Initial Catalog=POSDB; User ID=monish_db;Password=monimkd072000");

        public Unit()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i = 0;

            SqlCommand sqlCommand1 = sqlConnection.CreateCommand();
            sqlCommand1.CommandType = CommandType.Text;
            sqlCommand1.CommandText = "select * from Units where Unit = '"+textBox1.Text+"'";
            sqlCommand1.ExecuteNonQuery();
            DataTable dataTable1 = new DataTable();
            SqlDataAdapter sqlDataAdapter1 = new SqlDataAdapter(sqlCommand1);
            sqlDataAdapter1.Fill(dataTable1);

            i = Convert.ToInt32(dataTable1.Rows.Count.ToString());

            if (i == 0)
            {
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText = "insert into Units values('" + textBox1.Text + "')";
                sqlCommand.ExecuteNonQuery();

                display();
            }

            else
            {
                MessageBox.Show("Value already exists");
            }
        }

        private void Unit_Load(object sender, EventArgs e)
        {
            if(sqlConnection.State == ConnectionState.Open)
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
            sqlCommand.CommandText = "select * from Units";
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
            sqlCommand.CommandText = "delete from Units where id = " + id + "";
            sqlCommand.ExecuteNonQuery();

            display();
        }
    }
}
