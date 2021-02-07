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
    class EpisodioREP : IRepositorioItem
    {
        public bool Adicionar(object item, object pai)
        {
            var episodio = (EpisodioModel)item;
            var temporada = (TemporadaModel)pai;


            if (Buscar(item, pai) != null)
            {
                Utils.Pausar($"Já existe um episodio com o nome {temporada.Nome}");
                return false;
            }
            temporada.Episodios.Add(episodio);

            Utils.Pausar($"Episodio cadastrado com sucesso!!!");
            return true;
        }

        public object Buscar(object item, object pai)
        {
            var temporada = (TemporadaModel)pai;
            var episodio = (EpisodioModel)item;
             
            EpisodioModel busca = temporada.Episodios
                .Where(x => x.Nome.Equals(episodio.Nome))
                .SingleOrDefault();

            return busca;
        }

        public bool Deletar(object item, object pai)
        {

            var temporada = (TemporadaModel)pai;
            var atual = (EpisodioModel)Buscar(item, pai);
            if (Utils.Perguntar($"Tem certeza que deseja deletar o episodio {atual.Nome} ?"))
            { 
                temporada.Episodios.Remove(atual);
            }
            return true;
        }

        public bool Editar(object item, object original, object pai)
        {
            var temporada = (TemporadaModel)pai;
            var atual = (EpisodioModel)Buscar(item, pai);
            if (atual == null)
            {
                Utils.Pausar($"Episodio não localizado");
                return false;
            }
            else
            {
                 
                int posicaoTemporada = temporada.Episodios.IndexOf(atual); 
                temporada.Episodios[posicaoTemporada] = (EpisodioModel)item;
                Utils.Pausar($"Episodio atualizado com sucesso!!!");
                return true;

            }
        }



        public void Listar(object pai)
        {

            var temporada = (TemporadaModel)pai;
            var lista = temporada.Episodios;
            if (!lista.Any())
            {
                Utils.Pausar("Não há itens para exibir");
                return;

            }
            Console.WriteLine("Nome");
            foreach (var item in lista)
            {
                Console.WriteLine($"{item.Nome}");
            }
            Utils.Pausar();
        }
    }
}
