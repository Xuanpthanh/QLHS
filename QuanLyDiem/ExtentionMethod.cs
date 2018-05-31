using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using app = Microsoft.Office.Interop.Excel.Application;

namespace QuanLyDiem
{
    public class ComboboxItem
    {
        public string Value { get; set; }
        public string Name { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }

    public static class ExtentionMethod
    {
        static Thread tluu = null;
        private static void ChuyenSangGridView(DataGridView g, string duongDan)
        {
            app obj = new app();
            obj.Application.Workbooks.Add(Type.Missing);
            obj.Columns.ColumnWidth = 25;
            for (int i = 1; i < g.Columns.Count + 1; i++)
            {
                obj.Cells[1, i] = g.Columns[i - 1].HeaderText;

            }
            for (int i = 0; i < g.Rows.Count; i++)
            {
                for (int j = 0; j < g.Columns.Count; j++)
                {
                    if (g.Rows[i].Cells[j].Value != null)
                    {
                        obj.Cells[i + 2, j + 1] = g.Rows[i].Cells[j].Value.ToString();
                    }
                }
            }
            obj.ActiveWorkbook.SaveCopyAs(duongDan);
            obj.ActiveWorkbook.Saved = true;
            tluu.Abort();
        }

        public static void FixGridView(this DataGridView dtg, int width)
        {
            width = width - 5;
            foreach (DataGridViewColumn col in dtg.Columns)
            {
                col.Width = width / dtg.Columns.Count;
            }
        }

        public static void ChinhTieuDeDataGridView(this DataGridView dtg, string[] title)
        {
            int index = 0;
            foreach (DataGridViewColumn col in dtg.Columns)
            {
                col.HeaderText = title[index];
                index++;
            }
        }

        public static int XuatFileExcel(this DataGridView dtg)
        {
            SaveFileDialog openDlg = new SaveFileDialog();
            openDlg.InitialDirectory = @"C:\";
            openDlg.Filter = "Excel Files|*.xlsx;*.xlsm";
            openDlg.ShowDialog();
            string path = openDlg.FileName;
            if (path == "")
                return 0;
            tluu = new Thread(() => ChuyenSangGridView(dtg, path));
            tluu.Start();
            return 1;
        }
    }
}
