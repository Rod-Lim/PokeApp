using SQLite;
using PokeApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PokeApp
{
    public class PokemonDatabase
    {
        static SQLiteAsyncConnection Database;

        public static readonly AsyncLazy<PokemonDatabase> Instance = new AsyncLazy<PokemonDatabase>(async () =>
        {
            var instance = new PokemonDatabase();
            CreateTableResult result = await Database.CreateTableAsync<MyPokemon>();
            return instance;
        });

        public PokemonDatabase()
        {
            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        }

        public Task<List<MyPokemon>> GetItemsAsync()
        {
            return Database.Table<MyPokemon>().ToListAsync();
        }

        public Task<List<MyPokemon>> GetItemsNotDoneAsync()
        {
            // SQL queries are also possible
            return Database.QueryAsync<MyPokemon>("SELECT * FROM [MyPokemon] WHERE [Done] = 0");
        }

        public Task<MyPokemon> GetItemAsync(int id)
        {
            return Database.Table<MyPokemon>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(MyPokemon pokemon)
        {
           
            return Database.InsertAsync(pokemon);
            
        }

        public Task<int> DeleteItemAsync(MyPokemon pokemon)
        {
            return Database.DeleteAsync(pokemon);
        }
    }
}
