using FindANameServer.Domain;
using Microsoft.EntityFrameworkCore;

namespace FindANameServer.Infrastructure;

public class NamesRepository : INamesRepository
{
    private readonly NameDbContext _context;
    private readonly DbSet<CandidateName> _names;

    public NamesRepository(NameDbContext context)
    {
        _context = context;
        _names = context.CandidateNames;
    }

    public async Task<IEnumerable<CandidateName>> GetAll(User user)
    {
        var query = _names.AsNoTracking().Where(name => !name.RejectedBy.Contains(user));

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<CandidateName>> GetRandom(User user, int n)
    {
        var allNames = await _names.AsNoTracking().Where(name => !name.RejectedBy.Contains(user)).ToListAsync();

        var numberOfNames = allNames.Count();

        if (numberOfNames <= n)
        {
            return allNames;
        }

        HashSet<int> randomIndexes = [];
        Random rnd = new();

        while (randomIndexes.Count < n)
        {
            randomIndexes.Add(rnd.Next(numberOfNames));
        }

        List<CandidateName> randomNames = [];

        foreach (var i in randomIndexes)
        {
            randomNames.Add(allNames[i]);
        }

        return randomNames;
    }

    public async Task Add(string[] newNames)
    {
        foreach (var name in newNames)
        {
            _names.Add(new CandidateName { Name = name });
        }

        await _context.SaveChangesAsync();
    }

    public async Task Reject(User user, int[] rejected)
    {
        foreach (var id in rejected)
        {
            var name = await _names.AsTracking().FirstOrDefaultAsync(name => name.Id == id);

            name?.RejectedBy.Add(user);
        }

        await _context.SaveChangesAsync();
    }
}
