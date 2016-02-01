using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sitecore.Search;
using Sitecore.ContentSearch;
using Sitecore.Diagnostics;
using Sitecore.SecurityModel;

namespace Sitecore.Azure.Startup
{
  /// <summary>
  /// Provides a way to rebuild all Search and ContentSearch indexes in Sitecore application for the Azure platform.
  /// </summary>
  public class SearchIndexRebuilder
  {
    /// <summary>
    /// Rebuilds all Search and ContentSearch asynchronous.
    /// </summary>
    public static async void RebuildAllIndexesAsync()
    {
      List<Task> tasksList = new List<Task>();

      RebuildContentSearchIndexesAsync(ContentSearchManager.Indexes, tasksList);

      await Task.WhenAll(tasksList);
    }
    
    /// <summary>
    /// Rebuilds all Search and ContentSearch indexes.
    /// </summary>
    public static void RebuildAllIndexes()
    {
      RebuildContentSearchIndexes(ContentSearchManager.Indexes);
    }
    
    #region Private Methods

    /// <summary>
    /// Rebuilds the Search indexes asynchronous.
    /// </summary>
    /// <param name="searchIndexList">The search index list.</param>
    /// <param name="taskLists">The task lists.</param>
    private static void RebuildSearchIndexesAsync(IEnumerable<Index> searchIndexList, IList<Task> taskLists)
    {
      foreach (var searchIndex in searchIndexList)
      {
        try
        {
          var index = searchIndex;

          Task rebuildIndexTask = Task.Run(() =>
          {
            using (new SecurityDisabler())
            {
              index.Rebuild();
            }
          });

          taskLists.Add(rebuildIndexTask);
        }
        catch (Exception exception)
        {
          var message = string.Format("Exception occurred during rebuilding the '{0}' Search index.", searchIndex.Name);
          Log.Error(message, exception, typeof(SearchIndexRebuilder));
        }
      }
    }

    /// <summary>
    /// Rebuilds the ContentSearch indexes asynchronous.
    /// </summary>
    /// <param name="searchIndexList">The search index list.</param>
    /// <param name="taskLists">The task lists.</param>
    private static void RebuildContentSearchIndexesAsync(IEnumerable<ISearchIndex> searchIndexList, IList<Task> taskLists)
    {
      foreach (var searchIndex in searchIndexList)
      {
        try
        {
          var index = searchIndex;

          Task rebuildIndexTask = Task.Run(() =>
          {
            using (new SecurityDisabler())
            {
              index.Rebuild();
            }
          });

          taskLists.Add(rebuildIndexTask);
        }
        catch (Exception exception)
        {
          var message = string.Format("Exception occurred during rebuilding the '{0}' ContentSearch index.", searchIndex.Name);
          Log.Error(message, exception, typeof(SearchIndexRebuilder));
        }
      }
    }

    /// <summary>
    /// Rebuilds the Search indexes.
    /// </summary>
    /// <param name="indexList">The index list.</param>
    private static void RebuildSearchIndexes(IEnumerable<Index> indexList)
    {
      foreach (var index in indexList)
      {
        using (new SecurityDisabler())
        {
          try
          {
            index.Rebuild();
          }
          catch (Exception exception)
          {
            var message = string.Format("Exception occurred during rebuilding the '{0}' Search index.", index.Name);
            Log.Error(message, exception, typeof(SearchIndexRebuilder));
          }
        }
      }
    }

    /// <summary>
    /// Rebuilds the ContentSearch indexes.
    /// </summary>
    /// <param name="indexList">The index list.</param>
    private static void RebuildContentSearchIndexes(IEnumerable<ISearchIndex> indexList)
    {
      foreach (var index in indexList)
      {
        using (new SecurityDisabler())
        {
          try
          {
            index.Rebuild();
          }
          catch (Exception exception)
          {
            var message = string.Format("Exception occurred during rebuilding the '{0}' ContentSearch index.", index.Name);
            Log.Error(message, exception, typeof(SearchIndexRebuilder));
          }
        }
      }
    }

    #endregion
  }
}