using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CafeNew
{
    public partial class ItemsForm : Form
    {
        public ItemsForm()
        {
            InitializeComponent();
            populate();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Cafedb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        void populate()
        {

            Con.Open();
            String query = "select * from ItemTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ItemsGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            UserOrder order = new UserOrder();
            order.Show();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 login = new Form1();
            login.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ItemName.Text == "" || ItemNum.Text == "" || PriceCb.Text == "")
            {
                MessageBox.Show("Выберите данные для добавления");
            }
            else
            {
                Con.Open();
                string query = "insert into ItemTbl values(" + ItemNum.Text + ",'" + ItemName.Text + "','" + CatCb.SelectedItem.ToString() + "',"+PriceCb.Text+")";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Блюдо создано");
                Con.Close();
                populate();

            }
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ItemsGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ItemNum.Text = ItemsGV.Rows[e.RowIndex].Cells[0].Value.ToString();
            ItemName.Text = ItemsGV.Rows[e.RowIndex].Cells[1].Value.ToString();
            CatCb.SelectedItem = ItemsGV.Rows[e.RowIndex].Cells[2].Value.ToString();
            PriceCb.Text = ItemsGV.Rows[e.RowIndex].Cells[3].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (ItemNum.Text == "")
            {
                MessageBox.Show("Выберите объект для удаления");

            }
            else
            {
                Con.Open();
                string query = "delete from ItemTbl where ItemNum = '" + ItemNum.Text + "'";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Блюдо удалено");
                Con.Close();
                populate();

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (ItemNum.Text == "" || ItemName.Text == "" || PriceCb.Text == "")
            {
                MessageBox.Show("Выберите объект для редактирования");
            }
            else
            {
                Con.Open();
                string query = "update ItemTbl set ItemName='" + ItemName.Text + "',ItemNum='" + ItemNum.Text + "',Itemcat='" + CatCb.SelectedItem.ToString() + "'where ItemPrice='" + PriceCb.Text + "'";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Блюдо отредактировано");
                Con.Close();
                populate();
            }
        }
    }
}
