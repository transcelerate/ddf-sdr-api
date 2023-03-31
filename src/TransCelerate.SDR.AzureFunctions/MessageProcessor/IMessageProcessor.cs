namespace TransCelerate.SDR.AzureFunctions
{
    public interface IMessageProcessor
    {
        /// <summary>
        /// Process the Message for Change Audit
        /// </summary>
        /// <param name="message">Message from service bus</param>
        void ProcessMessage(string message);
    }
}