using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoseOfHope.Domain.Entities
{
    public class tabTipoUsuario
    {
        [Key]
        public int codigo { get; set; }

        public string descricao { get; set; }
    }
}
