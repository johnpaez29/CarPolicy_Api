using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.CarPolicy.Model.Service
{
    public class ResponseGeneric
    {
        public string? Message { get; set; }

        public int? Status { get; set; }

        public object? Data { get; set; }


    }
}
