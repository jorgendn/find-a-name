namespace FindANameServer.Domain;

public interface INamesRepository
{
    Task<IEnumerable<CandidateName>> Get(User user, int n);

    Task Add(string[] newNames);

    Task Reject(User user, int[] rejected);
}
