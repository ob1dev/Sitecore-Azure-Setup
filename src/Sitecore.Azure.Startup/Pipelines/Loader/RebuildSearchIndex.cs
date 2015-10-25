using System.IO;
using Sitecore.IO;
using Sitecore.Pipelines;

namespace Sitecore.Azure.Startup.Pipelines.Loader
{
  /// <summary>
  /// The processor that rebuilds search indexes on very first start.
  /// </summary>
  class RebuildSearchIndex
  {
    /// <summary>
    /// Processes the specified arguments.
    /// </summary>
    /// <param name="args">The arguments.</param>
    public void Process(PipelineArgs args)
    {
      var filePath = FileUtil.MakePath(FileUtil.MapPath(TempFolder.Folder), "startup.tmp", '\\');
      FileInfo info = new FileInfo(filePath);

      if (!info.Exists)
      {
        // Rebuild all search indexes only once when Sitecore is starting.
        SearchIndexRebuilder.RebuildAllIndexesAsync();

        info.Create().Close();
      }
    }
  }
}
