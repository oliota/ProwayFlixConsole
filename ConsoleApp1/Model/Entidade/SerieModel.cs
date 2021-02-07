using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Model.Entidade
{
    public class SerieModel
    {
        public string Nome { get; set; }

        public List<TemporadaModel> Temporadas { get; set; } = new List<TemporadaModel>();
    }
}
