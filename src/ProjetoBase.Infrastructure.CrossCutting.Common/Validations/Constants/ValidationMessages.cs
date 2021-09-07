namespace ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Constants
{
    public static class ValidationMessages
    {
        public const string EntidadeCodigoDeveSerPreenchido = "Código deve ser preenchido.";

        public const string PessoaApelidoDeveTerUmTamanhoMaximo = "Apelido / Nome fantasia deve ter no máximo 250 caracteres.";
        public const string PessoaCelularDeveSerPreenchido = "Celular deve ser preenchido.";
        public const string PessoaCelularDeveSerValido = "Celular deve ser válido.";
        public const string PessoaDocumentoDeveSerPreenchido = "CPF / CNPJ deve ser preenchido.";
        public const string PessoaDocumentoDeveSerUnico = "CPF / CNPJ deve ser único.";
        public const string PessoaDocumentoDeveSerValido = "CPF / CNPJ deve ser válido.";
        public const string PessoaEmailDeveSerPreenchido = "Email deve ser preenchido.";
        public const string PessoaEmailDeveSerValido = "Email deve ser válido.";
        public const string PessoaEmailDeveTerUmTamanhoMaximo = "Email deve ter no máximo 250 caracteres.";
        public const string PessoaNomeDeveSerPreenchido = "Nome / Razão social deve ser preenchido.";
        public const string PessoaNomeDeveTerUmTamanhoMaximo = "Nome / Razão social deve ter no máximo 250 caracteres.";
        public const string PessoaTelefoneDeveSerValido = "Telefone deve ser válido.";
        public const string PessoaTipoPessoaDeveSerPreenchido = "Tipo de pessoa deve ser preenchido.";

        public const string EnderecoBairroDeveSerPreenchido = "Bairro deve ser preenchido.";
        public const string EnderecoBairroDeveTerUmTamanhoMaximo = "Bairro deve ter no máximo 100 caracteres.";
        public const string EnderecoCepDeveSerPreenchido = "CEP deve ser preenchido.";
        public const string EnderecoCepDeveSerValido = "CEP deve ser válido.";
        public const string EnderecoCidadeDeveSerPreenchido = "Cidade deve ser preenchido.";
        public const string EnderecoCidadeDeveTerUmTamanhoMaximo = "Cidade deve ter no máximo 100 caracteres.";
        public const string EnderecoComplementoDeveTerUmTamanhoMaximo = "Complemento deve ter no máximo 50 caracteres.";
        public const string EnderecoDeveSerPreenchido = "Endereço deve ser preenchido.";
        public const string EnderecoEstadoDeveSerPreenchido = "Estado deve ser preenchido.";
        public const string EnderecoEstadoDeveTerUmTamanhoExato = "Estado deve ter exatamente 2 caracteres.";
        public const string EnderecoLogradouroDeveSerPreenchido = "Logradouro deve ser preenchido.";
        public const string EnderecoLogradouroDeveTerUmTamanhoMaximo = "Logradouro deve ter no máximo 100 caracteres.";
        public const string EnderecoNumeroDeveTerUmTamanhoMinimo = "Número deve ser maior que 0.";
        public const string EnderecoNumeroDeveTerUmTamanhoMaximo = "Número deve ser menor ou igual a 999.999.999.";

        public const string ClienteNaoDevemExistirPedidosAssociado = "Não é permitido remover cliente com pedidos associado.";

        public const string FornecedorNaoDevemExistirProdutosAssociado = "Não é permitido remover fornecedor com produtos associado.";

        public const string UnidadeMedidaNaoDevemExistirProdutosAssociado = "Não é permitido remover unidade de medida com produtos associado.";
        public const string UnidadeMedidaNomeDeveSerPreenchido = "Nome deve ser preenchido.";
        public const string UnidadeMedidaNomeDeveTerUmTamanhoMaximo = "Nome deve ter no máximo 50 caracteres.";
        public const string UnidadeMedidaSiglaDeveSerPreenchido = "Sigla deve ser preenchido.";
        public const string UnidadeMedidaSiglaDeveSerUnica = "Sigla deve ser única.";
        public const string UnidadeMedidaSiglaDeveTerUmTamanhoMaximo = "Sigla deve ter no máximo 5 caracteres.";

        public const string ProdutoDescricaoDeveSerPreenchido = "Descrição deve ser preenchido.";
        public const string ProdutoDescricaoDeveTerUmTamanhoMaximo = "Descrição deve ter no máximo 900 caracteres.";
        public const string ProdutoFornecedorDeveSerPreenchido = "Fornecedor deve ser preenchido.";
        public const string ProdutoNaoDevemExistirItensPedidoAssociado = "Não é permitido remover produto com itens de pedido associado.";
        public const string ProdutoNomeDeveSerPreenchido = "Nome deve ser preenchido.";
        public const string ProdutoNomeDeveSerUnico = "Nome deve ser único.";
        public const string ProdutoNomeDeveTerUmTamanhoMaximo = "Nome deve ter no máximo 50 caracteres.";
        public const string ProdutoUnidadeMedidaDeveSerPreenchido = "Unidade de medida deve ser preenchido.";
        public const string ProdutoValorDeveEstarEmUmIntervalo = "Valor deve ser maior ou igual a 0 e menor ou igual a 999.999.999.";

        public const string PedidoClienteDeveSerPreenchido = "Cliente deve ser preenchido.";
        public const string PedidoDataEntregaDeveTerUmValorMinimo = "Data da entrega deve ser maior ou igual a data do pedido.";
        public const string PedidoDataPedidoDeveTerUmValorMaximo = "Data do pedido deve ser menor ou igual a data de hoje.";
        public const string PedidoDataPedidoDeveTerUmValorMinimo = "Data do pedido deve ser maior ou igual a data de 01/01/1753.";
        public const string PedidoDescontoDeveEstarEmUmIntervalo = "Desconto deve ser maior ou igual a 0 e menor ou igual ao total do pedido.";
        public const string PedidoItensPedidoDeveSerPreenchido = "Itens do pedido deve ser preenchido.";
        public const string PedidoObservacaoDeveTerUmTamanhoMaximo = "Observação deve ter no máximo 900 caracteres.";
        public const string PedidoSubtotalDeveTerUmTamanhoExato = "Subtotal deve ser exatamente a soma do total dos itens do pedido.";
        public const string PedidoTotalDeveTerUmTamanhoExato = "Total deve ser exatamente a subtração entre subtotal e desconto.";

        public const string ItemPedidoDescontoDeveEstarEmUmIntervalo = "Desconto deve ser maior ou igual a 0 e menor ou igual ao total do item do pedido.";
        public const string ItemPedidoProdutoDeveSerPreenchido = "Produto deve ser preenchido.";
        public const string ItemPedidoPedidoDeveSerAssociado = "Pedido deve ser associado ao item.";
        public const string ItemPedidoQuantidadeDeveTerUmTamanhoMinimo = "Quantidade deve ser maior que 0.";
        public const string ItemPedidoQuantidadeDeveTerUmTamanhoMaximo = "Quantidade deve ser menor ou igual a 999.999.999.";
        public const string ItemPedidoSubtotalDeveTerUmTamanhoExato = "Subtotal deve ser exatamente a multiplicação entre quantidade de itens e valor do produto.";
        public const string ItemPedidoTotalDeveTerUmTamanhoExato = "Total deve ser exatamente a subtração entre subtotal e desconto.";

        public const string PapelDescricaoDeveSerPreenchido = "Descrição deve ser preenchido.";
        public const string PapelDescricaoDeveTerUmTamanhoMaximo = "Descrição deve ter no máximo 900 caracteres.";
        public const string PapelNaoDevemExistirUsuariosAssociado = "Não é permitido remover papel com usuários associado.";
        public const string PapelNomeDeveSerUnico = "Nome deve ser único.";
        public const string PapelNomeDeveSerPreenchido = "Nome deve ser preenchido.";
        public const string PapelNomeDeveSerValido = "Nome deve ser válido.";
        public const string PapelNomeDeveTerUmTamanhoMaximo = "Nome deve ter no máximo 250 caracteres.";

        public const string UsuarioAdministradorNaoPodeSerAtualizado = "Usuário administrador não pode ser atualizado.";
        public const string UsuarioCelularDeveSerPreenchido = "Celular deve ser preenchido.";
        public const string UsuarioCelularDeveSerValido = "Celular deve ser válido.";
        public const string UsuarioEmailDeveEstarConfirmado = "Email deve estar confirmado.";
        public const string UsuarioEmailDeveSerPreenchido = "Email deve ser preenchido.";
        public const string UsuarioEmailDeveSerUnico = "Email deve ser único.";
        public const string UsuarioEmailDeveSerValido = "Email deve ser válido.";
        public const string UsuarioEmailDeveTerUmTamanhoMaximo = "Email deve ter no máximo 250 caracteres.";
        public const string UsuarioEmailNaoDeveEstarConfirmado = "Email não deve estar confirmado.";
        public const string UsuarioNaoPodeSerRemovido = "Usuário não pode ser removido.";
        public const string UsuarioNomeDeveSerPreenchido = "Nome deve ser preenchido.";
        public const string UsuarioNomeDeveTerUmTamanhoMaximo = "Nome deve ter no máximo 250 caracteres.";
        public const string UsuarioPapelDeveEstarDefinido = "Papel deve estar definido.";
        public const string UsuarioPapelNaoDeveEstarDefinido = "Papel não deve estar definido.";
        public const string UsuarioSenhaDeveSerPreenchido = "Senha deve ser preenchido.";
        public const string UsuarioSenhaDeveTerUmTamanhoMinimo = "Senha deve ter no mínimo 6 caracteres.";
        public const string UsuarioSenhaDeveTerUmTamanhoMaximo = "Senha deve ter no máximo 100 caracteres.";

        public const string ContaAcessoInvalido = "Acesso inválido.";
        public const string ContaBloqueioDeveEstarHabilitado = "Bloqueio deve estar habilitado.";
        public const string ContaCodigoRecuperacaoDeveSerValido = "Código de recuperação deve ser válido.";
        public const string ContaLoginDeveSerUnico = "Login não deve estar associado a outra conta";
        public const string ContaSenhaDeveSerValida = "Senha deve ser válida.";
        public const string ContaSenhaDeveTerCaracterEmMaiusculo = "Senha deve ter ao menos um caracter em maiúsculo.";
        public const string ContaSenhaDeveTerCaracterEmMinusculo = "Senha deve ter ao menos um caracter em minúsculo.";
        public const string ContaSenhaDeveTerCaracterNaoAlfanumerico = "Senha deve ter ao menos um caracter não alfanumérico.";
        public const string ContaSenhaDeveTerCaracterUnico = "Senha deve ter ao menos um caracter único.";
        public const string ContaSenhaDeveTerNumero = "Senha deve ter ao menos um número.";
        public const string ContaSenhaNaoDeveEstarDefinida = "Senha não deve estar definida.";
        public const string ContaTokenDeveSerValido = "Token deve ser válido.";
        public const string ContaUsuarioBloqueado = "Usuário bloqueado.";
        public const string ContaUsuarioSemPremissaoAcesso = "Usuário sem permissão de acesso.";

        public const string RegistroAtivoNaoPodeSerAtivado = "Registro ativo não pode ser ativado.";
        public const string RegistroConflitouAoExecutarComando = "Ocorreu uma falha de concorrência ao executar o comando.";
        public const string RegistroFalhouAoExecutarComando = "Ocorreu uma falha ao executar o comando.";
        public const string RegistroInativoNaoPodeSerAtualizado = "Registro inativo não pode ser atualizado.";
        public const string RegistroInativoNaoPodeSerDesativado = "Registro inativo não pode ser desativado.";
        public const string RegistroInexistente = "Registro inexistente.";
    }
}
