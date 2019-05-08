using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace SourceCodeCounter
{
    public delegate void OnThreadFinished();

    public delegate void UpdateProcessBarEventHandle(int count, int currentCount, int totalFiles, int currentIndex, string currentFile);

    public class CodeCounter
    {
        private Form _owner;
        private Thread _thread; //使用线程统计
        private UpdateProcessBarEventHandle _event; //更新进度条
        private OnThreadFinished _finish;//线程处理完成
        private ICountStrategy _CountStrategy;//统计策略
        private bool _IsRunning = false; //标记线程是否运行，可以取消该变量

        private DateTime _begin;//开始时间
        private DateTime _end; //结束时间

        private int _TotalCount = 0; //所有代码行计数器
        private int _TotalCSFiles = 0;//*.cs文件数
        private int _TotalDir = 0;//枚举的目录总数
        private int _TotalFile = 0;//枚举的文件总数

        public CodeCounter(Form owner, ICountStrategy strategy, UpdateProcessBarEventHandle eventHander, OnThreadFinished finish)
        {
            _owner = owner;
            _CountStrategy = strategy;
            _event = eventHander;
            _finish = finish;
        }

        /// <summary>
        /// 重置计数器
        /// </summary>
        public void ResetCounter()
        {
            _TotalCount = 0;
            _TotalCSFiles = 0;
            _TotalDir = 0;
            _TotalFile = 0;
        }

        /// <summary>
        /// 开始统计
        /// </summary>
        /// <param name="folder">目录</param>
        public void Start(string folder)
        {
            this.ResetCounter();
            //实例化一个带参数的线程            
            _thread = new Thread(new ParameterizedThreadStart(this.DoProcessing));
            _thread.Start(folder);
            _begin = DateTime.Now;
        }

        /// <summary>
        /// 标记线程正在运行
        /// </summary>
        public bool IsRunning
        {
            get { return _IsRunning; }
        }

        /// <summary>
        /// 停止统计
        /// </summary>
        public void Stop()
        {
            _IsRunning = false;
            _thread.Abort();
        }

        /// <summary>
        /// 由线程调用的方法
        /// </summary>
        /// <param name="obj"></param>
        private void DoProcessing(object param)
        {
            string path = param as string;

            if (Directory.Exists(path))
            {
                //枚举目录下所有文件
                this.DoCountDirs(new string[] { path });

                _end = DateTime.Now;
                _owner.Invoke(_finish);
            }
        }

        /// <summary>
        /// 统计目录列表
        /// </summary>
        /// <param name="dirs"></param>
        private void DoCountDirs(string[] dirs)
        {
            _TotalDir += dirs.Length;
            foreach (string path in dirs)
            {
                string[] files = Directory.GetFiles(path, "*.cs");
                if (files.Length > 0)
                    this.DoCountFiles(files);//统计当前目录下的所有文件

                string[] subDirs = Directory.GetDirectories(path);
                if (subDirs.Length > 0)
                    this.DoCountDirs(subDirs);//统计当前目录下的所有子目录
            }
        }

        /// <summary>
        /// 统计文件列表
        /// </summary>
        /// <param name="files"></param>
        private void DoCountFiles(string[] files)
        {
            _TotalFile += files.Length;
            int currIndex = 0;
            foreach (string file in files)
            {
                int count = _CountStrategy.GetSourceCodeLines(file);
                _TotalCount = _TotalCount + count;

                //*.cs文件计数器
                if (_CountStrategy.CurrentFileInCounting) _TotalCSFiles += 1;

                //显示消息                
                _owner.Invoke(_event, _TotalCount, count, files.Length, currIndex, file);

                currIndex++;

                CountLog.Instance.Write(file, count, DateTime.Now);

                Thread.Sleep(100); //测试用人为制造延迟，正式发布时删除!
            }
        }

        /// <summary>
        /// 获取统计结果
        /// </summary>
        /// <returns></returns>
        public string GetLastResult()
        {
            StringBuilder sb = new StringBuilder();

            TimeSpan ts = _end - _begin;

            sb.AppendLine("共统计文件:" + _TotalFile.ToString());
            sb.AppendLine("共统计目录:" + _TotalDir.ToString());
            sb.AppendLine("共统计(*.cs)文件:" + _TotalCSFiles.ToString());
            sb.AppendLine("共代码行:" + _TotalCount.ToString());
            sb.AppendLine("用时:" + ts.TotalSeconds + "秒!");
            return sb.ToString();
        }

        /// <summary>
        /// 设置统计策略
        /// </summary>
        /// <param name="strategy">策略</param>
        public void SetStrategy(ICountStrategy strategy)
        {
            _CountStrategy = strategy;
        }
    }
}
