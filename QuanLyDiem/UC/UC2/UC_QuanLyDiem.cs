﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using BUS;
using DTO;

namespace QuanLyDiem.UC.UC2
{
    public partial class UC_QuanLyDiem : UserControl
    {
        private int vt = 0;
        private List<Scorce> list;
        public UC_QuanLyDiem()
        {
            InitializeComponent();
        }

        private void UC_QuanLyDiem_Load(object sender, EventArgs e)
        {
            LoadCombobox();
            LoadData(0);
        }

        void LoadCombobox()
        {
            cbxLop.DataSource = Lop_BUS.Instance.Lop_();
            cbxLop.DisplayMember = "TenLop";
            cbxLop.ValueMember = "MaLop";

            cbxLop.SelectedIndex = 0;


            cbxMonHoc.ValueMember = "MaMon";
            cbxMonHoc.DataSource = MonHoc_BUS.Instance.DanhSachMonHoc();
            cbxMonHoc.DisplayMember = "TenMon";


            cbxMonHoc.SelectedIndex = 0;
        }

        void SetViewData(string mahs, int vt)
        {
            var data = list.Where(x => x.MaHS.Equals(mahs)).FirstOrDefault();
            int n = list.IndexOf(data);
            float tb1 = 0, tb2 = 0;
            if (float.TryParse(txtTBKy1.Text, out tb1))
            {
                data.Diem_TB_ky1_moi = tb1;
            }
            else
            {
                MessageBox.Show("Sai định dạng dữ liệu", "Lỗi!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (float.TryParse(txtTBKy2.Text, out tb2))
            {
                data.Diem_TB_ky2_moi = tb2;
            }
            else
            {
                MessageBox.Show("Sai định dạng dữ liệu", "Lỗi!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            list.RemoveAt(n);
            list.Insert(n, data);

            dataGridView1.Refresh();
            dataGridView1.DataSource = list;

            dataGridView1.Rows[vt].Selected = true;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.ChinhTieuDeDataGridView(new string[] { "Mã học sinh", "Tên học sinh", "Tên môn", "Điểm TB kỳ 1 cũ", "Điểm TB kỳ 2 cũ", "", "", "Điểm TB kỳ 1 mới", "Điểm TB Kỳ 2 mới" });
        }

        void LoadData(int vt)
        {
            string maLop = cbxLop.SelectedValue.ToString();
            int maMon = int.Parse(cbxMonHoc.SelectedValue.ToString());

            list = Diem_BUS.Instance.LayDanhSachDiem_List(maLop, maMon);

            dataGridView1.DataSource = list;
            try
            {
                dataGridView1.Rows[vt].Selected = true;
            }
            catch { }
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.ChinhTieuDeDataGridView(new string[] { "Mã học sinh", "Tên học sinh", "Tên môn", "Điểm TB kỳ 1 cũ", "Điểm TB kỳ 2 cũ", "", "", "Điểm TB kỳ 1 mới", "Điểm TB Kỳ 2 mới" });
        }

        private void cbxLop_SelectedIndexChanged(object sender, EventArgs e)
        {
            //cbxMonHoc_SelectedIndexChanged(sender, e);
            //LoadData();
        }

        private void cbxMonHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            //cbxLop_SelectedIndexChanged(sender, e);

        }

        private void btnChonLocCN_Click(object sender, EventArgs e)
        {
            LoadData(0);
        }

        private void dtvDiem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                vt = dataGridView1.SelectedRows[0].Index;
                DataGridViewRow row = dataGridView1.SelectedRows[0];
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView1.SelectedRows[0];
                string maLop = row.Cells[5].Value.ToString();
                string maMon = row.Cells[6].Value.ToString();
                string maHS = row.Cells[0].Value.ToString();

                float tb1 = float.Parse(row.Cells[7].Value.ToString()), tb2 = float.Parse(row.Cells[8].Value.ToString()) ;

                Diem diem = new Diem();
                diem.MaHocSinh = maHS;
                diem.MaMonHoc = int.Parse(maMon);
                diem.DiemTB_Ky1 = tb1;
                diem.DiemTB_Ky2 = tb2;
                if (Diem_BUS.Instance.SuaDiem(diem) > 0)
                {
                    LoadData(vt);
                }
                else
                {
                    MessageBox.Show("Đã có lỗi xảy ra.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView1.SelectedRows[0];
                string mshs = row.Cells[0].Value.ToString();
                SetViewData(mshs, vt);
            }
        }
    }
}
