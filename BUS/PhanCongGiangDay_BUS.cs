using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DAL;
using DTO;

namespace BUS
{
    public class PhanCongGiangDay_BUS
    {
        private static PhanCongGiangDay_BUS instance;

        public static PhanCongGiangDay_BUS Instance
        {
            get { if (instance == null) instance = new PhanCongGiangDay_BUS(); return instance; }
            set { instance = value; }
        }

        public DataTable LayDanhSachPhanCongGiangDay()
        {
            string query = @"select * from fn_GetSchedulesTeach()";
            return DataProvider.Instance.ExcuteQuery(query);
        }

        public DataTable LayDanhSachPhanCongGiangDay(string tim)
        {
            string query = @"select * from fn_GetSchedulesTeachFillter( @tim )";
            return DataProvider.Instance.ExcuteQuery(query, new object[] { tim});
        }

        public DataTable LayDanhSachPhanCongTheoLop(string maLop)
        {
            string query = @"select * from fn_GetSchedulesTeachByClassID( @malop )";
            return DataProvider.Instance.ExcuteQuery(query, new object[] { maLop });
        }

        public DataTable LayDanhSachPhanCongTheoGiaoVien(string maGV)
        {
            string query = @"select * from fn_GetSchedulesTeachByTeacherID( @magv )";
            return DataProvider.Instance.ExcuteQuery(query, new object[] { maGV });
        }

        public DataTable LayDanhSachPhanCongTheoMonHoc(int maMon)
        {
            string query = @"select * from fn_GetSchedulesTeachBySubjectID( @mamon )";
            return DataProvider.Instance.ExcuteQuery(query, new object[] { maMon });
        }

        public int ThemPhanCongMonMoi(string maLop, string maGV, int maMon, DateTime ngayPhan)
        {
            string query = @"sb_AddNewScheduleTeach @malop , @magv , @mamon , @ngayphan";
            return DataProvider.Instance.ExcuteNonQuery(query, new object[] { maLop, maGV, maMon, ngayPhan });
        }
    }
}
