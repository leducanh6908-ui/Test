using System;
using System.Data;
using System.Data.SqlClient;
using DTO_QuanLyQuanNet;

namespace DAL_QuanLyQuanNet
{
    public class ChiTietDatDV_DAL
    {
        private static readonly string connectionString = @"Data Source=DESKTOP-4T4K5KV\SQLEXPRESS;Initial Catalog=PRO231_QuanLyQuanNet1903;Integrated Security=True;TrustServerCertificate=True";

        public static bool ThemChiTietDatDV(ChiTietDatDV_DTO dto)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO ChiTietDatDichVu (MaChiTiet, MaPhien, MaDichVu, SoLuong, DonGia) " +
                               "VALUES (@MaChiTiet, @MaPhien, @MaDichVu, @SoLuong, @DonGia)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaChiTiet", dto.MaChiTiet);
                cmd.Parameters.AddWithValue("@MaPhien", dto.MaPhien);
                cmd.Parameters.AddWithValue("@MaDichVu", dto.MaDichVu);
                cmd.Parameters.AddWithValue("@SoLuong", dto.SoLuong);
                cmd.Parameters.AddWithValue("@DonGia", dto.DonGia);

                try
                {
                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    // Có thể log lỗi tại đây
                    Console.WriteLine($"Lỗi khi thêm chi tiết đặt dịch vụ: {ex.Message}");
                    return false;
                }
            }
        }
    }
}