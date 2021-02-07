using ConsoleApp1.Business;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1.Model.Repositorio
{
    public class UsuarioREP : IRepositorio
    {
        public bool Adicionar(object item)
        {

            var usuario = (UsuarioModel)item;
            if (Buscar(item) != null)
            {
                Utils.Pausar($"Já existe um usuario com o email {usuario.Email}");
                return false;
            }
            Repositorios.Usuarios.Add(usuario);
            Utils.Pausar($"Usuario cadastrado com sucesso!!!");
            return true;
        }

        public object Buscar(object item)
        {
            var usuario = (UsuarioModel)item;

            UsuarioModel select = Repositorios.Usuarios
                .Where(x => x.Email.Equals(usuario.Email))
                .SingleOrDefault();

            return select;
        }
        public object Buscar(string logon, string senha)
        {
            UsuarioModel select = Repositorios.Usuarios
                .Where(x => x.Logon.Equals(logon))
                .Where(x => x.Senha.Equals(senha))
                .SingleOrDefault();


            return select;
        }

        public bool Deletar(object item)
        {
            var atual = (UsuarioModel)Buscar(item); 
            Repositorios.Usuarios.Remove(atual);
            return true;
        }

        public bool Editar(object item, object original)
        {
            var usuario = (UsuarioModel)item;
            var atual = (UsuarioModel)Buscar(original);
            if (atual == null)
            {
                Utils.Pausar($"Usuário não localizado");
                return false;
            }
            else
            {
                int posicao = Repositorios.Usuarios.IndexOf(atual);
                Repositorios.Usuarios[posicao] = usuario;
                Utils.Pausar($"Usuario atualizado com sucesso!!!");
                return true;

            }
        }

        public List<object> Listar(bool metodo)
        {
            return Repositorios.Usuarios.Cast<object>().ToList();
        }
        public void Listar()
        {
            var lista = Repositorios.Usuarios;
            if (!lista.Any())
            {
                Utils.Pausar("Não há itens para exibir");
                return;

            }
            Console.WriteLine("Nome\t\tEmail\t\tLogon\t\tSenha");
            foreach (var item in lista)
            {
                Console.WriteLine($"{item.Nome}\t\t{item.Email}\t\t{item.Logon}\t\t{item.Senha}");
            }
            Utils.Pausar();
        }
    }
}
