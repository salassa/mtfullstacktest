using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace mtfullstacktest.Data
{
    public class DatakodeposService : DatakodeposInterface
    {
        private static DBConnectionConfiguration _configurationdb;
        public DatakodeposService(DBConnectionConfiguration configurationdb)
        {
            _configurationdb = configurationdb;
        }
        public int cekDataSudahterdaftar(string strSQL, string kode)
        {
            int _i;
            using (SqlConnection condb = new SqlConnection(_configurationdb.Value))
            {
                condb.Open();
                using (SqlCommand cmd = new SqlCommand(strSQL, condb))
                {
                    cmd.Parameters.Add("@kode", System.Data.SqlDbType.VarChar).Value = kode;
                    SqlDataReader dr = cmd.ExecuteReader();
                    _i = 0;
                    if (dr.Read())
                    {
                        _i = dr.GetInt32(0);
                    }
                    condb.Close();
                    dr.Dispose();
                }
            }

            return _i;
        }
        public int CountDataKodepos(string fprovinsi, string fkabupaten)
        {
            int _i;
            using (SqlConnection condb = new SqlConnection(_configurationdb.Value))
            {
                string strSQL = "select count(rowid) jml " +
                    " from tb_kodepos_ind where rowid != 0 ";

                if (fprovinsi != "-")
                {
                    strSQL = strSQL + " and UPPER(provinsi) like @fprovinsi ";
                }
                if (fkabupaten != "-")
                {
                    strSQL = strSQL + " and UPPER(kabupaten) like @fkabupaten ";
                }

                condb.Open();
                using (SqlCommand cmd = new SqlCommand(strSQL, condb))
                {
                    cmd.Parameters.Add("@fprovinsi", System.Data.SqlDbType.VarChar).Value = "%" + fprovinsi.ToUpper() + "%";
                    cmd.Parameters.Add("@fkabupaten", System.Data.SqlDbType.VarChar).Value = "%" + fkabupaten.ToUpper() + "%";
                    SqlDataReader dr = cmd.ExecuteReader();
                    _i = 0;
                    if (dr.Read())
                    {
                        _i = dr.GetInt32(0);
                    }
                    condb.Close();
                    dr.Dispose();
                }
            }

            return _i;
        }
        public async Task<List<DataModel>> GetDatakodeposind(int skipOffset, int takeFetchnext, string orderBy, string fprovinsi, string fkabupaten, string direction = "ASC")
        {
            List<DataModel> datas = new List<DataModel>();
            using (SqlConnection con = new SqlConnection(_configurationdb.Value))
            {
                string strSQL = "select distinct nokdpos, kelurahan, kecamatan, kabupaten, provinsi, jenis, rowid " +
                    " from tb_kodepos_ind where rowid != 0 ";

                if (fprovinsi != "-")
                {
                    strSQL = strSQL + " and UPPER(provinsi) like @fprovinsi ";
                }
                if (fkabupaten != "-")
                {
                    strSQL = strSQL + " and UPPER(kabupaten) like @fkabupaten  ";
                }
                strSQL = strSQL + " ORDER BY " + orderBy + " " + direction + " OFFSET " + skipOffset + "  ROWS FETCH NEXT " + takeFetchnext + " ROWS ONLY ";

                using (SqlCommand cmd = new SqlCommand(strSQL, con))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Add("@fprovinsi", System.Data.SqlDbType.VarChar).Value = "%" + fprovinsi.ToUpper() + "%";
                    cmd.Parameters.Add("@fkabupaten", System.Data.SqlDbType.VarChar).Value = "%" + fkabupaten.ToUpper() + "%";
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            datas.Add(new DataModel()
                            {
                               rowid = Convert.ToInt32(sdr["rowid"].ToString()),
                                nokdpos = sdr["nokdpos"].ToString(),
                                kelurahan = sdr["kelurahan"].ToString(),
                                kecamatan = sdr["kecamatan"].ToString(),
                                kabupaten = sdr["kabupaten"].ToString(),
                                provinsi = sdr["provinsi"].ToString(),
                                jenis = sdr["jenis"].ToString()
                            });
                        }
                        con.Close();
                        return await Task.FromResult(datas);
                    }
                    
                }
                
            }
        }
        public async Task<bool> InsertKodepos(DataModel item)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_configurationdb.Value))
                {
                    string strSQL = "insert into tb_kodepos_ind(nokdpos, kelurahan, kecamatan, kabupaten, provinsi, jenis) values(@nokdpos, @kelurahan, @kecamatan, @kabupaten, @provinsi, @jenis) ";
                    using (SqlCommand cmd = new SqlCommand(strSQL, con))
                    {
                        con.Open();
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add("@nokdpos", System.Data.SqlDbType.VarChar).Value = item.nokdpos;
                        cmd.Parameters.Add("@kelurahan", System.Data.SqlDbType.VarChar).Value = item.kelurahan;
                        cmd.Parameters.Add("@kecamatan", System.Data.SqlDbType.VarChar).Value = item.kecamatan;
                        cmd.Parameters.Add("@kabupaten", System.Data.SqlDbType.VarChar).Value = item.kabupaten;
                        cmd.Parameters.Add("@provinsi", System.Data.SqlDbType.VarChar).Value = item.provinsi;
                        cmd.Parameters.Add("@jenis", System.Data.SqlDbType.VarChar).Value = item.jenis;
                        int i = await cmd.ExecuteNonQueryAsync();
                        if (i > 0)
                        {
                            return true;
                        }
                        return false;
                    }
                }
            }
            catch
            {
                throw;
            }
        }
        public async Task<bool> UpdateKodepos(DataModel item)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_configurationdb.Value))
                {
                    string strSQL = "update tb_kodepos_ind set nokdpos = @nokdpos, kelurahan = @kelurahan, kecamatan = @kecamatan, kabupaten = @kabupaten, provinsi = @provinsi, jenis = @jenis where rowid = @rowid ";
                    using (SqlCommand cmd = new SqlCommand(strSQL, con))
                    {
                        con.Open();
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add("@rowid", System.Data.SqlDbType.Int).Value = item.rowid;
                        cmd.Parameters.Add("@nokdpos", System.Data.SqlDbType.VarChar).Value = item.nokdpos;
                        cmd.Parameters.Add("@kelurahan", System.Data.SqlDbType.VarChar).Value = item.kelurahan;
                        cmd.Parameters.Add("@kecamatan", System.Data.SqlDbType.VarChar).Value = item.kecamatan;
                        cmd.Parameters.Add("@kabupaten", System.Data.SqlDbType.VarChar).Value = item.kabupaten;
                        cmd.Parameters.Add("@provinsi", System.Data.SqlDbType.VarChar).Value = item.provinsi;
                        cmd.Parameters.Add("@jenis", System.Data.SqlDbType.VarChar).Value = item.jenis;
                        int i = await cmd.ExecuteNonQueryAsync();
                        if (i > 0)
                        {
                            return true;
                        }
                        return false;
                    }
                }
            }
            catch
            {
                throw;
            }
        }
        public async Task<bool> DeleteKodepos(int rowid)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_configurationdb.Value))
                {
                    string strSQL = "delete from tb_kodepos_ind where rowid = @rowid ";
                    using (SqlCommand cmd = new SqlCommand(strSQL, con))
                    {
                        con.Open();
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add("@rowid", System.Data.SqlDbType.Int).Value = rowid;
                        int i = await cmd.ExecuteNonQueryAsync();
                        if (i > 0)
                        {
                            return true;
                        }
                        return false;
                    }
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
