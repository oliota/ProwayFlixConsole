using ConsoleApp1.Business;
using ConsoleApp1.Model.Entidade;
using ConsoleApp1.Model.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Model.Repositorio
{
    class TemporadaREP : IRepositorioItem
    {
        public bool Adicionar(object item, object pai)
        {
            var temporada = (TemporadaModel)item;

            var serie = (SerieModel)pai;
            if (Buscar(item, pai) != null)
            {
                Utils.Pausar($"Já existe uma temporada com o nome {temporada.Nome}");
                return false;
            }
            int posicao = Repositorios.Series.IndexOf(serie);
            Repositorios.Series[posicao].Temporadas.Add(temporada);

            Utils.Pausar($"Temporada cadastrada com sucesso!!!");
            return true;
        }

        public object Buscar(object item, object pai)
        {
            var serie = (SerieModel)pai;
            var temporada = (TemporadaModel)item;

            SerieModel select = Repositorios.Series
                .Where(x => x.Nome.Equals(serie.Nome))
                .SingleOrDefault();

            TemporadaModel busca = select.Temporadas
                .Where(x => x.Nome.Equals(temporada.Nome))
                .SingleOrDefault();

            return busca;
        }

        public bool Deletar(object item, object pai)
        {

            var serie = (SerieModel)pai;
            var atual = (TemporadaModel)Buscar(item, serie);
            if (Utils.Perguntar($"Tem certeza que deseja deletar a temporada {atual.Nome} ?"))
            {
                int posicao = Repositorios.Series.IndexOf(serie);
                Repositorios.Series[posicao].Temporadas.Remove(atual);
            }
            return true;
        }

        public bool Editar(object item, object original, object pai)
        {
            var serie = (SerieModel)pai;
            var atual = (TemporadaModel)Buscar(original, serie);
            if (atual == null)
            {
                Utils.Pausar($"Temporada não localizada");
                return false;
            }
            else
            {

                //int posicao = Repositorios.Series.IndexOf(serie);
                int posicaoTemporada = serie.Temporadas.IndexOf(atual);

                //Repositorios.Series[posicao].Temporadas[posicaoTemporada] = (TemporadaModel)item;
                serie.Temporadas[posicaoTemporada] = (TemporadaModel)item;
                Utils.Pausar($"Temporada atualizada com sucesso!!!");
                return true;

            }
        }



        public void Listar(object pai)
        {
            var serie = (SerieModel)pai;

            //int posicao = Repositorios.Series.IndexOf((SerieModel)pai);
            //var lista = Repositorios.Series[posicao].Temporadas;
            var lista = serie.Temporadas;
            if (!lista.Any())
            {
                Utils.Pausar("Não há itens para exibir");
                return;

            }
            Console.WriteLine("Nome\t\tEpisodios");
            foreach (var item in lista)
            {
                Console.WriteLine($"{item.Nome}\t\t{item.Episodios.Count}");
            }
            Utils.Pausar();
        }
    }
}
