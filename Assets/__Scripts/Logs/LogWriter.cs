using System;
using System.Diagnostics;
using System.IO;
using UnityEngine;

public static class LogWriter
{

    static string allLogsFile = Path.Combine(Application.persistentDataPath, "allLogs.txt");
    static string currentLogsFile = Path.Combine(Application.persistentDataPath, "currentLogs.txt");

    public static void WriteLog(string log)
    {
        string callingClass = GetCallingClassName();
        using (StreamWriter writer = File.AppendText(allLogsFile))
        {
            writer.WriteLine("[" + DateTime.Now + "] " + callingClass + ": " + log);
        }
        using (StreamWriter writer = File.AppendText(currentLogsFile))
        {
            writer.WriteLine("[" + DateTime.Now + "] " + callingClass + ": " + log);
        }
    }

    private static string GetCallingClassName()
    {
        var stackTrace = new StackTrace();
        var callingMethod = stackTrace.GetFrame(2).GetMethod();
        var callingClass = callingMethod.DeclaringType?.Name;
        return callingClass ?? "UnknownClass";
    }
}