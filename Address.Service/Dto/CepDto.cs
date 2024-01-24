using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Address.Service.Dto
{
    public class CepDto
    {
        [MaxLength(8, ErrorMessage = "O campo Cep deve ser do tipo string ou array com comprimento máximo de '8'.")]
        
        public string Cep { get; set; }
    }
}
