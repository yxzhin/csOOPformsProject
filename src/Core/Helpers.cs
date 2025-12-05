using System.IO;

namespace csOOPformsProject.Core
{
    public static class Helpers
    {
        public static string DataFolder =>
            GetSolutionFolderPathFromProject() + "\\src\\Data";
        public static string GetSolutionFolderPathFromProject()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string solutionPath =
                Path.GetDirectoryName(
                    Path.GetDirectoryName(
                        Path.GetDirectoryName(currentDirectory)));
            return solutionPath;
        }
    }
}
