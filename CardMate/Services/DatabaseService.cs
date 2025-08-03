using CardMate.Models;
using SQLite;

namespace CardMate.Services;
public class DatabaseService
{
    private SQLiteAsyncConnection _database;
    private bool _isInitialized = false;

    private async Task InitializeAsync()
    {
        if ( _isInitialized )
            return;

        var databasePath = Path.Combine(FileSystem.AppDataDirectory, "LoyaltyCards.db");

        _database = new SQLiteAsyncConnection(databasePath);
        await _database.CreateTableAsync<LoyaltyCard>();

        _isInitialized = true;
    }

    public async Task<List<LoyaltyCard>> GetCardsAsync()
    {
        await InitializeAsync();
        return await _database.Table<LoyaltyCard>()
            .OrderBy(c => c.StoreName)
            .ToListAsync();
    }

    public async Task<int> SaveCardAsync(LoyaltyCard card)
    {
        await InitializeAsync();
        if ( card.Id != 0 )
        {
            card.LastEdited = DateTime.Now;
            return await _database.UpdateAsync(card);
        }
        else
        {
            card.DateAdded = DateTime.Now;
            card.LastEdited = DateTime.Now;
            return await _database.InsertAsync(card);
        }
    }

    public async Task<int> DeleteCardAsync(LoyaltyCard card)
    {
        await InitializeAsync();
        return await _database.DeleteAsync(card);
    }
}
