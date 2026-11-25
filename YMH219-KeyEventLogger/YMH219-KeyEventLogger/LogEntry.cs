using System;

namespace YMH219_KeyEventLogger;

public class LogEntry
{
    public DateTime Timestamp { get; set; }
    public string KeyName { get; set; }
    public int KeyCode { get; set; }
    public string EventType { get; set; } 
    public string Modifiers { get; set; } 

    public LogEntry(string keyName, int keyCode, string eventType, string modifiers)
    {
        Timestamp = DateTime.Now;
        KeyName = keyName;
        KeyCode = keyCode;
        EventType = eventType;
        Modifiers = modifiers;
    }

    public override string ToString()
    {
        return $"[{Timestamp:HH:mm:ss.fff}] [{EventType,-7}] Tuş: {KeyName,-12} | Tuş Kodu: {KeyCode,-4} | Değiştirici: {Modifiers}";
    }

    public string ToDetailedFileFormat()
    {
        return $"[{Timestamp:dd.MM.yyyy HH:mm:ss.fff}] Olay: {EventType} | Tuş Adı: {KeyName} | Sanal Kod: {KeyCode} | Ek Tuşlar: {Modifiers}";
    }
}
