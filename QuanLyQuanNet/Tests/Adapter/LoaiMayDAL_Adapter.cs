using DAL_QuanLyQuanNet;
using DTO_QuanLyQuanNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.DAL_Interface;

namespace Tests.Adapter
{
    public class LoaiMayDAL_Adapter : ILoaiMayDAL_Test
    {
        public List<LoaiMay_DTO> LayDanhSach() => LoaiMay_DAL.LayDanhSach();

        public bool ThemLoaiMay(LoaiMay_DTO loai) => LoaiMay_DAL.ThemLoaiMay(loai);

        public bool SuaLoaiMay(LoaiMay_DTO loai) => LoaiMay_DAL.SuaLoaiMay(loai);

        public bool XoaLoaiMay(string maLoai) => LoaiMay_DAL.XoaLoaiMay(maLoai);
    }
}
