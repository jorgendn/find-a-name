namespace FindANameServer.Domain;

public interface INamesRepository
{
    Task<IEnumerable<CandidateName>> GetAll(User user);

    Task<IEnumerable<CandidateName>> GetRandom(User user, int n);

    Task Add(string[] newNames);

    Task Reject(User user, int[] rejected);
}
