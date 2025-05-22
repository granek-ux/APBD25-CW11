using APBD25_CW11.Data;
using Microsoft.EntityFrameworkCore;
namespace APBD25_CW11.Services;

public class DbService : IDbService
{
    
    private readonly DatabaseContext _context;
    public DbService(DatabaseContext context)
    {
        _context = context;
    }
    
}