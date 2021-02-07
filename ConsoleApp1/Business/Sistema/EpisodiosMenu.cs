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
    class EpisodiosMenu : Menu, IMenu, ICadastroItem
    {
         
        public TemporadaModel Temporada { get; set; }
        public EpisodioModel Episodio { get; set; }
        public EpisodioREP Rep { get; set; } = new EpisodioREP();
        public bool Adicionar(object pai)
        {
            Episodio = (EpisodioModel)FormularioCompleto();
            if (ValidarCompleto())
                return Rep.Adicionar( Episodio, Temporada);
            else
                return false;
        }
        public object Converter(string opcao)
        {
            Enum.TryParse(opcao, out Opcoes convertido);

            return convertido;

        }
        public bool Deletar(object pai)
        {
            Episodio = (EpisodioModel)FormularioSimples();
            if (ValidarSimples())
                return Rep.Deletar( Episodio, pai);
            else
                return false;
        }
        public bool Editar(object pai)
        {
            var Original = (EpisodioModel)FormularioSimples();
            if (ValidarSimples())
            {
                Episodio = (EpisodioModel)FormularioCompleto();
                if (ValidarCompleto())
                    return Rep.Editar( Episodio, Original, pai);
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
                    Listar(Temporada);
                    break;
                case Opcoes.Adicionar:
                    Adicionar(Temporada);
                    break;
                case Opcoes.Editar:
                    Editar(Temporada);
                    break;
                case Opcoes.Deletar:
                    Deletar(Temporada);
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
            Console.WriteLine("Menu de episodio, escolha uma opção:");
            foreach (var item in Enum.GetValues(typeof(Opcoes)))
                Console.WriteLine($"\t {(int)item} : {item}"); 
        }
        public object FormularioCompleto()
        {
            Episodio = new EpisodioModel();
            Console.Clear();
            Console.WriteLine("Informe o Nome");
            Episodio.Nome = Console.ReadLine();
            return Episodio;
        }
        public object FormularioSimples()
        {
            Episodio = new EpisodioModel();
            Console.Clear();

            Console.WriteLine("Informe o Nome atual do Episodio");
            Episodio.Nome = Console.ReadLine();
            return Episodio;
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
