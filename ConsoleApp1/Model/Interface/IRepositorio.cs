using System.Collections.Generic;

namespace ConsoleApp1.Model.Repositorio
{
    interface IRepositorio
    {
        bool Adicionar(object item);
        bool Editar(object item,object original);
        bool Deletar(object item);
        List<object> Listar(bool metodo);
        void Listar();
        object Buscar(object item);
    }
}
