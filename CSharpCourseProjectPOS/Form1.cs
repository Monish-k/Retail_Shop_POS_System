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
    public partial class login : Form
    {
        SqlConnection sqlConnection = new SqlConnection(@"Data Source=DESKTOP-J7SFGQV; Initial Catalog=POSDB; User ID=monish_db;Password=monimkd072000");

        public login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i = 0;

            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.CommandText = "select * from Registration where UserName='"+textBox1.Text+"' and PassWord='"+textBox2.Text+"'";
            sqlCommand.ExecuteNonQuery();
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            sqlDataAdapter.Fill(dataTable);

            i = Convert.ToInt32(dataTable.Rows.Count.ToString());
            
            if (i == 0)
            {
                MessageBox.Show("UserName or Password is incorrect");
            }

            else
            {
                MDIParent1 mDI = new MDIParent1();
                mDI.Show();
                this.Hide();
            }
            sqlConnection.Close();
        }

        private void login_Load(object sender, EventArgs e)
        {

            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }

            sqlConnection.Open();
 
        }
    }
}
