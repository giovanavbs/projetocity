using aspnet_projeto.Models;

namespace aspnet_projeto.Repositorio
{
    public interface IClienteRepositorio
    {
        // crud
        //login
        Cliente Login(string Email, string Senha); // chamando a model cliente e passando os parametros para o metodo login

        // cadastrar cliente
        void Cadastrar(Cliente cliente);
        //Buscar Todos os clientes
        IEnumerable<Cliente> TodosClientes(); //lista de ARRAY dos cliente tudo
    }
}
