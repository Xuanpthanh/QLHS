using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DTO;
using BUS;
using System.Globalization;

namespace QuanLyDiem.UC.UC1
{
    public partial class UC_QuanLyHoSoHocSinh : UserControl
    {
        public UC_QuanLyHoSoHocSinh()
        {
            InitializeComponent();
        }

        private void UC_QuanLyHoSoHocSinh_Load(object sender, EventArgs e)
        {
            cbxLop.DataSource = Lop_BUS.Instance.Lop_();
            cbxLop.ValueMember = "MaLop";
            cbxLop.DisplayMember = "TenLop";
            cbxLop.SelectedIndex = 0;

            txtMaHS.Text = HoSoHocSinh_BUS.Instance.GetNextStudentID();

            txtMaHS.Enabled = false;
            btnHuy.Enabled = false;
            btnLuu.Enabled = false;
            btnXoa.Enabled = false;
        }

        void RefeshData()
        {
            txtMaHS.Text = HoSoHocSinh_BUS.Instance.GetNextStudentID();
            txtHoTen.Text = string.Empty;
            txtDiaChi.Text = string.Empty;
            txtDiemVaoTruong.Text = string.Empty;
            txtHoTenBoMe.Text = string.Empty;
            txtSdt.Text = string.Empty;
            dtpNgaySinh.Value = DateTime.Now;
        }

        private DateTime ChuyenDoi(string time)
        {
            string[] temp = time.Split('-');
            string[] t = temp[2].Split(' ');
            int check = int.Parse(t[0]);
            string nam = string.Empty;
            if (check > DateTime.Now.Year)
            {
                nam = "19" + temp[2];
            }
            else
            {
                nam = "20" + temp[2];
            }

            int monthIndex = DateTime.ParseExact(temp[1], "MMM", CultureInfo.InvariantCulture).Month;
            string ngay = temp[0] + "/" + monthIndex.ToString() + "/" + nam;
            return DateTime.Parse(ngay);
        }

        void LoadData()
        {
            string malop = cbxLop.SelectedValue.ToString();

            DataTable dt = HoSoHocSinh_BUS.Instance.DanhSachHocSinhTheoLop(malop);
            string a = string.Empty;
            if (dt.Rows.Count > 0)
                a = dt.Rows[0][2].ToString();

            dataGridView1.DataSource = HoSoHocSinh_BUS.Instance.DanhSachHocSinhTheoLop(malop);
            dataGridView1.ChinhTieuDeDataGridView(new string[] { "Mã học sinh", "Họ tên", "Ngày sinh", "Giới tính", "Địa chỉ", "Điểm vào trường", "Họ tên bố mẹ", "Số điện thoại", "Lớp" });
        }

        private void cbxLop_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                btnXoa.Enabled = true;
                DataGridViewRow row = dataGridView1.SelectedRows[0];
                txtMaHS.Text = row.Cells[0].Value.ToString();
                txtHoTen.Text = row.Cells[1].Value.ToString();
                dtpNgaySinh.Value = DateTime.Parse(row.Cells[2].Value.ToString());
                rdoNu.Checked = row.Cells[3].Value.ToString() == "0" ? true : false;
                rdoNam.Checked = row.Cells[3].Value.ToString() == "0" ? false : true;
                txtDiaChi.Text = row.Cells[4].Value.ToString();
                txtDiemVaoTruong.Text = row.Cells[5].Value.ToString();
                txtHoTenBoMe.Text = row.Cells[6].Value.ToString();
                txtSdt.Text = row.Cells[7].Value.ToString();

            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtHoTen.Text))
            {
                if (!string.IsNullOrEmpty(txtDiaChi.Text))
                {
                    if (!string.IsNullOrEmpty(txtDiemVaoTruong.Text))
                    {
                        float diem = 0;
                        if (float.TryParse(txtDiemVaoTruong.Text, out diem))
                        {
                            if (!string.IsNullOrEmpty(txtHoTenBoMe.Text))
                            {
                                HoSoHocSinh hocSinh = new HoSoHocSinh();
                                hocSinh.HoTen = txtHoTen.Text;
                                hocSinh.NgaySinh = dtpNgaySinh.Value;
                                hocSinh.DiaChi = txtDiaChi.Text;
                                hocSinh.DiemVaoTruong = diem;
                                hocSinh.HoTenBoMe = txtHoTenBoMe.Text;
                                if (rdoNam.Checked)
                                {
                                    hocSinh.GioiTinh = 1;
                                }
                                else if (rdoNu.Checked)
                                {
                                    hocSinh.GioiTinh = 0;
                                }
                                else
                                {
                                    hocSinh.GioiTinh = -1;
                                }
                                hocSinh.DienThoai = txtSdt.Text;
                                hocSinh.MaHocSinh = txtMaHS.Text;
                                hocSinh.MaLop = cbxLop.SelectedValue.ToString();
                                if (HoSoHocSinh_BUS.Instance.KiemTraTonTai(txtMaHS.Text))
                                {
                                    //Sua thong tin hoc sinh
                                    if (HoSoHocSinh_BUS.Instance.SuaHocSinh(hocSinh) > 0)
                                    {
                                        LoadData();
                                        btnHuy.Enabled = false;
                                        btnLuu.Enabled = false;
                                        RefeshData();
                                    }
                                    else
                                    {
                                        MessageBox.Show("Đã có lỗi xảy ra, vui lòng thử lại sau.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                                else
                                {
                                    //Them moi hoc sinh
                                    if (HoSoHocSinh_BUS.Instance.ThemHocSinh(hocSinh) > 0)
                                    {
                                        LoadData();
                                        btnHuy.Enabled = false;
                                        btnLuu.Enabled = false;
                                        RefeshData();
                                    }
                                    else
                                    {
                                        MessageBox.Show("Đã có lỗi xảy ra, vui lòng thử lại sau.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Họ tên bố mẹ không được để trống.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Định dạng nhập vào không chính xác (chỉ nhập số).", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Điểm vào trường không được để trống.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Địa chỉ học sinh không được để trống.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Họ tên học sinh không được để trống.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            RefeshData();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string question = String.Format("Bạn có chắc chắn muốn xóa Học sinh với mã học sinh {0}", txtMaHS.Text);
            DialogResult dialog = MessageBox.Show(question, "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                if (HoSoHocSinh_BUS.Instance.XoaHocSinh(txtMaHS.Text) > 0)
                {
                    LoadData();
                    btnHuy.Enabled = false;
                    btnLuu.Enabled = false;
                    btnXoa.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Không thể xóa, không có thông tin về học sinh này trong hệ thống", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txtHoTen_TextChanged(object sender, EventArgs e)
        {
            btnLuu.Enabled = true; btnHuy.Enabled = true;
        }

        private void txtDiaChi_TextChanged(object sender, EventArgs e)
        {
            btnLuu.Enabled = true; btnHuy.Enabled = true;
        }

        private void txtDiemVaoTruong_TextChanged(object sender, EventArgs e)
        {
            btnLuu.Enabled = true; btnHuy.Enabled = true;
        }

        private void txtHoTenBoMe_TextChanged(object sender, EventArgs e)
        {
            btnLuu.Enabled = true; btnHuy.Enabled = true;
        }

        private void txtSdt_TextChanged(object sender, EventArgs e)
        {
            btnLuu.Enabled = true; btnHuy.Enabled = true;
        }

        private void dtpNgaySinh_ValueChanged(object sender, EventArgs e)
        {
            btnLuu.Enabled = true; btnHuy.Enabled = true;
        }
    }
}
