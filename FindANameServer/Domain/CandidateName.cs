namespace FindANameServer.Domain;

public class CandidateName
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public HashSet<User> RejectedBy { get; set; } = [];
}
