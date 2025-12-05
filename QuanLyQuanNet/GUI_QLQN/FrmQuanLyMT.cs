using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL_QuanLyQuanNet;
using GUI_QLQN;
using DTO_QuanLyQuanNet;


namespace GUI_QLQN
{
    public partial class FrmQuanLyMT : Form
    {
        private List<MayTinh_DTO> _dsMayTinh = new List<MayTinh_DTO>();
        public FrmQuanLyMT()
        {
            InitializeComponent();
            cboSoCot.Items.Clear();
            for (int i = 1; i <= 8; i++)
                cboSoCot.Items.Add(i.ToString());

            cboSoCot.SelectedIndex = 2; // Mặc định 3 cột
            LoadPanelMay(3);
        }
        private void LoadData()
        {
            _dsMayTinh = MayTinh_BUS.LayDanhSach();

            var danhSachTrangThai = LoaiTrangThai_BUS.LayTatCa();
            var danhSachLoaiMay = LoaiMay_BUS.LayDanhSach();

            var view = _dsMayTinh.Select(mt => new
            {
                mt.MaMay,
                mt.TenMay,
                MaLoaiMay = mt.MaLoaiMay,
                MaTrangThai = mt.MaTrangThai,
                TenLoaiMay = danhSachLoaiMay.FirstOrDefault(lm => lm.MaLoaiMay == mt.MaLoaiMay)?.TenLoaiMay ?? "",
                TenTrangThai = danhSachTrangThai.FirstOrDefault(tt => tt.MaTrangThai == mt.MaTrangThai)?.TenTrangThai ?? ""
            }).ToList();

            dgvQLMT.DataSource = view;
            dgvQLMT.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvQLMT.ReadOnly = true;
            dgvQLMT.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            DoiHeaderTiengViet();
        }

        private void LoadPanelMay(int soCot)
        {
            tableLayoutPanel1.Controls.Clear();
            tableLayoutPanel1.ColumnCount = soCot;
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;

            var dsMay = MayTinh_BUS.LayDanhSach()
                .Where(mt => mt.MaTrangThai != "TT02") // Ẩn máy tạm dừng
                .ToList();

            int col = 0, row = 0;
            tableLayoutPanel1.RowStyles.Clear();
            tableLayoutPanel1.ColumnStyles.Clear();
            for (int i = 0; i < soCot; i++)
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / soCot));

            foreach (var may in dsMay)
            {
                if (col == soCot)
                {
                    col = 0;
                    row++;
                    tableLayoutPanel1.RowCount = row + 1;
                    tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                }
                var uc = new ucTinhTrangMay();
                uc.MaMay = may.MaMay;
                uc.SetTenMay(may.TenMay);
                uc.CapNhatTrangThai(may.MaTrangThai);
                uc.Size = new Size(280, 100);

                tableLayoutPanel1.Controls.Add(uc, col, row);
                col++;
            }
        }

        private void DoiHeaderTiengViet()
        {
            dgvQLMT.Columns["MaMay"].HeaderText = "Mã Máy";
            dgvQLMT.Columns["TenMay"].HeaderText = "Tên Máy";
            dgvQLMT.Columns["TenLoaiMay"].HeaderText = "Loại Máy";
            dgvQLMT.Columns["TenTrangThai"].HeaderText = "Trạng Thái";

            dgvQLMT.Columns["MaLoaiMay"].Visible = false; // Ẩn cột MaLoaiMay
            dgvQLMT.Columns["MaTrangThai"].Visible = false; // Ẩn cột MaTrangThai
        }
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMM.Clear();
            txtTM.Clear();
            txtTKMT.Clear();

            if (cboLoaiMay.Items.Count > 0)
                cboLoaiMay.SelectedIndex = 0;
            if (cboMLM.Items.Count > 0)
                cboMLM.SelectedIndex = 0;

            LoadData();
        }
        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            string tuKhoa = txtTKMT.Text.Trim().ToLower();
            var ds = MayTinh_BUS.LayDanhSach()
                .Where(mt => mt.MaMay.ToLower().Contains(tuKhoa))
                .ToList();

            dgvQLMT.DataSource = ds;
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            string ma = txtMM.Text.Trim();
            if (MayTinh_BUS.Xoa(ma))
            {
                MessageBox.Show("🗑️ Xóa thành công!");
                LoadData();
            }
            else
            {
                MessageBox.Show("❌ Xóa thất bại. Mã có thể đang được dùng.");
            }
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            var may = new MayTinh_DTO(
    txtMM.Text.Trim(),
    txtTM.Text.Trim(),
    cboLoaiMay.SelectedValue.ToString(), // <-- Đây là combobox loại máy
    cboMLM.SelectedValue.ToString()
   );

            if (MayTinh_BUS.Sua(may))
            {
                MessageBox.Show("✅ Sửa thành công!");
                LoadData();
            }
            else
            {
                MessageBox.Show("❌ Sửa thất bại.");
            }
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            var may = new MayTinh_DTO(
    txtMM.Text.Trim(),
    txtTM.Text.Trim(),
    cboLoaiMay.SelectedValue.ToString(), // <-- Đây là combobox loại máy
    cboMLM.SelectedValue.ToString()      // <-- Đây là combobox trạng thái
);

  


            if (MayTinh_BUS.Them(may))
            {
                MessageBox.Show("✅ Thêm thành công!");
                LoadData();
            }
            else
            {
                MessageBox.Show("❌ Thêm thất bại. Kiểm tra lại dữ liệu.");
            }
        }
        private void LoadComboBoxTrangThai()
        {
            cboMLM.DataSource = LoaiTrangThai_BUS.LayTatCa(); // Load danh sách trạng thái
            cboMLM.DisplayMember = "TenTrangThai";
            cboMLM.ValueMember = "MaTrangThai";
        }
        private void FrmQuanLyMT_Load(object sender, EventArgs e)
        {
            LoadComboBoxLoaiMay();
            LoadComboBoxTrangThai();
            LoadData();
        }
        private void LoadComboBoxLoaiMay()
        {
            cboLoaiMay.DataSource = LoaiMay_BUS.LayDanhSach(); // Phải có BUS đã viết sẵn
            cboLoaiMay.DisplayMember = "TenLoaiMay";
            cboLoaiMay.ValueMember = "MaLoaiMay";
        }


        private void dgvQLMT_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvQLMT.Rows[e.RowIndex].DataBoundItem != null)
            {
                var row = dgvQLMT.Rows[e.RowIndex];
                txtMM.Text = row.Cells["MaMay"].Value?.ToString();
                txtTM.Text = row.Cells["TenMay"].Value?.ToString();

                // Lấy MaLoaiMay và MaTrangThai từ dòng được chọn
                string maLoaiMay = row.Cells["MaLoaiMay"].Value?.ToString();
                string maTrangThai = row.Cells["MaTrangThai"].Value?.ToString();

                if (!string.IsNullOrEmpty(maLoaiMay))
                    cboLoaiMay.SelectedValue = maLoaiMay;
                if (!string.IsNullOrEmpty(maTrangThai))
                    cboMLM.SelectedValue = maTrangThai;
            }
        }

        private void cboSoCot_SelectedIndexChanged(object sender, EventArgs e)
        {
            int soCot = int.Parse(cboSoCot.SelectedItem.ToString());
            LoadPanelMay(soCot);
        }
    }
}

