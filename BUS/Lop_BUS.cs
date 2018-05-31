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
    public class Lop_BUS
    {
        private static Lop_BUS instance;

        public static Lop_BUS Instance
        {
            get { if (instance == null) instance = new Lop_BUS(); return instance; }
            set { instance = value; }
        }

        public DataTable Lop_()
        {
            string query = @"select MaLop, TenLop from Lop";
            return DataProvider.Instance.ExcuteQuery(query);
        }

        public string LayTenLop(string maLop)
        {
            string query = @"select TenLop from Lop where MaLop = @malop";
            DataTable dt = DataProvider.Instance.ExcuteQuery(query, new object[] { maLop });
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0][0].ToString();
            }
            return string.Empty;
        }

        public bool KiemTraTonTai(string maLop)
        {
            string query = @"Select * from [dbo].[Lop] where MaLop = @malop";
            DataTable dt = DataProvider.Instance.ExcuteQuery(query, new object[] { maLop });
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public DataTable DanhSachLop_Report(string maLop)
        {
            string query = @"exec sb_GetListStudentByClassID @malop";
            return DataProvider.Instance.ExcuteQuery(query, new object[] { maLop });
        }

        public List<Lop> Lops()
        {
            List<Lop> lops = new List<Lop>();
            DataTable dt = DataProvider.Instance.ExcuteQuery(@"select MaLop, TenLop from Lop");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Lop temp = new Lop();
                    temp.MaLop = dt.Rows[0][0].ToString();
                    temp.TenLop = dt.Rows[0][1].ToString();
                    lops.Add(temp);
                }
            }
            return lops;
        }

        public string GetNextIDClass()
        {
            return DataProvider.Instance.GetValueFunction(@"SELECT [dbo].[FN_GetClassIDNext] ()");
        }

        public int ThemLop(Lop lop)
        {
            string query = @"INSERT INTO [dbo].[Lop] ( [MaLop] ,[TenLop] ,[NienKhoa] ,[SiSo] ,[GiaoVienChuNhiem]) VALUES ( @malop , @tenlop , @nienkhoa , @siso , @giaovienchunhiem )";
            return DataProvider.Instance.ExcuteNonQuery(query, new object[] { lop.MaLop, lop.TenLop, lop.NienKhoa, lop.SiSo, lop.GiaoVienChuNhiem });
        }

        public int SuaLop(Lop lop)
        {
            string query = @"sb_UpdateClass @malop , @tenlop , @nienkhoa , @siso , @giaovien_cn";
            return DataProvider.Instance.ExcuteNonQuery(query, new object[] { lop.MaLop, lop.TenLop, lop.NienKhoa, lop.SiSo, lop.GiaoVienChuNhiem });
        }

        public int XoaLop(string malop)
        {
            string query = @"DELETE FROM [dbo].[Lop] WHERE MaLop = @malop";
            return DataProvider.Instance.ExcuteNonQuery(query, new object[] { malop });
        }

        public DataTable DanhSachLop()
        {
            string query = @"SELECT  [MaLop]
      ,[TenLop]
      ,[NienKhoa]
      ,[SiSo]
      ,CanBoGiaoVien.HoTen
        ,CanBoGiaoVien.MaCanBoGiaoVien
  FROM [QuanLy_Diem].[dbo].[Lop] inner join CanBoGiaoVien on Lop.GiaoVienChuNhiem = CanBoGiaoVien.MaCanBoGiaoVien";
            return DataProvider.Instance.ExcuteQuery(query);
        }

        public DataTable DanhSachLop(string MaLop)
        {
            string query = @"  SELECT  [MaLop]
      ,[TenLop]
      ,[NienKhoa]
      ,[SiSo]
      ,CanBoGiaoVien.HoTen
        ,CanBoGiaoVien.MaCanBoGiaoVien
  FROM [QuanLy_Diem].[dbo].[Lop] inner join CanBoGiaoVien on Lop.GiaoVienChuNhiem = CanBoGiaoVien.MaCanBoGiaoVien
  where TenLop like '%' + @par1 + '%' or MaLop like '%' + @par2 + '%'";
            return DataProvider.Instance.ExcuteQuery(query, new object[] { MaLop, MaLop });
        }

        public DataTable DanhSachLop_TheoGV(string maGV)
        {
            string query = @"Select * from [dbo].[Lop] where GiaoVienChuNhiem = @maGV";
            return DataProvider.Instance.ExcuteQuery(query, new object[] { maGV });
        }

        public DataTable DanhSachLop_TheoHS(string maHS)
        {
            HoSoHocSinh hs = new HoSoHocSinh_BUS().LayThongTinHocSinh(maHS);
            string query = @"Select * from [dbo].[Lop] where MaLop = @malop";
            return DataProvider.Instance.ExcuteQuery(query, new object[] { hs.MaLop });
        }
    }
}
