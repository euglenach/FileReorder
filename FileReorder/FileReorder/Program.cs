using FileReorder;
using System.Collections.Immutable;

IContentSource<FileInfo> source = new FileTxtListSource();
IContentProcessor<FileInfo> processor = new FileProcessor(FileProcessor.OrderKind.CreatedAtAndUpdatedAt);

Reorder(source, processor);

static void Reorder<T>(IContentSource<T> source, IContentProcessor<T> processor)
{
    var contents = source.GetContents().ToImmutableArray();
    
    for(var i = 0; i < contents.Length; i++)
    {
        var content = contents[i];
        processor.Processing(content, i);
    }
}
