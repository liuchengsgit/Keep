using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceCodeCounter
{
    public interface ICountStrategy
    {
        bool CurrentFileInCounting { get; }

        int GetSourceCodeLines(string file);
    }
}
