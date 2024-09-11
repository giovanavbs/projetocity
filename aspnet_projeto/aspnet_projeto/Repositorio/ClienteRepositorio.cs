using MySql.Data.MySqlClient;
using aspnet_projeto.Models;
using System.Data; // sistema entender que vamos manipular dados
namespace aspnet_projeto.Repositorio
{
    // chamar a interface com herança
    public class ClienteRepositorio : IClienteRepositorio
    {
        //declarando a varival de da string de conxão

        private readonly string? _conexaoMySQL;

        //metodo da conexão com banco de dados
        public ClienteRepositorio(IConfiguration conf) => _conexaoMySQL = conf.GetConnectionString("ConexaoMySQL");

        //Login Cliente(metodo )

        public Cliente Login(string Email, string Senha)
        {
            //usando a variavel conexao 
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                //abre a conexão com o banco de dados
                 conexao.Open();

                // variavel cmd que receb o select do banco de dados buscando o email e a senha
                MySqlCommand cmd = new MySqlCommand("select * from cliente where email = @Email and senha = @Senha", conexao);

                //os paramentros do email e da senha 
                cmd.Parameters.Add("@Email", MySqlDbType.VarChar).Value = Email; // @email vem do banco e o azul vem do visual studio
                cmd.Parameters.Add("@Senha", MySqlDbType.VarChar).Value = Senha;

                //Le os dados que foram pegados do email e senha do banco de dados
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                //guarda os dados que foi pego do email e senha do banco de dados
                MySqlDataReader dr;

                //instanciando a model cliente
                Cliente cliente = new Cliente();
                //executando os comandos do mysql e passsando para a variavel dr
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                //verifica todos os dados que foram pego do banco e pega o email e senha
                while (dr.Read())
                {
                    cliente.Email = Convert.ToString(dr["email"]);
                    cliente.Senha = Convert.ToString(dr["senha"]);
                }
                return cliente;
            }
        }

        // metodo cadastrar cliente
        public void Cadastrar(Cliente cliente)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))

            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("insert into cliente (nome,telefone,email) values (@nome, @telefone, @email)", conexao); // @: PARAMETRO

                cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = cliente.Nome;
                cmd.Parameters.Add("@telefone", MySqlDbType.VarChar).Value = cliente.Telefone;
                cmd.Parameters.Add("@email", MySqlDbType.VarChar).Value = cliente.Email;

                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }
        //listando os crias cadastrados
        //lista todos os clientes

        public IEnumerable<Cliente> TodosClientes()
        {
            List<Cliente> Clientlist = new List<Cliente>();

            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * from cliente", conexao);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                conexao.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    Clientlist.Add(
                            new Cliente
                            {
                                Codigo = Convert.ToInt32(dr["codigo"]),
                                Nome = ((string)dr["nome"]),
                                Telefone = ((string)dr["telefone"]),
                                Email = ((string)dr["email"]),

                            });
                }
                return Clientlist;

            }
        }

        //buscar todos os clientes por id
        public Cliente ObterCliente(int Id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new("SELECT * from cliente ", conexao);
                cmd.Parameters.AddWithValue("@codigo", Id);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr;

                Cliente cliente = new Cliente();
                // retorna conjunto de resultado ,  é funcionalmente equivalente a chamar ExecuteReader().
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    cliente.Codigo = Convert.ToInt32(dr["codigo"]);
                    cliente.Nome = (string)(dr["nome"]);
                    cliente.Telefone = (string)(dr["telefone"]);
                    cliente.Email = (string)(dr["email"]);

                }
                return cliente;
            }
        }
    }
}
