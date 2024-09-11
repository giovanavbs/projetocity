﻿namespace aspnet_projeto.Models
{
    public class Cliente
    {
        //CRIANDO O ENCAPSULAMENTO DO OBJETO COM GET E SET
        // ARRAY
        public int Codigo { get; set; }
        public string? Nome { get; set; }
        public string? Telefone { get; set; }
        public string? Email { get; set; }
        public string? Senha { get; set; }
        public List<Cliente>? ListaCliente { get; set; }
    }
}
