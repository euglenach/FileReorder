using System.Globalization;
using System.Text;

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

    void IContentProcessor<FileInfo>.Processing(FileInfo content, int index)
    {
        ProcessingDryRun(content, index);
    }

    void Processing(FileInfo content, int index)
    {
        if(orderKind is OrderKind.CreatedAt or OrderKind.CreatedAtAndUpdatedAt)
        {
            content.CreationTime = origin.AddMinutes(index);
        }
        if(orderKind is OrderKind.UpdatedAt or OrderKind.CreatedAtAndUpdatedAt)
        {
            content.LastWriteTime = origin.AddMinutes(index);
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

    void ProcessingDryRun(FileInfo content, int index)
    {
        var sb = new StringBuilder();
        if(orderKind is OrderKind.CreatedAt or OrderKind.CreatedAtAndUpdatedAt)
        {
            sb.Append("CreatedAt:\n");
            sb.Append(content.CreationTime.ToString("yyyy/MM/dd hh:mm:ss"));
            sb.Append(" → ");
            sb.AppendLine(origin.AddMinutes(index).ToString("yyyy/MM/dd hh:mm:ss"));
        }
        if(orderKind is OrderKind.UpdatedAt or OrderKind.CreatedAtAndUpdatedAt)
        {
            sb.Append("UpdatedAt:\n");
            sb.Append(content.LastWriteTime.ToString("yyyy/MM/dd hh:mm:ss"));
            sb.Append(" → ");
            sb.AppendLine(origin.AddMinutes(index).ToString("yyyy/MM/dd hh:mm:ss"));
        }
        if(orderKind is OrderKind.InitialIndex)
        {
            if(content.DirectoryName is null) return;
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(content.Name);
            var newFileName = $"{index}_{fileNameWithoutExtension}{content.Extension}";
            var newPath = Path.Combine(content.DirectoryName, newFileName);

            if(File.Exists(newPath)) return;
            sb.Append("FilePath:\n");
            sb.Append(content.FullName);
            sb.Append(" → ");
            sb.AppendLine(newPath);
        }
        
        Console.WriteLine(sb.ToString());
    }
}
