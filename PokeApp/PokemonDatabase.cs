using SQLite;
using PokeApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PokeApp
{
    // Classe relative à la base de données.
    public class PokemonDatabase
    {
        static SQLiteAsyncConnection Database;

        // Variable qui permet l'instanciation de la base de données.
        public static readonly AsyncLazy<PokemonDatabase> Instance = new AsyncLazy<PokemonDatabase>(async () =>
        {
            var instance = new PokemonDatabase();
            CreateTableResult result = await Database.CreateTableAsync<MyPokemon>();
            return instance;
        });

        // Constructeur de la classe, permet de se connecter à la base de données.
        public PokemonDatabase()
        {
            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        }

        /* Fonction GetItemsAsync
        ** Cette dernière retourne la liste de tous les éléments présents dans la base (ici de type MyPokemon). */
        public Task<List<MyPokemon>> GetItemsAsync()
        {
            return Database.Table<MyPokemon>().ToListAsync();
        }

        /* Fonction GetItemsAsync
        ** Prends un entier ID en entrée et retourne l'élément correspondant dans la base de données (ici de type MyPokemon).*/
        public Task<MyPokemon> GetItemAsync(int id)
        {
            return Database.Table<MyPokemon>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        /* Fonction SaveItemAsync
        ** Prends un Pokemon de type MyPokemon en entrée et l'ajoute dans la base de données. */
        public Task<int> SaveItemAsync(MyPokemon pokemon)
        {
           
            return Database.InsertAsync(pokemon);
            
        }
        /* Fonction DeleteItemAsync
        ** Prends un Pokemon de type MyPokemon en entrée et le supprime la base de données. */
        public Task<int> DeleteItemAsync(MyPokemon pokemon)
        {
            return Database.DeleteAsync(pokemon);
        }
    }
}
