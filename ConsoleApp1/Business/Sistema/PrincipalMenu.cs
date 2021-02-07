using ConsoleApp1.Business.Sistema.Serie;
using ConsoleApp1.Model;
using ConsoleApp1.Model.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Business.Sistema
{
    class PrincipalMenu : IMenu
    {
        public string Escolha;
        private enum Opcoes
        {
            Sair = 0,
            Usuarios = 1,
            Filmes = 2,
            Series = 3,
            Relatorios = 4,
            Assistidos = 5
        }


        public object Converter(string opcao)
        {
            Enum.TryParse(opcao, out Opcoes convertido);
            return convertido;
        }

        public void ExecutarEscolha(object opcao)
        {
            switch (opcao)
            {
                case Opcoes.Sair:
                    Escolha = "0";
                    return;
                case Opcoes.Usuarios:
                    new UsuariosMenu().ExibirMenu();
                    break;
                case Opcoes.Filmes:
                    new FilmesMenu().ExibirMenu();
                    break;
                case Opcoes.Series:
                    new SeriesMenu().ExibirMenu();
                    break;
                case Opcoes.Assistidos:
                    new AssistidosMenu().ExibirMenu();
                    break;
                case Opcoes.Relatorios:
                    Console.WriteLine("Menu de relatorios");
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
            if (Repositorios.UsuarioLogado == null)
            {
                ExecutarEscolha(Opcoes.Sair);
                return;
            }
            Console.WriteLine($"Bem vindo, {Repositorios.UsuarioLogado.Nome}");
            Console.WriteLine("Menu de principal, escolha uma opção:");
            foreach (var item in Enum.GetValues(typeof(Opcoes)))
                Console.WriteLine($"\t {(int)item} : {item}");

        }
    }
}
