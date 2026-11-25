using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace YMH219_KeyEventLogger;

public class KeyLoggerManager
{
    public bool IsLoggingActive { get; private set; }
    public List<LogEntry> Logs { get; private set; }
    public Dictionary<string, int> KeyFrequencies { get; private set; }
    public int TotalKeyEvents => Logs.Count;

    public KeyLoggerManager()
    {
        Logs = new List<LogEntry>();
        KeyFrequencies = new Dictionary<string, int>();
        IsLoggingActive = false;
    }

    public void StartLogging()
    {
        IsLoggingActive = true;
    }

    public void StopLogging()
    {
        IsLoggingActive = false;
    }

    public void ClearLogs()
    {
        Logs.Clear();
        KeyFrequencies.Clear();
    }

    public LogEntry? ProcessKeyEvent(KeyEventArgs e, string eventType)
    {
        if (!IsLoggingActive) return null;

        
        List<string> modList = new List<string>();
        if (e.Control) modList.Add("Ctrl");
        if (e.Shift) modList.Add("Shift");
        if (e.Alt) modList.Add("Alt");
        string modifiers = modList.Count > 0 ? string.Join("+", modList) : "Yok";

        string keyName = e.KeyCode.ToString();
        int keyCode = (int)e.KeyCode;

        
        LogEntry entry = new LogEntry(keyName, keyCode, eventType, modifiers);
        Logs.Add(entry);

        
        if (eventType == "KeyDown")
        {
            if (KeyFrequencies.ContainsKey(keyName))
                KeyFrequencies[keyName]++;
            else
                KeyFrequencies[keyName] = 1;
        }

        return entry;
    }

    
    public string GetTopKeysSummary(int topCount = 3)
    {
        if (KeyFrequencies.Count == 0) return "Henüz tuş kaydı yok";

        var top = KeyFrequencies.OrderByDescending(x => x.Value).Take(topCount);
        return string.Join(" | ", top.Select(x => $"{x.Key}: {x.Value} kez"));
    }

    
    public void SaveLogsToFile(string filePath)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(filePath, false, System.Text.Encoding.UTF8))
            {
                writer.WriteLine("==========================================================");
                writer.WriteLine("          YMH219 - KEY EVENT LOGGER RAPORU               ");
                writer.WriteLine($"  Tarih: {DateTime.Now:dd.MM.yyyy HH:mm:ss}");
                writer.WriteLine($"  Toplam Kaydedilen Olay Sayısı: {Logs.Count}");
                writer.WriteLine($"  En Sık Basılan Tuşlar: {GetTopKeysSummary()}");
                writer.WriteLine("==========================================================");
                writer.WriteLine();

                foreach (var log in Logs)
                {
                    writer.WriteLine(log.ToDetailedFileFormat());
                }

                writer.WriteLine();
                writer.WriteLine("=================== RAPOR SONU ==========================");
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Dosya yazılırken bir hata oluştu: " + ex.Message);
        }
    }
}
