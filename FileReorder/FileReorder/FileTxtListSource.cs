namespace FileReorder;

public class FileTxtListSource : IContentSource<FileInfo>
{
    readonly string targetPath = @"ContentSource/FileTargetList";
    
    public IEnumerable<FileInfo> GetContents()
    {
        using var sr = new StreamReader(targetPath);
        
        while(sr.EndOfStream == false) {
            var line = sr.ReadLine();
            if(string.IsNullOrEmpty(line)) continue;
            yield return new FileInfo(line);
        }
    }
}
