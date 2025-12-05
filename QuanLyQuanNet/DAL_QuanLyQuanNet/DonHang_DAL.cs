using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DTO_QuanLyQuanNet;

public static class DonDatHang_DAL
{
    private static readonly string connectionString = @"Data Source=DESKTOP-4T4K5KV\SQLEXPRESS;Initial Catalog=PRO231_QuanLyQuanNet1903;Integrated Security=True;TrustServerCertificate=True";

    public static List<DonDatHang_DTO> LayTatCa()
    {
        var list = new List<DonDatHang_DTO>();
        using (var conn = new SqlConnection(connectionString))
        {
            conn.Open();
            var cmd = new SqlCommand("SELECT * FROM DonDatHang ORDER BY NgayTao DESC", conn);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new DonDatHang_DTO
                {
                    MaDonDatHang = reader["MaDonDatHang"].ToString(),
                    MaKhachHang = reader["MaKhachHang"].ToString(),
                    ThoiGianDat = Convert.ToDateTime(reader["ThoiGianDat"]),
                    TongTien = Convert.ToDecimal(reader["TongTien"]),
                    NgayTao = Convert.ToDateTime(reader["NgayTao"])
                });
            }
        }
        return list;
    }

    public static DonDatHang_DTO LayTheoMa(string maDon)
    {
        using (var conn = new SqlConnection(connectionString))
        {
            conn.Open();
            var cmd = new SqlCommand("SELECT * FROM DonDatHang WHERE MaDonDatHang = @ma", conn);
            cmd.Parameters.AddWithValue("@ma", maDon);
            var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new DonDatHang_DTO
                {
                    MaDonDatHang = reader["MaDonDatHang"].ToString(),
                    MaKhachHang = reader["MaKhachHang"].ToString(),
                    ThoiGianDat = Convert.ToDateTime(reader["ThoiGianDat"]),
                    TongTien = Convert.ToDecimal(reader["TongTien"]),
                    NgayTao = Convert.ToDateTime(reader["NgayTao"])
                };
            }
        }
        return null;
    }
}
