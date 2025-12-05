using DTO_QuanLyQuanNet;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_QuanLyQuanNet
{
    public class LoaiCLV_DAL
    {
        private static readonly string connectionString = @"Data Source=DESKTOP-4T4K5KV\SQLEXPRESS;Initial Catalog=PRO231_QuanLyQuanNet1903;Integrated Security=True;TrustServerCertificate=True";

        /* -------------------------------------------------- */
        /* 1. Lấy toàn bộ danh sách                           */
        /* -------------------------------------------------- */
        public List<LoaiCLV_DTO> LayDanhSachLoaiCLV()
        {
            var list = new List<LoaiCLV_DTO>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"SELECT 
                                    MaLoaiCa     AS MaLoai,
                                    TenLoaiCa    AS TenLoai,
                                    GioBatDau,
                                    GioKetThuc,
                                    LuongTheoGio,
                                    MaTrangThai
                                 FROM LoaiCa";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new LoaiCLV_DTO
                        {
                            MaLoai = reader["MaLoai"].ToString(),
                            TenLoai = reader["TenLoai"].ToString(),
                            GioBatDau = TimeSpan.Parse(reader["GioBatDau"].ToString()),
                            GioKetThuc = TimeSpan.Parse(reader["GioKetThuc"].ToString()),
                            LuongTheoGio = Convert.ToSingle(reader["LuongTheoGio"]),
                            MaTrangThai = reader["MaTrangThai"].ToString()
                        });
                    }
                }
            }
            return list;
        }

        /* -------------------------------------------------- */
        /* 2. Thêm mới                                         */
        /* -------------------------------------------------- */
        public bool ThemLoaiCLV(LoaiCLV_DTO loai)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO LoaiCa 
                                    (MaLoaiCa, TenLoaiCa, GioBatDau, GioKetThuc, LuongTheoGio, MaTrangThai)
                                 VALUES
                                    (@MaLoai, @TenLoai, @GioBatDau, @GioKetThuc, @LuongTheoGio, @MaTrangThai)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaLoai", loai.MaLoai);
                cmd.Parameters.AddWithValue("@TenLoai", loai.TenLoai);
                cmd.Parameters.AddWithValue("@GioBatDau", loai.GioBatDau);
                cmd.Parameters.AddWithValue("@GioKetThuc", loai.GioKetThuc);
                cmd.Parameters.AddWithValue("@LuongTheoGio", loai.LuongTheoGio);
                cmd.Parameters.AddWithValue("@MaTrangThai", loai.MaTrangThai);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        /* -------------------------------------------------- */
        /* 3. Cập nhật                                         */
        /* -------------------------------------------------- */
        public bool SuaLoaiCLV(LoaiCLV_DTO loai)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"UPDATE LoaiCa SET
                                    TenLoaiCa    = @TenLoai,
                                    GioBatDau     = @GioBatDau,
                                    GioKetThuc    = @GioKetThuc,
                                    LuongTheoGio  = @LuongTheoGio,
                                    MaTrangThai   = @MaTrangThai
                                 WHERE MaLoaiCa   = @MaLoai";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaLoai", loai.MaLoai);
                cmd.Parameters.AddWithValue("@TenLoai", loai.TenLoai);
                cmd.Parameters.AddWithValue("@GioBatDau", loai.GioBatDau);
                cmd.Parameters.AddWithValue("@GioKetThuc", loai.GioKetThuc);
                cmd.Parameters.AddWithValue("@LuongTheoGio", loai.LuongTheoGio);
                cmd.Parameters.AddWithValue("@MaTrangThai", loai.MaTrangThai);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        /* -------------------------------------------------- */
        /* 4. Xoá                                              */
        /* -------------------------------------------------- */
        public bool XoaLoaiCLV(string maLoai)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM LoaiCa WHERE MaLoaiCa = @MaLoai";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaLoai", maLoai);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        /* -------------------------------------------------- */
        /* 5. Tìm kiếm                                         */
        /* -------------------------------------------------- */
        public List<LoaiCLV_DTO> TimKiemLoaiCLV(string keyword)
        {
            var result = new List<LoaiCLV_DTO>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"SELECT 
                                    MaLoaiCa     AS MaLoai,
                                    TenLoaiCa    AS TenLoai,
                                    GioBatDau,
                                    GioKetThuc,
                                    LuongTheoGio,
                                    MaTrangThai
                                 FROM LoaiCa
                                 WHERE MaLoaiCa  LIKE @kw OR TenLoaiCa LIKE @kw";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@kw", "%" + keyword + "%");
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new LoaiCLV_DTO
                        {
                            MaLoai = reader["MaLoai"].ToString(),
                            TenLoai = reader["TenLoai"].ToString(),
                            GioBatDau = TimeSpan.Parse(reader["GioBatDau"].ToString()),
                            GioKetThuc = TimeSpan.Parse(reader["GioKetThuc"].ToString()),
                            LuongTheoGio = Convert.ToSingle(reader["LuongTheoGio"]),
                            MaTrangThai = reader["MaTrangThai"].ToString()
                        });
                    }
                }
            }
            return result;
        }
    }
}