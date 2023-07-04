using System;
using System.IO;
using System.Windows;

namespace production.二维码.mytool
{
    public class PathMonitor
    {
        private string path;
        private FileSystemWatcher watcher;

        // 当路径发生变化时调用的事件
        public event EventHandler<PathChangedEventArgs> PathChanged;

        public PathMonitor(string path)
        {
            this.path = path;

            // 如果指定路径不存在，则创建该路径

            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show("您没有创建目录的权限: " + ex.Message);
            }
            catch (IOException ex)
            {
                MessageBox.Show("创建目录时出错: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("出现意外错误: " + ex.Message);
            }


            // 创建一个 FileSystemWatcher 对象，并设置其监视的文件路径
            watcher = new FileSystemWatcher(path);

            // 设置监视配置项，包括监视子目录，当文件名、目录名或者文件属性变更时触发事件
            watcher.IncludeSubdirectories = true;
            watcher.EnableRaisingEvents = true;

            watcher.NotifyFilter = NotifyFilters.LastWrite |
                                  NotifyFilters.FileName |
                                  NotifyFilters.DirectoryName;
            // 监视 txt 文件类型
            watcher.Filter = "*.txt";
            // 指定事件处理程序（路径变化时调用 OnPathChanged 函数）
            watcher.Changed += new FileSystemEventHandler(OnPathChanged);
            watcher.Created += new FileSystemEventHandler(OnPathChanged);
            watcher.Deleted += new FileSystemEventHandler(OnPathChanged);
            watcher.Renamed += new RenamedEventHandler(OnPathRenamed);
        }

        private void OnPathChanged(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine($"Path {e.FullPath} has changed.");
            // 触发 PathChanged 事件
            this.PathChanged?.Invoke(this, new PathChangedEventArgs(e.FullPath));
        }

        private void OnPathRenamed(object sender, RenamedEventArgs e)
        {
            Console.WriteLine($"Path {e.OldFullPath} has been renamed to {e.FullPath}.");
            // 触发 PathChanged 事件
            this.PathChanged?.Invoke(this, new PathChangedEventArgs(e.FullPath));
        }
    }

    // PathMonitor 路径变化事件的参数类
    public class PathChangedEventArgs : EventArgs
    {
        public string Path { get; private set; }

        public PathChangedEventArgs(string path)
        {
            this.Path = path;
        }
    }
}
