namespace QueflityMVC.Web.Setup.Database;

public class PersistenceOptions
{
    public string ConnectionString { get; set; }
    
    public bool ShouldRetry { get; set; }
}