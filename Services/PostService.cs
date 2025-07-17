using System.IO;
using Markdig;

namespace ExpatHelpApp.Services;

public class PostService
{
    private readonly string _postFolder = Path.Combine(Environment.CurrentDirectory, "posts");
    private readonly MarkdownPipeline _pipeline;

    public PostService()
    {
        _pipeline = new MarkdownPipelineBuilder()
            .UseAdvancedExtensions()
            .Build();
    }

    public string GetPostHtml(string postName)
    {
        var filename = $"{postName}.md";
        var fullPath = Path.Combine(_postFolder, filename);

        if (!File.Exists(fullPath))
            return $"<p>{filename} with path {fullPath} not found.</p>";

        var markdown = File.ReadAllText(fullPath);
        return Markdown.ToHtml(markdown, _pipeline);
    }

    public List<string> GetAvailablePostSlugs()
    {
        return [.. Directory.EnumerateFiles(_postFolder, "*.md").Select(f => Path.GetFileNameWithoutExtension(f))];
    }
}