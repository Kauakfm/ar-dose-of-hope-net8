using DoseOfHope.Communication.Requests;
using DoseOfHope.Communication.Responses;
using DoseOfHope.Domain.Repositories.Usuario;
using DoseOfHope.Domain.Security.Tokens;
using DoseOfHope.Exception.ExceptionBase;
using DoseOfHope.Infrastructure.Encryption.Criptografar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoseOfHope.Application.UseCase.Login.FazerLogin
{
    public class FazerLoginUseCase : IFazerLoginUseCase
    {
        private readonly IUsuariosReadOnlyRepository _repository;
        private readonly IAccessTokenGenerator _accessTokenGenerator;
        private readonly ICriptografarUseCase _criptografarUseCase;

        public FazerLoginUseCase(IUsuariosReadOnlyRepository repository,
            IAccessTokenGenerator accessTokenGenerator,
            ICriptografarUseCase criptografarUseCase)
        {
            _repository = repository;
            _accessTokenGenerator = accessTokenGenerator;
            _criptografarUseCase = criptografarUseCase;
        }

        public async Task<ResponseRegistrarUsuarioJson> Execute(RequestLoginJson request)
        {
            var usuario = await _repository.GetUserbyEmailAndPassword(request.Email, request.Password);

            if (usuario is null)
                throw new InvalidLoginException();

            var tokens = await _accessTokenGenerator.Generate(usuario);

            return new ResponseRegistrarUsuarioJson
            {
                Nome = usuario.nome,
                TipoUsurioCodigo = usuario.tipoUsuarioCodigo,
                Accesstoken = tokens["accessToken"],
                RefreshToken = tokens["refreshToken"],
                UsuarioCodigo = _criptografarUseCase.Encrypt(usuario.codigo.ToString()),
                Avatar = usuario.foto
            };
        }

        public async Task<ResponseRegistrarUsuarioJson> LoginSemSenha(int usuarioCodigo)
        {
            var usuario = await _repository.GetByCodigo(usuarioCodigo);

            if (usuario is null)
                throw new InvalidLoginException();

            var tokens = await _accessTokenGenerator.Generate(usuario);

            return new ResponseRegistrarUsuarioJson
            {
                Nome = usuario.nome,
                TipoUsurioCodigo = usuario.tipoUsuarioCodigo,
                Accesstoken = tokens["accessToken"],
                RefreshToken = tokens["refreshToken"],
                UsuarioCodigo = _criptografarUseCase.Encrypt(usuario.codigo.ToString()),
                Avatar = usuario.foto
            };

        }
    }
}
