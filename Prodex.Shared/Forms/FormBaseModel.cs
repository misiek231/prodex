using System.Text.Json;

namespace Prodex.Shared.Forms
{
    public class FormBaseModel
    {
        public List<KeyValuePair<string, string>> Errors { get; set; }
        public bool HasErrors => Errors != null && Errors.Any();

        public void WithErrors(string errors)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            Errors = JsonSerializer.Deserialize<List<KeyValuePair<string, string>>>(errors, options);
        }
    }
}
