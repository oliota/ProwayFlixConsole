using ConsoleApp1.Model.Entidade;
using System.Collections.Generic;

namespace ConsoleApp1.Model.Repositorio
{
    public static class Repositorios
    {
        public static List<UsuarioModel> Usuarios { get; set; } = new List<UsuarioModel>();
        public static List<FilmeModel> Filmes { get; set; } = new List<FilmeModel>();
        public static List<SerieModel> Series { get; set; } = new List<SerieModel>();

        public static UsuarioModel UsuarioLogado { get; set; }
    }
}
