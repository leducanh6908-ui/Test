using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

public class KhachHang_DAL
{
    private static readonly string connectionString = @"Data Source=DESKTOP-4T4K5KV\SQLEXPRESS;Initial Catalog=PRO231_QuanLyQuanNet1903;Integrated Security=True;TrustServerCertificate=True";

    public List<KhachHang_DTO> GetAll()
    {
        var list = new List<KhachHang_DTO>();
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            string query = "SELECT * FROM KhachHang";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(MapKhachHang(reader));
                }
            }
        }
        return list;
    }

    public KhachHang_DTO LayTheoMa(string maKH)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            string query = "SELECT * FROM KhachHang WHERE MaKhachHang = @MaKH";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@MaKH", maKH);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                        return MapKhachHang(reader);
                }
            }
        }
        return null;
    }

    public static string LayLoaiKhach(string maKhachHang)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            string query = "SELECT MaLoaiKhachHang FROM KhachHang WHERE MaKhachHang = @MaKH";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@MaKH", maKhachHang);
                object result = cmd.ExecuteScalar();
                return result != null ? result.ToString() : "";
            }
        }
    }

    public static decimal LaySoDuKhachHang(string maKhachHang)
    {
        decimal soDu = 0;
        string query = "SELECT SoDuTaiKhoan FROM KhachHang WHERE MaKhachHang = @MaKhachHang";
        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@MaKhachHang", maKhachHang);
            conn.Open();
            object result = cmd.ExecuteScalar();
            if (result != null && result != DBNull.Value)
                soDu = Convert.ToDecimal(result);
        }
        return soDu;
    }

    public static void CapNhatSoDuKhachHang(string maKhachHang, decimal soDuMoi)
    {
        string query = "UPDATE KhachHang SET SoDuTaiKhoan = @SoDuMoi WHERE MaKhachHang = @MaKhachHang";
        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@SoDuMoi", soDuMoi);
            cmd.Parameters.AddWithValue("@MaKhachHang", maKhachHang);
            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }

    // Sửa lỗi: truyền đúng tham số SoDuTaiKhoan khi thêm mới
    public void Insert(KhachHang_DTO kh)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            string query = @"INSERT INTO KhachHang 
            (MaKhachHang, TenDangNhap, MatKhau, HoTen, SoDienThoai, Email, CCCD, NgayTao, MaTrangThai, MaLoaiKhachHang, SoDuTaiKhoan)
            VALUES (@MaKH, @TenDN, @MatKhau, @HoTen, @SDT, @Email, @CCCD, GETDATE(), @TrangThai, @LoaiKH, @SoDuTaiKhoan)";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@MaKH", kh.MaKhachHang);
                cmd.Parameters.AddWithValue("@TenDN", kh.TenDangNhap);
                cmd.Parameters.AddWithValue("@MatKhau", kh.MatKhau);
                cmd.Parameters.AddWithValue("@HoTen", kh.HoTen);
                cmd.Parameters.AddWithValue("@SDT", kh.SoDienThoai ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Email", kh.Email ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@CCCD", kh.CCCD ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@TrangThai", kh.MaTrangThai);
                cmd.Parameters.AddWithValue("@LoaiKH", kh.MaLoaiKhachHang);
                cmd.Parameters.AddWithValue("@SoDuTaiKhoan", kh.SoDuTaiKhoan);
                cmd.ExecuteNonQuery();
            }
        }
    }

    public void Update(KhachHang_DTO kh)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            string query = @"UPDATE KhachHang SET 
            TenDangNhap = @TenDN,
            MatKhau = @MatKhau,
            HoTen = @HoTen,
            SoDienThoai = @SDT,
            Email = @Email,
            CCCD = @CCCD,
            MaTrangThai = @TrangThai,
            MaLoaiKhachHang = @LoaiKH,
            SoDuTaiKhoan = @SoDuTaiKhoan
            WHERE MaKhachHang = @MaKH";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@MaKH", kh.MaKhachHang);
                cmd.Parameters.AddWithValue("@TenDN", kh.TenDangNhap);
                cmd.Parameters.AddWithValue("@MatKhau", kh.MatKhau);
                cmd.Parameters.AddWithValue("@HoTen", kh.HoTen);
                cmd.Parameters.AddWithValue("@SDT", kh.SoDienThoai ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Email", kh.Email ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@CCCD", kh.CCCD ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@TrangThai", kh.MaTrangThai);
                cmd.Parameters.AddWithValue("@LoaiKH", kh.MaLoaiKhachHang);
                cmd.Parameters.AddWithValue("@SoDuTaiKhoan", kh.SoDuTaiKhoan);
                cmd.ExecuteNonQuery();
            }
        }
    }

    public void Delete(string maKH)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            string query = "UPDATE KhachHang SET MaTrangThai = @TrangThai WHERE MaKhachHang = @MaKH";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@MaKH", maKH);
                cmd.Parameters.AddWithValue("@TrangThai", "TT02"); // TT02 = tạm dừng
                cmd.ExecuteNonQuery();
            }
        }
    }

    // Hàm map dữ liệu từ SqlDataReader sang DTO
    private KhachHang_DTO MapKhachHang(SqlDataReader reader)
    {
        return new KhachHang_DTO
        {
            MaKhachHang = reader["MaKhachHang"].ToString(),
            TenDangNhap = reader["TenDangNhap"].ToString(),
            MatKhau = reader["MatKhau"].ToString(),
            HoTen = reader["HoTen"].ToString(),
            SoDienThoai = reader["SoDienThoai"].ToString(),
            Email = reader["Email"].ToString(),
            CCCD = reader["CCCD"].ToString(),
            NgayTao = reader["NgayTao"] == DBNull.Value ? null : (DateTime?)reader["NgayTao"],
            MaTrangThai = reader["MaTrangThai"].ToString(),
            MaLoaiKhachHang = reader["MaLoaiKhachHang"].ToString(),
            SoDuTaiKhoan = reader["SoDuTaiKhoan"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["SoDuTaiKhoan"])
        };
    }
}
