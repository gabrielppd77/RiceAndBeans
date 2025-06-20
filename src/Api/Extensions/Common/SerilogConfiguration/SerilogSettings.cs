namespace Api.Extensions.Common.SerilogConfiguration;

public class SerilogSettings
{
    public const string SectionName = "SerilogSettings";

    public required string SeqServerUrl { get; set; }
    public required string EmailConfigFrom { get; set; }
    public required string EmailConfigTo { get; set; }
    public required string EmailConfigHost { get; set; }
    public required int EmailConfigPort { get; set; }
    public required string EmailConfigCrendentialUserName { get; set; }
    public required string EmailConfigCrendentialPassword { get; set; }
}