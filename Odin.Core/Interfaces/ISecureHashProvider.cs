using System;
using System.Collections.Generic;
using System.Text;

namespace Odin.Core.Helper
{
    public interface ISecureHashProvider
    {
        string Hash(string text);
        bool Verify(string text, string hash);
    }
}
