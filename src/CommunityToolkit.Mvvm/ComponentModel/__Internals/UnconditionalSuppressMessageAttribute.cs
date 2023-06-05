// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;

namespace CommunityToolkit.Mvvm.ComponentModel.__Internals
{
    internal class UnconditionalSuppressMessageAttribute : Attribute
    {
        private string v1;
        private string v2;
        private string justification;

        public UnconditionalSuppressMessageAttribute(string v1, string v2, string Justification)
        {
            this.v1 = v1;
            this.v2 = v2;
            justification = Justification;
        }
    }
}