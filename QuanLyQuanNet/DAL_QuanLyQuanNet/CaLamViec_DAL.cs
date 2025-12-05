using DTO_QuanLyQuanNet;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL_QuanLyQuanNet
{
    public class CaLamViec_DAL
    {
        private static readonly string connectionString = @"Data Source=DESKTOP-4T4K5KV\SQLEXPRESS;Initial Catalog=PRO231_QuanLyQuanNet1903;Integrated Security=True;TrustServerCertificate=True";

        // 1. Lấy toàn bộ danh sách
        public List<CaLamViec_DTO> LayDanhSach()
        {
            var list = new List<CaLamViec_DTO>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM CaLamViec", conn);
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    list.Add(new CaLamViec_DTO
                    {
                        MaCa = rd["MaCa"].ToString(),
                        MaNhanVien = rd["MaNhanVien"].ToString(),
                        MaLoaiCa = rd["MaLoaiCa"].ToString(),
                        NgayLam = Convert.ToDateTime(rd["NgayLam"]),
                        NgayTao = Convert.ToDateTime(rd["NgayTao"]),
                        GhiChu = rd["GhiChu"].ToString()
                    });
                }
            }
            return list;
        }

        // 2. Thêm mới
        public bool Them(CaLamViec_DTO clv)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"INSERT INTO CaLamViec 
                    (MaCa, MaNhanVien, MaLoaiCa, NgayLam, NgayTao, GhiChu) 
                    VALUES (@MaCa, @MaNhanVien, @MaLoaiCa, @NgayLam, @NgayTao, @GhiChu)", conn);

                cmd.Parameters.AddWithValue("@MaCa", clv.MaCa);
                cmd.Parameters.AddWithValue("@MaNhanVien", clv.MaNhanVien);
                cmd.Parameters.AddWithValue("@MaLoaiCa", clv.MaLoaiCa);
                cmd.Parameters.AddWithValue("@NgayLam", clv.NgayLam);
                cmd.Parameters.AddWithValue("@NgayTao", clv.NgayTao);
                cmd.Parameters.AddWithValue("@GhiChu", clv.GhiChu);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // 3. Cập nhật
        public bool Sua(CaLamViec_DTO clv)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"UPDATE CaLamViec SET 
                    MaNhanVien=@MaNhanVien, MaLoaiCa=@MaLoaiCa, 
                    NgayLam=@NgayLam, NgayTao=@NgayTao, GhiChu=@GhiChu 
                    WHERE MaCa=@MaCa", conn);

                cmd.Parameters.AddWithValue("@MaCa", clv.MaCa);
                cmd.Parameters.AddWithValue("@MaNhanVien", clv.MaNhanVien);
                cmd.Parameters.AddWithValue("@MaLoaiCa", clv.MaLoaiCa);
                cmd.Parameters.AddWithValue("@NgayLam", clv.NgayLam);
                cmd.Parameters.AddWithValue("@NgayTao", clv.NgayTao);
                cmd.Parameters.AddWithValue("@GhiChu", clv.GhiChu);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // 4. Xoá
        public bool Xoa(string maCa)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM CaLamViec WHERE MaCa=@MaCa", conn);
                cmd.Parameters.AddWithValue("@MaCa", maCa);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // 5. Tìm kiếm
        public List<CaLamViec_DTO> TimKiem(string keyword)
        {
            var list = new List<CaLamViec_DTO>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM CaLamViec WHERE MaNhanVien LIKE @keyword", conn);
                cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    list.Add(new CaLamViec_DTO
                    {
                        MaCa = rd["MaCa"].ToString(),
                        MaNhanVien = rd["MaNhanVien"].ToString(),
                        MaLoaiCa = rd["MaLoaiCa"].ToString(),
                        NgayLam = Convert.ToDateTime(rd["NgayLam"]),
                        NgayTao = Convert.ToDateTime(rd["NgayTao"]),
                        GhiChu = rd["GhiChu"].ToString()
                    });
                }
            }
            return list;
        }

        // 6. Danh sách mã nhân viên (gợi ý cho ComboBox)
        public List<string> LayDanhSachMaNhanVien()
        {
            var list = new List<string>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT MaNhanVien FROM NhanVien", conn);
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    list.Add(rd["MaNhanVien"].ToString());
                }
            }
            return list;
        }
    }
}