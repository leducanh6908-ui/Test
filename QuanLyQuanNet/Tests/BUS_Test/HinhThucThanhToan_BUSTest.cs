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
    public class HinhThucThanhToan_BUSTest
    {
        private HinhThucThanhToanBus_Wrapper bus;

        [SetUp]
        public void Setup() => bus = new HinhThucThanhToanBus_Wrapper();

        [Test]
        public void HT01_GetAll_ReturnsInitialData()
        {
            var result = bus.GetAll();
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.That(result[0].TenHinhThuc, Is.EqualTo("Tiền mặt"));
        }

        [Test]
        public void HT02_Add_Valid_Success()
        {
            var ht = new HinhThucThanhToan_DTO("HT03", "Quét mã QR");
            Assert.That(bus.Add(ht), Is.True);
            Assert.That(bus.GetAll(), Has.Count.EqualTo(3));
        }

        [Test]
        public void HT03_Add_MissingMa_Fail()
        {
            var ht = new HinhThucThanhToan_DTO("", "Momo");
            Assert.That(bus.Add(ht), Is.False);
        }

        [Test]
        public void HT04_Add_MissingTen_Fail()
        {
            var ht = new HinhThucThanhToan_DTO("HT04", "");
            Assert.That(bus.Add(ht), Is.False);
        }

        [Test]
        public void HT05_Add_DuplicateMa_Fail()
        {
            var ht = new HinhThucThanhToan_DTO("HT01", "Tiền mặt mới");
            Assert.That(bus.Add(ht), Is.False);
        }

        [Test]
        public void HT06_Update_Valid_Success()
        {
            var ht = new HinhThucThanhToan_DTO("HT02", "Chuyển khoản ngân hàng");
            Assert.That(bus.Update(ht), Is.True);
            Assert.That(bus.GetAll().Find(x => x.MaHinhThuc == "HT02")?.TenHinhThuc, Is.EqualTo("Chuyển khoản ngân hàng"));
        }

        [Test]
        public void HT07_Update_MissingTen_Fail()
        {
            var ht = new HinhThucThanhToan_DTO("HT01", "");
            Assert.That(bus.Update(ht), Is.False);
        }

        [Test]
        public void HT08_Update_NotExist_Fail()
        {
            var ht = new HinhThucThanhToan_DTO("HT99", "ZaloPay");
            Assert.That(bus.Update(ht), Is.False);
        }

        [Test]
        public void HT09_Delete_Valid_Success()
        {
            Assert.That(bus.Delete("HT02"), Is.True);
            Assert.That(bus.GetAll(), Has.Count.EqualTo(1));
        }

        [Test]
        public void HT10_Delete_NotExist_Fail()
        {
            Assert.That(bus.Delete("HT99"), Is.False);
        }

        [Test]
        public void HT11_Search_ByMa_Found()
        {
            var result = bus.Search("HT01");
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0].TenHinhThuc, Is.EqualTo("Tiền mặt"));
        }

        [Test]
        public void HT12_Search_ByTen_Found()
        {
            var result = bus.Search("tiền mặt");
            Assert.That(result, Has.Count.EqualTo(1));
        }

        [Test]
        public void HT13_Search_NotFound_ReturnsEmpty()
        {
            Assert.That(bus.Search("Momo"), Is.Empty);
        }

        [Test]
        public void HT14_Search_EmptyKeyword_ReturnsAll()
        {
            Assert.That(bus.Search(""), Has.Count.EqualTo(2));
        }
    }
}
