namespace Synapse.Services.Controller.Dal
{
    public class ControllerDalExtensionBase : IControllerDalExtension
    {
        public string Key { get; set; }
        public string Type { get; set; }
        public object Config { get; set; }
    }
}