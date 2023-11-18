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
    public partial class ViewOrders : Form
    {
        public ViewOrders()
        {
            InitializeComponent();
            
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Cafedb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        void populate()
        {

            Con.Open();
            String query = "select * from OrdersTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            OrdersGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void categorycb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void OrdersGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                //printDocument1.Print();
            }
      
           
        }

       // private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
      //  {
            
        //   e.Graphics.DrawString("======== Order Summary ========", new Font("Century", 25, FontStyle.Bold), Brushes.Red, new Point(200));
            
        //}

        private void ViewOrders_Load(object sender, EventArgs e)
        {
            populate();
        }
    }
}
