﻿using AutoMapper;
using DoseOfHope.Communication.Requests;
using DoseOfHope.Communication.Responses;
using DoseOfHope.Domain.Entities;
using DoseOfHope.Domain.Repositories;
using DoseOfHope.Domain.Repositories.Usuario;
using DoseOfHope.Domain.Security.Tokens;
using DoseOfHope.Exception.ExceptionBase;

namespace DoseOfHope.Application.UseCase.Usuario.Registrar;

public class RegistrarUsuarioUseCase : IRegistrarUsuarioUseCase
{
    private readonly IUsuariosWriteOnlyRepository _repositoryUser;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IAccessTokenGenerator _tokenGenerator;
    public RegistrarUsuarioUseCase(
        IUsuariosWriteOnlyRepository usuarioRepository, 
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IAccessTokenGenerator tokenGenerator)
    {
        _repositoryUser = usuarioRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _tokenGenerator = tokenGenerator;
    }
    public async Task<ResponseRegistrarUsuarioJson> Executar(RequestUsuarioJson request)
    {
        Validar(request);

        var entity = _mapper.Map<tabUsuario>(request);

        await _repositoryUser.Add(entity);

        await _unitOfWork.commit();

        var roles = new tabUsuario_tabRoles
        {
            roleCodigo = 2,
            usuarioCodigo = entity.codigo
        };

        await _repositoryUser.AddRoles(roles);
        await _unitOfWork.commit();

        var tokens = await _tokenGenerator.Generate(entity);

        return new ResponseRegistrarUsuarioJson 
        {
            Nome = entity.nome,
            Accesstoken = tokens["accessToken"],
            RefreshToken = tokens["refreshToken"],
        };
    }

    private void Validar(RequestUsuarioJson request)
    {
        var response = new UsuarioValidacao().Validate(request);

        if (!response.IsValid)
        {
            var errorMessages = response.Errors.Select(errorMessages => errorMessages.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
