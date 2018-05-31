using QuanLyDiem.UC.UC2;
using QuanLyDiem.UC.UC3;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DTO;
using QuanLyDiem.UC;

namespace QuanLyDiem
{
    public partial class TeacherFrm : Form
    {
        private CanBoGiaoVien giaoVien { get; set; }
        public TeacherFrm(CanBoGiaoVien giaoVien)
        {
            InitializeComponent();
            this.giaoVien = giaoVien;
        }

        private void btnQuanLyDiem_Click(object sender, EventArgs e)
        {
            UC_QuanLyDiem quanLyDiem = new UC_QuanLyDiem();
            mainPanel.Controls.Clear();
            quanLyDiem.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(quanLyDiem);
        }

        private void TeacherFrm_Load(object sender, EventArgs e)
        {
            lbName.Text = giaoVien.HoTen + " Giáo Viên";

            btnQuanLyDiem.selected = true;

            mainPanel.Controls.Clear();
            mainPanel.Controls.Add(new UC_QuanLyDiem());
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
            if (menuPanel.Width == 54)
            {
                menuPanel.Width = 270;
                bunifuImageButton3.Location = new Point(237, 10);
                panel1.Height = 130;
                lbName.Visible = true;
                lbTile.Visible = true;
                ptbLogo.Visible = true;
                // bunifuTransition2.ShowSync(menuPanel);
            }
            else
            {
                menuPanel.Width = 54;
                bunifuImageButton3.Location = new Point(12, 10);
                panel1.Height = 53;
                lbName.Visible = false;
                lbTile.Visible = false;
                ptbLogo.Visible = false;
                //bunifuTransition1.ShowSync(menuPanel);
            }
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            UC_XemPhanCongGiangDay phanCongGiangDay = new UC_XemPhanCongGiangDay();
            mainPanel.Controls.Clear();
            phanCongGiangDay.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(phanCongGiangDay);
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            XemDanhSachLop xemDanhSachLop = new XemDanhSachLop(giaoVien.MaCanBoGiaoVien);
            mainPanel.Controls.Clear();
            xemDanhSachLop.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(xemDanhSachLop);
        }
    }
}
