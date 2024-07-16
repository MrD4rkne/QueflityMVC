namespace QueflityMVC.Web.Setup.Database;

public class PersistenceConfig
{
    public string ConnectionString { get; set; }
    
    public bool ShouldRetry { get; set; }
}