using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_QuanLyQuanNet;

namespace DAL_QuanLyQuanNet
{
    public class ThongBao_DAL
    {
        private static readonly string connectionString = @"Data Source=DESKTOP-4T4K5KV\SQLEXPRESS;Initial Catalog=PRO231_QuanLyQuanNet1903;Integrated Security=True;TrustServerCertificate=True";

        public DataTable LayDanhSach(string keyword = "")
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM ThongBaoHetGio WHERE MaThongBao LIKE @kw OR NoiDung LIKE @kw";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@kw", "%" + keyword + "%");
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
        public bool Them(ThongBao_DTO tb)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "INSERT INTO ThongBaoHetGio VALUES (@MaTB, @MaPhien, @MaNV, @TG, @Doc, @NoiDung)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaTB", tb.MaThongBao);
                cmd.Parameters.AddWithValue("@MaPhien", tb.MaPhien);
                cmd.Parameters.AddWithValue("@MaNV", tb.MaNhanVien);
                cmd.Parameters.AddWithValue("@TG", tb.ThoiGianThongBao);
                cmd.Parameters.AddWithValue("@Doc", tb.TrangThaiDoc);
                cmd.Parameters.AddWithValue("@NoiDung", tb.NoiDung);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
        public bool CapNhat(ThongBao_DTO tb)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "UPDATE ThongBaoHetGio SET MaPhien=@MaPhien, MaNhanVien=@MaNV, ThoiGianThongBao=@TG, TrangThaiDoc=@Doc, NoiDung=@NoiDung WHERE MaThongBao=@MaTB";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaPhien", tb.MaPhien);
                cmd.Parameters.AddWithValue("@MaNV", tb.MaNhanVien);
                cmd.Parameters.AddWithValue("@TG", tb.ThoiGianThongBao);
                cmd.Parameters.AddWithValue("@Doc", tb.TrangThaiDoc);
                cmd.Parameters.AddWithValue("@NoiDung", tb.NoiDung);
                cmd.Parameters.AddWithValue("@MaTB", tb.MaThongBao);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Xoa(string maThongBao)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "DELETE FROM ThongBaoHetGio WHERE MaThongBao = @MaTB";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaTB", maThongBao);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool KiemTraTonTai(string maThongBao)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "SELECT COUNT(*) FROM ThongBaoHetGio WHERE MaThongBao = @MaTB";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaTB", maThongBao);
                conn.Open();
                return (int)cmd.ExecuteScalar() > 0;
            }
        }
    }
}
