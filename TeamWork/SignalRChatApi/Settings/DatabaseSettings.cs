namespace SignalRChatApi.Settings
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string MessageCollectionName { get ; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}