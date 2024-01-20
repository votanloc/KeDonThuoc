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
using Microsoft.VisualBasic;

namespace KeDonThuoc
{
    public partial class KeDon : Form
    {
        public KeDon()
        {
            InitializeComponent();
        }

        public void ExecuteQuery(string query)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@MABN", tbMABN.Text);
                command.Parameters.AddWithValue("@HOTEN", tbHoTen.Text.Trim());
                command.Parameters.AddWithValue(@"Gioitinh", cbGioiTInh.Text.Trim());
                command.Parameters.AddWithValue(@"Tuoi", tbTuoi.Text.Trim());
                command.Parameters.AddWithValue(@"SDT", tbSDT.Text.Trim());
                command.Parameters.AddWithValue(@"DiaChi", tbDiaChi.Text.Trim());
                command.Parameters.AddWithValue(@"NgayDangKy", dateDangKy.Text);
                command.ExecuteNonQuery();
            }
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
                    dataviewBenhNhan.DataSource = dt;
                    dinhdangluoi();
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
        private void dinhdangluoi()
        {
            dataviewBenhNhan.ReadOnly = true;
            dataviewBenhNhan.Columns[0].Visible = false;
            dataviewBenhNhan.Columns[1].HeaderText = "Họ và Tên";
            dataviewBenhNhan.Columns[1].Width = 180;
            dataviewBenhNhan.Columns[2].HeaderText = "Tuổi";
            dataviewBenhNhan.Columns[2].Width = 50;

            dataviewBenhNhan.AllowUserToAddRows = false;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnTaiKham_Click(object sender, EventArgs e)
        {

        }

        private void btnNhapMoi_Click(object sender, EventArgs e)
        {
            tbMABN.BackColor = SystemColors.Info;
            tbHoTen.Focus();
        }

        private void tbHoTen_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cbGioiTInh.Focus();
            }
        }

        private void cbGioiTInh_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                tbTuoi.Focus();
            }
        }

        private void tbTuoi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                tbSDT.Focus();
            }
        }

        private void tbSDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                tbDiaChi.Focus();
            }
        }

        private void tbDiaChi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnLuu_Click(sender, e);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            MessageBox.Show("hI HI");
        }

        private void dataviewBenhNhan_MouseClick(object sender, MouseEventArgs e)
        {
            tbMABN.ReadOnly = true;
        }

        private void dataviewBenhNhan_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            tbMABN.ReadOnly = true;
            tbMABN.Text = dataviewBenhNhan.Rows[e.RowIndex].Cells[0].Value.ToString();
            tbHoTen.Text = dataviewBenhNhan.Rows[e.RowIndex].Cells[1].Value.ToString();
        }
    }
}
