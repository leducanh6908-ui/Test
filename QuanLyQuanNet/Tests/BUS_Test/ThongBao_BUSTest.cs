using DTO_QuanLyQuanNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Wrapper;

namespace Tests.BUS_Test
{
    [TestFixture]
    public class ThongBao_BUSTest
    {
        private ThongBaoBus_Wrapper bus;

        [SetUp]
        public void Setup() => bus = new ThongBaoBus_Wrapper();

        [Test] public void TB01_LayDanhSach_Returns3Records() => Assert.That(bus.LayDanhSach(), Has.Count.EqualTo(3));

        [Test]
        public void TB02_LayDanhSach_Search_TB001_Found()
        {
            var result = bus.LayDanhSach("TB001");
            Assert.That(result, Has.Count.EqualTo(1));
        }

        [Test]
        public void TB03_LayDanhSach_Search_het_gio_Found()
        {
            var result = bus.LayDanhSach("hết giờ");
            Assert.That(result, Has.Count.AtLeast(1));
        }

        [Test]
        public void TB04_Them_Valid_Success()
        {
            var tb = new ThongBao_DTO
            {
                MaThongBao = "TB004",
                MaPhien = "PH004",
                MaNhanVien = "NV003",
                ThoiGianThongBao = DateTime.Now,
                TrangThaiDoc = false,
                NoiDung = "Máy PC010 bị mất mạng"
            };

            Assert.That(bus.Them(tb), Is.True);
            Assert.That(bus.LayDanhSach(), Has.Count.EqualTo(4));
        }

        [Test]
        public void TB05_Them_MissingMa_Fail()
        {
            var tb = new ThongBao_DTO { MaPhien = "PH005", NoiDung = "Test" };
            Assert.That(bus.Them(tb), Is.False);
        }

        [Test]
        public void TB06_Them_DuplicateMa_Fail()
        {
            var tb = new ThongBao_DTO
            {
                MaThongBao = "TB001",
                MaPhien = "PH999",
                NoiDung = "Trùng mã"
            };
            Assert.That(bus.Them(tb), Is.False);
        }

        [Test]
        public void TB07_CapNhat_Valid_Success()
        {
            var tb = new ThongBao_DTO
            {
                MaThongBao = "TB002",
                TrangThaiDoc = true,
                NoiDung = "Đã xử lý xong"
            };

            Assert.That(bus.CapNhat(tb), Is.True);
            Assert.That(bus.LayDanhSach("TB002")[0].TrangThaiDoc, Is.True);
        }

        [Test]
        public void TB08_Xoa_Valid_Success()
        {
            Assert.That(bus.Xoa("TB003"), Is.True);
            Assert.That(bus.LayDanhSach(), Has.Count.EqualTo(2));
        }

        [Test]
        public void TB09_KiemTraTonTai_Exist_ReturnsTrue()
        {
            Assert.That(bus.KiemTraTonTai("TB001"), Is.True);
        }

        [Test]
        public void TB10_KiemTraTonTai_NotExist_ReturnsFalse()
        {
            Assert.That(bus.KiemTraTonTai("TB999"), Is.False);
        }
    }
}
