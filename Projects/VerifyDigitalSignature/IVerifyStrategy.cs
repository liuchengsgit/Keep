using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VerifyDigitalSignature
{
    public interface IVerifyStrategy
    {
        bool CurrentFileInCounting { get; }
        int GetSourceCodeLines(string file);
    }
}
