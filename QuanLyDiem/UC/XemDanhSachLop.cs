using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using BUS;

namespace QuanLyDiem.UC
{
    public partial class XemDanhSachLop : UserControl
    {
        private string maGV { get; set; }
        public XemDanhSachLop(string maGV)
        {
            InitializeComponent();
            this.maGV = maGV;
        }

        private void XemDanhSachLop_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        void LoadData()
        {
            dataGridView1.DataSource = HoSoHocSinh_BUS.Instance.layDanhSachHocSinh(maGV);
        }
    }
}
