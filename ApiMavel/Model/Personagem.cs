using System.Collections.Generic;

namespace Model
{
    public class Personagem
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string UrlImage { get; set; }
        public List<Evento> Eventos { get; set; } = new List<Evento>();
    }
}
