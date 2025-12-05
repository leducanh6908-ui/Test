using DTO_QuanLyQuanNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.DAL_Interface
{
    public interface ILoaiMayDAL_Test
    {
        List<LoaiMay_DTO> LayDanhSach();
        bool ThemLoaiMay(LoaiMay_DTO loai);
        bool SuaLoaiMay(LoaiMay_DTO loai);
        bool XoaLoaiMay(string maLoai);
    }
}
