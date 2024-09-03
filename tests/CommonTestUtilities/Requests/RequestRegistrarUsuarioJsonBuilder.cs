using Bogus;
using Bogus.Extensions.Brazil;
using DoseOfHope.Communication.Enums;
using DoseOfHope.Communication.Requests;

namespace CommonTestUtilities.Requests;

public class RequestRegistrarUsuarioJsonBuilder
{
    public static RequestUsuarioJson Build()
    {
        var faker = new Faker("pt_BR");

        return new RequestUsuarioJson
        {
            nome = faker.Name.FullName(),
            email = faker.Internet.Email(),
            senha = faker.Internet.Password(),
            //telefone = faker.Phone.PhoneNumber("###########"),
            dataNascimento = faker.Date.Past(30, DateTime.Now.AddYears(-18)),
            //rg = faker.Random.AlphaNumeric(9),
            cpf = faker.Person.Cpf(),
            //generoCodigo = faker.PickRandom<TipoGenero>(),
            //tipoUsuarioCodigo = faker.PickRandom<1>(),
            //cep = faker.Address.ZipCode("########"),
            //rua = faker.Address.StreetName(),
            //bairro = faker.Address.SecondaryAddress(),
            //cidade = faker.Address.City(),
            //uf = 1,
            //numeroResidencia = faker.Random.Number(1, 9999).ToString(),
            //complemento = faker.Address.BuildingNumber(),
            dataEmailEnviado = faker.Date.Recent(),
            unidadeCodigo = faker.PickRandom<TipoUnidade>()
        };
    }
}
