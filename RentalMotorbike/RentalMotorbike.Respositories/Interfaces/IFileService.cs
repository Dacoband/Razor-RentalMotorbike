using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalMotorbike.Respositories.Interfaces
{
    public interface IFileService<G>
    {
        Task<List<G>> ReadFileAsync(string filePath);
        Task WriteFileAsync(List<G> data, string filePath);
    }
}
