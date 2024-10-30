using RentalMotorbike.BusinessObject;
using RentalMotorbike.Respositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RentalMotorbike.Respositories.Implements
{
    public class FileService<G> : IFileService<G>
    {
        public async Task<List<G>> ReadFileAsync(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
                return new List<G>();

            var extension = Path.GetExtension(filePath).ToLower();
            if (extension == ".json")
            {
                var json = await File.ReadAllTextAsync(filePath);
                return JsonSerializer.Deserialize<List<G>>(json) ?? new List<G>();
            }
            else
            {
                throw new NotSupportedException($"File extension '{extension}' is not supported.");
            }
        }

        public async Task WriteFileAsync(List<G> data, string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));

            var extension = Path.GetExtension(filePath).ToLower();
            if (extension == ".json")
            {
                var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
                await File.WriteAllTextAsync(filePath, json);
            }
            else
            {
                throw new NotSupportedException($"File extension '{extension}' is not supported.");
            }

        }
    }
}
