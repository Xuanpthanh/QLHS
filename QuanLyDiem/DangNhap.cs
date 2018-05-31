using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using BUS;
using DTO;

namespace QuanLyDiem
{
    public partial class DangNhap : Form
    {
        public DangNhap()
        {
            InitializeComponent();
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            onDangNhap();
        }

        void onDangNhap()
        {
            CanBoGiaoVien giaoVien = new CanBoGiaoVien();
            int check = CanBoGiaoVien_BUS.Instance.KiemTraDangNhap(txtTaiKhoan.Text, txtMatKhau.Text, out giaoVien);
            if (check == -1)
            {
                MessageBox.Show("Tai Khoan khong ton tai.", "Loi!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (check == 0)
            {
                MessageBox.Show("Sai Mat khau, vui long kiem tra lai.", "Loi!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (giaoVien.LoaiTaiKhoan == 1)
                {
                    this.Hide();
                    new AdminFrm(giaoVien).Show();
                }
                else if (giaoVien.LoaiTaiKhoan == 2)
                {
                    this.Hide();
                    new TeacherFrm(giaoVien).Show();
                }
            }
        }

        private void txtMatKhau_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                onDangNhap();
            }
        }
    }
}
