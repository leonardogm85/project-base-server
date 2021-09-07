using ProjetoBase.Domain.Enums;
using ProjetoBase.Domain.ValueObjects;
using ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Constants;
using ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Contracts;
using ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Notifications;
using System;

namespace ProjetoBase.Domain.Entities
{
    public abstract class Pessoa : Entity
    {
        protected Pessoa()
        {
        }

        public Pessoa(Guid id, bool ativo, byte[] versao, TipoPessoa tipoPessoa, string apelido, string nome, string documento, string email, string celular,
                      string telefone, Endereco endereco)
            : base(id, ativo, versao)
        {
            TipoPessoa = tipoPessoa;
            Apelido = apelido;
            Nome = nome;
            Documento = documento;
            Email = email;
            Celular = celular;
            Telefone = telefone;
            Endereco = endereco;
        }

        public TipoPessoa TipoPessoa { get; private set; }
        public string Apelido { get; private set; }
        public string Nome { get; private set; }
        public string Documento { get; private set; }
        public string Email { get; private set; }
        public string Celular { get; private set; }
        public string Telefone { get; private set; }
        public Endereco Endereco { get; private set; }

        public override NotificationResult Validar()
        {
            var result = new NotificationResult();

            result.AddNotifications(new NotificationContract()
                .IsntEmpty(Id, ValidationMessages.EntidadeCodigoDeveSerPreenchido)

                .IsDefined(TipoPessoa, ValidationMessages.PessoaTipoPessoaDeveSerPreenchido)

                .HasMaxLength(Apelido, 250, ValidationMessages.PessoaApelidoDeveTerUmTamanhoMaximo)

                .IsntNullOrWhiteSpace(Nome, ValidationMessages.PessoaNomeDeveSerPreenchido)
                .HasMaxLength(Nome, 250, ValidationMessages.PessoaNomeDeveTerUmTamanhoMaximo)

                .IsntNullOrWhiteSpace(Documento, ValidationMessages.PessoaDocumentoDeveSerPreenchido)

                .IsntNullOrWhiteSpace(Email, ValidationMessages.PessoaEmailDeveSerPreenchido)
                .HasMaxLength(Email, 250, ValidationMessages.PessoaEmailDeveTerUmTamanhoMaximo)
                .IsEmail(Email, ValidationMessages.PessoaEmailDeveSerValido)

                .IsntNullOrWhiteSpace(Celular, ValidationMessages.PessoaCelularDeveSerPreenchido)
                .IsPhone(Celular, ValidationMessages.PessoaCelularDeveSerValido)

                .IsPhone(Telefone, ValidationMessages.PessoaTelefoneDeveSerValido)

                .IsntNull(Endereco, ValidationMessages.EnderecoDeveSerPreenchido));

            switch (TipoPessoa)
            {
                case TipoPessoa.PessoaFisica:
                    result.AddNotifications(new NotificationContract().IsCpf(Documento, ValidationMessages.PessoaDocumentoDeveSerValido));
                    break;
                case TipoPessoa.PessoaJuridica:
                    result.AddNotifications(new NotificationContract().IsCnpj(Documento, ValidationMessages.PessoaDocumentoDeveSerValido));
                    break;
                default:
                    result.AddNotifications(new NotificationContract().IsDocument(Documento, ValidationMessages.PessoaDocumentoDeveSerValido));
                    break;
            }

            if (Endereco == null)
            {
                return result;
            }

            result.AddNotifications(new NotificationContract()
                .IsntNullOrWhiteSpace(Endereco.Cep, ValidationMessages.EnderecoCepDeveSerPreenchido)
                .IsZipCode(Endereco.Cep, ValidationMessages.EnderecoCepDeveSerValido)

                .IsntNullOrWhiteSpace(Endereco.Logradouro, ValidationMessages.EnderecoLogradouroDeveSerPreenchido)
                .HasMaxLength(Endereco.Logradouro, 100, ValidationMessages.EnderecoLogradouroDeveTerUmTamanhoMaximo)

                .IsGreaterThan(Endereco.Numero, 0, ValidationMessages.EnderecoNumeroDeveTerUmTamanhoMinimo)
                .IsLowerOrEqualsThan(Endereco.Numero, 999999999, ValidationMessages.EnderecoNumeroDeveTerUmTamanhoMaximo)

                .HasMaxLength(Endereco.Complemento, 50, ValidationMessages.EnderecoComplementoDeveTerUmTamanhoMaximo)

                .IsntNullOrWhiteSpace(Endereco.Bairro, ValidationMessages.EnderecoBairroDeveSerPreenchido)
                .HasMaxLength(Endereco.Bairro, 100, ValidationMessages.EnderecoBairroDeveTerUmTamanhoMaximo)

                .IsntNullOrWhiteSpace(Endereco.Cidade, ValidationMessages.EnderecoCidadeDeveSerPreenchido)
                .HasMaxLength(Endereco.Cidade, 100, ValidationMessages.EnderecoCidadeDeveTerUmTamanhoMaximo)

                .IsntNullOrWhiteSpace(Endereco.Estado, ValidationMessages.EnderecoEstadoDeveSerPreenchido)
                .HasLength(Endereco.Estado, 2, ValidationMessages.EnderecoEstadoDeveTerUmTamanhoExato));

            return result;
        }
    }
}
