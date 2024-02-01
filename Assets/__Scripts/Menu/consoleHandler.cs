using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class consoleHandler : MonoBehaviour
{
    public TMP_Text consoleText;
    public Toggle allToggle;
    public Scrollbar logsScrollbar;
    public Scrollbar logsScrollbar10;
    public Scrollbar logsScrollVertical;
    public GameObject textArea;

    private Vector3 textAreaCoordinates;

    private string allLogsFilePath;
    private string currentLogsFilePath;

    private int pageNumber = 0;
    public int linesPerPage = 0;
    
    private void Start()
    {
        textAreaCoordinates = textArea.transform.position;
        string currentLogsFile = Path.Combine(Application.persistentDataPath, "currentLogs.txt");
        File.WriteAllText(currentLogsFile, string.Empty);
        allLogsFilePath = Path.Combine(Application.persistentDataPath, "allLogs.txt");
        currentLogsFilePath = Path.Combine(Application.persistentDataPath, "currentLogs.txt");
        ReloadText();
    }
    public void ChangeFontSize(string size)
    {
        int intSize = int.Parse(size);
        consoleText.fontSize = intSize;
    }

    public void ReloadText()
    {
        int numOfLines = linesPerFontSize((int)consoleText.fontSize);
        float scrollValue = (logsScrollbar.value + logsScrollbar10.value/10)/1.1f;
        string logsFilePath = currentLogsFilePath;
        if (allToggle.isOn) logsFilePath = allLogsFilePath;
        int linesInFile = TotalLines(logsFilePath);
        int endLine = Mathf.RoundToInt(scrollValue * linesInFile);
        if (endLine < numOfLines) endLine = numOfLines;
        int startLine = endLine - numOfLines;  
        string[] lines = new string[numOfLines];
        using (StreamReader sr = new StreamReader(logsFilePath))
        {
            string line;
            int lineCount = 0;
          
            while((line = sr.ReadLine()) != null)
            {
                if(lineCount >= startLine && lineCount < endLine)
                {
                    lines[lineCount - startLine] = line;
                }
                lineCount++;
            }
        }

        string text = "";
        for(int i = 0; i < lines.Length; i++)
        {
            text += lines[i] + "\n";
        }

        consoleText.text = text;
    }

    int linesPerFontSize(int fontSize)
    {
        return Mathf.FloorToInt(500/fontSize*0.975f);
    }

    int TotalLines(string filePath)
    {
        using (StreamReader r = new StreamReader(filePath))
        {
            int i = 0;
            while (r.ReadLine() != null) { i++; }
            return i;
        }
    }

    public void Test()
    {
        LogWriter.WriteLog("Test");
        ReloadText();
    }

    public void ScrollSideways()
    {
        float value = logsScrollVertical.value;
        textArea.transform.position = new Vector3(textAreaCoordinates.x - value*1000, textAreaCoordinates.y, textAreaCoordinates.z);
    }
}
