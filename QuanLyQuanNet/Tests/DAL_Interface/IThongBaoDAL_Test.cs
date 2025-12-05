using DTO_QuanLyQuanNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.DAL_Interface
{
    public interface IThongBaoDAL_Test
    {
        List<ThongBao_DTO> LayDanhSach(string keyword = "");
        bool Them(ThongBao_DTO tb);
        bool CapNhat(ThongBao_DTO tb);
        bool Xoa(string maThongBao);
        bool KiemTraTonTai(string maThongBao);
    }
}
