using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi1.Model;

namespace WebApi1.Controllers
{
    public class ValuesController : ApiController
    {
        private readonly ITestModelClass _testModel;
        private readonly ISomeRepo _someRepo;

        public ValuesController(ITestModelClass testModel, ISomeRepo someRepo)
        {
            _testModel = testModel;
            _someRepo = someRepo;
        }
        // GET api/values 
        public async Task<IHttpActionResult> Get()
        {
            return Ok(await _someRepo.GetSomething());
        }

        // GET api/values/5 
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values 
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5 
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5 
        public void Delete(int id)
        {
        }
    }
}
