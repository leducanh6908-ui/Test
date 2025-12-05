using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DTO_QuanLyQuanNet;

namespace DAL_QuanLyQuanNet
{
    public class LoaiMay_DAL
    {
        private static string chuoiKetNoi = @"Data Source=DESKTOP-4T4K5KV\SQLEXPRESS;Initial Catalog=PRO231_QuanLyQuanNet1903;Integrated Security=True;TrustServerCertificate=True";

        public static List<LoaiMay_DTO> LayDanhSach()
        {
            List<LoaiMay_DTO> ds = new List<LoaiMay_DTO>();
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM LoaiMayTinh", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ds.Add(new LoaiMay_DTO(
                        reader["MaLoaiMay"].ToString(),
                        reader["TenLoaiMay"].ToString(),
                        reader["MaTrangThai"].ToString()
                    ));
                }
                conn.Close();
            }
            return ds;
        }

        public static bool ThemLoaiMay(LoaiMay_DTO loai)
        {
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO LoaiMayTinh VALUES (@ma, @ten, @tt)", conn);
                cmd.Parameters.AddWithValue("@ma", loai.MaLoaiMay);
                cmd.Parameters.AddWithValue("@ten", loai.TenLoaiMay);
                cmd.Parameters.AddWithValue("@tt", loai.MaTrangThai);
                return cmd.ExecuteNonQuery() > 0;
            }
        }
        public static bool SuaLoaiMay(LoaiMay_DTO loai)
        {
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                    "UPDATE LoaiMayTinh SET TenLoaiMay = @ten, MaTrangThai = @tt WHERE MaLoaiMay = @ma", conn);
                cmd.Parameters.AddWithValue("@ma", loai.MaLoaiMay);
                cmd.Parameters.AddWithValue("@ten", loai.TenLoaiMay);
                cmd.Parameters.AddWithValue("@tt", loai.MaTrangThai);
                return cmd.ExecuteNonQuery() > 0;
            }
        }
        public static bool XoaLoaiMay(string maLoai)
        {
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                    "UPDATE LoaiMayTinh SET MaTrangThai = @tt WHERE MaLoaiMay = @ma", conn);
                cmd.Parameters.AddWithValue("@ma", maLoai);
                cmd.Parameters.AddWithValue("@tt", "TT02"); // TT02 = tạm dừng
                return cmd.ExecuteNonQuery() > 0;
            }
        }

    }
}
