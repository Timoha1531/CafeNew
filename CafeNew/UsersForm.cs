using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace CafeNew
{

    public partial class UsersForm : Form
    {
        
        public UsersForm()
        {
            InitializeComponent();
            populate();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Cafedb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        void populate()
        {
           
            Con.Open();
            String query = "select * from UsersTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            UsersGV.DataSource = ds.Tables[0];
            Con.Close();
            }
        
        private void button3_Click(object sender, EventArgs e)
        {
            UserOrder uorder = new UserOrder();
            uorder.Show();
            this.Hide()
;        }

       private void button4_Click(object sender, EventArgs e)
        {
            ItemsForm item = new ItemsForm();
            item.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 login = new Form1();
            login.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Con.Open();
            string query = "insert into UsersTbl values ('" + uname.Text + "','" + uphone.Text + "','" + upassword.Text + "')";
            SqlCommand cmd = new SqlCommand (query, Con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Пользователь создан");
            Con.Close();
            populate();
        }

        private void ItemsGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
            uname.Text = UsersGV.Rows[e.RowIndex].Cells[0].Value.ToString();
            uphone.Text = UsersGV.Rows[e.RowIndex].Cells[1].Value.ToString();
            upassword.Text = UsersGV.Rows[e.RowIndex].Cells[2].Value.ToString();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void uname_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(uphone.Text == "")
            {
                MessageBox.Show("Выберите объект для удаления");

            }
            else
            {
                Con.Open();
                string query = "delete from UsersTbl where Uphone = '" + uphone.Text + "'" ;
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Пользователь удален");
                Con.Close();
                populate();

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (uphone.Text == "" || upassword.Text == "" || uname.Text == "") 
            {
                MessageBox.Show("Выберите объект для редактирования");
            }
            else
            {
                Con.Open();
                string query = "update UsersTbl set Uname='" + uname.Text + "',Upassword='" + upassword.Text + "'where Uphone='" + uphone.Text+"'";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Пользователь отредактирован");
                Con.Close();
                populate();
            }
        }
    }
}
