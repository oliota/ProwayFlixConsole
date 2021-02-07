using ConsoleApp1.Model;
using ConsoleApp1.Model.Repositorio;
using System;
using System.Text;

namespace ConsoleApp1.Business.Sistema
{
    class FilmesMenu : Menu,IMenu, ICadastro
    {
        public FilmeModel Filme { get; set; }
        public FilmeREP Rep { get; set; } = new FilmeREP();
        public bool Adicionar()
        {
            Filme = (FilmeModel)FormularioCompleto();
            if (ValidarCompleto())
                return Rep.Adicionar(Filme);
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
            Filme = (FilmeModel)FormularioSimples();
            if (ValidarSimples())
                return Rep.Deletar(Filme);
            else
                return false;
        }
        public bool Editar()
        {
            var Original = (FilmeModel)FormularioSimples();
            if (ValidarSimples())
            {
                Filme = (FilmeModel)FormularioCompleto();
                if (ValidarCompleto())
                    return Rep.Editar(Filme, Original);
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
            Console.WriteLine("Menu de filmes, escolha uma opção:");
            foreach (var item in Enum.GetValues(typeof(Opcoes)))
                Console.WriteLine($"\t {(int)item} : {item}");
        }
        public object FormularioCompleto()
        {
            Filme = new FilmeModel();
            Console.Clear();
            Console.WriteLine("Informe o Nome");
            Filme.Nome = Console.ReadLine();

            Console.WriteLine("Informe o Ano");
            Filme.Ano =Int32.Parse(Console.ReadLine()); 
             
            return Filme;
        }
        public object FormularioSimples()
        {
            Filme = new FilmeModel();
            Console.Clear();

            Console.WriteLine("Informe o Nome atual do filme");
            Filme.Nome = Console.ReadLine();
            return Filme;
        }
        public void Listar()
        {
            Rep.Listar();
        }
        public bool ValidarCompleto()
        {
            var MensagemErro = new StringBuilder();
            if (string.IsNullOrWhiteSpace(Filme.Nome))
                MensagemErro.AppendLine($"Nome não pode ficar em branco");
            if (string.IsNullOrWhiteSpace(Filme.Ano.ToString()))
                MensagemErro.AppendLine($"Ano não pode ficar em branco"); 
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
            if (string.IsNullOrWhiteSpace(Filme.Nome))
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
