using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Model.Entidade
{
    public class TemporadaModel
    {
        public string Nome { get; set; }
        public List<EpisodioModel> Episodios { get; set; } = new List<EpisodioModel>();
    }
}
