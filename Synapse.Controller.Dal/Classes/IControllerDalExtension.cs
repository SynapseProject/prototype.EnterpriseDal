namespace Synapse.Services.Controller.Dal
{
    public interface IControllerDalExtension
    {
        string Key { get; set; }
        string Type { get; set; }
        object Config { get; set; }
    }
}