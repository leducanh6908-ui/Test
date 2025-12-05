using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI_QLQN
{
    public partial class ucTinhTrangMay : UserControl
    {
        public event EventHandler<string> MaySelected;
        public string MaMay { get; set; }
        public bool DangSuDung { get; set; }

        public ucTinhTrangMay()
        {
            InitializeComponent();
        }

        public void CapNhatTrangThai(string maTrangThai)
        {
            maTrangThai = maTrangThai?.Trim().ToUpper(); // xử lý null, khoảng trắng, chữ thường

            switch (maTrangThai)
            {
                case "TT03": // Sẵn sàng
                    this.BorderStyle = BorderStyle.FixedSingle;
                    btnMay.FillColor = Color.DeepSkyBlue;
                    DangSuDung = false;
                    break;

                case "TT01": // Hoạt động
                    this.BorderStyle = BorderStyle.FixedSingle;
                    DangSuDung = true;
                    btnMay.FillColor = Color.Red;
                    break;

                case "TT02": // Không hoạt động
                case "TT04": // Bảo trì
                    this.BorderStyle = BorderStyle.FixedSingle;
                    btnMay.FillColor = Color.Gray;
                    DangSuDung = true;
                    break;

                default:
                    this.BorderStyle = BorderStyle.FixedSingle;
                    btnMay.FillColor = Color.LightGray;
                    DangSuDung = true;
                    break;
            }
        }

        public void SetTenMay(string tenMay)
        {
            btnMay.Text = tenMay;
        }

        private void btnMay_Click(object sender, EventArgs e)
        {
            if (!DangSuDung)
            {
                MaySelected?.Invoke(this, MaMay);
            }
        }
    }
}
