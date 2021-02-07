using ConsoleApp1.Model;
using ConsoleApp1.Model.Entidade;
using ConsoleApp1.Model.Repositorio;
using System;
using System.Text;

namespace ConsoleApp1.Business.Sistema.Serie
{
    class SeriesMenu : Menu, IMenu, ICadastro
    {
        public enum OpcoesExtras
        {
            Temporadas = 5
        }
        public SerieModel Serie { get; set; }
        public SerieREP Rep { get; set; } = new SerieREP();
        public bool Adicionar()
        {
            Serie = (SerieModel)FormularioCompleto();
            if (ValidarCompleto())
                return Rep.Adicionar(Serie);
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
        public bool Deletar()
        {
            Serie = (SerieModel)FormularioSimples();
            if (ValidarSimples())
                return Rep.Deletar(Serie);
            else
                return false;
        }
        public bool Editar()
        {
            var Original = (SerieModel)FormularioSimples();
            if (ValidarSimples())
            {
                Serie = (SerieModel)FormularioCompleto();
                if (ValidarCompleto())
                    return Rep.Editar(Serie, Original);
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
                    Listar();
                    break;
                case Opcoes.Adicionar:
                    Adicionar();
                    break;
                case Opcoes.Editar:
                    Editar();
                    break;
                case Opcoes.Deletar:
                    Deletar();
                    break;
                case OpcoesExtras.Temporadas:

                    var serie=FormularioSimples();
                    if (ValidarSimples())
                    {
                        Serie = (SerieModel)Rep.Buscar(serie);
                        if (Serie == null)
                        {
                            Utils.Pausar("Serie não localizada");
                            return;
                        }
                        var TemporadasMenu = new TemporadasMenu();
                        TemporadasMenu.Serie = Serie;
                        TemporadasMenu.ExibirMenu();
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
            Console.WriteLine("Menu de series, escolha uma opção:");
            foreach (var item in Enum.GetValues(typeof(Opcoes)))
                Console.WriteLine($"\t {(int)item} : {item}");
            foreach (var item in Enum.GetValues(typeof(OpcoesExtras)))
                Console.WriteLine($"\t {(int)item} : {item}");
        }
        public object FormularioCompleto()
        {
            Serie = new SerieModel();
            Console.Clear();
            Console.WriteLine("Informe o Nome");
            Serie.Nome = Console.ReadLine();
            return Serie;
        }
        public object FormularioSimples()
        {
            Serie = new SerieModel();
            Console.Clear();

            Console.WriteLine("Informe o Nome atual da Serie");
            Serie.Nome = Console.ReadLine();
            return Serie;
        }
        public void Listar()
        {
            Rep.Listar();
        }
        public bool ValidarCompleto()
        {
            var MensagemErro = new StringBuilder();
            if (string.IsNullOrWhiteSpace(Serie.Nome))
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
            if (string.IsNullOrWhiteSpace(Serie.Nome))
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
