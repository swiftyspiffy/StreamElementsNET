namespace StreamElementsNET.Models.Store
{
    public class StoreRedemption
    {
        public string ItemId { get; }
        public string Username { get; }
        public string Type { get; }
        public string StoreItemName { get; }
        public string Message { get; }

        public StoreRedemption(string itemId, string username, string type, string storeItemName, string message)
        {
            ItemId = itemId;
            Username = username;
            Type = type;
            StoreItemName = storeItemName;
            Message = message;
        }
    }
}
