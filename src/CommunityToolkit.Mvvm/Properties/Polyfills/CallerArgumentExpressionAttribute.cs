// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;

namespace CommunityToolkit.Mvvm
{
    internal class CallerArgumentExpressionAttribute : Attribute
    {
        private string v;

        public CallerArgumentExpressionAttribute(string v)
        {
            this.v = v;
        }
    }
}