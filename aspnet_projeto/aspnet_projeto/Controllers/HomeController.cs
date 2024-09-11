using aspnet_projeto.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using aspnet_projeto.Repositorio;
using aspnet_projeto.Libraries.Login;

namespace aspnet_projeto.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger; // recurso essencial para detectar ou investigar problemas(loggs) na injeção de dependencias
                                                          // interface = definir um conjunto de comportamentos que varias classes diferentes devem implementar
                                                          // - o mais proximo de uma classe abstrata(mas a abstrata nao deixa manipular varias classes diferentes)
        
        private IClienteRepositorio? _clienteRepositorio; // _ = variavel(significa prioridade), ? = significa que vai receber conjunto de string
        private LoginCliente _loginCliente;

        public HomeController(ILogger<HomeController> logger, IClienteRepositorio clienteRepositorio, LoginCliente loginCliente) // classe por isso sem o _(se passar como variavel vem nulo) - cliente(interface), login(classe)
        {
            _logger = logger;
            _clienteRepositorio = clienteRepositorio;
            _loginCliente = loginCliente;
        }
        // pagina index do cliente

        public IActionResult Index() // mais uma interface
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // Página de Login
        // chamar a view da pagina login
        public IActionResult Login()
        {
            return View();
        }
        // Carrega a a tela login
        // Carrega a a tela login
        // 2 login pq um recebe os dados

        [HttpPost]
        public IActionResult Login(Cliente cliente)
        {
            Cliente loginDB = _clienteRepositorio.Login(cliente.Email, cliente.Senha);

            if (loginDB.Email != null && loginDB.Senha != null)
            {
                _loginCliente.Login(loginDB);
                return new RedirectResult(Url.Action(nameof(PainelCliente)));
            }
            else
            {
                //Erro na sessão
                ViewData["msg"] = "Errou a senha bobona"; // sacola que leva mensagem
                return View();
            }

        }

        public IActionResult PainelCliente()
        { // retorna na pagina a lista de todos os clientes cadastrados seila
            return View(_clienteRepositorio.TodosClientes()); //chamando um metodo novo pra mostrar os crias cadastrados 
                                                            // o metodo é criando no clienterepositorio e eh chamado no Iclienterepositorio
        }
        
        // criando pagina cadastro de cliente, toda vez que for fazrt o cadastrar, alterar(crud) precida do httpost (duplo actionresult)
        public IActionResult CadastrarCliente()
        {
            return View();
        }

        // pagina cadastro cliente que envia os dados(post)
        [HttpPost]
        public IActionResult CadastrarCliente(Cliente cliente)
        {
            // metodo cadastrar
            _clienteRepositorio.Cadastrar(cliente);

            //redireciona para PainelCliente
            return RedirectToAction(nameof(PainelCliente)); // nameof - buscar o nome de algo
        }

        
    }
}
