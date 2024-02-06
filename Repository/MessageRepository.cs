
using System.Text.Json;
public class MessageRepository
{
    private const string FilePath = "messages.json";
    private List<Message> _messages;

    public MessageRepository()
    {
        _messages = LoadMessages(FilePath);
    }

    public IEnumerable<Message> GetMessages() => _messages;

    public void AddMessage(Message message)
    {
        _messages.Insert(0, message);
        SaveMessages(FilePath, _messages);
    }
    public void UpdateMessage(Message message)
    {
        SaveMessages(FilePath, _messages);
    }

    public Message? GetMessageById(string id)
    {
        return _messages.FirstOrDefault(m => m.Id == id);
    }

    private List<Message> LoadMessages(string filePath)
    {
        if (!File.Exists(filePath)) return new List<Message>();
        var json = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<List<Message>>(json) ?? new List<Message>();
    }

    private void SaveMessages(string filePath, List<Message> messages)
    {
        var json = JsonSerializer.Serialize(messages, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(filePath, json);
    }
    public bool DeleteMessage(string id)
    {
        var message = _messages.FirstOrDefault(m => m.Id == id);
        if (message != null)
        {
            _messages.Remove(message);
            SaveMessages(FilePath, _messages);
            return true; 
        }
        return false;
    }
}
