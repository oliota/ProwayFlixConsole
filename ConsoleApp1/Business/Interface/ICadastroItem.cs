using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Business.Interface
{
    interface ICadastroItem
    {
        object FormularioCompleto();
        object FormularioSimples();
        bool ValidarCompleto();
        bool ValidarSimples();
        void Listar(object pai);
        bool Adicionar(object pai);
        bool Editar(object pai);
        bool Deletar(object pai);
    }
}
