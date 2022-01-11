using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mtfullstacktest.Data
{
   
    public class DatakodeposController : ControllerBase
    {
        private readonly DatakodeposInterface IDatakodepos;

        public DatakodeposController(DatakodeposInterface idatakodepos)
        {
            IDatakodepos = idatakodepos;
        }

        [HttpGet]
        [Route("api/kodepos/list/{skipOffset}/{takeFetchnext}/{orderBy}/{fprovinsi}/{fkabupaten}/{direction}")]
        public async Task<JsonResult> GetDataKodeposind(int skipOffset, int takeFetchnext, string orderBy, string fprovinsi, string fkabupaten, string direction = "ASC")
        {
            List<DataModel> datas = new List<DataModel>();
            datas = await IDatakodepos.GetDatakodeposind(skipOffset, takeFetchnext, orderBy, fprovinsi, fkabupaten, direction);
            return new JsonResult(datas);
        }
        [HttpPost]
        [Route("api/datakodepos/insert")]
        public async Task<ActionResult<bool>> InsertKodepos([FromBody] DataModel item)
        {
            bool result = await IDatakodepos.InsertKodepos(item);
            return result;

        }
        [HttpPost]
        [Route("api/datakodepos/update")]
        public async Task<ActionResult<bool>> IpdateKodepos([FromBody] DataModel item)
        {
            bool result = await IDatakodepos.UpdateKodepos(item);
            return result;

        }
        [HttpDelete]
        [Route("api/datakodepos/delete/{rowid}")]
        public async Task<ActionResult<bool>> DeleteKodepos(int rowid)
        {
            bool result = await IDatakodepos.DeleteKodepos(rowid);
            return result;

        }
    }
}
