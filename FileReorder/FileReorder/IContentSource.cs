namespace FileReorder;

public interface IContentSource<T>
{
    IEnumerable<T> GetContents();
}
