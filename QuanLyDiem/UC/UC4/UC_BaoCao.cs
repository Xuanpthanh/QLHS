using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyDiem.UC.UC4
{
    public partial class UC_BaoCao : UserControl
    {
        private string maGV { get; set; }
        public UC_BaoCao(string maGV)
        {
            InitializeComponent();
            this.maGV = maGV;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Report_DanhSachHocSinh(maGV).ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Report_DanhSachGiaoVien(maGV).ShowDialog();
        }
    }
}
