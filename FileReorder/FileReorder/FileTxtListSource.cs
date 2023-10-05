namespace FileReorder;

public class FileTxtListSource : IContentSource<FileInfo>
{
    readonly string targetPath = @"ContentSource/FileTargetList";
    
    public IEnumerable<FileInfo> GetContents()
    {
        return Enumerable.Empty<FileInfo>();
    }
}
