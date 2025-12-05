using System;
using System.Linq;
using System.Windows.Forms;
using DTO_QuanLyQuanNet;
using BLL_QuanLyQuanNet;
using System.Collections.Generic;

namespace GUI_QLQN
{
    public partial class FrmQuanLyChucVu : Form
    {
        private List<ChucVuViewModel> _danhSachView = new List<ChucVuViewModel>();
        public FrmQuanLyChucVu()
        {
            InitializeComponent();
            LoadTrangThai();
            LoadData();
        }

        private void LoadData()
        {
            _danhSachView = ChucVu_BUS.GetAllView();
            dgvQLCV.DataSource = null;
            dgvQLCV.DataSource = _danhSachView;

            dgvQLCV.Columns["MaChucVu"].HeaderText = "Mã Chức Vụ";
            dgvQLCV.Columns["TenChucVu"].HeaderText = "Tên Chức Vụ";
            dgvQLCV.Columns["MaTrangThai"].Visible = false;
            dgvQLCV.Columns["TenTrangThai"].HeaderText = "Trạng Thái";

            dgvQLCV.ClearSelection();
        }

        private void LoadTrangThai()
        {
            cboTrangThai.DataSource = LoaiTrangThai_BUS.LayTatCa();
            cboTrangThai.DisplayMember = "TenTrangThai";
            cboTrangThai.ValueMember = "MaTrangThai";
            cboTrangThai.SelectedIndex = -1;
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtTenCV.Text))
            {
                MessageBox.Show("Tên chức vụ không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (txtTenCV.Text.Length > 50)
            {
                MessageBox.Show("Tên chức vụ không được vượt quá 50 ký tự!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (cboTrangThai.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn trạng thái!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            string maMoi = GenerateMaChucVu();

            var cv = new ChucVu_DTO
            {
                MaChucVu = maMoi,
                TenChucVu = txtTenCV.Text.Trim(),
                MaTrangThai = cboTrangThai.SelectedValue.ToString()
            };

            if (ChucVu_BUS.Them(cv))
            {
                MessageBox.Show("Thêm chức vụ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                ClearForm();
            }
            else
            {
                MessageBox.Show("Thêm thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvQLCV.CurrentRow?.DataBoundItem is ChucVuViewModel cvView)
            {
                if (!ValidateInput()) return;

                var cv = new ChucVu_DTO
                {
                    MaChucVu = cvView.MaChucVu,
                    TenChucVu = txtTenCV.Text.Trim(),
                    MaTrangThai = cboTrangThai.SelectedValue.ToString()
                };

                if (ChucVu_BUS.CapNhat(cv))
                {
                    MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvQLCV.CurrentRow?.DataBoundItem is ChucVuViewModel cvView)
            {
                var confirm = MessageBox.Show("Bạn có chắc chắn muốn xóa chức vụ này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                {
                    if (ChucVu_BUS.Xoa(cvView.MaChucVu))
                    {
                        MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                        ClearForm();
                    }
                    else
                    {
                        MessageBox.Show("Xóa thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ClearForm();
            LoadData();
        }

        private void ClearForm()
        {
            txtMaCV.Text = GenerateMaChucVu(); // ⚡ Tự tạo mã mới mỗi lần làm mới
            txtTenCV.Clear();
            cboTrangThai.SelectedIndex = -1;

            txtMaCV.Enabled = true;
            dgvQLCV.ClearSelection(); // bỏ chọn dòng đang chọn (nếu có)
        }

        public static string GenerateMaChucVu()
        {
            var danhSach = ChucVu_BUS.LayTatCa();
            int max = 0;

            foreach (var cv in danhSach)
            {
                if (cv.MaChucVu.StartsWith("CV") && int.TryParse(cv.MaChucVu.Substring(2), out int so))
                {
                    if (so > max) max = so;
                }
            }

            return "CV" + (max + 1).ToString("D2"); // VD: CV001
        }


        private void dgvChucVu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvQLCV.Rows[e.RowIndex].DataBoundItem is ChucVuViewModel cv)
            {
                txtMaCV.Text = cv.MaChucVu;
                txtTenCV.Text = cv.TenChucVu;
                cboTrangThai.Text = cv.TenTrangThai; // dùng Text để hiển thị đúng tên
            }
            txtMaCV.Enabled = false;
        }

        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(keyword))
            {
                LoadData();
                return;
            }

            var ketQua = _danhSachView.Where(cv =>
                (cv.MaChucVu?.ToLower().Contains(keyword) ?? false) ||
                (cv.TenChucVu?.ToLower().Contains(keyword) ?? false) ||
                (cv.TenTrangThai?.ToLower().Contains(keyword) ?? false)
            ).ToList();

            dgvQLCV.DataSource = null;
            dgvQLCV.DataSource = ketQua;
            dgvQLCV.ClearSelection();

            if (ketQua.Count == 0)
                MessageBox.Show("Không tìm thấy chức vụ phù hợp!", "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}