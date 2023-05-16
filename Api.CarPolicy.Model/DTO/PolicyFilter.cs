using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.CarPolicy.Model.DTO
{
    public class PolicyFilter
    {
        public string? PolicyNumber { get; set; }

        public string? Plate { get; set; }

    }
}
