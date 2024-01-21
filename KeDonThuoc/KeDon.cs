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
using System.Text.RegularExpressions;

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
            btnLuu.Enabled = true;
        }
        
        private void tbHoTen_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                tbHoTen.Text = tbHoTen.Text.ToUpper();
                cbGioiTInh.Focus();
            }
        }

        private void cbGioiTInh_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 || e.KeyChar == 9)
            {
                cbGioiTInh.Text = cbGioiTInh.Text.ToUpper();
                if (cbGioiTInh.Text != "NỮ" && cbGioiTInh.Text != "NAM")
                {
                    MessageBox.Show("Vui lòng chọn lại giới tính");
                    cbGioiTInh.Focus();
                }
                else
                {
                    tbTuoi.Focus();
                }
            }
        }

        private void tbTuoi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (tbTuoi.Text.Length == 10 || tbTuoi.Text == "" || tbTuoi.Text.Length == 2 || tbTuoi.Text.Length == 4 || tbTuoi.Text.Length == 1)
                {
                    tbSDT.Focus();
                }
                else 
                {
                    MessageBox.Show("Nhập lại tuổi");
                    tbTuoi.Focus();
                }
            }
        }

        private void tbSDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (tbSDT.Text == "" || tbSDT.Text.Length == 9 || tbSDT.Text.Length == 10)
                {
                    tbDiaChi.Focus();
                }
                else {
                    MessageBox.Show("Nhập lại số điện thoại");
                    tbSDT.Focus();
                }
            }
        }

        private void tbDiaChi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnLuu_Click(sender, e);
            }
        }
        
        public int TaoMABN()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.connectionString))
            {
                string query = "select MAX(MABN) FROM BENHNHAN";
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
                int maxValue = (int)command.ExecuteScalar();
                int newValue = maxValue + 1;
                return newValue;
                //return MABN_MOI = newValue;
            }
        }
        //public int MABN_MOI;
        private void btnLuu_Click(object sender, EventArgs e)
        {
            // nếu mã bn rỗng tạo mã bn mới
            if (tbMABN.Text == "") 
            {
                int newValue = TaoMABN();
                MessageBox.Show(newValue.ToString());
            }
            // tạo thêm lịch sử
            else
            {
                
            }
            
            // kiểm tra mã bn có chưa
            /*string query = "insert into";
            using (SqlConnection connection = new SqlConnection(ConnectionString.connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                // không tồn tại
                if (!reader.Read())
                {

                }
                else 
                { 
                
                }
            }*/
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

        private void tbHoTen_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
