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
    public partial class Sales : Form
    {
        SqlConnection sqlConnection = new SqlConnection(@"Data Source=DESKTOP-J7SFGQV; Initial Catalog=POSDB; User ID=monish_db; Password=monimkd072000");
        DataTable dataTable1 = new DataTable();
        Decimal total = 0;

        public Sales()
        {
            InitializeComponent();
        }

        private void Sales_Load(object sender, EventArgs e)
        {
            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }
            sqlConnection.Open();
            dataTable1.Clear();
            dataTable1.Columns.Add("Product");
            dataTable1.Columns.Add("Price");
            dataTable1.Columns.Add("Quantity");
            dataTable1.Columns.Add("Total");


        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                total = 0;
                dataTable1.Rows.RemoveAt(Convert.ToInt32(dataGridView1.CurrentCell.RowIndex.ToString()));

                foreach(DataRow dr in dataTable1.Rows)
                {
                    total += Convert.ToDecimal(dr["Total"]);
                    label10.Text = total.ToString();
                }
            }
            catch(Exception ex)
            {

            }
        }

        private void textBox3_KeyUp(object sender, KeyEventArgs e)
        {
            listBox1.Visible = true;
            listBox1.Items.Clear();

            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.CommandText = "select *  from Stocks where ProductName like('"+textBox3.Text+"%')";
            sqlCommand.ExecuteNonQuery();

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();

            sqlDataAdapter.Fill(dataTable);

            foreach (DataRow dr in dataTable.Rows)
            {
                listBox1.Items.Add(dr["ProductName"].ToString());
            }

        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if(e.KeyCode == Keys.Down)
                {
                    this.listBox1.SelectedIndex += 1;
                }

                if (e.KeyCode == Keys.Up)
                {
                    this.listBox1.SelectedIndex -= 1;
                }

                if (e.KeyCode == Keys.Enter)
                {
                    textBox3.Text = listBox1.SelectedItem.ToString();
                    listBox1.Visible = false;
                    textBox4.Focus();
                }
            }
            catch(Exception ex)
            {

            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                listBox1.Focus();
                listBox1.SelectedIndex = 0;

            }
        }

        private void textBox4_Enter(object sender, EventArgs e)
        {

            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.CommandText = "select top 1 * from Purchases where ProductName = '" + textBox3.Text + "' order by id desc";
            sqlCommand.ExecuteNonQuery();

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();

            sqlDataAdapter.Fill(dataTable);

            foreach (DataRow dr in dataTable.Rows)
            {
                textBox4.Text = dr["Price"].ToString();
            }

        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            try
            {
                textBox6.Text = (Convert.ToDecimal(textBox5.Text)* Convert.ToDecimal(textBox4.Text)).ToString();
            }
            catch(Exception ex)
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int stock = 0;

            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.CommandText = "select * from Stocks where ProductName = '" + textBox3.Text + "'";
            sqlCommand.ExecuteNonQuery();

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);

            foreach(DataRow dr in dataTable.Rows)
            {
                stock = Convert.ToInt32(dr["Quantity"].ToString());
            }

            if (Convert.ToInt32(textBox5.Text) <= stock)
            {
                DataRow dataRow = dataTable1.NewRow();
                dataRow["Product"] = textBox3.Text;
                dataRow["Price"] = textBox4.Text;
                dataRow["Quantity"] = textBox5.Text;
                dataRow["Total"] = textBox6.Text;
                dataTable1.Rows.Add(dataRow);

                dataGridView1.DataSource = dataTable1;

                total += Convert.ToDecimal(dataRow["Total"].ToString());
                label10.Text = total.ToString();

            }

            else
            {
                MessageBox.Show("Quantity Not Available in Stock");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string orderID = "";

            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.CommandText = "insert into UserOrders values('"+textBox1.Text+"','"+textBox2.Text+"','"+comboBox1.SelectedItem.ToString()+"','"+dateTimePicker1.Value.ToString("dd-mm-yyyy")+"')";
            sqlCommand.ExecuteNonQuery();

            SqlCommand sqlCommand2 = sqlConnection.CreateCommand();
            sqlCommand2.CommandType = CommandType.Text;
            sqlCommand2.CommandText = "select top 1 * from UserOrders order by id desc";
            sqlCommand2.ExecuteNonQuery();

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand2);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);

            foreach (DataRow dr in dataTable.Rows)
            {
                orderID = dr["id"].ToString();
            }

            foreach(DataRow dr in dataTable1.Rows)
            {
                int qty = 0;
                string productname = "";

                SqlCommand sqlCommand3 = sqlConnection.CreateCommand();
                sqlCommand3.CommandType = CommandType.Text;
                sqlCommand3.CommandText = "insert into UserItemOrder values("+orderID.ToString()+",'"+ dr["Product"].ToString() + "','" + dr["Price"].ToString() + "','" + dr["Quantity"].ToString() + "','" + dr["Total"].ToString() + "')";
                sqlCommand3.ExecuteNonQuery();

                qty = Convert.ToInt32(dr["Quantity"].ToString());
                productname = dr["Product"].ToString();

                SqlCommand sqlCommand4 = sqlConnection.CreateCommand();
                sqlCommand4.CommandType = CommandType.Text;
                sqlCommand4.CommandText = "update Stocks set Quantity = Quantity - "+ qty +" where ProductName= '"+productname+"'";
                sqlCommand4.ExecuteNonQuery();
            }
            dataTable1.Clear();
            dataGridView1.DataSource = dataTable1;

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";

            MessageBox.Show("Report at the printer");
        }
    }
}
