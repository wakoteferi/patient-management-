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

namespace CURDOperation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection conn;
        SqlCommand cmd;
        private void Form1_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(@"Data Source=DESKTOP-OUEOS0Q\SQLEXPRESS;Initial Catalog=WAKO;Integrated Security=True");
            cmd = new SqlCommand();
            cmd.Connection = conn;
        }

        private void btninsert_Click(object sender, EventArgs e)
        {
            string query = $"insert into pdata values('{txtid.Text.ToString()}','{txtname.Text}','{txtsalary.Text.ToString()}','{textsex.Text}','{txtaddress.Text}','{textsymp.Text}')";
            cmd.CommandText = query;
            conn.Open();
            cmd.ExecuteNonQuery();
            cleardata();
            conn.Close();
            displaydata();
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update pdata set pname='" + txtname.Text + "',kebele='" + txtaddress.Text + "',page='" + txtsalary.Text.ToString() +"' where pid='" + txtid.Text.ToString() + "' ";
            cmd.ExecuteNonQuery();
            conn.Close();
            displaydata();
            cleardata();
        }
        private void cleardata()
        {
            txtid.Clear();
            txtname.Clear();
            txtaddress.Clear();
            txtsalary.Clear();

        }

        private void btnshow_Click(object sender, EventArgs e)
        {
            displaydata();
        }
        private void displaydata()
        {
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from pdata";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            string query = $"delete pdata where pid='{txtid.Text.ToString()}'";
            cmd.CommandText = query;
            conn.Open();
            cmd.ExecuteNonQuery();
            dataGridView1.DataSource = query;
            cleardata();
            conn.Close();
            displaydata();
        }

        private void btnfind_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from pdata where pid='" + txtsearch.Text + "'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            txtname.Text = dt.ToString();
            txtaddress.Text = dt.ToString();
            txtsalary.Text = dt.ToString();
            dataGridView1.DataSource = dt;
            conn.Close();
        }

        private void btnsave_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
