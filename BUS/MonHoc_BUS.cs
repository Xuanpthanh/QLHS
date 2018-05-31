using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using DAL;
using DTO;

namespace BUS
{
    public class MonHoc_BUS
    {
        private static MonHoc_BUS instance;

        public static MonHoc_BUS Instance
        {
            get { if (instance == null) instance = new MonHoc_BUS(); return instance; }
            set { instance = value; }
        }

        public bool KiemTraTonTai(int maMon)
        {
            string query = @"select * from [dbo].[MonHoc] where MaMon = @mamon";
            DataTable dt = DataProvider.Instance.ExcuteQuery(query, new object[] { maMon });
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public DataTable DanhSachMon(string timKiem)
        {
            int maMon = 0;
            int.TryParse(timKiem, out maMon);
            string query = @"select * from MonHoc where MaMon = @mamon or TenMon like '%' + @tenmon + '%'";
            return DataProvider.Instance.ExcuteQuery(query, new object[] { maMon, timKiem });
        }

        public int NextID()
        {
            string query = @"select [dbo].fn_GetNextSubjectID()";
            string result = DataProvider.Instance.GetValueFunction(query).ToString();

            return (int.Parse(result) + 1);
        }

        public DataTable DanhSachMonHoc()
        {
            string query = @"select * from MonHoc";
            return DataProvider.Instance.ExcuteQuery(query);
        }

        public MonHoc ThongTinMonHoc(int MaMon)
        {
            string query = @"select * MonHoc where MaMon = @mamon";
            DataTable dt = DataProvider.Instance.ExcuteQuery(query, new object[] { MaMon});
            MonHoc monHoc = new MonHoc();
            monHoc.MaMonHoc = int.Parse(dt.Rows[0][0].ToString());
            monHoc.TenMon = dt.Rows[0][1].ToString();
            monHoc.SoTiet = int.Parse(dt.Rows[0][2].ToString());
            return monHoc;
        }

        public int ThemMonHoc(MonHoc monHoc)
        {
            string query = @"INSERT INTO [dbo].[MonHoc] ( [TenMon] ,[SoTiet] ) VALUES ( @tenmon , @sotiet )";
            return DataProvider.Instance.ExcuteNonQuery(query, new object[] { monHoc.TenMon, monHoc.SoTiet });
        }

        public int SuaMonHoc(MonHoc monHoc)
        {
            string query = @"sb_UpdateSubject @mamon , @tenmon , @sotiet";
            return DataProvider.Instance.ExcuteNonQuery(query, new object[] { monHoc.MaMonHoc, monHoc.TenMon, monHoc.SoTiet });
        }

        public int XoaMonHoc(int maMon)
        {
            string query = @"delete MonHoc where MaMon = @mamon";
            return DataProvider.Instance.ExcuteNonQuery(query, new object[] { maMon });
        }
    }
}
