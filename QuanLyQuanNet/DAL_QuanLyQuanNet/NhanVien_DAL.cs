
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

public class NhanVien_DAL
{
    private static readonly string connectionString = @"Data Source=DESKTOP-4T4K5KV\SQLEXPRESS;Initial Catalog=PRO231_QuanLyQuanNet1903;Integrated Security=True;TrustServerCertificate=True";

    public static List<NhanVien_DTO> GetAll()
    {
        var list = new List<NhanVien_DTO>();
        using (var conn = new SqlConnection(connectionString))
        {
            conn.Open();
            string query = "SELECT * FROM NhanVien";
            var cmd = new SqlCommand(query, conn);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new NhanVien_DTO
                {
                    MaNhanVien = reader["MaNhanVien"].ToString(),
                    HoTen = reader["HoTen"].ToString(),
                    Email = reader["Email"].ToString(),
                    MatKhau = reader["MatKhau"].ToString(),
                    MaChucVu = reader["MaChucVu"].ToString(),
                    MaTrangThai = reader["MaTrangThai"].ToString(),
                    NgayTao = Convert.ToDateTime(reader["NgayTao"])
                });
            }
        }
        return list;
    }

    public static void Add(NhanVien_DTO nv)
    {
        using (var conn = new SqlConnection(connectionString))
        {
            conn.Open();
            string query = "INSERT INTO NhanVien VALUES (@MaNV, @HoTen, @Email, @MatKhau, @MaCV, @MaTT, @NgayTao)";
            var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@MaNV", nv.MaNhanVien);
            cmd.Parameters.AddWithValue("@HoTen", nv.HoTen);
            cmd.Parameters.AddWithValue("@Email", nv.Email);
            cmd.Parameters.AddWithValue("@MatKhau", nv.MatKhau);
            cmd.Parameters.AddWithValue("@MaCV", nv.MaChucVu);
            cmd.Parameters.AddWithValue("@MaTT", nv.MaTrangThai);
            cmd.Parameters.AddWithValue("@NgayTao", nv.NgayTao);
            cmd.ExecuteNonQuery();
        }
    }

    public static void Update(NhanVien_DTO nv)
    {
        using (var conn = new SqlConnection(connectionString))
        {
            conn.Open();
            string query = "UPDATE NhanVien SET HoTen=@HoTen, Email=@Email, MatKhau=@MatKhau, MaChucVu=@MaCV, MaTrangThai=@MaTT WHERE MaNhanVien=@MaNV";
            var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@MaNV", nv.MaNhanVien);
            cmd.Parameters.AddWithValue("@HoTen", nv.HoTen);
            cmd.Parameters.AddWithValue("@Email", nv.Email);
            cmd.Parameters.AddWithValue("@MatKhau", nv.MatKhau);
            cmd.Parameters.AddWithValue("@MaCV", nv.MaChucVu);
            cmd.Parameters.AddWithValue("@MaTT", nv.MaTrangThai);
            cmd.ExecuteNonQuery();
        }
    }

    public static void Delete(string maNV)
    {
        using (var conn = new SqlConnection(connectionString))
        {
            conn.Open();
            string query = "UPDATE NhanVien SET MaTrangThai = @tt WHERE MaNhanVien = @MaNV";
            var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@MaNV", maNV);
            cmd.Parameters.AddWithValue("@tt", "TT02"); // TT02 = tạm dừng
            cmd.ExecuteNonQuery();
        }
    }

    public static NhanVien_DTO KiemTraDangNhap(string maNV, string matKhau)
    {
        NhanVien_DTO nv = null;
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            string query = "SELECT * FROM NhanVien WHERE MaNhanVien = @maNV AND MatKhau = @mk";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@maNV", maNV);
            cmd.Parameters.AddWithValue("@mk", matKhau);

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                nv = new NhanVien_DTO
                {
                    MaNhanVien = reader["MaNhanVien"].ToString(),
                    HoTen = reader["HoTen"].ToString(),
                    MatKhau = reader["MatKhau"].ToString(),
                    MaChucVu = reader["MaChucVu"].ToString(),
                    MaTrangThai = reader["MaTrangThai"].ToString(),
                    NgayTao = Convert.ToDateTime(reader["NgayTao"])
                    // nếu có thêm trường nào thì bổ sung vào đây
                };
            }
        }
        return nv;
    }
    public static bool KiemTraMatKhau(string maNV, string matKhau)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            string query = "SELECT COUNT(*) FROM NhanVien WHERE MaNhanVien = @maNV AND MatKhau = @matKhau";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@maNV", maNV);
            cmd.Parameters.AddWithValue("@matKhau", matKhau);

            int count = (int)cmd.ExecuteScalar();
            return count > 0;
        }
    }
    public static bool DoiMatKhau(string maNV, string matKhauMoi)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            string query = "UPDATE NhanVien SET MatKhau = @matKhauMoi WHERE MaNhanVien = @maNV";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@maNV", maNV);
            cmd.Parameters.AddWithValue("@matKhauMoi", matKhauMoi);
            return cmd.ExecuteNonQuery() > 0;
        }
    }
}