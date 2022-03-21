using System;
using System.Collections.Generic;
using System.Text;
using PokeApiNet;
using SQLite;

namespace PokeApp.Models
{
    /* Classe MyPokemon
    ** C'est la classe à laquelle est associée tous nos pokémon, ceux qui proviennent de la PokeApi ainsi que ceux créés par l'utilisateur.
    ** Ces derniers son également dans une base de données de la même forme que la classe, avec l'ID en PrimaryKey et AutoIncrement. */ 
    [Table("Pokemons")]
    public class MyPokemon
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int HP { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int SpeAttack { get; set; }
        public int SpeDefense { get; set; }
        public int Speed { get; set; }
        public string Type1 { get; set; }
        public string Type2 { get; set; }
    }
}
