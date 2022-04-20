using Microsoft.Extensions.Configuration;
using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class PersonagemBL
    {
        private readonly IConfiguration _config;

        public PersonagemBL(IConfiguration config)
        {
            _config = config;
        }

        public List<Personagem> ListaPersonagens()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                string ts = DateTime.Now.Ticks.ToString();
                string publicKey = _config.GetSection("MarvelComicsAPI:PublicKey").Value;
                string hash = GerarHash(ts, publicKey, _config.GetSection("MarvelComicsAPI:PrivateKey").Value);

                HttpResponseMessage response = client.GetAsync(
                    _config.GetSection("MarvelComicsAPI:BaseURL").Value +
                    $"characters?ts={ts}&apikey={publicKey}&hash={hash}").Result;
                //$"name={Uri.EscapeUriString("Captain America")}").Result;

                response.EnsureSuccessStatusCode();
                string conteudo = response.Content.ReadAsStringAsync().Result;

                dynamic resultado = JsonConvert.DeserializeObject(conteudo);

                List<Personagem> listaPersonagem = new List<Personagem>();

                foreach (var item in resultado.data.results)
                {
                    Personagem personagem = new Personagem();

                    personagem.Nome = item.name;
                    personagem.Descricao = item.description;
                    personagem.UrlImage = item.thumbnail.path + "." + item.thumbnail.extension;

                    foreach (var even in item.comics.items)
                    {
                        Evento evento = new Evento();

                        evento.Titulo = even.name;
                        evento.Descricao = even.name;
                        evento.ImagePersonContent = even.resourceURI;

                        personagem.Eventos.Add(evento);
                    }

                    listaPersonagem.Add(personagem);
                }
                return listaPersonagem;
            }
        }

        public List<Personagem> BuscaPersonagens(string nomePersonagem)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                string ts = DateTime.Now.Ticks.ToString();
                string publicKey = _config.GetSection("MarvelComicsAPI:PublicKey").Value;
                string hash = GerarHash(ts, publicKey, _config.GetSection("MarvelComicsAPI:PrivateKey").Value);

                HttpResponseMessage response = client.GetAsync(
                    _config.GetSection("MarvelComicsAPI:BaseURL").Value +
                    $"characters?ts={ts}&apikey={publicKey}&hash={hash}&" +
                    $"nameStartsWith={Uri.EscapeUriString(nomePersonagem)}").Result;

                response.EnsureSuccessStatusCode();
                string conteudo = response.Content.ReadAsStringAsync().Result;

                dynamic resultado = JsonConvert.DeserializeObject(conteudo);

                List<Personagem> listaPersonagem = new List<Personagem>();

                foreach (var item in resultado.data.results)
                {
                    Personagem personagem = new Personagem();

                    personagem.Nome = item.name;
                    personagem.Descricao = item.description;
                    personagem.UrlImage = item.thumbnail.path + "." + item.thumbnail.extension;

                    foreach (var even in item.comics.items)
                    {
                        Evento evento = new Evento();

                        evento.Titulo = even.name;
                        evento.Descricao = even.name;
                        evento.ImagePersonContent = even.resourceURI;

                        personagem.Eventos.Add(evento);
                    }

                    listaPersonagem.Add(personagem);
                }
                return listaPersonagem;
            }
        }

        private string GerarHash(string ts, string publicKey, string privateKey)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(ts + privateKey + publicKey);
            var gerador = MD5.Create();
            byte[] bytesHash = gerador.ComputeHash(bytes);

            return BitConverter.ToString(bytesHash)
                .ToLower().Replace("-", String.Empty);
        }
    }
}
