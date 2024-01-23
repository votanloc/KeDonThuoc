using Microsoft.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
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
                command.Parameters.AddWithValue(@"dateDangky", dateDangKy.Text);
                command.ExecuteNonQuery();
                connection.Close();
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
            string query = "select benhnhan.mabn, benhnhan.hoten, benhnhan.tuoi, benhnhan.gioitinh, benhnhan.sdt,benhnhan.diachi, lichsu.ngaylapphieu from Benhnhan join lichsu on benhnhan.mabn = lichsu.mabn";
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
            string query = "select ngaylapphieu from lichsu where mabn = '" + tbMABN + "'";
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
            dataviewBenhNhan.Columns[3].HeaderText = "Phái";
            dataviewBenhNhan.Columns[3].Width = 50;
            dataviewBenhNhan.Columns[4].HeaderText = "Số điện thoại";
            dataviewBenhNhan.Columns[4].Width = 110;
            dataviewBenhNhan.Columns[5].Visible = false;
            dataviewBenhNhan.Columns[6].HeaderText = "Ngày lập phiếu";
            dataviewBenhNhan.Columns[6].Width = 70;


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
                else
                {
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
                //string query = "select MAX(MABN) FROM BENHNHAN";
                string query = "SELECT COUNT(*) FROM benhnhan WHERE mabn IS NOT NULL";
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
                int maxValue = (int)command.ExecuteScalar();
                if (maxValue == 0)
                {
                    int new_mabn = 1;
                    return new_mabn;
                }
                else
                {
                    int new_mabn = maxValue + 1;
                    return new_mabn;
                }
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
                    command.Parameters.AddWithValue("@TUOI", tbTuoi.Text.Trim());
                    command.Parameters.AddWithValue("@GIOITINH", cbGioiTInh.Text.Trim());
                    command.Parameters.AddWithValue("@SDT", tbSDT.Text.Trim());
                    command.Parameters.AddWithValue("@DIACHI", tbDiaChi.Text.Trim());
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                using (SqlConnection connection = new SqlConnection(ConnectionString.connectionString))
                {
                    DateTime today = DateTime.Now;
                    string giohientai = today.ToString("yyyy-MM-dd HH:mm");
                    string query = "insert into lichsu (MABN,Ngaylapphieu) values (@MABN,@ngayhientai)";
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@MABN", new_mabn.ToString());
                    command.Parameters.AddWithValue("@ngayhientai", giohientai);
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                tbMABN.Text = new_mabn.ToString();
            }
            // tạo thêm lịch sử
            else
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString.connectionString))
                {
                    DateTime today = DateTime.Now;
                    string giohientai = today.ToString("yyyy-MM-dd HH:mm");
                    string query = "insert into lichsu (MABN,Ngaylapphieu) values (@MABN, @ngayhientai)";
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@MABN", tbMABN.Text.Trim());
                    command.Parameters.AddWithValue("@ngayhientai", giohientai);
                    command.ExecuteNonQuery();
                    connection.Close();
                }

            }
            btnLuu.Enabled = false;
            tbMABN.BackColor = SystemColors.Control;
            btnNhapMoi.Focus();
            LoadDanhSach();
        }

        private void dataviewBenhNhan_MouseClick(object sender, MouseEventArgs e)
        {
            tbMABN.ReadOnly = true;
        }
        private void LoadToathuoc()
        {
            DateTime ngaydangky = dateDangKy.Value;
            // Chuyển đổi ngaydangky sang chuỗi theo định dạng yyyy-MM-dd HH:mm:ss tt
            string ngaydangky_str = ngaydangky.ToString("yyyy-MM-dd HH:mm:ss tt");
            //string query = "select chandoan,loidan from lichsu where mabn = '"+tbMABN.Text+"'";

            using (SqlConnection connection = new SqlConnection(ConnectionString.connectionString))
            {
                //string query = "select MAX(MABN) FROM BENHNHAN";
                string query = "SELECT COUNT(*) FROM lichsu WHERE mabn = '"+tbMABN.Text+"' and ngaylapphieu = '"+ngaydangky_str+"'";
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
                int maxValue = (int)command.ExecuteScalar();
                if (maxValue == 0)
                {
                    return;
                }
                else
                {
                    using (SqlConnection connection1 = new SqlConnection(ConnectionString.connectionString))
                    {
                        string query1 = "select chandoan from lichsu where mabn = '" + tbMABN.Text + "' and ngaylapphieu = '" + ngaydangky_str + "'";
                        connection.Open();
                        SqlCommand command1 = new SqlCommand(query, connection);
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                tbChandoan.Text += reader.GetString(0) + Environment.NewLine;
                            }
                        }
                        reader.Close();
                        connection.Close();
                    }
                }
            }

            using (SqlConnection connection = new SqlConnection(ConnectionString.connectionString))
            {
                //string query = "select MAX(MABN) FROM BENHNHAN";
                string query = "SELECT COUNT(*) FROM lichsu WHERE loidan IS NULL and mabn = '" + tbMABN.Text + "' and ngaylapphieu = '" + ngaydangky_str + "'";
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
                int maxValue = (int)command.ExecuteScalar();
                if (maxValue == 0)
                {
                    return;
                }
                else
                {
                    using (SqlConnection connection1 = new SqlConnection(ConnectionString.connectionString))
                    {
                        string query1 = "select loidan from lichsu where mabn = '" + tbMABN.Text + "' and ngaylapphieu = '" + ngaydangky_str + "'";
                        connection.Open();
                        SqlCommand command1 = new SqlCommand(query, connection);
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                tbLoiDan.Text += reader.GetString(1) + Environment.NewLine;
                            }
                        }
                        reader.Close();
                        connection.Close();
                    }
                }
            }

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
            dateDangKy.Text = dataviewBenhNhan.Rows[e.RowIndex].Cells[6].Value.ToString();
            //LoadLichsu();
            LoadToathuoc();

        }

        private void tbHoTen_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.connectionString))
            {
                string query = "update benhnhan set hoten = @hoten, tuoi = @tuoi, gioitinh = @gioitinh, sdt = @sdt, diachi = @diachi where mabn = @mabn";
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@mabn", tbMABN.Text);
                command.Parameters.AddWithValue("@hoten", tbHoTen.Text.Trim());
                command.Parameters.AddWithValue(@"tuoi", tbTuoi.Text.Trim());
                command.Parameters.AddWithValue(@"gioitinh", cbGioiTInh.Text.Trim());
                command.Parameters.AddWithValue(@"sdt", tbSDT.Text.Trim());
                command.Parameters.AddWithValue(@"diachi", tbDiaChi.Text.Trim());
                command.ExecuteNonQuery();
                connection.Close();
                btnCapNhat.Enabled = false;
                LoadDanhSach();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.connectionString))
            {
                DialogResult dialogResult = MessageBox.Show("Bạn có muốn xóa lượt khám này?", "Xóa khám", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    DateTime ngaydangky = dateDangKy.Value;
                    // Chuyển đổi ngaydangky sang chuỗi theo định dạng yyyy-MM-dd HH:mm:ss tt
                    string ngaydangky_str = ngaydangky.ToString("yyyy-MM-dd HH:mm:ss tt");
                    string query = "delete lichsu where mabn = @mabn and ngaylapphieu = '" + ngaydangky_str + "'";
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@mabn", tbMABN.Text.Trim());
                    command.ExecuteNonQuery();
                    connection.Close();
                    btnCapNhat.Enabled = false;
                    LoadDanhSach();
                }
                else if (dialogResult == DialogResult.No)
                {
                    return;
                }

            }
        }

        private void btnLuuChandoan_Click(object sender, EventArgs e)
        {
            DateTime ngaydangky = dateDangKy.Value;
            // Chuyển đổi ngaydangky sang chuỗi theo định dạng yyyy-MM-dd HH:mm:ss tt
            string ngaydangky_str = ngaydangky.ToString("yyyy-MM-dd HH:mm:ss tt");
            string query = "update lichsu set chandoan = @chandoan, loidan = @loidan where mabn = @mabn and Ngaylapphieu = '" + ngaydangky_str + "'";
            using (SqlConnection connection = new SqlConnection(ConnectionString.connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@chandoan", tbChandoan.Text.Trim());
                command.Parameters.AddWithValue("@mabn", tbMABN.Text.Trim());
                command.Parameters.AddWithValue("@loidan", tbLoiDan.Text.Trim());
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        private void tbChandoan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                DateTime ngaydangky = dateDangKy.Value;
                // Chuyển đổi ngaydangky sang chuỗi theo định dạng yyyy-MM-dd HH:mm:ss tt
                string ngaydangky_str = ngaydangky.ToString("yyyy-MM-dd HH:mm:ss tt");
                string query = "update lichsu set chandoan = @chandoan, loidan = @loidan where mabn = @mabn and Ngaylapphieu = '" + ngaydangky_str + "'";
                using (SqlConnection connection = new SqlConnection(ConnectionString.connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@chandoan", tbChandoan.Text.Trim());
                    command.Parameters.AddWithValue("@mabn", tbMABN.Text.Trim());
                    command.Parameters.AddWithValue("@loidan", tbLoiDan.Text.Trim());
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        private void tbLoiDan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                DateTime ngaydangky = dateDangKy.Value;
                // Chuyển đổi ngaydangky sang chuỗi theo định dạng yyyy-MM-dd HH:mm:ss tt
                string ngaydangky_str = ngaydangky.ToString("yyyy-MM-dd HH:mm:ss tt");
                string query = "update lichsu set chandoan = @chandoan, loidan = @loidan where mabn = @mabn and Ngaylapphieu = '" + ngaydangky_str + "'";
                using (SqlConnection connection = new SqlConnection(ConnectionString.connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@chandoan", tbChandoan.Text.Trim());
                    command.Parameters.AddWithValue("@mabn", tbMABN.Text.Trim());
                    command.Parameters.AddWithValue("@loidan", tbLoiDan.Text.Trim());
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
    }
}
