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
    public class Diem_BUS
    {
        private static Diem_BUS instance;

        public static Diem_BUS Instance
        {
            get { if (instance == null) instance = new Diem_BUS(); return instance; }
            set { instance = value; }
        }

        public DataTable LayDiemTheoMon(int mamon)
        {
            string query = @"select * from fn_GetScorceByIDSubject(@mamon)";
            return DataProvider.Instance.ExcuteQuery(query, new object[] { mamon });
        }
        public DataTable LayDiemTheoMaHS(string mahs)
        {
            string query = @"select * from sb_GetScorceByIDSubject(@mahs)";
            return DataProvider.Instance.ExcuteQuery(query, new object[] { mahs });
        }

        public DataTable LayDanhSachDiem(string maLop, int maMon)
        {
            string query = @"select * from [dbo].fn_GetScorceStudent( @malop , @mamon ) order by HoTen DESC";
            return DataProvider.Instance.ExcuteQuery(query, new object[] { maLop, maMon });
        }

        public int SuaDiem(Diem diem)
        {
            string query = @"sb_UpdateScorce @mahs , @mamon , @diemky1 , @diemky2";
            return DataProvider.Instance.ExcuteNonQuery(query, new object[] { diem.MaHocSinh, diem.MaMonHoc, diem.DiemTB_Ky1, diem.DiemTB_Ky2 });
        }
    }
}
