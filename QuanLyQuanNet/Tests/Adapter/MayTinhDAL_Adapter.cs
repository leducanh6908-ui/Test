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
    public class MayTinhDAL_Adapter : IMayTinhDAL_Test
    {
        public List<MayTinh_DTO> LayDanhSach() => MayTinh_DAL.LayDanhSach();

        public MayTinh_DTO LayMayTheoMa(string ma) => MayTinh_DAL.LayMayTheoMa(ma);

        public bool ThemMay(MayTinh_DTO mt) => MayTinh_DAL.ThemMay(mt);

        public bool SuaMay(MayTinh_DTO mt) => MayTinh_DAL.SuaMay(mt);

        public bool XoaMay(string ma) => MayTinh_DAL.XoaMay(ma);

        public bool CapNhatTrangThaiMay(string maMay, string maTrangThai)
            => MayTinh_DAL.CapNhatTrangThaiMay(maMay, maTrangThai);
    }
}
