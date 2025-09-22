using System;


namespace Common.Models.Notebook;

public class NotebookSection
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Content { get; set; } // Markdown or plain text
    public string Output { get; set; } // Output for the section
    public OutputPosition Position { get; set; } = OutputPosition.After;
}

public enum OutputPosition
{
    Before,
    After
}