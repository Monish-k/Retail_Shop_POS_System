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
    public partial class AddProductName : Form
    {
        SqlConnection sqlConnection = new SqlConnection(@"Data Source=DESKTOP-J7SFGQV; Initial Catalog=POSDB; User ID=monish_db;Password=monimkd072000");

        public AddProductName()
        {
            InitializeComponent();
        }

        private void AddProductName_Load(object sender, EventArgs e)
        {
            if(sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();

            }
            sqlConnection.Open();
            fillDropDown();
            fillDatagrid();
        }

        public void fillDatagrid()
        {

            SqlCommand sqlCommand2 = sqlConnection.CreateCommand();
            sqlCommand2.CommandType = CommandType.Text;
            sqlCommand2.CommandText = "select * from ProductNames";
            sqlCommand2.ExecuteNonQuery();
            DataTable dataTable2 = new DataTable();
            SqlDataAdapter sqlDataAdapter2 = new SqlDataAdapter(sqlCommand2);
            sqlDataAdapter2.Fill(dataTable2);

            dataGridView1.DataSource = dataTable2;
        }

        public void fillDropDown()
        {
            comboBox1.Items.Clear();

            SqlCommand sqlCommand1 = sqlConnection.CreateCommand();
            sqlCommand1.CommandType = CommandType.Text;
            sqlCommand1.CommandText = "select * from Units";
            sqlCommand1.ExecuteNonQuery();
            DataTable dataTable1 = new DataTable();
            SqlDataAdapter sqlDataAdapter1 = new SqlDataAdapter(sqlCommand1);
            sqlDataAdapter1.Fill(dataTable1);

            foreach(DataRow dr in dataTable1.Rows)
            {
                comboBox1.Items.Add(dr["Unit"].ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";

            SqlCommand sqlCommand2 = sqlConnection.CreateCommand();
            sqlCommand2.CommandType = CommandType.Text;
            sqlCommand2.CommandText = "insert into ProductNames values('"+textBox1.Text+"','"+comboBox1.SelectedItem.ToString()+"')";
            sqlCommand2.ExecuteNonQuery();

            MessageBox.Show("Inserted Successfully");
            textBox1.Text = "";
            

            fillDatagrid();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());

            panel2.Visible = true;
            comboBox2.Items.Clear();

            SqlCommand sqlCommand3 = sqlConnection.CreateCommand();
            sqlCommand3.CommandType = CommandType.Text;
            sqlCommand3.CommandText = "select * from Units";
            sqlCommand3.ExecuteNonQuery();
            DataTable dataTable3 = new DataTable();
            SqlDataAdapter sqlDataAdapter3 = new SqlDataAdapter(sqlCommand3);
            sqlDataAdapter3.Fill(dataTable3);

            foreach (DataRow dr in dataTable3.Rows)
            {
                comboBox2.Items.Add(dr["Unit"].ToString());
            }

            SqlCommand sqlCommand1 = sqlConnection.CreateCommand();
            sqlCommand1.CommandType = CommandType.Text;
            sqlCommand1.CommandText = "select * from ProductNames where id = '"+ i +"'";
            sqlCommand1.ExecuteNonQuery();
            DataTable dataTable1 = new DataTable();
            SqlDataAdapter sqlDataAdapter1 = new SqlDataAdapter(sqlCommand1);
            sqlDataAdapter1.Fill(dataTable1);

            foreach (DataRow dr in dataTable1.Rows)
            {
                textBox2.Text = dr["ProductName"].ToString();
                comboBox2.SelectedItem =  dr["Units"].ToString();

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int i;
            i = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());

            SqlCommand sqlCommand5 = sqlConnection.CreateCommand();
            sqlCommand5.CommandType = CommandType.Text;
            sqlCommand5.CommandText = "update ProductNames set ProductName = '"+textBox2.Text+"',units='"+comboBox2.SelectedItem.ToString()+"' where id = "+ i +"";
            sqlCommand5.ExecuteNonQuery();

            panel2.Visible = false;

            fillDatagrid();
        }
    }
}
