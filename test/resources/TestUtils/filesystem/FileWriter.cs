/// <summary>
/// FileWriter.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Tests.Utils
{
    using System;
    using System.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Writes file in the test output folder.
    /// </summary>
    public class FileWriter
    {
        private TestContext context;

        public FileWriter(TestContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            this.context = context;
        }

        /// <summary>
        /// Writes UTF-8 content to test result directory.
        /// </summary>
        /// <param name="content"></param>
        /// <param name="fileName"></param>
        public void WriteToFile(string content, string fileName, FileType fileType = FileType.None)
        {
            string folderPath = Path.GetFullPath(this.context.DeploymentDirectory);
            string filePath = Path.Combine(Path.GetFullPath(this.context.TestRunDirectory), 
                string.Concat(fileName, fileType.FileExtension()));

            FileAttributes attributes = File.GetAttributes(folderPath);
            if (!attributes.HasFlag(FileAttributes.Normal))
            {
                AddAttribute(attributes, FileAttributes.Normal);
                File.SetAttributes(folderPath, attributes);
            }

            using (StreamWriter writer = File.CreateText(filePath))
            {
                writer.Write(content);
            }
        }

        private static FileAttributes RemoveAttribute(FileAttributes attributes, FileAttributes attributesToRemove)
        {
            return attributes & ~attributesToRemove;
        }

        private static FileAttributes AddAttribute(FileAttributes attributes, FileAttributes attributesToAdd)
        {
            return attributes | attributesToAdd;
        }
    }
}
