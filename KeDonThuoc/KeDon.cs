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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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
            string query = "select benhnhan.mabn, benhnhan.hoten, benhnhan.tuoi, benhnhan.gioitinh, benhnhan.sdt,benhnhan.diachi from Benhnhan join lichsu on benhnhan.mabn = lichsu.mabn";
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

        private void LoadLichsu()
        {
            string query = "select ngaylapphieu from lichsu where mabn = '"+tbMABN+"'";
            using (SqlConnection connection = new SqlConnection(ConnectionString.connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                // Thêm dữ liệu vào ListView
                //listLichSu.Items.Add(reader["Ngay lap phieu"].ToString("dd/MM/yyyy"));

                // Đóng kết nối đến cơ sở dữ liệu SQL
                connection.Close();
            }
        }
        private void dinhdangluoi()
        {
            dataviewBenhNhan.ReadOnly = true;
            dataviewBenhNhan.Columns[0].Visible = false;
            dataviewBenhNhan.Columns[1].HeaderText = "Họ và Tên";
            dataviewBenhNhan.Columns[1].Width = 180;
            dataviewBenhNhan.Columns[1].DefaultCellStyle.Format = "";
            dataviewBenhNhan.Columns[2].HeaderText = "Tuổi";
            dataviewBenhNhan.Columns[2].Width = 50;

            dataviewBenhNhan.AllowUserToAddRows = false;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnTaiKham_Click(object sender, EventArgs e)
        {
            btnLuu_Click(sender, e);
        }

        private void btnNhapMoi_Click(object sender, EventArgs e)
        {
            tbMABN.BackColor = SystemColors.Info;
            tbMABN.Text = "";
            tbHoTen.Text = "";
            cbGioiTInh.Text = "Nam";
            tbTuoi.Text = "";
            tbSDT.Text = "";
            tbDiaChi.Text = "";
            tbHoTen.Focus();
            btnLuu.Enabled = true;
        }
        
        private void tbHoTen_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                tbHoTen.Text = tbHoTen.Text.ToUpper().Trim();
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
                int new_mabn = maxValue + 1;
                return new_mabn;
            }
        }
        //public int MABN_MOI;
        private void btnLuu_Click(object sender, EventArgs e)
        {
            // nếu mã bn rỗng tạo mã bn mới
            if (tbMABN.Text == "") 
            {
                int new_mabn = TaoMABN();
                // thêm bn
                using (SqlConnection connection = new SqlConnection(ConnectionString.connectionString))
                {
                    string query = "insert into benhnhan values (@MABN,@HOTEN,@TUOI,@GIOITINH,@SDT,@DIACHI)";
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@MABN", new_mabn.ToString().Trim());
                    command.Parameters.AddWithValue("@HOTEN", tbHoTen.Text.Trim());
                    command.Parameters.AddWithValue("@TUOI",tbTuoi.Text.Trim());
                    command.Parameters.AddWithValue("@GIOITINH",cbGioiTInh.Text.Trim());
                    command.Parameters.AddWithValue("@SDT",tbSDT.Text.Trim());
                    command.Parameters.AddWithValue("@DIACHI",tbDiaChi.Text.Trim());
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                using (SqlConnection connection = new SqlConnection(ConnectionString.connectionString))
                {
                    string query = "insert into lichsu (MABN,Ngaylapphieu) values (@MABN, getdate())";
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@MABN", new_mabn.ToString());
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            // tạo thêm lịch sử
            else
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString.connectionString))
                {
                    string query = "insert into lichsu (MABN,Ngaylapphieu) values (@MABN, getdate())";
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@MABN", tbMABN.Text.Trim());
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            tbMABN.Text = "";
            tbHoTen.Text = "";
            cbGioiTInh.Text = "Nam";
            tbTuoi.Text = "";
            tbSDT.Text = "";
            tbDiaChi.Text = "";
            btnLuu.Enabled = false;
            tbMABN.BackColor = SystemColors.Control;
            btnNhapMoi.Focus();
            LoadDanhSach();
        }

        private void dataviewBenhNhan_MouseClick(object sender, MouseEventArgs e)
        {
            tbMABN.ReadOnly = true;
        }

        private void dataviewBenhNhan_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //select benhnhan.mabn, benhnhan.hoten, benhnhan.tuoi, benhnhan.gioitinh, benhnhan.sdt,benhnhan.diachi
            tbMABN.ReadOnly = true;
            tbMABN.Text = dataviewBenhNhan.Rows[e.RowIndex].Cells[0].Value.ToString();
            tbHoTen.Text = dataviewBenhNhan.Rows[e.RowIndex].Cells[1].Value.ToString();
            tbTuoi.Text = dataviewBenhNhan.Rows[e.RowIndex].Cells[2].Value.ToString();
            cbGioiTInh.Text = dataviewBenhNhan.Rows[e.RowIndex].Cells[3].Value.ToString();
            tbSDT.Text = dataviewBenhNhan.Rows[e.RowIndex].Cells[4].Value.ToString();
            tbDiaChi.Text = dataviewBenhNhan.Rows[e.RowIndex].Cells[5].Value.ToString();
            //LoadLichsu();
        }

        private void tbHoTen_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
