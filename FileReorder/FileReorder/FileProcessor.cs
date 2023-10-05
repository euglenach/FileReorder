namespace FileReorder;

public class FileProcessor : IContentProcessor<FileInfo>
{
    public enum OrderKind
    {
        CreatedAt, UpdatedAt, CreatedAtAndUpdatedAt, InitialIndex
    }

    private readonly OrderKind orderKind;
    private readonly DateTime origin;

    public FileProcessor(OrderKind orderKind)
    {
        this.orderKind = orderKind;
        origin = DateTime.Now;
    }

    public void Processing(FileInfo content, int index)
    {
        if(orderKind is OrderKind.CreatedAt or OrderKind.CreatedAtAndUpdatedAt)
        {
            content.CreationTime = origin.AddHours(index);
        }
        if(orderKind is OrderKind.UpdatedAt or OrderKind.CreatedAtAndUpdatedAt)
        {
            content.LastWriteTime = origin.AddHours(index);
        }
        if(orderKind is OrderKind.InitialIndex)
        {
            if(content.DirectoryName is null) return;
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(content.Name);
            var newFileName = $"{index}_{fileNameWithoutExtension}{content.Extension}";
            var newPath = Path.Combine(content.DirectoryName, newFileName);

            if(File.Exists(newPath)) return;
            content.MoveTo(newPath);
        }
    }
}
