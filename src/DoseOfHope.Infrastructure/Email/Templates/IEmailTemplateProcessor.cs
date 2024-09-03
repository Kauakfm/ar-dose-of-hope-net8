namespace DoseOfHope.Infrastructure.Email.Templates;

public interface IEmailTemplateProcessor
{
    string LoadTemplate(string templateName);
    string ProcessTemplate(string template, Dictionary<string, string> placeholders);
}
