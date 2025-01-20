namespace Viis.Test
{
    public class WatchFileChange
    {
        FileSystemWatcher watcher;
        public void StartWatching(string path, string filter)
        {
            watcher = new FileSystemWatcher();
            watcher.IncludeSubdirectories = false;
            watcher.Path = path;
            watcher.Filter = filter;
            watcher.Created += new FileSystemEventHandler(OnCreated);

            // Begin watching.
            watcher.EnableRaisingEvents = true;
        }

        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            File.AppendAllLines(Path.Combine(Directory.GetCurrentDirectory(), "log.txt"),
                new string[] { DateTime.Now.ToString() + " - File: " + e.FullPath + " " + e.ChangeType });
        }
    }
}