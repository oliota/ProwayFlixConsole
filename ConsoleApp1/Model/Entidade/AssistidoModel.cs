using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Model.Entidade
{
    public class AssistidoModel
    {
        public FilmeModel Filme { get; set; } = new FilmeModel();
        public SerieModel Serie { get; set; } = new SerieModel();
        public UsuarioModel Usuario { get; set; } = new UsuarioModel();
    }
}
