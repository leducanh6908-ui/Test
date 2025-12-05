using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_QuanLyQuanNet
{
    public class ThongKe_DAL
    {
        private static readonly string connectionString = @"Data Source=DESKTOP-4T4K5KV\SQLEXPRESS;Initial Catalog=PRO231_QuanLyQuanNet1903;Integrated Security=True";
        public DataTable GetAll()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "SELECT * FROM ThanhToan";
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
        public DataTable LayDoanhThuTheoNgay(DateTime from, DateTime to)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = @"
                SELECT 
                    CAST(ThoiGianThanhToan AS DATE) AS Ngay,
                    SUM(TongTien) AS TongTien
                FROM ThanhToan
                WHERE ThoiGianThanhToan BETWEEN @from AND @to
                GROUP BY CAST(ThoiGianThanhToan AS DATE)
                ORDER BY Ngay";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@from", from);
                cmd.Parameters.AddWithValue("@to", to);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
        public DataTable LayTiLeChonMay(DateTime from, DateTime to)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = @"
            SELECT mt.TenMay, COUNT(*) AS SoLuotSuDung
            FROM PhienSuDung ps
            JOIN MayTinh mt ON ps.MaMay = mt.MaMay
            WHERE ps.ThoiGianBatDau BETWEEN @from AND @to
            GROUP BY mt.TenMay
        ";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@from", from);
                cmd.Parameters.AddWithValue("@to", to);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
        public DataTable LayDoanhThuNhanVien(DateTime from, DateTime to)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = @"
            SELECT nv.HoTen, SUM(ps.TongTien) AS TongTien
            FROM ThongBaoHetGio tbhg
            JOIN NhanVien nv ON tbhg.MaNhanVien = nv.MaNhanVien
            JOIN PhienSuDung ps ON tbhg.MaPhien = ps.MaPhien
            WHERE tbhg.ThoiGianThongBao BETWEEN @from AND @to
            GROUP BY nv.HoTen
        ";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@from", from);
                cmd.Parameters.AddWithValue("@to", to);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

    }
}
