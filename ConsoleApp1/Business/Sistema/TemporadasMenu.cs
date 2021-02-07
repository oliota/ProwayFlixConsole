using ConsoleApp1.Business.Interface;
using ConsoleApp1.Model;
using ConsoleApp1.Model.Entidade;
using ConsoleApp1.Model.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Business.Sistema
{
    class TemporadasMenu : Menu, IMenu, ICadastroItem
    {
        public enum OpcoesExtras
        {
            Episodios = 5
        }
        public SerieModel Serie { get; set; }
        public TemporadaModel Temporada { get; set; }
        public TemporadaREP Rep { get; set; } = new TemporadaREP();
        public bool Adicionar(object pai)
        {
            Temporada = (TemporadaModel)FormularioCompleto();
            if (ValidarCompleto())
                return Rep.Adicionar(Temporada, Serie);
            else
                return false;
        }
        public object Converter(string opcao)
        {
            Enum.TryParse(opcao, out Opcoes convertido);

            if (Enum.IsDefined(typeof(Opcoes), convertido))
                return convertido;
            else
            {
                Enum.TryParse(opcao, out OpcoesExtras extra);
                return extra;
            }
        }
        public bool Deletar(object pai)
        {
            Temporada = (TemporadaModel)FormularioSimples();
            if (ValidarSimples())
                return Rep.Deletar(Temporada, pai);
            else
                return false;
        }
        public bool Editar(object pai)
        {
            var Original = (TemporadaModel)FormularioSimples();
            if (ValidarSimples())
            {
                Temporada = (TemporadaModel)FormularioCompleto();
                if (ValidarCompleto())
                    return Rep.Editar(Temporada, Original, pai);
                else
                    return false;
            }
            else
                return false;

        }
        public void ExecutarEscolha(object opcao)
        {
            switch (opcao)
            {
                case Opcoes.Voltar:
                    Escolha = "0";
                    break;
                case Opcoes.Listar:
                    Listar(Serie);
                    break;
                case Opcoes.Adicionar:
                    Adicionar(Serie);
                    break;
                case Opcoes.Editar:
                    Editar(Serie);
                    break;
                case Opcoes.Deletar:
                    Deletar(Serie);
                    break;
                case OpcoesExtras.Episodios:
                    var temporada = FormularioSimples();
                    if (ValidarSimples())
                    {
                        Temporada = (TemporadaModel)Rep.Buscar(temporada,Serie);
                        if (Serie == null)
                        {
                            Utils.Pausar("Temporada não localizada");
                            return;
                        }
                        var EpisodiosMenu = new EpisodiosMenu();
                        EpisodiosMenu.Temporada = Temporada;
                        EpisodiosMenu.ExibirMenu();
                    }

                    break;
                default:
                    Utils.Pausar("Opção inválida");
                    break;
            }
        }
        public void ExibirMenu()
        {
            do
            {
                ExibirOpcoes();
                Escolha = Console.ReadLine();
                ExecutarEscolha(Converter(Escolha));
            } while (!Escolha.Equals("0"));
        }
        public void ExibirOpcoes()
        {
            Console.Clear();
            Console.WriteLine("Menu de temporadas, escolha uma opção:");
            foreach (var item in Enum.GetValues(typeof(Opcoes)))
                Console.WriteLine($"\t {(int)item} : {item}");
            foreach (var item in Enum.GetValues(typeof(OpcoesExtras)))
                Console.WriteLine($"\t {(int)item} : {item}");
        }
        public object FormularioCompleto()
        {
            Temporada = new TemporadaModel();
            Console.Clear();
            Console.WriteLine("Informe o Nome");
            Temporada.Nome = Console.ReadLine();
            return Temporada;
        }
        public object FormularioSimples()
        {
            Temporada = new TemporadaModel();
            Console.Clear();

            Console.WriteLine("Informe o Nome atual da Temporada");
            Temporada.Nome = Console.ReadLine();
            return Temporada;
        }
        public void Listar(object pai)
        {
            Rep.Listar(pai);
        }
        public bool ValidarCompleto()
        {
            var MensagemErro = new StringBuilder();
            if (string.IsNullOrWhiteSpace(Temporada.Nome))
                MensagemErro.AppendLine($"Nome não pode ficar em branco");

            if (!string.IsNullOrWhiteSpace(MensagemErro.ToString()))
            {
                Utils.Pausar(MensagemErro.ToString());
                return false;
            }

            return true;
        }
        public bool ValidarSimples()
        {
            var MensagemErro = new StringBuilder();
            if (string.IsNullOrWhiteSpace(Temporada.Nome))
                MensagemErro.AppendLine($"Nome não pode ficar em branco");
            if (!string.IsNullOrWhiteSpace(MensagemErro.ToString()))
            {
                Utils.Pausar(MensagemErro.ToString());
                return false;
            }

            return true;
        }
    }
}
