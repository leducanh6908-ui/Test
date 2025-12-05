using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using DTO_QuanLyQuanNet;

namespace DAL_QuanLyQuanNet
{
    public class ChucVu_DAL
    {
        private static readonly string connectionString = @"Data Source=DESKTOP-4T4K5KV\SQLEXPRESS;Initial Catalog=PRO231_QuanLyQuanNet1903;Integrated Security=True;TrustServerCertificate=True";

        public static List<ChucVu_DTO> LayTatCa()
        {
            var list = new List<ChucVu_DTO>();
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT * FROM ChucVu", conn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new ChucVu_DTO
                    (
                        reader["MaChucVu"].ToString(),
                        reader["TenChucVu"].ToString(),
                        reader["MaTrangThai"].ToString()
                    ));
                }
            }
            return list;
        }


        public static bool Them(ChucVu_DTO cv)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("INSERT INTO ChucVu VALUES (@ma, @ten, @tt)", conn);
                cmd.Parameters.AddWithValue("@ma", cv.MaChucVu);
                cmd.Parameters.AddWithValue("@ten", cv.TenChucVu);
                cmd.Parameters.AddWithValue("@tt", cv.MaTrangThai);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public static bool CapNhat(ChucVu_DTO cv)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("UPDATE ChucVu SET TenChucVu = @ten, MaTrangThai = @tt WHERE MaChucVu = @ma", conn);
                cmd.Parameters.AddWithValue("@ma", cv.MaChucVu);
                cmd.Parameters.AddWithValue("@ten", cv.TenChucVu);
                cmd.Parameters.AddWithValue("@tt", cv.MaTrangThai);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public static bool Xoa(string ma)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("DELETE FROM ChucVu WHERE MaChucVu = @ma", conn);
                cmd.Parameters.AddWithValue("@ma", ma);
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}