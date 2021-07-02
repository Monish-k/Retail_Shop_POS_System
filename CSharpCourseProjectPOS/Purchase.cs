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
    public partial class Purchase : Form
    {
        SqlConnection sqlConnection = new SqlConnection(@"Data Source=DESKTOP-J7SFGQV; Initial Catalog=POSDB; User ID=monish_db; Password=monimkd072000");

        public Purchase()
        {
            InitializeComponent();
        }

        private void Purchase_Load(object sender, EventArgs e)
        {
            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }

            sqlConnection.Open();
            fillcomboBox1();
            fillcomboBox2();
        }

        public void fillcomboBox1()
        {
            comboBox1.Items.Clear();
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.CommandText = "Select * From ProductNames";
            sqlCommand.ExecuteNonQuery();

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();

            sqlDataAdapter.Fill(dataTable);

            foreach(DataRow dr in dataTable.Rows)
            {
                comboBox1.Items.Add(dr["ProductName"].ToString());
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.CommandText = "Select * From ProductNames where ProductName='"+comboBox1.SelectedItem.ToString()+"'";
            sqlCommand.ExecuteNonQuery();

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();

            sqlDataAdapter.Fill(dataTable);

            foreach (DataRow dr in dataTable.Rows)
            {
                label3.Text = dr["Units"].ToString();
            }
        }

        public void fillcomboBox2()
        {
            comboBox2.Items.Clear();
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.CommandText = "Select * From DealerInformation";
            sqlCommand.ExecuteNonQuery();

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();

            sqlDataAdapter.Fill(dataTable);

            foreach (DataRow dr in dataTable.Rows)
            {
                comboBox2.Items.Add(dr["DealerName"].ToString());
            }

        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            textBox3.Text = (Convert.ToDecimal(textBox2.Text)*Convert.ToDecimal(textBox1.Text)).ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i;


            SqlCommand sqlCommand1 = sqlConnection.CreateCommand();
            sqlCommand1.CommandType = CommandType.Text;
            sqlCommand1.CommandText = "Select * From Stocks where ProductName='" + comboBox1.SelectedItem.ToString() + "'";
            sqlCommand1.ExecuteNonQuery();

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand1);
            DataTable dataTable = new DataTable();

            sqlDataAdapter.Fill(dataTable);

            i = Convert.ToInt32(dataTable.Rows.Count.ToString());

            if (i == 0)
            {
                SqlCommand sqlCommand2 = sqlConnection.CreateCommand();
                sqlCommand2.CommandType = CommandType.Text;
                sqlCommand2.CommandText = "insert into Stocks values('" + comboBox1.SelectedItem.ToString() + "','" + textBox1.Text + "','" + label3.Text + "')";
                sqlCommand2.ExecuteNonQuery();
                MessageBox.Show("Record Inserted Successfully");

                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText = "insert into Purchases values('" + comboBox1.SelectedItem.ToString() + "','" + textBox1.Text + "','" + label3.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + dateTimePicker1.Value.ToString("dd-mm-yyyy") + "','" + comboBox2.SelectedItem.ToString() + "','" + comboBox3.SelectedItem.ToString() + "','" + dateTimePicker2.Value.ToString("dd-mm-yyyy") + "','" + textBox4.Text + "')";
                sqlCommand.ExecuteNonQuery();
                MessageBox.Show("Record Inserted Successfully");

            }

            else
            {

                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText = "update Stocks set Quantity = Quantity + " + textBox1.Text + " where ProductName = '" + comboBox1.Text + "'";
                sqlCommand.ExecuteNonQuery();
                MessageBox.Show("Record Inserted Successfully");

                SqlCommand sqlCommand2 = sqlConnection.CreateCommand();
                sqlCommand2.CommandType = CommandType.Text;
                sqlCommand2.CommandText = "insert into Purchases values('" + comboBox1.SelectedItem.ToString() + "','" + textBox1.Text + "','" + label3.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + dateTimePicker1.Value.ToString("dd-mm-yyyy") + "','" + comboBox2.SelectedItem.ToString() + "','" + comboBox3.SelectedItem.ToString() + "','" + dateTimePicker2.Value.ToString("dd-mm-yyyy") + "','" + textBox4.Text + "')";
                sqlCommand2.ExecuteNonQuery();
                MessageBox.Show("Record Inserted Successfully");

            }
            
        }
    }
}
