using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi1.Model
{
    public interface ITestModelClass
    {
        string GetSomeStuff();
    }

    public class TestModelClass : ITestModelClass
    {
        public string GetSomeStuff()
        {

            return "testing 123";
        }
    }
}
