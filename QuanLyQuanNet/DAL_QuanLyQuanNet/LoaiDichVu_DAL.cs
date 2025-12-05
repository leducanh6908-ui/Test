using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DTO_QuanLyQuanNet;
using DAL_QuanLyQuanNet;

namespace DAL_QuanLyQuanNet
{
    public class LoaiDichVu_DAL
    {
        private static readonly string connectionString = @"Data Source=DESKTOP-4T4K5KV\SQLEXPRESS;Initial Catalog=PRO231_QuanLyQuanNet1903;Integrated Security=True;TrustServerCertificate=True";

        // Lấy danh sách tất cả loại dịch vụ
        public List<LoaiDichVu_DTO> GetAll()
        {
            List<LoaiDichVu_DTO> list = new List<LoaiDichVu_DTO>();
            string query = "SELECT * FROM LoaiDichVu";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    LoaiDichVu_DTO ldv = new LoaiDichVu_DTO
                    {
                        MaLoaiDichVu = reader["MaLoaiDichVu"].ToString(),      
                        TenLoaiDichVu = reader["TenLoaiDichVu"].ToString(),
                        MaTrangThai = reader["MaTrangThai"].ToString(),
                        NgayTao = Convert.ToDateTime(reader["NgayTao"])
                    };
                    list.Add(ldv);
                }
            }

            return list;

        }

        // Thêm loại dịch vụ mới
        public bool Add(LoaiDichVu_DTO ldv)
        {
            //tạo câu lệnh SQL để thêm loại dịch vụ
            string query = "INSERT INTO LoaiDichVu (MaLoaiDichVu, TenLoaiDichVu, MaTrangThai, NgayTao) " +
                           "VALUES (@MaLoaiDichVu, @TenLoaiDichVu, @MaTrangThai, @NgayTao)";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(
                query, conn))
            {
                cmd.Parameters.AddWithValue("@MaLoaiDichVu", ldv.MaLoaiDichVu);
                cmd.Parameters.AddWithValue("@TenLoaiDichVu", ldv.TenLoaiDichVu);
                cmd.Parameters.AddWithValue("@MaTrangThai", ldv.MaTrangThai);
                cmd.Parameters.AddWithValue("@NgayTao", ldv.NgayTao);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // Cập nhật loại dịch vụ
        public bool Update(LoaiDichVu_DTO ldv)
        {
            string query = "UPDATE LoaiDichVu SET TenLoaiDichVu = @TenLoaiDichVu, MaTrangThai = @MaTrangThai, NgayTao = @NgayTao " +
                           "WHERE MaLoaiDichVu = @MaLoaiDichVu";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@MaLoaiDichVu", ldv.MaLoaiDichVu);
                cmd.Parameters.AddWithValue("@TenLoaiDichVu", ldv.TenLoaiDichVu);
                cmd.Parameters.AddWithValue("@MaTrangThai", ldv.MaTrangThai);
                cmd.Parameters.AddWithValue("@NgayTao", ldv.NgayTao);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // Xóa loại dịch vụ
        public bool Delete(string maLoaiDV)
        {
            string query = "UPDATE LoaiDichVu SET MaTrangThai = @MaTrangThai WHERE MaLoaiDichVu = @MaLoaiDichVu";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@MaLoaiDichVu", maLoaiDV);
                cmd.Parameters.AddWithValue("@MaTrangThai", "TT02"); // TT02 = Ngừng hoạt động
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // Tìm kiếm loại dịch vụ theo mã hoặc tên, chỉ lấy loại đang hoạt động
        public List<LoaiDichVu_DTO> Search(string keyword)
        {
            List<LoaiDichVu_DTO> list = new List<LoaiDichVu_DTO>();
            string query = "SELECT * FROM LoaiDichVu WHERE MaTrangThai = 'TT01' AND (MaLoaiDichVu LIKE @Keyword OR TenLoaiDichVu LIKE @Keyword)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    LoaiDichVu_DTO ldv = new LoaiDichVu_DTO
                    {
                        MaLoaiDichVu = reader["MaLoaiDichVu"].ToString(),
                        TenLoaiDichVu = reader["TenLoaiDichVu"].ToString(),
                        MaTrangThai = reader["MaTrangThai"].ToString(),
                        NgayTao = Convert.ToDateTime(reader["NgayTao"])
                    };
                    list.Add(ldv);
                }
            }

            return list;
        }
    }
}
