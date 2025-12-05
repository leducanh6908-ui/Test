using DTO_QuanLyQuanNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.DAL_Interface
{
    public interface IMayTinhDAL_Test
    {
        List<MayTinh_DTO> LayDanhSach();
        MayTinh_DTO LayMayTheoMa(string ma);
        bool ThemMay(MayTinh_DTO mt);
        bool SuaMay(MayTinh_DTO mt);
        bool XoaMay(string ma);
        bool CapNhatTrangThaiMay(string maMay, string maTrangThai);
    }
}
