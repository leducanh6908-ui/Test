using DTO_QuanLyQuanNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Wrapper
{
    public class MayTinhBus_Wrapper
    {
        private readonly List<MayTinh_DTO> _data = new List<MayTinh_DTO>
        {
            new MayTinh_DTO("PC001", "Máy 01", "LM01", "TT01"), // LM01: Loại thường, TT01: Trống
            new MayTinh_DTO("PC002", "Máy 02", "LM02", "TT02"), // LM02: Loại VIP, TT02: Đang dùng
            new MayTinh_DTO("PC003", "Máy 03", "LM01", "TT03")  // TT03: Bảo trì
        };

        public List<MayTinh_DTO> LayDanhSach() => _data.ToList();

        public MayTinh_DTO LayMayTheoMa(string ma)
        {
            if (string.IsNullOrWhiteSpace(ma)) return null;
            return _data.FirstOrDefault(x => x.MaMay.Equals(ma, StringComparison.OrdinalIgnoreCase));
        }

        public bool Them(MayTinh_DTO mt)
        {
            if (string.IsNullOrWhiteSpace(mt.MaMay) ||
                string.IsNullOrWhiteSpace(mt.TenMay) ||
                string.IsNullOrWhiteSpace(mt.MaLoaiMay))
                return false;

            if (_data.Any(x => x.MaMay.Equals(mt.MaMay, StringComparison.OrdinalIgnoreCase)))
                return false;

            // Tạo bản sao đầy đủ, nếu MaTrangThai null thì mặc định "TT01"
            var newMt = new MayTinh_DTO(
                mt.MaMay,
                mt.TenMay,
                mt.MaLoaiMay,
                mt.MaTrangThai ?? "TT01"
            );

            _data.Add(newMt);
            return true;
        }

        public bool Sua(MayTinh_DTO mt)
        {
            if (string.IsNullOrWhiteSpace(mt.MaMay)) return false;

            var existing = _data.FirstOrDefault(x => x.MaMay == mt.MaMay);
            if (existing == null) return false;

            // Chỉ cập nhật những trường có giá trị mới
            existing.TenMay = mt.TenMay ?? existing.TenMay;
            existing.MaLoaiMay = mt.MaLoaiMay ?? existing.MaLoaiMay;
            existing.MaTrangThai = mt.MaTrangThai ?? existing.MaTrangThai;

            return true;
        }

        public bool Xoa(string ma)
        {
            if (string.IsNullOrWhiteSpace(ma)) return false;
            var mt = _data.FirstOrDefault(x => x.MaMay == ma);
            if (mt == null) return false;
            _data.Remove(mt);
            return true;
        }

        public bool CapNhatTrangThaiMay(string maMay, string maTrangThai)
        {
            if (string.IsNullOrWhiteSpace(maMay) || string.IsNullOrWhiteSpace(maTrangThai))
                return false;

            var mt = _data.FirstOrDefault(x => x.MaMay == maMay);
            if (mt == null) return false;

            mt.MaTrangThai = maTrangThai;
            return true;
        }
    }
}
