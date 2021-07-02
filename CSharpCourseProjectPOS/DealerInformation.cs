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
    public partial class DealerInformation : Form
    {
        SqlConnection sqlConnection = new SqlConnection(@"Data Source=DESKTOP-J7SFGQV; Initial Catalog=POSDB; User ID=monish_db; Password=monimkd072000");
        public DealerInformation()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.CommandText = "insert into DealerInformation values('"+textBox1.Text+"','"+ textBox2.Text + "','"+ textBox3.Text + "','"+ textBox4.Text + "','"+ textBox5.Text + "')";
            
            sqlCommand.ExecuteNonQuery();

            MessageBox.Show("Inserted Successfully");

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";

            filldatagrid();
        }

        private void DealerInformation_Load(object sender, EventArgs e)
        {
            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }
            sqlConnection.Open();
            filldatagrid();
        }

        public void filldatagrid()
        {
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.CommandText = "Select * from DealerInformation";
            sqlCommand.ExecuteNonQuery();

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();

            sqlDataAdapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            int i;
            i = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());

            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.CommandText = "delete from DealerInformation where id="+i+"";
            sqlCommand.ExecuteNonQuery();
            MessageBox.Show("Deleted Successfully");
            filldatagrid();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int i;
            i = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());

            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.CommandText = "update DealerInformation set DealerName='" + textBox1.Text + "',DealerStoreName='" + textBox2.Text + "',ContactNo='" + textBox3.Text + "',Address='" + textBox4.Text + "',City='" + textBox5.Text + "' where id ="+i+"";

            sqlCommand.ExecuteNonQuery();

            MessageBox.Show("Updated Successfully");

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";

            filldatagrid();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());

            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.CommandText = "select * from DealerInformation where id=" + i + "";
            sqlCommand.ExecuteNonQuery();

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();

            sqlDataAdapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;

            foreach (DataRow dr in dataTable.Rows)
            {
                textBox1.Text = dr["DealerName"].ToString();
                textBox2.Text = dr["DealerStoreName"].ToString();
                textBox3.Text = dr["ContactNo"].ToString();
                textBox4.Text = dr["Address"].ToString();
                textBox5.Text = dr["City"].ToString();
            }
        }
    }
}
