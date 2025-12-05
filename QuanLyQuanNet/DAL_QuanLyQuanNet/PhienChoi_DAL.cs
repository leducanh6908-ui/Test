using DTO_QuanLyQuanNet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL_QuanLyQuanNet
{
    public class PhienChoi_DAL
    {
        private static readonly string connectionString = @"Data Source=DESKTOP-4T4K5KV\SQLEXPRESS;Initial Catalog=PRO231_QuanLyQuanNet1903;Integrated Security=True;TrustServerCertificate=True";

        public static List<PhienChoi_DTO> GetAll()
        {
            List<PhienChoi_DTO> ds = new List<PhienChoi_DTO>();
            string query = "SELECT * FROM PhienSuDung WHERE MaTrangThai != 'TT02'";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ds.Add(new PhienChoi_DTO
                    {
                        MaPhien = reader["MaPhien"].ToString(),
                        MaKhachHang = reader["MaKhachHang"].ToString(),
                        MaMay = reader["MaMay"].ToString(),
                        ThoiGianBatDau = reader["ThoiGianBatDau"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["ThoiGianBatDau"]),
                        ThoiGianKetThuc = reader["ThoiGianKetThuc"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["ThoiGianKetThuc"]),
                        TongSoGio = reader["TongSoGio"] == DBNull.Value ? 0 : Convert.ToDouble(reader["TongSoGio"]),
                        TongTien = reader["TongTien"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["TongTien"]),
                        SoTienConLai = reader["SoTienConLai"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["SoTienConLai"]),
                        NgayTao = reader["NgayTao"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["NgayTao"]),
                        MaTrangThai = reader["MaTrangThai"].ToString()
                    });
                }
            }

            return ds;
        }

        public static PhienChoi_DTO LayPhienChoiTheoMa(string maPhien)
        {
            string query = "SELECT * FROM PhienChoi WHERE MaPhien = @MaPhien";
            SqlParameter[] parameters = { new SqlParameter("@MaPhien", maPhien) };
            DataTable dt = DataProvider.TruyVanDuLieu(query, parameters);

            if (dt.Rows.Count == 0)
                return null;

            DataRow row = dt.Rows[0];
            return new PhienChoi_DTO
            {
                MaPhien = row["MaPhien"].ToString(),
                MaKhachHang = row["MaKhachHang"].ToString(),
                MaMay = row["MaMay"].ToString(),
                ThoiGianBatDau = row["ThoiGianBatDau"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ThoiGianBatDau"]),
                ThoiGianKetThuc = row["ThoiGianKetThuc"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ThoiGianKetThuc"]),
                TongSoGio = row["TongSoGio"] == DBNull.Value ? 0 : Convert.ToDouble(row["TongSoGio"]),
                TongTien = row["TongTien"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TongTien"]),
                SoTienConLai = row["SoTienConLai"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SoTienConLai"]),
                NgayTao = row["NgayTao"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["NgayTao"]),
                MaTrangThai = row["MaTrangThai"].ToString()
            };
        }

        public static bool ThemPhienChoi(PhienChoi_DTO phien)
        {
            string query = @"INSERT INTO PhienSuDung
        (MaPhien, MaKhachHang, MaMay, ThoiGianBatDau, ThoiGianKetThuc, TongSoGio, TongTien, SoTienConLai, NgayTao, MaTrangThai)
        VALUES (@MaPhien, @MaKhachHang, @MaMay, @TGBD, @TGKT, @SoGio, @TongTien, @ConLai, @NgayTao, @TrangThai)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@MaPhien", phien.MaPhien);
                cmd.Parameters.AddWithValue("@MaKhachHang", (object)phien.MaKhachHang ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@MaMay", phien.MaMay);
                cmd.Parameters.AddWithValue("@TGBD", phien.ThoiGianBatDau == default(DateTime) ? (object)DateTime.Now : phien.ThoiGianBatDau);
                cmd.Parameters.AddWithValue("@TGKT", DBNull.Value); // luôn null khi thêm mới
                cmd.Parameters.AddWithValue("@SoGio", 0);
                cmd.Parameters.AddWithValue("@TongTien", 0);
                cmd.Parameters.AddWithValue("@ConLai", phien.SoTienConLai); // Thay vì 0
                cmd.Parameters.AddWithValue("@NgayTao", phien.NgayTao == default(DateTime) ? (object)DateTime.Now : phien.NgayTao);
                cmd.Parameters.AddWithValue("@TrangThai", phien.MaTrangThai);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // Chỉ cập nhật thông tin chung, không cập nhật thời gian kết thúc/tính tiền
        public static bool CapNhatPhienChoi(PhienChoi_DTO phien)
        {
            string query = @"UPDATE PhienSuDung
    SET MaKhachHang = @MaKH,
        MaMay = @MaMay,
        ThoiGianBatDau = @TGBD,
        NgayTao = @NgayTao,
        MaTrangThai = @TrangThai,
        SoTienConLai = @ConLai
    WHERE MaPhien = @MaPhien";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@MaPhien", phien.MaPhien);
                cmd.Parameters.AddWithValue("@MaKH", (object)phien.MaKhachHang ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@MaMay", phien.MaMay);
                cmd.Parameters.AddWithValue("@TGBD", phien.ThoiGianBatDau);
                cmd.Parameters.AddWithValue("@ConLai", phien.SoTienConLai);
                cmd.Parameters.AddWithValue("@NgayTao", phien.NgayTao);
                cmd.Parameters.AddWithValue("@TrangThai", phien.MaTrangThai);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // Kết thúc phiên chơi: cập nhật thời gian kết thúc và tính tiền
        public static bool CapNhatKetThucPhien(string maPhien, DateTime thoiGianKetThuc, double tongSoGio, decimal tongTien, decimal soTienConLai)
        {
            string query = @"UPDATE PhienSuDung
        SET ThoiGianKetThuc = @TGKT,
            TongSoGio = @SoGio,
            TongTien = @TongTien,
            SoTienConLai = @ConLai
        WHERE MaPhien = @MaPhien";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@TGKT", thoiGianKetThuc);
                cmd.Parameters.AddWithValue("@SoGio", tongSoGio);
                cmd.Parameters.AddWithValue("@TongTien", tongTien);
                cmd.Parameters.AddWithValue("@ConLai", soTienConLai);
                cmd.Parameters.AddWithValue("@MaPhien", maPhien);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // Xóa mềm
        public static bool XoaPhienChoi_Mem(string maPhien)
        {
            string query = "UPDATE PhienSuDung SET MaTrangThai = 'TT02' WHERE MaPhien = @MaPhien";
            SqlParameter[] parameters = { new SqlParameter("@MaPhien", maPhien) };
            return DataProvider.ThucThiCauLenh(query, parameters);
        }
        public static List<PhienChoi_DTO> TimKiemPhien(string tuKhoa)
        {
            List<PhienChoi_DTO> ds = new List<PhienChoi_DTO>();
            string query = @"SELECT * FROM PhienSuDung
                             WHERE (MaPhien LIKE @kw OR MaKhachHang LIKE @kw OR MaMay LIKE @kw)
                             AND MaTrangThai != 'TT02'";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@kw", $"%{tuKhoa}%");
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ds.Add(new PhienChoi_DTO
                    {
                        MaPhien = reader["MaPhien"].ToString(),
                        MaKhachHang = reader["MaKhachHang"].ToString(),
                        MaMay = reader["MaMay"].ToString(),
                        ThoiGianBatDau = Convert.ToDateTime(reader["ThoiGianBatDau"]),
                        ThoiGianKetThuc = reader["ThoiGianKetThuc"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["ThoiGianKetThuc"]),
                        TongSoGio = Convert.ToDouble(reader["TongSoGio"]),
                        TongTien = Convert.ToDecimal(reader["TongTien"]),
                        SoTienConLai = Convert.ToDecimal(reader["SoTienConLai"]),
                        NgayTao = Convert.ToDateTime(reader["NgayTao"]),
                        MaTrangThai = reader["MaTrangThai"].ToString()
                    });
                }
            }

            return ds;
        }
        public static decimal TinhTienPhien(string maPhien)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_TinhTienPhienChoi", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@MaPhien", maPhien);

                conn.Open();
                object result = cmd.ExecuteScalar();
                return (result != null && result != DBNull.Value) ? Convert.ToDecimal(result) : 0;
            }
        }

        public static decimal LayDonGiaHienTai()
        {
            decimal donGia = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT TOP 1 DonGia FROM GiaGioChoi WHERE ThoiGianApDung <= GETDATE() ORDER BY ThoiGianApDung DESC";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                object result = cmd.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                    donGia = Convert.ToDecimal(result);
            }
            return donGia;
        }

        public static string GenerateMaPhienMoi()
        {
            string maPhienMoi = "PS001";
            string query = "SELECT MaPhien FROM PhienSuDung";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                int maxSo = 0;
                while (reader.Read())
                {
                    string ma = reader["MaPhien"].ToString();
                    if (ma.Length > 2 && int.TryParse(ma.Substring(2), out int so))
                    {
                        if (so > maxSo)
                            maxSo = so;
                    }
                }
                maxSo++;
                maPhienMoi = "PS" + maxSo.ToString("D3");
            }

            return maPhienMoi;
        }
    }
}