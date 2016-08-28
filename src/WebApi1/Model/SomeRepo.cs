using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stateless1;

namespace WebApi1.Model
{
    public interface ISomeRepo
    {
        Task<string> GetSomething();
    }

    public class SomeRepo : ISomeRepo
    {
        private readonly IStateless1 _service;

        public SomeRepo(IStateless1 service)
        {
            _service = service;
        }

        public async Task<string> GetSomething()
        {
            return await _service.GetHello();
        }
    }
}
