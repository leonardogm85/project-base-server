﻿namespace ProjetoBase.Domain.ValueObjects
{
    public class Endereco : ValueObject
    {
        protected Endereco()
        {
        }

        public Endereco(string cep, string logradouro, int numero, string complemento, string bairro, string cidade, string estado)
        {
            Cep = cep;
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
        }

        public string Cep { get; private set; }
        public string Logradouro { get; private set; }
        public int Numero { get; private set; }
        public string Complemento { get; private set; }
        public string Bairro { get; private set; }
        public string Cidade { get; private set; }
        public string Estado { get; private set; }
    }
}
