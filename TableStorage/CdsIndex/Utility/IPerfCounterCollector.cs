//-----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//-----------------------------------------------------------------------------

namespace Microsoft.BizQA.Common
{
    public interface IPerfCounterCollector
    {
        IPerfCounterScope BeginPerfScope(string name);
    }
}
