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
    public partial class UC_QuanLyLop : UserControl
    {
        public UC_QuanLyLop()
        {
            InitializeComponent();
        }

        private void UC_QuanLyLop_Load(object sender, EventArgs e)
        {
            txtMaLop.Enabled = false;
            LoadCombobox();
            LoadData();

            btnHuy.Enabled = false;
            btnLuu.Enabled = false;
            btnXoa.Enabled = false;
        }

        public void LoadCombobox()
        {
            cbxGiaoVien.Items.Add(new ComboboxItem() { Value = "-1", Name = "===Chọn Giáo Viên===" });
            List<CanBoGiaoVien> list = CanBoGiaoVien_BUS.Instance.LayDanhSachCanBoGiaoVien_List();
            foreach (var item in list)
            {
                ComboboxItem i = new ComboboxItem();
                i.Value = item.MaCanBoGiaoVien;
                i.Name = item.HoTen;
                cbxGiaoVien.Items.Add(i);
            }
            cbxGiaoVien.SelectedIndex = 0;
        }

        public void LoadData()
        {
            txtMaLop.Text = Lop_BUS.Instance.GetNextIDClass();
            dataGridView1.Refresh();
            dataGridView1.DataSource = Lop_BUS.Instance.DanhSachLop();

            dataGridView1.Columns[5].Visible = false;

            dataGridView1.ChinhTieuDeDataGridView(new string[] { "Mã lớp", "Tên lớp", "Niên khóa", "Sĩ Số", "Giáo viên chủ nhiệm", "" });
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTenLop.Text))
            {
                if (!string.IsNullOrEmpty(txtNienKhoa.Text))
                {
                    if (!string.IsNullOrEmpty(txtSiSo.Text))
                    {
                        int siso = 0;
                        if (int.TryParse(txtSiSo.Text, out siso))
                        {
                            ComboboxItem item = cbxGiaoVien.SelectedItem as ComboboxItem;
                            if (!item.Value.Equals("-1"))
                            {
                                Lop lop = new Lop();
                                lop.MaLop = txtMaLop.Text;
                                lop.TenLop = txtTenLop.Text;
                                lop.NienKhoa = txtNienKhoa.Text;
                                lop.SiSo = siso;
                                lop.GiaoVienChuNhiem = item.Value;

                                if (Lop_BUS.Instance.KiemTraTonTai(lop.MaLop))
                                {
                                    if (Lop_BUS.Instance.SuaLop(lop) > 0)
                                    {
                                        LoadData();
                                        btnHuy.Enabled = false;
                                        btnLuu.Enabled = false;
                                        RefeshData();
                                    }
                                }
                                else
                                {
                                    if (Lop_BUS.Instance.ThemLop(lop) > 0)
                                    {
                                        LoadData();
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
                                MessageBox.Show("Hãy chọn một giáo viên.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Định dạng nhập vào không chính xác (chỉ nhập số).", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Không được để trống mục sĩ số của lớp.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Không được để trống mục niên khóa của lớp.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Tên lớp không được để trống.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        void RefeshData()
        {
            txtMaLop.Text = Lop_BUS.Instance.GetNextIDClass();
            txtNienKhoa.Text = string.Empty;
            txtSiSo.Text = string.Empty;
            txtTenLop.Text = string.Empty;
            btnHuy.Enabled = false;
            btnLuu.Enabled = false;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            RefeshData();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                btnXoa.Enabled = true;
                btnLuu.Enabled = true;
                DataGridViewRow row = dataGridView1.SelectedRows[0];
                txtMaLop.Text = row.Cells[0].Value.ToString();
                txtTenLop.Text = row.Cells[1].Value.ToString();
                txtNienKhoa.Text = row.Cells[2].Value.ToString();
                txtSiSo.Text = row.Cells[3].Value.ToString();
                string maGV = row.Cells[5].Value.ToString();

                int vt = cbxGiaoVien.FindStringExact(row.Cells[4].Value.ToString());
                cbxGiaoVien.SelectedIndex = vt;
            }
        }

        private void txtTenLop_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTenLop.Text))
            {
                btnLuu.Enabled = true;
                btnHuy.Enabled = true;
            }
        }

        private void txtNienKhoa_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNienKhoa.Text))
            {
                btnLuu.Enabled = true;
                btnHuy.Enabled = true;
            }
        }

        private void txtSiSo_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSiSo.Text))
            {
                btnLuu.Enabled = true;
                btnHuy.Enabled = true;
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string question = String.Format("Bạn có chắc chắn muốn xóa lớp với mã lớp {0}", txtMaLop.Text);
            DialogResult dialog = MessageBox.Show(question, "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(dialog == DialogResult.Yes)
            {
                if (Lop_BUS.Instance.XoaLop(txtMaLop.Text) > 0)
                {
                    LoadData();
                    btnHuy.Enabled = false;
                    btnLuu.Enabled = false;
                    btnXoa.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Không thể xóa, không có thông tin về lớp này trong hệ thống", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTimKiem.Text))
            {
                dataGridView1.DataSource = Lop_BUS.Instance.DanhSachLop(txtTimKiem.Text);
            }
            else
            {
                LoadData();
            }
        }
    }
}
