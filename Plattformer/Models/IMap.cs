using System.Collections.Generic;

namespace Plattformer.Models
{
    public interface IMap
    {
        List<List<char>> Cells { get; set; }
    }
}