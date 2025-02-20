using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Financas.Api.Contract
{
    public class ModelErrorContract
    {
        public int Status { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime DateTime { get; set; }
    }
}