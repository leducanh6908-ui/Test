using System.Collections.Generic;
using System.Data.SqlClient;
using DTO_QuanLyQuanNet;

namespace DAL_QuanLyQuanNet
{
    public class MayTinh_DAL
    {
        private static string chuoiKetNoi = @"Data Source=DESKTOP-4T4K5KV\SQLEXPRESS;Initial Catalog=PRO231_QuanLyQuanNet1903;Integrated Security=True;TrustServerCertificate=True";

        public static List<MayTinh_DTO> LayDanhSach()
        {
            var ds = new List<MayTinh_DTO>();
            try
            {
                using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT MaMay, TenMay, MaLoaiMay, MaTrangThai FROM MayTinh", conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ds.Add(new MayTinh_DTO(
                                reader["MaMay"].ToString(),
                                reader["TenMay"].ToString(),
                                reader["MaLoaiMay"].ToString(),
                                reader["MaTrangThai"].ToString()
                            ));
                        }
                    }
                }
            }
            catch
            {
                // Optionally log the error
                ds = new List<MayTinh_DTO>();
            }
            return ds;
        }

        //lấy theo mã
        public static MayTinh_DTO LayMayTheoMa(string ma)
        {
            MayTinh_DTO may = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT MaMay, TenMay, MaLoaiMay, MaTrangThai FROM MayTinh WHERE MaMay = @ma", conn))
                    {
                        cmd.Parameters.AddWithValue("@ma", ma);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                may = new MayTinh_DTO(
                                    reader["MaMay"].ToString(),
                                    reader["TenMay"].ToString(),
                                    reader["MaLoaiMay"].ToString(),
                                    reader["MaTrangThai"].ToString()
                                );
                            }
                        }
                    }
                }
            }
            catch
            {
                may = null;
            }
            return may;
        }

        //cập nhật trạng thái máy
        public static bool CapNhatTrangThaiMay(string maMay, string maTrangThai)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE MayTinh SET MaTrangThai = @tt WHERE MaMay = @ma", conn))
                    {
                        cmd.Parameters.AddWithValue("@ma", maMay);
                        cmd.Parameters.AddWithValue("@tt", maTrangThai);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch
            {
                // Optionally log the error
                return false;
            }
        }



        public static bool ThemMay(MayTinh_DTO may)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(
                        "INSERT INTO MayTinh (MaMay, TenMay, MaLoaiMay, MaTrangThai) VALUES (@ma, @ten, @loai, @tt)", conn))
                    {
                        cmd.Parameters.AddWithValue("@ma", may.MaMay);
                        cmd.Parameters.AddWithValue("@ten", may.TenMay);
                        cmd.Parameters.AddWithValue("@loai", may.MaLoaiMay);
                        cmd.Parameters.AddWithValue("@tt", may.MaTrangThai);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch
            {
                // Optionally log the error
                return false;
            }
        }

        public static bool SuaMay(MayTinh_DTO may)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(
                        "UPDATE MayTinh SET TenMay = @ten, MaLoaiMay = @loai, MaTrangThai = @tt WHERE MaMay = @ma", conn))
                    {
                        cmd.Parameters.AddWithValue("@ma", may.MaMay);
                        cmd.Parameters.AddWithValue("@ten", may.TenMay);
                        cmd.Parameters.AddWithValue("@loai", may.MaLoaiMay);
                        cmd.Parameters.AddWithValue("@tt", may.MaTrangThai);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch
            {
                // Optionally log the error
                return false;
            }
        }

        public static bool XoaMay(string ma)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(
                        "UPDATE MayTinh SET MaTrangThai = @tt WHERE MaMay = @ma", conn))
                    {
                        cmd.Parameters.AddWithValue("@ma", ma);
                        cmd.Parameters.AddWithValue("@tt", "TT02"); // TT02 = tạm dừng
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch
            {
                // Optionally log the error
                return false;
            }
        }
    }
}
