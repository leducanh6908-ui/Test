using DAL_QuanLyQuanNet;
using DTO_QuanLyQuanNet;
using Tests.DAL_Interface;

namespace Tests.Adapters
{
    public class HinhThucThanhToanDAL_Adapter : IHinhThucThanhToanDAL_Test
    {
        public List<HinhThucThanhToan_DTO> LayTatCa() => HinhThucThanhToan_DAL.LayTatCa();
        public bool Them(HinhThucThanhToan_DTO dv) => HinhThucThanhToan_DAL.Them(dv);
        public bool Sua(HinhThucThanhToan_DTO dv) => HinhThucThanhToan_DAL.Sua(dv);
        public bool Xoa(string ma) => HinhThucThanhToan_DAL.Xoa(ma);
    }
}
