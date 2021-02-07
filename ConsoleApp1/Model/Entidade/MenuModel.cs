using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Model
{
    class Menu
    {
        public string Escolha;
        public enum Opcoes
        {
            Voltar = 0,
            Listar = 1,
            Adicionar = 2,
            Editar = 3,
            Deletar = 4
        }
    }
}
