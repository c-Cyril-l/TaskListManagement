using System.Threading.Tasks;

namespace TaskListManagement.Desktop.Services.Abstractions
{
    public interface IFileHelper
    {
        /// <summary>
        /// Read File content.
        /// </summary>
        /// <param name="filePath">File Path to read.</param>
        /// <returns>File content</returns>
        Task<string> ReadFileAsync(string filePath);

        /// <summary>
        /// Write content to file.
        /// </summary>
        /// <param name="path">Destination file path.</param>
        /// <param name="content">content of the file.</param>
        void WriteFileAsync(string path, string content);
    }
}