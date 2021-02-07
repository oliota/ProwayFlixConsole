using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Business
{
    interface ICadastro
    {
        object FormularioCompleto();
        object FormularioSimples();
        bool ValidarCompleto();
        bool ValidarSimples(); 
        void Listar();
        bool Adicionar();
        bool Editar();
        bool Deletar();
    }
}
