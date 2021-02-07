using ConsoleApp1.Business.Sistema;
using ConsoleApp1.Model;
using ConsoleApp1.Model.Repositorio;
using System;
using System.Text;

namespace ConsoleApp1.Business
{
    public class Login  
    {
        protected enum Opcoes
        {
            Sair = 0,
            Entrar = 1,
            Cadastrar = 2
        }
        protected UsuarioModel Usuario { get; set; }
        protected int Tentativas { get; set; }



        public void FormularioEntrar()
        {
            Usuario = new UsuarioModel();
            Tentativas = 3;
            do
            {
                Console.Clear();
                Console.WriteLine("Informe o Logon");
                Usuario.Logon = Console.ReadLine();

                Console.WriteLine("Informe a Senha");
                Usuario.Senha = Console.ReadLine();
                if (--Tentativas == 0)
                    return;
            } while (!ValidarEntrada() || Tentativas == 0);
            Entrar();
        }

        public bool ValidarEntrada()
        {
            var select = (UsuarioModel)new UsuarioREP().Buscar(Usuario.Logon, Usuario.Senha);
            if (select != null)
            {
                Usuario = select;
                return true;
            }
            else
            { 
                Utils.Pausar($"Falha ao logar - tentativas restantes: {Tentativas}");
                return false;
            }

        }
        public void Entrar()
        {
            Repositorios.UsuarioLogado = Usuario;
            var sistema = new PrincipalMenu();
            sistema.ExibirMenu();
        }


        public void FormularioCadastrar()
        {
            Usuario = new UsuarioModel();
            Tentativas = 3;
            do
            {
                Console.Clear();
                Console.WriteLine("Informe o Nome");
                Usuario.Nome = Console.ReadLine();

                Console.WriteLine("Informe o Email");
                Usuario.Email = Console.ReadLine();

                Console.WriteLine("Informe o Logon");
                Usuario.Logon = Console.ReadLine();

                Console.WriteLine("Informe a Senha");
                Usuario.Senha = Console.ReadLine();
                if (--Tentativas == 0)
                    return;
            } while (!ValidarCadastro() || Tentativas == 0);
            Cadastrar();

        }

        public bool ValidarCadastro()
        {
            var MensagemErro = new StringBuilder();
            if (string.IsNullOrWhiteSpace(Usuario.Nome))
                MensagemErro.AppendLine($"Nome não pode ficar em branco");
            if (string.IsNullOrWhiteSpace(Usuario.Email))
                MensagemErro.AppendLine($"Email não pode ficar em branco");
            if (string.IsNullOrWhiteSpace(Usuario.Logon))
                MensagemErro.AppendLine($"Logon não pode ficar em branco");
            if (string.IsNullOrWhiteSpace(Usuario.Senha))
                MensagemErro.AppendLine($"Senha não pode ficar em branco");
            if (new UsuarioREP().Buscar(Usuario)!=null)
                MensagemErro.AppendLine($"Já existe um usuario com o email {Usuario.Email}");

            if (!string.IsNullOrWhiteSpace(MensagemErro.ToString()))
            {
                Console.WriteLine(MensagemErro.ToString());  
                Utils.Pausar($"Falha ao cadastrar - tentativas restantes: {Tentativas}");
                return false; 
            }

            return true;
        }
         
        public void Cadastrar()
        {
            new UsuarioREP().Adicionar(Usuario); 
        } 

    }
}
