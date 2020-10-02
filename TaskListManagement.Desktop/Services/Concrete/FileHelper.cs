using System.IO;
using System.Text;
using System.Threading.Tasks;
using TaskListManagement.Desktop.Services.Abstractions;

namespace TaskListManagement.Desktop.Services.Concrete
{
    public class FileHelper : IFileHelper
    {
        /// <summary>
        /// Determines whether the file exists.
        /// </summary>
        /// <param name="filename">File path to check its existence.</param>
        /// <returns>Whether the file exists.</returns>
        private static bool IsFileExits(string filename)
        {
            return File.Exists(filename);
        }

        /// <summary>
        /// Read File content.
        /// </summary>
        /// <param name="filePath">File Path to read.</param>
        /// <returns>File content</returns>
        public async Task<string> ReadFileAsync(string filePath)
        {
            return !IsFileExits(filePath) ? await new Task<string>(() => "").ConfigureAwait(true) : await File.ReadAllTextAsync(filePath, Encoding.UTF8).ConfigureAwait(true);
        }

        /// <summary>
        /// Write content to file.
        /// </summary>
        /// <param name="path">Destination file path.</param>
        /// <param name="content">content of the file.</param>
        public void WriteFileAsync(string path, string content)
        {
            File.WriteAllText(path, content, Encoding.UTF8);
        }

    }
}