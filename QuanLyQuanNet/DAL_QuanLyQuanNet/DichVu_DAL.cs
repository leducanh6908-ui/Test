using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DTO_QuanLyQuanNet; // Namespace chứa DichVu_DTO

namespace DAL_QuanLyQuanNet
{
    public class DichVu_DAL
    {
        private static readonly string connectionString = @"Data Source=DESKTOP-4T4K5KV\SQLEXPRESS;Initial Catalog=PRO231_QuanLyQuanNet1903;Integrated Security=True;TrustServerCertificate=True";

        // Lấy danh sách tất cả dịch vụ
        public List<DichVu_DTO> GetAll()
        {
            List<DichVu_DTO> ds = new List<DichVu_DTO>();
            string query = "SELECT MaDichVu, TenDichVu, MaLoaiDichVu, DonGia, NgayTao, AnhSP, MaTrangThai FROM DichVu";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ds.Add(new DichVu_DTO
                        {
                            MaDV = reader["MaDichVu"].ToString(),
                            TenDV = reader["TenDichVu"].ToString(),
                            MaLoaiDV = reader["MaLoaiDichVu"].ToString(),
                            DonGia = reader.GetDecimal(reader.GetOrdinal("DonGia")),
                            NgayTao = reader["NgayTao"] != DBNull.Value ? Convert.ToDateTime(reader["NgayTao"]) : DateTime.MinValue,
                            AnhSP = reader["AnhSP"] != DBNull.Value ? (byte[])reader["AnhSP"] : null,
                            MaTrangThai = reader["MaTrangThai"] != DBNull.Value ? reader["MaTrangThai"].ToString() : null
                        });
                    }
                }
            }
            return ds;
        }

        // Thêm mới dịch vụ
        public bool Add(DichVu_DTO dv)
        {
            string query = "INSERT INTO DichVu (MaDichVu, TenDichVu, MaLoaiDichVu, DonGia, NgayTao, AnhSP, MaTrangThai) " +
                           "VALUES (@MaDichVu, @TenDichVu, @MaLoaiDichVu, @DonGia, @NgayTao, @AnhSP, @MaTrangThai)";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@MaDichVu", dv.MaDV);
                cmd.Parameters.AddWithValue("@TenDichVu", dv.TenDV);
                cmd.Parameters.AddWithValue("@MaLoaiDichVu", dv.MaLoaiDV);
                cmd.Parameters.AddWithValue("@DonGia", dv.DonGia);
                cmd.Parameters.AddWithValue("@NgayTao", dv.NgayTao);
                cmd.Parameters.AddWithValue("@AnhSP", (object)dv.AnhSP ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@MaTrangThai", "TT01"); // Luôn mặc định là TT01
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // Cập nhật dịch vụ
        public bool Update(DichVu_DTO dv)
        {
            string query = "UPDATE DichVu SET TenDichVu = @TenDichVu, MaLoaiDichVu = @MaLoaiDichVu, DonGia = @DonGia, NgayTao = @NgayTao, AnhSP = @AnhSP, MaTrangThai = @MaTrangThai " +
                           "WHERE MaDichVu = @MaDichVu";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@MaDichVu", dv.MaDV);
                cmd.Parameters.AddWithValue("@TenDichVu", dv.TenDV);
                cmd.Parameters.AddWithValue("@MaLoaiDichVu", dv.MaLoaiDV);
                cmd.Parameters.AddWithValue("@DonGia", dv.DonGia);
                cmd.Parameters.AddWithValue("@NgayTao", dv.NgayTao);
                cmd.Parameters.AddWithValue("@AnhSP", (object)dv.AnhSP ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@MaTrangThai", (object)dv.MaTrangThai ?? DBNull.Value);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // Xóa (soft delete) dịch vụ
        public bool Delete(string maDichVu)
        {
            string query = "UPDATE DichVu SET MaTrangThai = @MaTrangThai WHERE MaDichVu = @MaDichVu";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@MaDichVu", maDichVu);
                cmd.Parameters.AddWithValue("@MaTrangThai", "TT02"); // TT02 = tạm dừng
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // Tìm kiếm theo mã dịch vụ
        public List<DichVu_DTO> Search(string keyword)
        {
            List<DichVu_DTO> list = new List<DichVu_DTO>();
            string query = "SELECT MaDichVu, TenDichVu, MaLoaiDichVu, DonGia, NgayTao, AnhSP, MaTrangThai FROM DichVu WHERE MaDichVu LIKE @Keyword";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    DichVu_DTO dv = new DichVu_DTO
                    {
                        MaDV = reader["MaDichVu"].ToString(),
                        TenDV = reader["TenDichVu"].ToString(),
                        MaLoaiDV = reader["MaLoaiDichVu"].ToString(),
                        DonGia = Convert.ToDecimal(reader["DonGia"]),
                        NgayTao = reader["NgayTao"] != DBNull.Value ? Convert.ToDateTime(reader["NgayTao"]) : DateTime.MinValue,
                        AnhSP = reader["AnhSP"] != DBNull.Value ? (byte[])reader["AnhSP"] : null,
                        MaTrangThai = reader["MaTrangThai"] != DBNull.Value ? reader["MaTrangThai"].ToString() : null
                    };
                    list.Add(dv);
                }
            }

            return list;
        }

        public string GetNextMaDichVu()
        {
            string query = "SELECT TOP 1 MaDichVu FROM DichVu ORDER BY MaDichVu DESC";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                var result = cmd.ExecuteScalar();
                if (result != null)
                {
                    string lastMa = result.ToString();
                    // Giả sử mã dạng DV001, DV002,...
                    int num = 1;
                    if (lastMa.Length > 2 && int.TryParse(lastMa.Substring(2), out num))
                    {
                        num++;
                    }
                    return "DV" + num.ToString("D3");
                }
                else
                {
                    return "DV001";
                }
            }
        }
    }
}
