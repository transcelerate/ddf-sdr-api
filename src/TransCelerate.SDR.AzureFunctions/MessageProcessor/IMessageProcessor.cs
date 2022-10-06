namespace TransCelerate.SDR.AzureFunctions
{
    public interface IMessageProcessor
    {
        void ProcessMessage(string message);
    }
}