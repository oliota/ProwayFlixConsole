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
    class AssistidosMenu : Menu, IMenu, ICadastro
    {
        public AssistidoModel Assistido { get; set; }
        public AssistidoREP Rep { get; set; } = new AssistidoREP();
        public bool Adicionar()
        {
            Assistido = (AssistidoModel)FormularioCompleto();
            if (ValidarCompleto())
                return Rep.Adicionar(Assistido);
            else
                return false;
        }

        public object Converter(string opcao)
        {
            Enum.TryParse(opcao, out Opcoes convertido);
            return convertido;
        }

        public bool Deletar()
        {
            Assistido = (AssistidoModel)FormularioSimples();
            if (ValidarSimples())
                return Rep.Deletar(Assistido);
            else
                return false;
        }

        public bool Editar()
        {
            var Original = (AssistidoModel)FormularioSimples();
            if (ValidarSimples())
            {
                Assistido = (AssistidoModel)FormularioCompleto();
                if (ValidarCompleto())
                    return Rep.Editar(Assistido, Original);
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
            Console.WriteLine("Menu de assistidos, escolha uma opção:");
            foreach (var item in Enum.GetValues(typeof(Opcoes)))
                Console.WriteLine($"\t {(int)item} : {item}");
        }

        public object FormularioCompleto()
        {
            Assistido = new AssistidoModel();
            Console.Clear();
            Console.WriteLine("Informe o email do Usuario"); 
            Assistido.Usuario.Email = Console.ReadLine();

            Console.WriteLine("Informe o nome do Filme");
            Assistido.Filme.Nome = Console.ReadLine();


            Console.WriteLine("Informe o nome da Serie");
            Assistido.Serie.Nome = Console.ReadLine();

            return Assistido;
        }

        public object FormularioSimples()
        {
            Assistido = new AssistidoModel();
            Console.Clear();
            Console.WriteLine("Informe o email do Usuario");
            Assistido.Usuario.Email = Console.ReadLine();

            Console.WriteLine("Informe o nome do Filme");
            Assistido.Filme.Nome = Console.ReadLine();


            Console.WriteLine("Informe o nome da Serie");
            Assistido.Serie.Nome = Console.ReadLine();

            return Assistido;
        }

        public void Listar()
        {
            Rep.Listar();
        }

        public bool ValidarCompleto()
        {
            var MensagemErro = new StringBuilder();
            if (string.IsNullOrWhiteSpace(Assistido.Usuario.Email))
                MensagemErro.AppendLine($"Email do Usuario não pode ficar em branco");

            var Usuario = new UsuarioREP().Buscar(Assistido.Usuario);
            if (Usuario == null)
                MensagemErro.AppendLine($"Usuario não localizado");
            else
                Assistido.Usuario = (UsuarioModel)Usuario;


            if (string.IsNullOrWhiteSpace(Assistido.Filme.Nome) && string.IsNullOrWhiteSpace(Assistido.Serie.Nome))
            {

                MensagemErro.AppendLine($"Informe um filme ou uma serie");
            }

            if (!string.IsNullOrWhiteSpace(Assistido.Filme.Nome))
            {
                var Filme = new FilmeREP().Buscar(Assistido.Filme);
                if (Filme == null)
                    MensagemErro.AppendLine($"Filme não localizado");
                else
                    Assistido.Filme = (FilmeModel)Filme;
            }

            if (!string.IsNullOrWhiteSpace(Assistido.Serie.Nome))
            {
                var Serie = new SerieREP().Buscar(Assistido.Serie);
                if (Serie == null)
                    MensagemErro.AppendLine($"Serie não localizada");
                else
                    Assistido.Serie = (SerieModel)Serie;
            }

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
            if (string.IsNullOrWhiteSpace(Assistido.Usuario.Email))
                MensagemErro.AppendLine($"Email do Usuario não pode ficar em branco");


            if (string.IsNullOrWhiteSpace(Assistido.Filme.Nome) && string.IsNullOrWhiteSpace(Assistido.Serie.Nome))
            {

                MensagemErro.AppendLine($"Informe um filme ou uma serie");
            }

            if (!string.IsNullOrWhiteSpace(MensagemErro.ToString()))
            {
                Utils.Pausar(MensagemErro.ToString());
                return false;
            }

            return true;
        }
    }
}
