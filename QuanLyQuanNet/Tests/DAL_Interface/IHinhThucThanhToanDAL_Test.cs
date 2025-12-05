using DTO_QuanLyQuanNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.DAL_Interface
{
    public interface IHinhThucThanhToanDAL_Test
    {
        List<HinhThucThanhToan_DTO> LayTatCa();
        bool Them(HinhThucThanhToan_DTO dv);
        bool Sua(HinhThucThanhToan_DTO dv);
        bool Xoa(string ma);
    }
}
