//-----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//-----------------------------------------------------------------------------

using System;

namespace Microsoft.BizQA.Common
{
    public interface IPerfCounterScope : IDisposable
    {
        string Name { get; }
    }
}
