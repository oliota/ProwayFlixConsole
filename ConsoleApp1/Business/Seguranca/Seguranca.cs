using ConsoleApp1.Business;
using System;

namespace ConsoleApp1
{
    class Seguranca : Login, IMenu
    {

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
                    break;
                case Opcoes.Entrar:
                    FormularioEntrar();
                    break;
                case Opcoes.Cadastrar:
                    FormularioCadastrar();
                    break;
                default:  
                    Utils.Pausar("Opção inválida");
                    break;
            }
        } 
        public void ExibirMenu()
        {
            string opcao;
            do
            {
                ExibirOpcoes();
                opcao = Console.ReadLine();
                ExecutarEscolha(Converter(opcao));
            } while (!opcao.Equals("0"));
        }

        public void ExibirOpcoes()
        {
            Console.Clear();
            Console.WriteLine("Menu de segurança, escolha uma opção:");
            foreach (var item in Enum.GetValues(typeof(Opcoes)))
                Console.WriteLine($"\t {(int)item} : {item}");
        }
    }
}
