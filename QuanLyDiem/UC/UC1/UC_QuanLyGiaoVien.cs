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

namespace QuanLyDiem.UC.UC1
{
    public partial class UC_QuanLyGiaoVien : UserControl
    {
        public UC_QuanLyGiaoVien()
        {
            InitializeComponent();
        }

        private void UC_QuanLyGiaoVien_Load(object sender, EventArgs e)
        {
            LoadCombobox();
            LoadData(CanBoGiaoVien_BUS.Instance.LayDanhSachCanBoGiaoVien_List());
            txtMaGV.Text = CanBoGiaoVien_BUS.Instance.GetNextTeacherID();
            txtMaGV.Enabled = false;
        }

        void LoadCombobox()
        {
            cbxLoaiTK.Items.AddRange(new object[] { new ComboboxItem() { Value = "-1", Name = "===Chọn===" }, new ComboboxItem() { Value = "1", Name = "Admin" }, new ComboboxItem() { Value = "2", Name = "Giáo viên" } });
            cbxLoaiTK.SelectedIndex = 0;
        }

        void RefeshData()
        {
            txtMaGV.Text = CanBoGiaoVien_BUS.Instance.GetNextTeacherID();
            txtHoTen.Text = string.Empty;
            txtDiaChi.Text = string.Empty;
            txtMatKhau.Text = string.Empty;
            txtSdt.Text = string.Empty;
            txtTaiKhoan.Text = string.Empty;

            btnHuy.Enabled = false;
            btnLuu.Enabled = false;
        }

        void LoadData(List<CanBoGiaoVien> t)
        {
            dataGridView1.Refresh();
            List<CanBoGiaoVien> list = t;
            List<GiaoVien> giaoViens = new List<GiaoVien>();
            foreach (var item in list)
            {
                GiaoVien temp = new GiaoVien();
                temp.HoTen = item.HoTen;
                temp.DiaChi = item.DiaChi;
                temp.MaCanBoGiaoVien = item.MaCanBoGiaoVien;
                temp.SoDienThoai = item.SoDienThoai;
                temp.TaiKhoan = item.TaiKhoan;
                if (item.LoaiTaiKhoan == 2)
                {
                    temp.LoaiTaiKhoan = "Giáo viên";
                }
                else
                {
                    temp.LoaiTaiKhoan = "Admin";
                }
                giaoViens.Add(temp);
            }
            dataGridView1.DataSource = giaoViens;
            dataGridView1.ChinhTieuDeDataGridView(new string[] { "Mã Giáo Viên", "Họ Tên", "Địa Chỉ", "Số Điện Thoại", "Tên Tài Khoản", "Loại Tài Khoản" });
            
        }

