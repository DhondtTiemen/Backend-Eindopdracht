namespace Eindopdracht.Configuration;

public class DatabaseSettings
{
    public string? ConnectionString { get; set; }
    public string? DatabaseName { get; set; }
    public string? SetCollection { get; set; }
    public string? ThemeCollection { get; set; }
    public string? CustomerCollection { get; set; }
    public string? OrderCollection { get; set; }
}