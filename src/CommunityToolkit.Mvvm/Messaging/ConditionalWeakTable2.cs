// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;

namespace CommunityToolkit.Mvvm.Messaging;

internal class ConditionalWeakTable2<T1, T2>
{
    internal ConditionalWeakTable2<object, object?>.Enumerator GetEnumerator()
    {
        throw new NotImplementedException();
    }

    internal object GetValue<TToken>(object recipient, Func<object, Dictionary2<TToken, object?>> value) where TToken : IEquatable<TToken>
    {
        throw new NotImplementedException();
    }

    internal object Remove(object recipient)
    {
        throw new NotImplementedException();
    }

    internal bool TryGetValue(object recipient, out object _)
    {
        throw new NotImplementedException();
    }

    internal class Enumerator
    {
        internal object? GetKey()
        {
            throw new NotImplementedException();
        }

        internal object? GetValue()
        {
            throw new NotImplementedException();
        }

        internal bool MoveNext()
        {
            throw new NotImplementedException();
        }
    }
}