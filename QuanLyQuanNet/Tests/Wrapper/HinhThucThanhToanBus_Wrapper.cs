using DTO_QuanLyQuanNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Wrapper
{
    public class HinhThucThanhToanBus_Wrapper
    {
        private readonly List<HinhThucThanhToan_DTO> _data = new List<HinhThucThanhToan_DTO>
        {
            new HinhThucThanhToan_DTO("HT01", "Tiền mặt"),
            new HinhThucThanhToan_DTO("HT02", "Chuyển khoản")
        };

        public List<HinhThucThanhToan_DTO> GetAll() => _data.ToList();

        public bool Add(HinhThucThanhToan_DTO ht)
        {
            if (string.IsNullOrWhiteSpace(ht.MaHinhThuc) ||
                string.IsNullOrWhiteSpace(ht.TenHinhThuc))
                return false;

            if (_data.Any(x => x.MaHinhThuc.Equals(ht.MaHinhThuc, StringComparison.OrdinalIgnoreCase)))
                return false;

            _data.Add(new HinhThucThanhToan_DTO(ht.MaHinhThuc, ht.TenHinhThuc));
            return true;
        }

        public bool Update(HinhThucThanhToan_DTO ht)
        {
            var existing = _data.FirstOrDefault(x => x.MaHinhThuc == ht.MaHinhThuc);
            if (existing == null) return false;
            if (string.IsNullOrWhiteSpace(ht.TenHinhThuc)) return false;

            existing.TenHinhThuc = ht.TenHinhThuc;
            return true;
        }

        public bool Delete(string maHinhThuc)
        {
            var item = _data.FirstOrDefault(x => x.MaHinhThuc == maHinhThuc);
            if (item == null) return false;
            _data.Remove(item);
            return true;
        }

        public List<HinhThucThanhToan_DTO> Search(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword)) return GetAll();
            return _data.Where(x =>
                x.MaHinhThuc.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                x.TenHinhThuc.Contains(keyword, StringComparison.OrdinalIgnoreCase)
            ).ToList();
        }
    }
}
