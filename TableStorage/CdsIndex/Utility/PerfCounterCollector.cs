//-----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//-----------------------------------------------------------------------------

using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using TableStorage.CdsIndex.Utility;

namespace Microsoft.BizQA.Common
{
    public class PerfCounterCollector : IPerfCounterCollector
    {
        private readonly ILogger _mdmLogger;

        public PerfCounterCollector()
        {
            _mdmLogger = new MyDummyLogger();
        }

        /// <summary>
        /// Implement perf counter scope alone
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IPerfCounterScope BeginPerfScope(string name)
        {
            return new PerfCounterScope(name, _mdmLogger);
        }

        private class PerfCounterScope : IPerfCounterScope
        {
            private readonly Stopwatch _stopwatch;
            private readonly ILogger _mdmLogger;

            public PerfCounterScope(string name, ILogger mdmLogger)
            {
                Name = name;
                _mdmLogger = mdmLogger;
                _stopwatch = Stopwatch.StartNew();
            }

            public string Name { get; set; }

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            private void Dispose(bool disposing)
            {
                if (disposing)
                {
                    if (_stopwatch != null && _stopwatch.IsRunning)
                    {
                        _stopwatch.Stop();

                        // Temporally add metrics to log, will be removed once MDM ready
                        _mdmLogger.LogInformation($"[Metrics]{Name}={_stopwatch.ElapsedMilliseconds}");
                    }
                }
            }
        }
    }
}
