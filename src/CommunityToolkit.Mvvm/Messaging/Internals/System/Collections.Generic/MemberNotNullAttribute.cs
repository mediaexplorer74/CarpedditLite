// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace System.Collections.Generic
{
    internal class MemberNotNullAttribute : Attribute
    {
        private string v1;
        private string v2;

        public MemberNotNullAttribute(string v1, string v2)
        {
            this.v1 = v1;
            this.v2 = v2;
        }
    }
}