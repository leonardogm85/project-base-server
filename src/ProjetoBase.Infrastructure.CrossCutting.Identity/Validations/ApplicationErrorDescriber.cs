using Microsoft.AspNetCore.Identity;
using ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Constants;

namespace ProjetoBase.Infrastructure.CrossCutting.Identity.Validations
{
    public class ApplicationErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError ConcurrencyFailure() => new ApplicationError(nameof(ConcurrencyFailure), ValidationMessages.RegistroConflitouAoExecutarComando);
        public override IdentityError DefaultError() => new ApplicationError(nameof(DefaultError), ValidationMessages.RegistroFalhouAoExecutarComando);
        public override IdentityError DuplicateEmail(string email) => new ApplicationError(nameof(DuplicateEmail), ValidationMessages.UsuarioEmailDeveSerUnico);
        public override IdentityError DuplicateRoleName(string role) => new ApplicationError(nameof(DuplicateRoleName), ValidationMessages.PapelNomeDeveSerUnico);
        public override IdentityError DuplicateUserName(string userName) => new ApplicationError(nameof(DuplicateUserName), ValidationMessages.UsuarioEmailDeveSerUnico);
        public override IdentityError InvalidEmail(string email) => new ApplicationError(nameof(InvalidEmail), ValidationMessages.UsuarioEmailDeveSerValido);
        public override IdentityError InvalidRoleName(string role) => new ApplicationError(nameof(InvalidRoleName), ValidationMessages.PapelNomeDeveSerValido);
        public override IdentityError InvalidToken() => new ApplicationError(nameof(InvalidToken), ValidationMessages.ContaTokenDeveSerValido);
        public override IdentityError InvalidUserName(string userName) => new ApplicationError(nameof(InvalidUserName), ValidationMessages.UsuarioEmailDeveSerValido);
        public override IdentityError LoginAlreadyAssociated() => new ApplicationError(nameof(LoginAlreadyAssociated), ValidationMessages.ContaLoginDeveSerUnico);
        public override IdentityError PasswordMismatch() => new ApplicationError(nameof(PasswordMismatch), ValidationMessages.ContaSenhaDeveSerValida);
        public override IdentityError PasswordRequiresDigit() => new ApplicationError(nameof(PasswordRequiresDigit), ValidationMessages.ContaSenhaDeveTerNumero);
        public override IdentityError PasswordRequiresLower() =>
            new ApplicationError(nameof(PasswordRequiresLower), ValidationMessages.ContaSenhaDeveTerCaracterEmMinusculo);
        public override IdentityError PasswordRequiresNonAlphanumeric() =>
            new ApplicationError(nameof(PasswordRequiresNonAlphanumeric), ValidationMessages.ContaSenhaDeveTerCaracterNaoAlfanumerico);
        public override IdentityError PasswordRequiresUniqueChars(int uniqueChars) =>
            new ApplicationError(nameof(PasswordRequiresUniqueChars), ValidationMessages.ContaSenhaDeveTerCaracterUnico);
        public override IdentityError PasswordRequiresUpper() =>
            new ApplicationError(nameof(PasswordRequiresUpper), ValidationMessages.ContaSenhaDeveTerCaracterEmMaiusculo);
        public override IdentityError PasswordTooShort(int length) =>
            new ApplicationError(nameof(PasswordTooShort), ValidationMessages.UsuarioSenhaDeveTerUmTamanhoMinimo);
        public override IdentityError RecoveryCodeRedemptionFailed() =>
            new ApplicationError(nameof(RecoveryCodeRedemptionFailed), ValidationMessages.ContaCodigoRecuperacaoDeveSerValido);
        public override IdentityError UserAlreadyHasPassword() =>
            new ApplicationError(nameof(UserAlreadyHasPassword), ValidationMessages.ContaSenhaNaoDeveEstarDefinida);
        public override IdentityError UserAlreadyInRole(string role) =>
            new ApplicationError(nameof(UserAlreadyInRole), ValidationMessages.UsuarioSenhaDeveTerUmTamanhoMinimo);
        public override IdentityError UserLockoutNotEnabled() =>
            new ApplicationError(nameof(UserLockoutNotEnabled), ValidationMessages.ContaBloqueioDeveEstarHabilitado);
        public override IdentityError UserNotInRole(string role) => new ApplicationError(nameof(UserNotInRole), ValidationMessages.UsuarioPapelDeveEstarDefinido);
    }
}
