using DTO_QuanLyQuanNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Wrapper
{
    public class LoaiMayBus_Wrapper
    {
        private readonly List<LoaiMay_DTO> _data = new List<LoaiMay_DTO>
        {
            new LoaiMay_DTO("LM01", "Máy Thường", "TT01"),
            new LoaiMay_DTO("LM02", "Máy VIP", "TT01"),
            new LoaiMay_DTO("LM03", "Máy Gaming", "TT01")
        };

        public List<LoaiMay_DTO> LayDanhSach()
            => _data.Where(x => x.MaTrangThai == "TT01").ToList(); // Chỉ lấy loại đang hoạt động

        public bool Them(LoaiMay_DTO loai)
        {
            if (string.IsNullOrWhiteSpace(loai.MaLoaiMay) ||
                string.IsNullOrWhiteSpace(loai.TenLoaiMay))
                return false;

            if (_data.Any(x => x.MaLoaiMay.Equals(loai.MaLoaiMay, StringComparison.OrdinalIgnoreCase)))
                return false;

            var newLoai = new LoaiMay_DTO(loai.MaLoaiMay, loai.TenLoaiMay, "TT01");
            _data.Add(newLoai);
            return true;
        }

        public bool Sua(LoaiMay_DTO loai)
        {
            if (string.IsNullOrWhiteSpace(loai.MaLoaiMay)) return false;

            var existing = _data.FirstOrDefault(x => x.MaLoaiMay == loai.MaLoaiMay);
            if (existing == null || existing.MaTrangThai != "TT01") return false;

            if (string.IsNullOrWhiteSpace(loai.TenLoaiMay)) return false;

            existing.TenLoaiMay = loai.TenLoaiMay;
            return true;
        }

        public bool Xoa(string maLoai)
        {
            var loai = _data.FirstOrDefault(x => x.MaLoaiMay == maLoai && x.MaTrangThai == "TT01");
            if (loai == null) return false;

            loai.MaTrangThai = "TT02"; // Soft delete
            return true;
        }
    }
}
