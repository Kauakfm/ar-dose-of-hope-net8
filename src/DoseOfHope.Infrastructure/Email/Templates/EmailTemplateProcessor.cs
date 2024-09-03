using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoseOfHope.Infrastructure.Email.Templates
{
    public class EmailTemplateProcessor : IEmailTemplateProcessor
    {
        private readonly string _baseDirectory;

        public EmailTemplateProcessor()
        {
            _baseDirectory = $"{AppDomain.CurrentDomain.BaseDirectory}/Content/EmailHtml/";
        }

        public string LoadTemplate(string templateName)
        {
            var pathTemplate = Path.Combine(_baseDirectory, $"{templateName}.html");
            return File.ReadAllText(pathTemplate);
        }

        public string ProcessTemplate(string template, Dictionary<string, string> placeholders)
        {
            foreach (var placeholder in placeholders)
            {
                template = template.Replace(placeholder.Key, placeholder.Value);
            }

            return template;
        }
    }
}
