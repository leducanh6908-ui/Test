using System.Collections.Generic;
using DTO_QuanLyQuanNet;
using DAL_QuanLyQuanNet;
using System.Linq;


namespace BLL_QuanLyQuanNet
{
    public class ChucVu_BUS
    {

        public static List<ChucVu_DTO> LayTatCa() => ChucVu_DAL.LayTatCa();

        public static List<ChucVuViewModel> GetAllView()
        {
            var dsCV = ChucVu_DAL.LayTatCa();
            var dsTrangThai = LoaiTrangThai_BUS.LayTatCa(); // trạng thái

            var result = from cv in dsCV
                         join tt in dsTrangThai on cv.MaTrangThai equals tt.MaTrangThai
                         select new ChucVuViewModel
                         {
                             MaChucVu = cv.MaChucVu,
                             TenChucVu = cv.TenChucVu,
                             MaTrangThai = cv.MaTrangThai,
                             TenTrangThai = tt.TenTrangThai
                         };

            return result.ToList();
        }

        public static bool Them(ChucVu_DTO cv) => ChucVu_DAL.Them(cv);
        public static bool CapNhat(ChucVu_DTO cv) => ChucVu_DAL.CapNhat(cv);
        public static bool Xoa(string ma) => ChucVu_DAL.Xoa(ma);
    }
}