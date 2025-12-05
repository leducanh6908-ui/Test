using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DTO_QuanLyQuanNet;

namespace DAL_QuanLyQuanNet
{
    public static class HinhThucThanhToan_DAL
    {
        private static readonly string connectionString = @"Data Source=DESKTOP-4T4K5KV\SQLEXPRESS;Initial Catalog=PRO231_QuanLyQuanNet1903;Integrated Security=True;TrustServerCertificate=True";

        public static List<HinhThucThanhToan_DTO> LayTatCa()
        {
            var list = new List<HinhThucThanhToan_DTO>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM HinhThucThanhToan";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new HinhThucThanhToan_DTO(
                        reader["MaHinhThuc"].ToString(),
                        reader["TenHinhThuc"].ToString()
                    ));
                }
            }
            return list;
        }

        public static bool Them(HinhThucThanhToan_DTO h)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"INSERT INTO HinhThucThanhToan (MaHinhThuc, TenHinhThuc) 
                             VALUES (@Ma, @Ten)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Ma", h.MaHinhThuc);
                cmd.Parameters.AddWithValue("@Ten", h.TenHinhThuc);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public static bool Sua(HinhThucThanhToan_DTO h)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"UPDATE HinhThucThanhToan 
                             SET TenHinhThuc = @Ten 
                             WHERE MaHinhThuc = @Ma";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Ma", h.MaHinhThuc);
                cmd.Parameters.AddWithValue("@Ten", h.TenHinhThuc);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public static bool Xoa(string maHT)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM HinhThucThanhToan WHERE MaHinhThuc = @Ma";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Ma", maHT);
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }

}
