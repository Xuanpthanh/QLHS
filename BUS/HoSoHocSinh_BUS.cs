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
    public class HoSoHocSinh_BUS
    {
        private static HoSoHocSinh_BUS instance;

        public static HoSoHocSinh_BUS Instance
        {
            get { if (instance == null) instance = new HoSoHocSinh_BUS(); return instance; }
            set { instance = value; }
        }

        public DataTable DanhSachHocSinh()
        {
            string query = @"  select [MaHocSinh]
      ,[HoTen]
      ,[NgaySinh]
      ,[GioiTinh]
      ,[DiaChi]
      ,[DiemVaoTruong]
      ,[HoTenBoMe]
      ,[SoDienThoai]
	  ,Lop.TenLop
	  from [QuanLy_Diem].[dbo].HoSoHocSinh
	  inner join Lop on HoSoHocSinh.MaLop = Lop.MaLop";
            return DataProvider.Instance.ExcuteQuery(query);
        }

        public HoSoHocSinh LayThongTinHocSinh(string maHS)
        {
            string query = @"sb_GetInfoStudent @mahs";
            DataTable dt = DataProvider.Instance.ExcuteQuery(query, new object[] { maHS});
            HoSoHocSinh hs = new HoSoHocSinh();
            hs.MaHocSinh = dt.Rows[0][0].ToString();
            hs.HoTen = dt.Rows[0][1].ToString();
            hs.NgaySinh = DateTime.Parse(dt.Rows[0][2].ToString());
            hs.GioiTinh = int.Parse(dt.Rows[0][3].ToString());
            hs.DiaChi = dt.Rows[0][4].ToString();
            hs.DiemVaoTruong = double.Parse(dt.Rows[0][5].ToString());
            hs.HoTenBoMe = dt.Rows[0][6].ToString();
            hs.DienThoai = dt.Rows[0][7].ToString();
            hs.MaLop = dt.Rows[0][8].ToString();

            return hs;
        }

        public DataTable DanhSachHocSinhTheoLop(string malop)
        {
            string query = @"select [MaHocSinh]
      ,[HoTen]
      ,[NgaySinh]
      ,[GioiTinh]
      ,[DiaChi]
      ,[DiemVaoTruong]
      ,[HoTenBoMe]
      ,[SoDienThoai]
	  ,Lop.TenLop
	  from [QuanLy_Diem].[dbo].HoSoHocSinh
	  inner join Lop on HoSoHocSinh.MaLop = Lop.MaLop where HoSoHocSinh.MaLop = @malop";
            return DataProvider.Instance.ExcuteQuery(query, new object[] { malop });
        }

        public DataTable layDanhSachHocSinh(string maGV)
        {
            string query = @"exec db_LayDanhSachHocSinh @malop";
            return DataProvider.Instance.ExcuteQuery(query, new object[] { maGV });
        }

        public bool KiemTraTonTai(string MaHS)
        {
            string query = @"  select [MaHocSinh]
      ,[HoTen]
      ,[NgaySinh]
      ,[GioiTinh]
      ,[DiaChi]
      ,[DiemVaoTruong]
      ,[HoTenBoMe]
      ,[SoDienThoai]
	  from [QuanLy_Diem].[dbo].HoSoHocSinh where [MaHocSinh] = @mahs";
            DataTable dt = DataProvider.Instance.ExcuteQuery(query, new object[] { MaHS });
            if(dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string GetNextStudentID()
        {
            return DataProvider.Instance.GetValueFunction(@"SELECT [dbo].[FN_GetStudentIDNext] ()");
        }

        public int ThemHocSinh(HoSoHocSinh hs)
        {
            string query = @"INSERT INTO [dbo].[HoSoHocSinh] ( [MaHocSinh] , [HoTen] , [NgaySinh] , [GioiTinh] , [DiaChi] , [DiemVaoTruong] , [HoTenBoMe] , [SoDienThoai] , [MaLop] ) VALUES ( @mahs , @tenhs , @ngaysinh , @gioitinh , @diachi , @diemvaotruong , @hotenbome , @sodienthoai , @malop )";
            return DataProvider.Instance.ExcuteNonQuery(query, new object[] { hs.MaHocSinh, hs.HoTen, hs.NgaySinh, hs.GioiTinh, hs.DiaChi, hs.DiemVaoTruong, hs.HoTenBoMe, hs.DienThoai, hs.MaLop });
        }

        public int SuaHocSinh(HoSoHocSinh hs)
        {
            string query = @"sb_UpdateStudent @mahs , @hoten , @ngaysinh , @gioitinh , @diachi , @diemvaotruong , @hotenbome , @sdt , @malop";
            return DataProvider.Instance.ExcuteNonQuery(query, new object[] { hs.MaHocSinh, hs.HoTen, hs.NgaySinh, hs.GioiTinh, hs.DiaChi, hs.DiemVaoTruong, hs.HoTenBoMe, hs.DienThoai, hs.MaLop});
        }

        public int XoaHocSinh(string mahs)
        {
            string query = @"Delete HoSoHocSinh where MaHocSinh = @mahs";
            return DataProvider.Instance.ExcuteNonQuery(query, new object[] { mahs });
        }
    }
}
