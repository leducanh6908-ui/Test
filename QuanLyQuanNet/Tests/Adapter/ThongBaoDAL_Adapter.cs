using DAL_QuanLyQuanNet;
using DTO_QuanLyQuanNet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.DAL_Interface;

namespace Tests.Adapter
{
    public class ThongBaoDAL_Adapter : IThongBaoDAL_Test
    {
        private readonly ThongBao_DAL _dal = new ThongBao_DAL();

        public List<ThongBao_DTO> LayDanhSach(string keyword = "")
        {
            DataTable dt = _dal.LayDanhSach(keyword);
            var list = new List<ThongBao_DTO>();

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new ThongBao_DTO
                {
                    MaThongBao = row["MaThongBao"].ToString(),
                    MaPhien = row["MaPhien"].ToString(),
                    MaNhanVien = row["MaNhanVien"].ToString(),
                    ThoiGianThongBao = Convert.ToDateTime(row["ThoiGianThongBao"]),
                    TrangThaiDoc = Convert.ToBoolean(row["TrangThaiDoc"]),
                    NoiDung = row["NoiDung"].ToString()
                });
            }
            return list;
        }

        public bool Them(ThongBao_DTO tb) => _dal.Them(tb);

        public bool CapNhat(ThongBao_DTO tb) => _dal.CapNhat(tb);

        public bool Xoa(string maThongBao) => _dal.Xoa(maThongBao);

        public bool KiemTraTonTai(string maThongBao) => _dal.KiemTraTonTai(maThongBao);
    }
}
