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
    public class MayTinh_BUSTest
    {
        private MayTinhBus_Wrapper bus;

        [SetUp]
        public void Setup() => bus = new MayTinhBus_Wrapper();

        [Test]
        public void MT01_LayDanhSach_Returns3Records()
        {
            Assert.That(bus.LayDanhSach(), Has.Count.EqualTo(3));
        }

        [Test]
        public void MT02_LayMayTheoMa_Valid()
        {
            var may = bus.LayMayTheoMa("PC001");
            Assert.That(may, Is.Not.Null);
            Assert.That(may.TenMay, Is.EqualTo("Máy 01"));
            Assert.That(may.MaLoaiMay, Is.EqualTo("LM01"));
        }

        [Test]
        public void MT03_Them_Valid_Success()
        {
            var mt = new MayTinh_DTO("PC004", "Máy 04", "LM02", "TT01");
            Assert.That(bus.Them(mt), Is.True);
            Assert.That(bus.LayDanhSach(), Has.Count.EqualTo(4));
        }

        [Test]
        public void MT04_Them_MissingMa_Fail()
        {
            var mt = new MayTinh_DTO("", "Máy X", "LM01", "TT01");
            Assert.That(bus.Them(mt), Is.False);
        }

        [Test]
        public void MT05_Them_MissingMaLoaiMay_Fail()
        {
            var mt = new MayTinh_DTO("PC005", "Máy 05", null, "TT01");
            Assert.That(bus.Them(mt), Is.False);
        }

        [Test]
        public void MT06_Them_DuplicateMa_Fail()
        {
            var mt = new MayTinh_DTO("PC001", "Máy VIP", "LM02", "TT02");
            Assert.That(bus.Them(mt), Is.False);
        }

        [Test]
        public void MT07_Sua_TenMayVaLoaiMay_Success()
        {
            var mt = new MayTinh_DTO("PC002", "Máy Gaming", "LM02", "TT02");
            Assert.That(bus.Sua(mt), Is.True);
            var updated = bus.LayMayTheoMa("PC002");
            Assert.That(updated.TenMay, Is.EqualTo("Máy Gaming"));
            Assert.That(updated.MaLoaiMay, Is.EqualTo("LM02"));
        }

        [Test]
        public void MT08_CapNhatTrangThai_Success()
        {
            Assert.That(bus.CapNhatTrangThaiMay("PC001", "TT02"), Is.True);
            Assert.That(bus.LayMayTheoMa("PC001").MaTrangThai, Is.EqualTo("TT02"));
        }

        [Test]
        public void MT09_Xoa_Valid_Success()
        {
            Assert.That(bus.Xoa("PC003"), Is.True);
            Assert.That(bus.LayDanhSach(), Has.Count.EqualTo(2));
        }

        [Test]
        public void MT10_Xoa_NotExist_Fail()
        {
            Assert.That(bus.Xoa("PC999"), Is.False);
        }
    }
}
