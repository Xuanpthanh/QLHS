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

namespace QuanLyDiem.UC.UC3
{
    public partial class UC_XemPhanCongGiangDay : UserControl
    {
        public UC_XemPhanCongGiangDay()
        {
            InitializeComponent();
        }

        private void UC_XemPhanCongGiangDay_Load(object sender, EventArgs e)
        {
            LoadCombobox();
            LoadData();
        }

        void LoadCombobox()
        {
            cbxLop.DataSource = Lop_BUS.Instance.DanhSachLop();
            cbxLop.DisplayMember = "TenLop";
            cbxLop.ValueMember = "MaLop";

            cbxGiaoVien.DataSource = CanBoGiaoVien_BUS.Instance.LayDanhSachGiaoVien();
            cbxGiaoVien.DisplayMember = "HoTen";
            cbxGiaoVien.ValueMember = "MaCanBoGiaoVien";

            cbxMonHoc.ValueMember = "MaMon";
            cbxMonHoc.DataSource = MonHoc_BUS.Instance.DanhSachMonHoc();
            cbxMonHoc.DisplayMember = "TenMon";
           

            cbxLop.SelectedIndex = 0;
            cbxGiaoVien.SelectedIndex = 0;
            cbxMonHoc.SelectedIndex = 0;
        }

        void LoadData()
        {
            dataGridView1.DataSource = PhanCongGiangDay_BUS.Instance.LayDanhSachPhanCongGiangDay();
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.ChinhTieuDeDataGridView(new string[] { "Mã lớp", "Tên lớp", "Giáo viên", "Tên môn", "Ngày phân công", "", "" });
        }

        private void cbxLop_SelectedIndexChanged(object sender, EventArgs e)
        {
            string maLop = cbxLop.SelectedValue.ToString();
            dataGridView1.DataSource = PhanCongGiangDay_BUS.Instance.LayDanhSachPhanCongTheoLop(maLop);
            dataGridView1.ChinhTieuDeDataGridView(new string[] { "Mã lớp", "Tên lớp", "Giáo viên", "Tên môn", "Ngày phân công"});
        }

        private void cbxGiaoVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            string maGV = cbxGiaoVien.SelectedValue.ToString();
            dataGridView1.DataSource = PhanCongGiangDay_BUS.Instance.LayDanhSachPhanCongTheoGiaoVien(maGV);
            dataGridView1.ChinhTieuDeDataGridView(new string[] { "Mã lớp", "Tên lớp", "Giáo viên", "Tên môn", "Ngày phân công" });
        }

        private void cbxMonHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            int maMon = int.Parse(cbxMonHoc.SelectedValue.ToString());
            dataGridView1.DataSource = PhanCongGiangDay_BUS.Instance.LayDanhSachPhanCongTheoMonHoc(maMon);
            dataGridView1.ChinhTieuDeDataGridView(new string[] { "Mã lớp", "Tên lớp", "Giáo viên", "Tên môn", "Ngày phân công" });

        }
    }
}
