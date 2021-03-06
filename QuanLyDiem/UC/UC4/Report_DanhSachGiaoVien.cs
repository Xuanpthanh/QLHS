﻿using BUS;
using Microsoft.Reporting.WinForms;
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

namespace QuanLyDiem.UC.UC4
{
    public partial class Report_DanhSachGiaoVien : Form
    {
        private string maGV { get; set; }
        public Report_DanhSachGiaoVien(string maGV)
        {
            InitializeComponent();
            this.maGV = maGV;
        }

        private void Report_DanhSachGiaoVien_Load(object sender, EventArgs e)
        {
            LoadCombobox();
            //this.reportViewer1.RefreshReport();
        }

        void LoadCombobox()
        {
            cbxLop.DataSource = Lop_BUS.Instance.Lop_();
            cbxLop.DisplayMember = "TenLop";
            cbxLop.ValueMember = "MaLop";

            cbxLop.SelectedIndex = 0;
        }

        private void cbxLop_SelectedIndexChanged(object sender, EventArgs e)
        {
            reportViewer1.Reset();
            string maLop = cbxLop.SelectedValue.ToString();
            DataTable dt = CanBoGiaoVien_BUS.Instance.LayDanhSachGiaoVienReport(maLop);
            ReportDataSource rds = new ReportDataSource("DataSet1", dt);
            reportViewer1.LocalReport.DataSources.Add(rds);
            string maGv = CanBoGiaoVien_BUS.Instance.LayTenGiaoVienChuNhiem(maLop);
            string tenLop = Lop_BUS.Instance.LayTenLop(maLop);
            string tenGV = CanBoGiaoVien_BUS.Instance.LayThongTinGiaoVien(maGV).HoTen;

            reportViewer1.LocalReport.ReportPath = "../../Report_DanhSachGiaoVien.rdlc";

            ReportParameter[] rParams = new ReportParameter[]
            {
                new ReportParameter("nameClass", tenLop),
                new ReportParameter("date", DateTime.Now.Day.ToString()),
                new ReportParameter("month", DateTime.Now.Month.ToString()),
                new ReportParameter("year", DateTime.Now.Year.ToString()),
                new ReportParameter("gv", maGv),
                new ReportParameter("nguoilap", tenGV)
            };
            reportViewer1.LocalReport.SetParameters(rParams);
            reportViewer1.RefreshReport();
        }
    }
}
