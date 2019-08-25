using System;
using System.IO;

namespace DockerResources
{
    public class DockerSecrets
    {
        public static string Get(string key)
        {
            const string DOCKER_SECRET_PATH = "/run/secrets/";
            if (Directory.Exists(DOCKER_SECRET_PATH))
            {
                IFileProvider provider = new PhysicalFileProvider(DOCKER_SECRET_PATH);
                IFileInfo fileInfo = provider.GetFileInfo(key);
                if (fileInfo.Exists)
                {
                    using (var stream = fileInfo.CreateReadStream())
                    using (var streamReader = new StreamReader(stream))
                    {
                        return streamReader.ReadToEnd();
                    }
                }
            }
  
            return Configuration.GetValue<string>(key);
        }
    }
}