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
    public partial class UC_QuanLyMonHoc : UserControl
    {
        public UC_QuanLyMonHoc()
        {
            InitializeComponent();
        }

        private void UC_QuanLyMonHoc_Load(object sender, EventArgs e)
        {
            LoadData();
            btnHuy.Enabled = false;
            btnLuu.Enabled = false;
            btnXoa.Enabled = false;
            txtMaMon.Enabled = false;
        }

        void LoadData()
        {
            dataGridView1.Refresh();
            dataGridView1.DataSource = MonHoc_BUS.Instance.DanhSachMonHoc();
            txtMaMon.Text = MonHoc_BUS.Instance.NextID().ToString();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView1.SelectedRows[0];
                txtMaMon.Text = row.Cells[0].Value.ToString();
                txtTenTenMon.Text = row.Cells[1].Value.ToString();
                txtSoTiet.Text = row.Cells[2].Value.ToString();

                btnXoa.Enabled = true;
            }
        }

        private void RefeshData()
        {
            txtMaMon.Text = MonHoc_BUS.Instance.NextID().ToString();
            txtSoTiet.Text = string.Empty;
            txtTenTenMon.Text = string.Empty;
        }

        private void txtTenTenMon_TextChanged(object sender, EventArgs e)
        {
            btnHuy.Enabled = true;
            btnLuu.Enabled = true;
        }

        private void txtSoTiet_TextChanged(object sender, EventArgs e)
        {
            btnHuy.Enabled = true;
            btnLuu.Enabled = true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTenTenMon.Text))
            {
                if (!string.IsNullOrEmpty(txtSoTiet.Text))
                {
                    int sotiet = 0;
                    if (int.TryParse(txtSoTiet.Text, out sotiet))
                    {
                        MonHoc monHoc = new MonHoc();
                        monHoc.TenMon = txtTenTenMon.Text;
                        monHoc.SoTiet = sotiet;
                        if (MonHoc_BUS.Instance.KiemTraTonTai(int.Parse(txtMaMon.Text)))
                        {
                            monHoc.MaMonHoc = int.Parse(txtMaMon.Text);
                            if (MonHoc_BUS.Instance.SuaMonHoc(monHoc) > 0)
                            {
                                LoadData();
                                btnHuy.Enabled = false;
                                btnLuu.Enabled = false;
                                RefeshData();
                            }
                            else
                            {
                                MessageBox.Show("Đã xảy ra lỗi trong quá trình xử lý. Vui lòng thử lại sau.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            if (MonHoc_BUS.Instance.ThemMonHoc(monHoc) > 0)
                            {
                                LoadData();
                                btnHuy.Enabled = false;
                                btnLuu.Enabled = false;
                                RefeshData();
                            }
                            else
                            {
                                MessageBox.Show("Đã xảy ra lỗi trong quá trình xử lý. Vui lòng thử lại sau.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Định dạng nhập liệu không chính xác (Chỉ nhập số)", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Số tiết học không được để trống", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Tên môn học không được để trống.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string question = String.Format("Bạn có chắc chắn muốn xóa Môn học với mã môn học {0}", txtMaMon.Text);
            DialogResult dialog = MessageBox.Show(question, "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                if (MonHoc_BUS.Instance.XoaMonHoc(int.Parse(txtMaMon.Text)) > 0)
                {
                    LoadData();
                    btnHuy.Enabled = false;
                    btnLuu.Enabled = false;
                    btnXoa.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Không thể xóa, không có thông tin về môn học này trong hệ thống", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTimKiem.Text))
            {
                dataGridView1.DataSource = MonHoc_BUS.Instance.DanhSachMon(txtTimKiem.Text);
            }
            else
            {
                LoadData();
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            RefeshData();
            btnHuy.Enabled = false;
            btnLuu.Enabled = false;
            btnXoa.Enabled = false;
        }
    }
}
