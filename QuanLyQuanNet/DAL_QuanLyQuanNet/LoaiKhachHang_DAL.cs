using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DTO_QuanLyQuanNet;

namespace DAL_QuanLyQuanNet
{
    public class LoaiKhachHang_DAL
    {
        private static readonly string connectionstring = @"Data Source=DESKTOP-4T4K5KV\SQLEXPRESS;Initial Catalog=PRO231_QuanLyQuanNet1903;Integrated Security=True;TrustServerCertificate=True";
        public static List<LoaiKhachHang_DTO> GetAll()
        {
            List<LoaiKhachHang_DTO> list = new List<LoaiKhachHang_DTO>();
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                string query = "SELECT * FROM LoaiKhachHang";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new LoaiKhachHang_DTO(
                        reader["MaLoaiKhachHang"].ToString(),
                        reader["TenLoai"].ToString(),
                        reader["MaTrangThai"].ToString()
                    ));
                }
            }
            return list;
        }
        public static bool Them(LoaiKhachHang_DTO loai)
        {
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                string query = "INSERT INTO LoaiKhachHang VALUES (@Ma, @Ten, @TrangThai)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Ma", loai.MaLoaiKhachHang);
                cmd.Parameters.AddWithValue("@Ten", loai.TenLoai);
                cmd.Parameters.AddWithValue("@TrangThai", loai.MaTrangThai);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public static bool Sua(LoaiKhachHang_DTO loai)
        {
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                string query = "UPDATE LoaiKhachHang SET TenLoai = @Ten, MaTrangThai = @TrangThai WHERE MaLoaiKhachHang = @Ma";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Ten", loai.TenLoai);
                cmd.Parameters.AddWithValue("@TrangThai", loai.MaTrangThai);
                cmd.Parameters.AddWithValue("@Ma", loai.MaLoaiKhachHang);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public static bool Xoa(string ma)
        {
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                string query = "DELETE FROM LoaiKhachHang WHERE MaLoaiKhachHang = @Ma";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Ma", ma);
                return cmd.ExecuteNonQuery() > 0;
            }
        }
        public static List<LoaiKhachHang_DTO> TimKiem(string tuKhoa)
        {
            List<LoaiKhachHang_DTO> list = new List<LoaiKhachHang_DTO>();
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                string query = @"SELECT * FROM LoaiKhachHang 
                         WHERE MaLoaiKhachHang LIKE @kw OR TenLoai LIKE @kw";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@kw", "%" + tuKhoa + "%");

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new LoaiKhachHang_DTO(
                        reader["MaLoaiKhachHang"].ToString(),
                        reader["TenLoai"].ToString(),
                        reader["MaTrangThai"].ToString()
                    ));
                }
            }
            return list;
        }

    }
}
