using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DTO_QuanLyQuanNet;

namespace DAL_QuanLyQuanNet
{
    public class LoaiTrangThai_DAL
    {
        private static string chuoiKetNoi = @"Data Source=DESKTOP-4T4K5KV\SQLEXPRESS;Initial Catalog=PRO231_QuanLyQuanNet1903;Integrated Security=True;TrustServerCertificate=True";

        public static List<LoaiTrangThai_DTO> LayTatCa()
        {
            List<LoaiTrangThai_DTO> ds = new List<LoaiTrangThai_DTO>();
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM LoaiTrangThai", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ds.Add(new LoaiTrangThai_DTO(
                        reader["MaTrangThai"].ToString(),
                        reader["TenTrangThai"].ToString(),
                        reader["MoTa"]?.ToString()
                    ));
                }
            }
            return ds;
        }

        public static bool Them(LoaiTrangThai_DTO ltt)
        {
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO LoaiTrangThai VALUES (@ma, @ten, @mota)", conn);
                cmd.Parameters.AddWithValue("@ma", ltt.MaTrangThai);
                cmd.Parameters.AddWithValue("@ten", ltt.TenTrangThai);
                cmd.Parameters.AddWithValue("@mota", ltt.MoTa ?? (object)DBNull.Value);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public static bool Sua(LoaiTrangThai_DTO ltt)
        {
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE LoaiTrangThai SET TenTrangThai = @ten, MoTa = @mota WHERE MaTrangThai = @ma", conn);
                cmd.Parameters.AddWithValue("@ma", ltt.MaTrangThai);
                cmd.Parameters.AddWithValue("@ten", ltt.TenTrangThai);
                cmd.Parameters.AddWithValue("@mota", ltt.MoTa ?? (object)DBNull.Value);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public static bool Xoa(string ma)
        {
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM LoaiTrangThai WHERE MaTrangThai = @ma", conn);
                cmd.Parameters.AddWithValue("@ma", ma);
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
