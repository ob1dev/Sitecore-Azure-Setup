using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Pipelines;

namespace Sitecore.Azure.Startup.Pipelines.Loader
{
  /// <summary>
  /// The processor that initialize connection string based on the Service Configuration file.
  /// </summary>
  class InitializeConnectionStrings
  {
    /// <summary>
    /// Processes the specified arguments.
    /// </summary>
    /// <param name="args">The arguments.</param>
    public void Process(PipelineArgs args)
    {
      ConfigRegistration.OverwriteConnectionStrings();
    }
  }
}
