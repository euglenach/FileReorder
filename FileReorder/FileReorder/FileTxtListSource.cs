namespace FileReorder;

public class FileTxtListSource : IContentSource<FileInfo>
{
    readonly string targetPath = @"../../../ContentSource/FileTargetList";
    
    public IEnumerable<FileInfo> GetContents()
    {
        using var sr = new StreamReader(targetPath);
        
        while(sr.EndOfStream == false) {
            var line = sr.ReadLine();
            if(string.IsNullOrEmpty(line)) continue;
            if(line[0] == '"' && line[^1] == '"') line = line[1..^1];
            yield return new FileInfo(line);
        }
    }
}
