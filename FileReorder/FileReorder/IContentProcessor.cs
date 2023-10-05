namespace FileReorder;

public interface IContentProcessor<T>
{
    void Processing(T content, int index);
}
