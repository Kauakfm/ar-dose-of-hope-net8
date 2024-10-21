using CommonTestUtilities.Requests;
using DoseOfHope.Application.UseCase.Usuario;
using DoseOfHope.Exception;
using FluentAssertions;

namespace Validators.Tests.Usuario.Registrar;

public class RegistrarUsuarioValidacaoTests
{
    [Fact]
    public void Success()
    {
        //Arrenge
        //var validator = new UsuarioValidacao();
        //var request = RequestRegistrarUsuarioJsonBuilder.Build();
        //Act
        //var result = validator.Validate(request);
        //Assert
        //result.IsValid.Should().BeTrue(); //usando o fluentAssertions
        //Assert.True(result.IsValid); // usando o proprio dotNet
    }

    [Fact]
    public void Error_Name_Empty()
    {
        //Arrenge
        //var validator = new UsuarioValidacao();
        //var request = RequestRegistrarUsuarioJsonBuilder.Build();
        //request.nome = string.Empty;
        //Act
        //var result = validator.Validate(request);  
        //Assert                                            
        //result.IsValid.Should().BeFalse();                       
        //result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.NOME_OBRIGATORIO));
    }                                                    
}
