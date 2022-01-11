using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mtfullstacktest.Data
{
    public interface DatakodeposInterface
    {
        public int cekDataSudahterdaftar(string strSQL, string kode);
        public int CountDataKodepos(string fprovinsi, string fkabupaten);
        public Task<List<DataModel>> GetDatakodeposind(int skipOffset, int takeFetchnext, string orderBy, string fprovinsi, string fkabupaten, string direction = "ASC");
        public Task<bool> InsertKodepos(DataModel item);
        public Task<bool> UpdateKodepos(DataModel item);
        public Task<bool> DeleteKodepos(int rowid);
    }
}
