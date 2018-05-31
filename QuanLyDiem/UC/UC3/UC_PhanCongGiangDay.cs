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

namespace QuanLyDiem.UC.UC3
{
    public partial class UC_PhanCongGiangDay : UserControl
    {
        private int vt = 0;
        public UC_PhanCongGiangDay()
        {
            InitializeComponent();
        }

        private void UC_PhanCongGiangDay_Load(object sender, EventArgs e)
        {
            LoadCombobox();
            LoadData(0);
        }

        void LoadCombobox()
        {
            cbxLop.DataSource = Lop_BUS.Instance.DanhSachLop();
            cbxLop.DisplayMember = "TenLop";
            cbxLop.ValueMember = "MaLop";

            
            cbxGiaoVien.DataSource = CanBoGiaoVien_BUS.Instance.LayDanhSachGiaoVien();
            cbxGiaoVien.DisplayMember = "HoTen";
            cbxGiaoVien.ValueMember = "MaCanBoGiaoVien";

            
            cbxMonHoc.DataSource = MonHoc_BUS.Instance.DanhSachMonHoc();
            cbxMonHoc.DisplayMember = "TenMon";
            cbxMonHoc.ValueMember = "MaMon";

            cbxGiaoVien.SelectedIndex = 0;
            cbxLop.SelectedIndex = 0;
            cbxMonHoc.SelectedIndex = 0;
        }

        void LoadData(int vt)
        {
            dataGridView1.DataSource = PhanCongGiangDay_BUS.Instance.LayDanhSachPhanCongGiangDay();
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.ChinhTieuDeDataGridView(new string[] { "Mã lớp", "Tên lớp", "Giáo viên", "Tên môn", "Ngày phân công", "", "" });
            try
            {
                dataGridView1.Rows[vt].Selected = true;
            }
            catch { }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dataGridView1.SelectedRows.Count > 0)
            {
                vt = dataGridView1.SelectedRows[0].Index;
                DataGridViewRow row = dataGridView1.SelectedRows[0];
                string maLop = row.Cells[0].Value.ToString();
                string maGV = row.Cells[5].Value.ToString();
                string maMon = row.Cells[6].Value.ToString();

                cbxGiaoVien.SelectedValue = maGV;
                cbxLop.SelectedValue = maLop;
                cbxMonHoc.SelectedValue = maMon;
                dtpNgay.Value = DateTime.Parse(row.Cells[4].Value.ToString());
            }
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

        private void btnPhanCong_Click(object sender, EventArgs e)
        {
            string maLop = cbxLop.SelectedValue.ToString();
            string maGV = cbxGiaoVien.SelectedValue.ToString();
            string maMon = cbxMonHoc.SelectedValue.ToString();
            DateTime t = dtpNgay.Value;

            if(PhanCongGiangDay_BUS.Instance.ThemPhanCongMonMoi(maLop, maGV, int.Parse(maMon), t) > 0)
            {
                LoadData(vt);
            }
            else
            {
                MessageBox.Show("Giáo viên đã nhận phân công của môn học.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTim.Text))
            {
                dataGridView2.DataSource = PhanCongGiangDay_BUS.Instance.LayDanhSachPhanCongGiangDay(txtTim.Text);
                dataGridView2.Columns[5].Visible = false;
                dataGridView2.Columns[6].Visible = false;
                dataGridView2.ChinhTieuDeDataGridView(new string[] { "Mã lớp", "Tên lớp", "Giáo viên", "Tên môn", "Ngày phân công", "", "" });
            }
            else
            {
                LoadData2();
            }
        }

        void LoadData2()
        {
            dataGridView2.DataSource = PhanCongGiangDay_BUS.Instance.LayDanhSachPhanCongGiangDay();
            dataGridView2.Columns[5].Visible = false;
            dataGridView2.Columns[6].Visible = false;
            dataGridView2.ChinhTieuDeDataGridView(new string[] { "Mã lớp", "Tên lớp", "Giáo viên", "Tên môn", "Ngày phân công", "", "" });
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage1"])//your specific tabname
            {
                LoadData(0);
            }
            else
            {
                LoadData2();
            }
        }
    }
}
