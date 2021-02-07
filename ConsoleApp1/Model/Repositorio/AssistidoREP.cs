using ConsoleApp1.Business;
using ConsoleApp1.Model.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Model.Repositorio
{
    class AssistidoREP : IRepositorio
    {
        public bool Adicionar(object item)
        {
            var assistido = (AssistidoModel)item;

            Repositorios.Assistidos.Add(assistido);
            Utils.Pausar($"Registro de consumo cadastrado com sucesso!!!");
            return true;
        }

        public object Buscar(object item)
        {
            var assistido = (AssistidoModel)item;

            AssistidoModel select = Repositorios.Assistidos
                .Where(x => x.Usuario.Email.Equals(assistido.Usuario.Email))
                .Where(x => x.Filme.Nome.Equals(assistido.Filme.Nome))
                .Where(x => x.Serie.Nome.Equals(assistido.Serie.Nome))
                .SingleOrDefault();

            return select;
        }

        public object Buscar(object item, string tipo)
        {
            var assistido = (AssistidoModel)item;
            AssistidoModel select;
            if (tipo.Equals("Filme"))
            {
                select = Repositorios.Assistidos
                  .Where(x => x.Usuario.Email.Equals(assistido.Usuario.Email))
                  .Where(x => x.Filme.Nome.Equals(assistido.Filme.Nome))
                  .SingleOrDefault();

            }
            else
            {
                select = Repositorios.Assistidos
                              .Where(x => x.Usuario.Email.Equals(assistido.Usuario.Email))
                              .Where(x => x.Serie.Nome.Equals(assistido.Serie.Nome))
                              .SingleOrDefault();
            }

            return select;
        }

        public bool Deletar(object item)
        {
            var atual = (AssistidoModel)Buscar(item);
            if (Utils.Perguntar($"Tem certeza que deseja deletar o registro de consumo " +
                $"{atual.Usuario.Nome} / {atual.Filme.Nome} / {atual.Serie.Nome}    ?"))
                Repositorios.Assistidos.Remove(atual);
            return true;
        }

        public bool Editar(object item, object original)
        {
            var assistido = (AssistidoModel)item;
            var atual = (AssistidoModel)Buscar(original);
            if (atual == null)
            {
                Utils.Pausar($"Registro de consumo não localizado");
                return false;
            }
            else
            {
                int posicao = Repositorios.Assistidos.IndexOf(atual);
                Repositorios.Assistidos[posicao] = assistido;
                Utils.Pausar($"Registro de consumo atualizado com sucesso!!!");
                return true;

            }
        }

        public List<object> Listar(bool metodo)
        {
            return Repositorios.Assistidos.Cast<object>().ToList();
        }

        public void Listar()
        {
            var lista = Repositorios.Assistidos;
            if (!lista.Any())
            {
                Utils.Pausar("Não há itens para exibir");
                return;

            }
            Console.WriteLine("Usuario\t\tFilme\t\tSerie");
            foreach (var item in lista)
            {
                Console.WriteLine($"{item.Usuario.Nome}\t\t{item.Filme.Nome}\t\t{item.Serie.Nome}");
            }
            Utils.Pausar();
        }
    }
}
