using ConsoleApp1.Business;
using ConsoleApp1.Model.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Model.Repositorio
{
    class SerieREP : IRepositorio
    {
        public bool Adicionar(object item)
        {
            var serie = (SerieModel)item;
            if (Buscar(item) != null)
            {
                Utils.Pausar($"Já existe uma serie com o nome {serie.Nome}");
                return false;
            }
            Repositorios.Series.Add(serie);
            Utils.Pausar($"Serie cadastrada com sucesso!!!");
            return true;
        }

        public object Buscar(object item)
        {
            var serie = (SerieModel)item;

            SerieModel select = Repositorios.Series
                .Where(x => x.Nome.Equals(serie.Nome))
                .SingleOrDefault();

            return select;
        }

        public bool Deletar(object item)
        {
            var atual = (SerieModel)Buscar(item);
            if (Utils.Perguntar($"Tem certeza que deseja deletar a serie {atual.Nome} ?"))
                Repositorios.Series.Remove(atual);
            return true;
        }

        public bool Editar(object item, object original)
        {
            var serie = (SerieModel)item;
            var atual = (SerieModel)Buscar(original);
            if (atual == null)
            {
                Utils.Pausar($"Serie não localizada");
                return false;
            }
            else
            {
                int posicao = Repositorios.Series.IndexOf(atual);
                Repositorios.Series[posicao] = serie;
                Utils.Pausar($"Serie atualizada com sucesso!!!");
                return true;

            }
        }

        public List<object> Listar(bool metodo)
        {
            return Repositorios.Series.Cast<object>().ToList();
        }

        public void Listar()
        {
            var lista = Repositorios.Series;
            if (!lista.Any())
            {
                Utils.Pausar("Não há itens para exibir");
                return;

            }
            Console.WriteLine("Nome\t\tTemporadas");
            foreach (var item in lista)
            {
                Console.WriteLine($"{item.Nome}\t\t{item.Temporadas.Count}");
            }
            Utils.Pausar();
        }
    }
}
