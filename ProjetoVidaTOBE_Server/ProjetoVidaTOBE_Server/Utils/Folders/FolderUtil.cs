using System;
using System.IO;
using System.Linq;
using System.Text;

namespace ProjetoVidaTOBE_Server.Utils
{
    public class FolderUtil
    {
        public static void Create(string path, bool removeLastFolder = false)
        {
            var uri = new Uri(path);

            var url = new StringBuilder();
            var i = 0;
            foreach (var segment in uri.Segments)
            {
                i++;

                if (segment == "/" || removeLastFolder && uri.Segments.Count() == i)
                    continue;

                url.Append(segment);

                if (!string.IsNullOrEmpty(Path.GetExtension(url.ToString())))
                    continue;

                if (segment == Path.GetPathRoot(path).Replace("\\", "/"))
                    if (DriveInfo.GetDrives().All(x => x.Name.ToUpper().Equals(segment)))
                        throw new Exception(string.Format("O driver {0} não existe.", segment));
                    else
                        continue;

              
             }
            if (!Directory.Exists(@"C:\PROJETO_VIDA_LOG"))
            {
                Directory.CreateDirectory(url.ToString());
            }
        }

        public static void Delete(string path)
        {
            if (!Directory.Exists(path))
                return;

            DeleteFiles(path);

            foreach (var directory in Directory.GetDirectories(path))
                Delete(directory);

            //Deleta a pasta do projeto atual
            Directory.Delete(path);
        }

        private static void DeleteFiles(string path)
        {
            foreach (var filePath in Directory.GetFiles(path))
                File.Delete(filePath);
        }
    }
}
