/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) [MaCanBoGiaoVien]
      ,[HoTen]
      ,[DiaChi]
      ,[SoDienThoai]
      ,[TaiKhoan]
      ,[MatKhau]
      ,[LoaiTaiKhoan]
  FROM [QuanLy_Diem].[dbo].[CanBoGiaoVien]

  delete Lop where MaLop = 'Lop0000002'


  go
  create trigger trg_DeleteLop
  on Lop
  for Delete
  as
  begin
  declare @malop varchar(10)
  select @malop = MaLop from deleted
	if(exists(select * from PhanCongGiangDay where MaLop = @malop))
	begin
		delete PhanCongGiangDay where MaLop = @malop
		--delete HoSoHocSinh where MaLop = @malop
	end
  end

  select * from PhanCongGiangDay
  select * from  HoSoHocSinh where MaLop = 'Lop0000002'

  delete PhanCongGiangDay where MaLop='Lop0000002'
  select * from PhanCongGiangDay where MaLop='Lop0000002'

  go
   create trigger trg_DeleteGiaoVien
  on CanBoGiaoVien
  for Delete
  as
  begin
  declare @magv varchar(10)
  select @magv = MaCanBoGiaoVien from deleted
	if(exists(select * from PhanCongGiangDay where MaLop = @magv))
	begin
		delete PhanCongGiangDay where MaCanBoGiaoVien = @magv
		delete Lop where GiaoVienChuNhiem = @magv
	end
  end

    select * from CanBoGiaoVien where MaCanBoGiaoVien = 'GV00000001'
  select * from PhanCongGiangDay where MaCanBoGiaoVien = 'GV00000001'
  select * from Lop where GiaoVienChuNhiem = 'GV00000001'

  delete CanBoGiaoVien where MaCanBoGiaoVien = 'GV00000001'

    go
   create trigger trg_DeleteMonHoc
  on MonHoc
  for Delete
  as
  begin
  declare @maMon int
  select @maMon = MaMon from deleted
	if(exists(select * from PhanCongGiangDay where MaMon = @maMon))
	begin
		delete PhanCongGiangDay where MaMon = @maMon
		delete Diem where MaMonHoc = @maMon
	end
  end

select * from MonHoc where MaMon = 1
select * from PhanCongGiangDay where MaMon = 1
select * from Diem where MaMonHoc = 1
delete MonHoc where MaMon = 1

select IDENT_CURRENT('MonHoc')

go
create proc sb_UpdateScorceOnInsertStudent
@maHS varchar(10)
as
begin
	declare @mamon int = 0
	while(@mamon <= IDENT_CURRENT('MonHoc'))
	begin
		if(exists(select * from MonHoc where MaMon = @mamon))
		begin
			INSERT INTO [dbo].[Diem]
           ([MaHocSinh]
           ,[MaMonHoc]
           ,[DiemTB_Ky1]
           ,[DiemTB_Ky2])
			VALUES
           (@maHS, @mamon, 0, 0)
		end
		set @mamon = @mamon + 1
		
	end
	
end

go

create trigger trg_InsertStudent
on HoSoHocSinh
for insert
as
begin
	declare @mahs varchar(10)
	select @mahs = MaHocSinh from inserted
	exec sb_UpdateScorceOnInsertStudent @mahs
end


create trigger tgg_insertMonHoc
  on [dbo].[MonHoc]
  for insert
  as
  begin
  declare @Idx int
	declare @MaHS varchar(50) = 'HS00000001'
	set @Idx = 1;
	declare @maMon int
	declare @total int
	select @maMon = MaMon from inserted
	select @total = count(*) from HoSoHocSinh
	while(@Idx < @total)
	begin
		if(exists(select * from HoSoHocSinh where MaHocSinh = @MaHS))
		begin
			INSERT INTO [dbo].[Diem]
           ([MaHocSinh]
           ,[MaMonHoc]
           ,[DiemTB_Ky1]
           ,[DiemTB_Ky2])
			VALUES
           (@MaHS, @maMon, 0, 0)
		end
		
		set @Idx = @Idx + 1
		set @MaHS = 'HS'+REPLICATE('0', 8-LEN(cast(@Idx as varchar))) + CAST(@Idx as varchar)
	end
  end