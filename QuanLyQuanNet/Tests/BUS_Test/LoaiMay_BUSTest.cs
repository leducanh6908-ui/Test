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
    public class LoaiMay_BUSTest
    {
        private LoaiMayBus_Wrapper bus;

        [SetUp]
        public void Setup() => bus = new LoaiMayBus_Wrapper();

        [Test] public void LM01_LayDanhSach_Returns3Active() => Assert.That(bus.LayDanhSach(), Has.Count.EqualTo(3));

        [Test]
        public void LM02_Them_Valid_Success()
        {
            var loai = new LoaiMay_DTO("LM04", "Máy Ultra", "TT01");
            Assert.That(bus.Them(loai), Is.True);
            Assert.That(bus.LayDanhSach(), Has.Count.EqualTo(4));
        }

        [Test]
        public void LM03_Them_MissingMa_Fail()
        {
            var loai = new LoaiMay_DTO("", "Máy Test", "TT01");
            Assert.That(bus.Them(loai), Is.False);
        }

        [Test]
        public void LM04_Them_MissingTen_Fail()
        {
            var loai = new LoaiMay_DTO("LM05", "", "TT01");
            Assert.That(bus.Them(loai), Is.False);
        }

        [Test]
        public void LM05_Them_DuplicateMa_Fail()
        {
            var loai = new LoaiMay_DTO("LM01", "Máy VIP 2", "TT01");
            Assert.That(bus.Them(loai), Is.False);
        }

        [Test]
        public void LM06_Sua_Valid_Success()
        {
            var loai = new LoaiMay_DTO("LM02", "Máy VIP Pro", "TT01");
            Assert.That(bus.Sua(loai), Is.True);
            Assert.That(bus.LayDanhSach().Find(x => x.MaLoaiMay == "LM02")?.TenLoaiMay, Is.EqualTo("Máy VIP Pro"));
        }

        [Test]
        public void LM07_Sua_NotExist_Fail()
        {
            var loai = new LoaiMay_DTO("LM99", "Không tồn tại", "TT01");
            Assert.That(bus.Sua(loai), Is.False);
        }

        [Test]
        public void LM08_Sua_AlreadyDeleted_Fail()
        {
            bus.Xoa("LM03");
            var loai = new LoaiMay_DTO("LM03", "Máy Cũ", "TT01");
            Assert.That(bus.Sua(loai), Is.False);
        }

        [Test]
        public void LM09_Xoa_Valid_SoftDelete()
        {
            Assert.That(bus.Xoa("LM01"), Is.True);
            Assert.That(bus.LayDanhSach(), Has.Count.EqualTo(2)); // Bị loại khỏi danh sách
        }

        [Test]
        public void LM10_Xoa_NotExist_Fail()
        {
            Assert.That(bus.Xoa("LM99"), Is.False);
        }

        [Test]
        public void LM11_Xoa_AlreadyDeleted_Fail()
        {
            bus.Xoa("LM02");
            Assert.That(bus.Xoa("LM02"), Is.False); // Xóa lần 2 → fail
        }
    }
}
