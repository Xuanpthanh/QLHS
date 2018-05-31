using QuanLyDiem.UC.UC1;
using QuanLyDiem.UC.UC3;
using QuanLyDiem.UC.UC4;
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

namespace QuanLyDiem
{
    public partial class AdminFrm : Form
    {
        private CanBoGiaoVien giaoVien { get; set; }
        public AdminFrm(CanBoGiaoVien giaoVien)
        {
            InitializeComponent();
            btnQuanLyMonHoc.selected = true;
            mainPanel.Controls.Clear();
            mainPanel.Controls.Add(new UC_QuanLyMonHoc());
            this.giaoVien = giaoVien;
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
            if(menuPanel.Width== 54)
            {
                menuPanel.Width = 235;
                bunifuImageButton3.Location = new Point(199, 10);
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

        private void btnQuanLyMonHoc_Click(object sender, EventArgs e)
        {
            UC_QuanLyMonHoc quanLyMonHoc = new UC_QuanLyMonHoc();
            mainPanel.Controls.Clear();
            quanLyMonHoc.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(quanLyMonHoc);
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            UC_QuanLyLop quanLyLop = new UC_QuanLyLop();
            mainPanel.Controls.Clear();
            quanLyLop.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(quanLyLop);
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            UC_QuanLyHoSoHocSinh quanLyHoSoHocSinh = new UC_QuanLyHoSoHocSinh();
            mainPanel.Controls.Clear();
            quanLyHoSoHocSinh.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(quanLyHoSoHocSinh);
        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            UC_QuanLyGiaoVien quanLyGiaoVien = new UC_QuanLyGiaoVien();
            mainPanel.Controls.Clear();
            quanLyGiaoVien.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(quanLyGiaoVien);
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            UC_PhanCongGiangDay phanCongGiangDay = new UC_PhanCongGiangDay();
            mainPanel.Controls.Clear();
            phanCongGiangDay.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(phanCongGiangDay);
        }

        private void bunifuFlatButton5_Click(object sender, EventArgs e)
        {
            UC_BaoCao baoCao = new UC_BaoCao(giaoVien.MaCanBoGiaoVien);
            mainPanel.Controls.Clear();
            baoCao.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(baoCao);
        }

        private void AdminFrm_Load(object sender, EventArgs e)
        {
            lbName.Text = giaoVien.HoTen + " Admin";
        }
    }
}
