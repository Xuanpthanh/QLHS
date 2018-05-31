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
    public class CanBoGiaoVien_BUS
    {
        private static CanBoGiaoVien_BUS instance;

        public static CanBoGiaoVien_BUS Instance
        {
            get { if (instance == null) instance = new CanBoGiaoVien_BUS(); return instance; }
            set { instance = value; }
        }

        public string LayTenGiaoVienChuNhiem(string maLop)
        {
            string query = @"select [dbo].fn_GetNameTeacher( @malop )";
            DataTable dt = DataProvider.Instance.ExcuteQuery(query, new object[] { maLop });
            if(dt.Rows.Count > 0)
            {
                return dt.Rows[0][0].ToString();
            }
            return string.Empty;
        }

        public DataTable LayDanhSachGiaoVien()
        {
            string query = @"SELECT [MaCanBoGiaoVien]
      ,[HoTen]
      ,[DiaChi]
      ,[SoDienThoai]
      ,[TaiKhoan]
      ,[LoaiTaiKhoan]
  FROM [QuanLy_Diem].[dbo].[CanBoGiaoVien]";
            return DataProvider.Instance.ExcuteQuery(query);
        }

        public DataTable LayDanhSachGiaoVienReport(string maLop)
        {
            string query = @"exec sb_GetListTeacherByClassID @malop";
            return DataProvider.Instance.ExcuteQuery(query, new object[] { maLop });
        }

        public DataTable LayDanhSachGiaoVien(string maGV)
        {
            string query = @"SELECT [MaCanBoGiaoVien]
      ,[HoTen]
      ,[DiaChi]
      ,[SoDienThoai]
      ,[TaiKhoan]
      ,[LoaiTaiKhoan]
  FROM [QuanLy_Diem].[dbo].[CanBoGiaoVien] where [MaCanBoGiaoVien] like '%' + @par1 +'%' or [HoTen] like '%' + @par2 + '%' or [TaiKhoan] like '%' + @par3 + '%'";
            return DataProvider.Instance.ExcuteQuery(query, new object[] { maGV, maGV, maGV});
        }

        public List<CanBoGiaoVien> LayDanhSachCanBoGiaoVien_Search(string maGV)
        {
            List<CanBoGiaoVien> list = new List<CanBoGiaoVien>();
            DataTable dt = LayDanhSachGiaoVien(maGV);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string MaGV = dt.Rows[i][0].ToString();
                    list.Add(LayThongTinGiaoVien(MaGV));
                }
            }
            return list;
        }

        public List<CanBoGiaoVien> LayDanhSachCanBoGiaoVien_List()
        {
            List<CanBoGiaoVien> list = new List<CanBoGiaoVien>();
            DataTable dt = LayDanhSachGiaoVien();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string MaGV = dt.Rows[i][0].ToString();
                    list.Add(LayThongTinGiaoVien(MaGV));
                }
            }
            return list;
        }

        public CanBoGiaoVien LayThongTinGiaoVien(string MaGiaoVien)
        {
            string query = @"Select [MaCanBoGiaoVien]
      ,[HoTen]
      ,[DiaChi]
      ,[SoDienThoai]
      ,[TaiKhoan]
      ,[MatKhau]
      ,[LoaiTaiKhoan] from CanBoGiaoVien where MaCanBoGiaoVien = @magv";
            DataTable dt = DataProvider.Instance.ExcuteQuery(query, new object[] { MaGiaoVien });
            CanBoGiaoVien giaoVien = new CanBoGiaoVien();
            giaoVien.MaCanBoGiaoVien = dt.Rows[0][0].ToString();
            giaoVien.HoTen = dt.Rows[0][1].ToString();
            giaoVien.DiaChi = dt.Rows[0][2].ToString();
            giaoVien.SoDienThoai = dt.Rows[0][3].ToString();
            giaoVien.TaiKhoan = dt.Rows[0][4].ToString();
            giaoVien.MatKhau = dt.Rows[0][5].ToString();
            giaoVien.LoaiTaiKhoan = int.Parse(dt.Rows[0][6].ToString());
            return giaoVien;
        }

        public int KiemTraDangNhap(string taiKhoan, string matKhau, out CanBoGiaoVien giaoVien)
        {
            string query = @"select * from CanBoGiaoVien where TaiKhoan = @taikhoan";
            DataTable temp = DataProvider.Instance.ExcuteQuery(query, new object[] { taiKhoan });
            if (temp.Rows.Count > 0)
            {
                //Co tai khoan
                string magv = temp.Rows[0][0].ToString();
                CanBoGiaoVien t = LayThongTinGiaoVien(magv);
                if (t.MatKhau.Equals(matKhau))
                {
                    //Dung mat khau, dang nhap thanh cong
                    giaoVien = t;
                    return 1;
                }
                else
                {
                    //Sai mat khau
                    giaoVien = null;
                    return 0;
                }
            }
            else
            {
                //Khong co tai khoan trong csdl
                giaoVien = null;
                return -1;
            }
        }

        public bool KiemTraTonTai(string maGV)
        {
            string query = @"select * from [dbo].[CanBoGiaoVien] where [MaCanBoGiaoVien] = @magv";
            DataTable dt = DataProvider.Instance.ExcuteQuery(query, new object[] { maGV });
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string GetNextTeacherID()
        {
            return DataProvider.Instance.GetValueFunction("select [dbo].[FN_GetTeacherIDNext]()");
        }

        public int ThemGiaoVien(CanBoGiaoVien giaoVien)
        {
            string query = @"INSERT INTO [dbo].[CanBoGiaoVien] ( [MaCanBoGiaoVien] , [HoTen] , [DiaChi] , [SoDienThoai] , [TaiKhoan] , [MatKhau] , [LoaiTaiKhoan] ) VALUES ( @magv , @tengv , @diachi , @sdt , @taikhoan , @matkhau , @loai )";
            return DataProvider.Instance.ExcuteNonQuery(query, new object[] { giaoVien.MaCanBoGiaoVien, giaoVien.HoTen, giaoVien.DiaChi, giaoVien.SoDienThoai, giaoVien.TaiKhoan, giaoVien.MatKhau, giaoVien.LoaiTaiKhoan });
        }

        public int SuaThongTinGiaoVien(CanBoGiaoVien giaoVien)
        {
            string query = @"sb_UpdateTeacher @magv , @tengv , @diachi , @sdt  , @taikhoan , @matkhau , @loai";
            return DataProvider.Instance.ExcuteNonQuery(query, new object[] { giaoVien.MaCanBoGiaoVien, giaoVien.HoTen, giaoVien.DiaChi, giaoVien.SoDienThoai, giaoVien.TaiKhoan, giaoVien.MatKhau, giaoVien.LoaiTaiKhoan });
        }

        public int XoaGiaoVien(string magv)
        {
            string query = @"delete CanBoGiaoVien where MaCanBoGiaoVien = @magv";
            return DataProvider.Instance.ExcuteNonQuery(query, new object[] { magv });
        }
    }
}
