using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

namespace KeDonThuoc
{
    public partial class KeDon : Form
    {
        public KeDon()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void KeDon_Load(object sender, EventArgs e)
        {
            LoadDanhSach();
        }

        private void LoadDanhSach()
        {
            string query = "select * from Benhnhan";
            using (SqlConnection connection = new SqlConnection(ConnectionString.connectionString))
            {
                try 
                { 
                    connection.Open();
                    SqlDataAdapter da = new SqlDataAdapter(query, connection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                    //dinhdangluoi();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối hoặc truy vấn: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}