        public class GiaoVien
        {
            public string MaCanBoGiaoVien { get; set; }
            public string HoTen { get; set; }
            public string DiaChi { get; set; }
            public string SoDienThoai { get; set; }
            public string TaiKhoan { get; set; }
            public string LoaiTaiKhoan { get; set; }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtHoTen.Text))
            {
                if (!string.IsNullOrEmpty(txtDiaChi.Text))
                {
                    if (!string.IsNullOrEmpty(txtTaiKhoan.Text))
                    {
                        if (!string.IsNullOrEmpty(txtMatKhau.Text))
                        {
                            ComboboxItem item = cbxLoaiTK.SelectedItem as ComboboxItem;
                            if (!item.Value.Equals("-1"))
                            {
                                if (!string.IsNullOrEmpty(txtSdt.Text))
                                {
                                    CanBoGiaoVien giaoVien = new CanBoGiaoVien();
                                    giaoVien.MaCanBoGiaoVien = txtMaGV.Text;
                                    giaoVien.HoTen = txtHoTen.Text;
                                    giaoVien.DiaChi = txtDiaChi.Text;
                                    giaoVien.TaiKhoan = txtTaiKhoan.Text;
                                    giaoVien.MatKhau = txtMatKhau.Text;
                                    giaoVien.SoDienThoai = txtSdt.Text;
                                    giaoVien.LoaiTaiKhoan = int.Parse(item.Value);
                                    if (CanBoGiaoVien_BUS.Instance.KiemTraTonTai(txtMaGV.Text))
                                    {
                                        if (CanBoGiaoVien_BUS.Instance.SuaThongTinGiaoVien(giaoVien) > 0)
                                        {
                                            LoadData(CanBoGiaoVien_BUS.Instance.LayDanhSachCanBoGiaoVien_List());
                                            btnHuy.Enabled = false;
                                            btnLuu.Enabled = false;
                                            RefeshData();
                                        }
                                        else
                                        {
                                            MessageBox.Show("Thêm mới không thành công, đã có lỗi xảy ra.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                    }
                                    else
                                    {
                                        if(CanBoGiaoVien_BUS.Instance.ThemGiaoVien(giaoVien) > 0)
                                        {
                                            LoadData(CanBoGiaoVien_BUS.Instance.LayDanhSachCanBoGiaoVien_List());
                                            btnHuy.Enabled = false;
                                            btnLuu.Enabled = false;
                                            RefeshData();
                                        }
                                        else
                                        {
                                            MessageBox.Show("Thêm mới không thành công, đã có lỗi xảy ra.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Trường điện thoại không được để trống.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Chọn loại tài khoản.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Mật khẩu không được để trống.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Tên tài khoản không được để trống.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Địa chỉ không được để trống.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Họ tên giáo viên không được để trống.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            RefeshData();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string question = String.Format("Bạn có chắc chắn muốn xóa giáo viên với mã giáo viên {0}", txtMaGV.Text);
            DialogResult dialog = MessageBox.Show(question, "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                if (CanBoGiaoVien_BUS.Instance.KiemTraTonTai(txtMaGV.Text))
                {
                    if (CanBoGiaoVien_BUS.Instance.XoaGiaoVien(txtMaGV.Text) > 0)
                    {
                        LoadData(CanBoGiaoVien_BUS.Instance.LayDanhSachCanBoGiaoVien_List());
                        btnHuy.Enabled = false;
                        btnLuu.Enabled = false;
                        btnXoa.Enabled = false;
                    }
                    else
                    {
                        MessageBox.Show("Đã xảy ra lỗi trong quá trình xử lý, Vui lòng thử lại sau.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Không thể xóa, không có thông tin về giáo viên này trong hệ thống", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSearch.Text))
            {
                LoadData(CanBoGiaoVien_BUS.Instance.LayDanhSachCanBoGiaoVien_Search(txtSearch.Text));
            }else
            {
                LoadData(CanBoGiaoVien_BUS.Instance.LayDanhSachCanBoGiaoVien_List());
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView1.SelectedRows[0];
                txtMaGV.Text = row.Cells[0].Value.ToString();
                txtHoTen.Text = row.Cells[1].Value.ToString();
                txtDiaChi.Text = row.Cells[2].Value.ToString();
                txtSdt.Text = row.Cells[3].Value.ToString();
                txtTaiKhoan.Text = row.Cells[4].Value.ToString();
                //txtMatKhau.Text = row.Cells[5].Value.ToString();
                cbxLoaiTK.SelectedIndex = cbxLoaiTK.FindString(row.Cells[5].Value.ToString());
            }
        }

        private void txtHoTen_TextChanged(object sender, EventArgs e)
        {
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
        }

        private void txtDiaChi_TextChanged(object sender, EventArgs e)
        {
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
        }

        private void txtTaiKhoan_TextChanged(object sender, EventArgs e)
        {
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
        }

        private void txtMatKhau_TextChanged(object sender, EventArgs e)
        {
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
        }

        private void txtSdt_TextChanged(object sender, EventArgs e)
        {
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
        }
    }
}
