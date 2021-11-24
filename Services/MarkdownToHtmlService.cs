using Ganss.XSS;
using Markdig;

namespace Coursework.Services
{
    public class MarkdownToHtmlService
    {
        public string ConvertMarkdownToHtml(string markdown)
        {
            var sanitizer = new HtmlSanitizer();
            return sanitizer.Sanitize(Markdown.ToHtml(markdown));
        }
    }
}
