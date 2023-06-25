namespace SignalRChatApi.Settings
{
    public interface IDatabaseSettings
    {
        public string MessageCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
