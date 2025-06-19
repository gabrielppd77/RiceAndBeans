namespace Api.Extensions.Common.SerilogConfiguration;

public class SerilogSettings
{
    public const string SectionName = "SerilogSettings";

    public string SeqServerUrl { get; set; } = "";

    public string EmailConfigFrom { get; set; } = "";
    public string EmailConfigTo { get; set; } = "";
    public string EmailConfigHost { get; set; } = "";
    public int EmailConfigPort { get; set; } = 0;
    public string EmailConfigCrendentialUserName { get; set; } = "";
    public string EmailConfigCrendentialPassword { get; set; } = "";
}