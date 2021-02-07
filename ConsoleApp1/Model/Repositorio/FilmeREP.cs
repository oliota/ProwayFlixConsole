using ConsoleApp1.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Model.Repositorio
{
    class FilmeREP : IRepositorio
    {
        public bool Adicionar(object item)
        {
            var filme = (FilmeModel)item;
            if (Buscar(item) != null)
            {
                Utils.Pausar($"Já existe um filme com o nome {filme.Nome}");
                return false;
            }
            Repositorios.Filmes.Add(filme);
            Utils.Pausar($"Filme cadastrado com sucesso!!!");
            return true;
        }

        public object Buscar(object item)
        {
            var filme = (FilmeModel)item;

            FilmeModel select = Repositorios.Filmes
                .Where(x => x.Nome.Equals(filme.Nome))
                .SingleOrDefault();

            return select;
        }

        public bool Deletar(object item)
        {
            var atual = (FilmeModel)Buscar(item); 
            if (Utils.Perguntar($"Tem certeza que deseja deletar o filme {atual.Nome} ?"))
                Repositorios.Filmes.Remove(atual); 
            return true;
        }

        public bool Editar(object item, object original)
        {
            var filme = (FilmeModel)item;
            var atual = (FilmeModel)Buscar(original);
            if (atual == null)
            {
                Utils.Pausar($"Filme não localizado");
                return false;
            }
            else
            {
                int posicao = Repositorios.Filmes.IndexOf(atual);
                Repositorios.Filmes[posicao] = filme;
                Utils.Pausar($"Filme atualizado com sucesso!!!");
                return true;

            }
        }

        public List<object> Listar(bool metodo)
        {
            return Repositorios.Filmes.Cast<object>().ToList();
        }

        public void Listar()
        {
            var lista = Repositorios.Filmes;
            if (!lista.Any())
            {
                Utils.Pausar("Não há itens para exibir");
                return;

            }
            Console.WriteLine("Nome\t\tAno");
            foreach (var item in lista)
            {
                Console.WriteLine($"{item.Nome}\t\t{item.Ano}");
            }
            Utils.Pausar();
        }
    }
}
