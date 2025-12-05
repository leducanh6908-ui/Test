using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DTO_QuanLyQuanNet;

namespace DAL_QuanLyQuanNet
{
    public class ThanhToan_DAL
    {
        private static string chuoiKetNoi = @"Data Source=DESKTOP-4T4K5KV\SQLEXPRESS;Initial Catalog=PRO231_QuanLyQuanNet1903;Integrated Security=True;TrustServerCertificate=True";

        // DAL_QuanLyQuanNet/KHachHang_DAL.cs

        public static List<ThanhToan_DTO> LayTatCa()
        {
            var ds = new List<ThanhToan_DTO>();
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                conn.Open();
                string query = "SELECT * FROM ThanhToan";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ds.Add(new ThanhToan_DTO(
                        reader["MaThanhToan"].ToString(),
                        reader["MaKhachHang"].ToString(),
                        reader["MaTaiKhoan"].ToString(),
                        Convert.ToDateTime(reader["ThoiGianThanhToan"]),
                        Convert.ToDecimal(reader["TongTien"]),
                        Convert.ToDateTime(reader["NgayTao"])
                    ));
                }
            }
            return ds;
        }

        public static DataTable LayLichSuThanhToan()
        {
            using (var conn = new SqlConnection(chuoiKetNoi))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT * FROM vw_LichSuThanhToan", conn);
                var adapter = new SqlDataAdapter(cmd);
                var dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        public static ThanhToan_DTO LayTheoMa(string ma)
        {
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                conn.Open();
                string query = "SELECT TOP 1 * FROM ThanhToan WHERE MaThanhToan = @ma OR MaKhachHang = @ma";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ma", ma);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new ThanhToan_DTO(
                            reader["MaThanhToan"].ToString(),
                            reader["MaKhachHang"].ToString(),
                            reader["MaTaiKhoan"].ToString(),
                            Convert.ToDateTime(reader["ThoiGianThanhToan"]),
                            Convert.ToDecimal(reader["TongTien"]),
                            Convert.ToDateTime(reader["NgayTao"])
                        );
                    }
                }
            }
            return null;
        }

        public static List<ThanhToan_DTO> TimKiemTheoMa(string ma)
        {
            var ds = new List<ThanhToan_DTO>();
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                conn.Open();
                string query = "SELECT * FROM ThanhToan WHERE MaThanhToan LIKE @ma OR MaKhachHang LIKE @ma";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ma", "%" + ma + "%");
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ds.Add(new ThanhToan_DTO(
                        reader["MaThanhToan"].ToString(),
                        reader["MaKhachHang"].ToString(),
                        reader["MaTaiKhoan"].ToString(),
                        Convert.ToDateTime(reader["ThoiGianThanhToan"]),
                        Convert.ToDecimal(reader["TongTien"]),
                        Convert.ToDateTime(reader["NgayTao"])
                    ));
                }
            }
            return ds;
        }

        // Thanh toán cho khách có tài khoản (có khuyến mãi, trừ tiền)
        public static (bool Success, string Message, decimal SoGio, decimal TienGio, decimal TienDV, decimal GiamGia, decimal TongPhaiTra)
        ThanhToanCoKhuyenMai(string maPhien, decimal donGiaGio)
        {
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            using (SqlCommand cmd = new SqlCommand("usp_TinhTongTien_CoKhuyenMai", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaPhien", maPhien);
                cmd.Parameters.AddWithValue("@DonGiaGio", donGiaGio);

                try
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            decimal soGio = reader.GetDecimal(reader.GetOrdinal("SoGioChoi"));
                            decimal tienGio = reader.GetDecimal(reader.GetOrdinal("TienGio"));
                            decimal tienDV = reader.GetDecimal(reader.GetOrdinal("TienDichVu"));
                            decimal giamGia = reader.GetDecimal(reader.GetOrdinal("GiamGia"));
                            decimal tongPhaiTra = reader.GetDecimal(reader.GetOrdinal("TongPhaiTra"));
                            return (true, "Thanh toán thành công!", soGio, tienGio, tienDV, giamGia, tongPhaiTra);
                        }
                    }
                    return (false, "Không có dữ liệu trả về!", 0, 0, 0, 0, 0);
                }
                catch (SqlException ex)
                {
                    return (false, ex.Message, 0, 0, 0, 0, 0);
                }
            }
        }

        // Tính tiền cho khách vãng lai (không trừ tài khoản)
        public static (bool Success, string Message, decimal SoGio, decimal TienGio, decimal TienDV, decimal GiamGia, decimal TongCanThu)
        TinhTienXuatHoaDon(string maPhien, decimal donGiaGio)
        {
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            using (SqlCommand cmd = new SqlCommand("usp_TinhTienXuatHoaDon", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaPhien", maPhien);
                cmd.Parameters.AddWithValue("@DonGiaGio", donGiaGio);

                try
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            decimal soGio = reader.GetDecimal(reader.GetOrdinal("SoGioChoi"));
                            decimal tienGio = reader.GetDecimal(reader.GetOrdinal("TienGio"));
                            decimal tienDV = reader.GetDecimal(reader.GetOrdinal("TienDichVu"));
                            decimal giamGia = reader.GetDecimal(reader.GetOrdinal("GiamGia"));
                            decimal tongCanThu = reader.GetDecimal(reader.GetOrdinal("TongCanThu"));
                            return (true, "Tính tiền thành công!", soGio, tienGio, tienDV, giamGia, tongCanThu);
                        }
                    }
                    return (false, "Không có dữ liệu trả về!", 0, 0, 0, 0, 0);
                }
                catch (SqlException ex)
                {
                    return (false, ex.Message, 0, 0, 0, 0, 0);
                }
            }
        }

        public static (bool Success, string Message, decimal SoGio, decimal TienGio, decimal TienDV, decimal GiamGia, decimal TongPhaiTra)
        TinhTienDuKien(DateTime thoiGianBatDau, DateTime thoiGianKetThuc, decimal donGiaGio, decimal tienDV = 0, decimal giamGia = 0)
        {
            // Tính số giờ chơi, làm tròn 2 chữ số thập phân
            decimal soGio = Math.Round((decimal)(thoiGianKetThuc - thoiGianBatDau).TotalHours, 2);
            if (soGio < 0) soGio = 0;

            // Tiền giờ chơi, làm tròn 0 chữ số thập phân
            decimal tienGio = Math.Round(soGio * donGiaGio, 0);

            // Tổng phải trả
            decimal tongPhaiTra = tienGio + tienDV - giamGia;
            if (tongPhaiTra < 0) tongPhaiTra = 0;

            return (true, "", soGio, tienGio, tienDV, giamGia, tongPhaiTra);
        }
    }
}
