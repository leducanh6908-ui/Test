using DTO_QuanLyQuanNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Wrapper
{
    public class ThongBaoBus_Wrapper
    {
        private readonly List<ThongBao_DTO> _data = new List<ThongBao_DTO>
        {
            new ThongBao_DTO
            {
                MaThongBao = "TB001",
                MaPhien = "PH001",
                MaNhanVien = "NV001",
                ThoiGianThongBao = DateTime.Now.AddMinutes(-30),
                TrangThaiDoc = false,
                NoiDung = "Phiên PH001 sắp hết giờ (còn 5 phút)"
            },
            new ThongBao_DTO
            {
                MaThongBao = "TB002",
                MaPhien = "PH002",
                MaNhanVien = "NV002",
                ThoiGianThongBao = DateTime.Now.AddMinutes(-10),
                TrangThaiDoc = true,
                NoiDung = "Phiên PH002 đã hết giờ"
            },
            new ThongBao_DTO
            {
                MaThongBao = "TB003",
                MaPhien = "PH003",
                MaNhanVien = "NV001",
                ThoiGianThongBao = DateTime.Now.AddHours(-2),
                TrangThaiDoc = false,
                NoiDung = "Máy PC005 bị treo, cần kiểm tra"
            }
        };

        public List<ThongBao_DTO> LayDanhSach(string keyword = "")
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return _data.ToList();

            keyword = keyword.ToLower();
            return _data.Where(x =>
                x.MaThongBao.ToLower().Contains(keyword) ||
                x.MaPhien.ToLower().Contains(keyword) ||
                x.NoiDung.ToLower().Contains(keyword)
            ).ToList();
        }

        public bool Them(ThongBao_DTO tb)
        {
            if (string.IsNullOrWhiteSpace(tb.MaThongBao) ||
                string.IsNullOrWhiteSpace(tb.MaPhien) ||
                string.IsNullOrWhiteSpace(tb.MaNhanVien) ||
                string.IsNullOrWhiteSpace(tb.NoiDung))
                return false;

            if (_data.Any(x => x.MaThongBao == tb.MaThongBao))
                return false;

            _data.Add(tb);
            return true;
        }

        public bool CapNhat(ThongBao_DTO tb)
        {
            var existing = _data.FirstOrDefault(x => x.MaThongBao == tb.MaThongBao);
            if (existing == null) return false;

            existing.MaPhien = tb.MaPhien;
            existing.MaNhanVien = tb.MaNhanVien;
            existing.ThoiGianThongBao = tb.ThoiGianThongBao;
            existing.TrangThaiDoc = tb.TrangThaiDoc;
            existing.NoiDung = tb.NoiDung;

            return true;
        }

        public bool Xoa(string maThongBao)
        {
            var tb = _data.FirstOrDefault(x => x.MaThongBao == maThongBao);
            if (tb == null) return false;
            _data.Remove(tb);
            return true;
        }

        public bool KiemTraTonTai(string maThongBao)
            => _data.Any(x => x.MaThongBao == maThongBao);
    }
}
