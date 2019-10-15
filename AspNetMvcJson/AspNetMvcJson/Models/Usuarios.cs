using AspNetMvcJson.Repository;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace AspNetMvcJson.Models
{
    public class Usuarios : IEntidade
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [Required(ErrorMessage = "insira um nome")]
        [JsonProperty("name")]
        public string Name { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail em formato inválido")]
        [Required(ErrorMessage = "Insira um e-mail")]
        [JsonProperty("email")]
        public string Email { get; set; }
    }
}