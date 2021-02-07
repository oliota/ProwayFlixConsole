using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Model.Interface
{
    interface IRepositorioItem
    {
        bool Adicionar(object item,object pai);
        bool Editar(object item, object original, object pai);
        bool Deletar(object item, object pai); 
        void Listar(object pai);
        object Buscar(object item, object pai);
    }
}
