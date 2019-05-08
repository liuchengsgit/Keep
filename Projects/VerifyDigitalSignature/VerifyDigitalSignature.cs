using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace VerifyDigitalSignature
{
    /// <summary>
    /// call this method after the thread finised
    /// </summary>
    public delegate void OnThreadFinished();

    /// <summary>
    /// Update ProcessBar
    /// </summary>
    public delegate void UpdateProcessBarEventHandle(int count, int currentCount, int totalFiles, int curIndex, string currentFile);

    public class VerifyDigitalSignature
    {
        private Form _owner;
        private Thread _thread;    
        private UpdateProcessBarEventHandle _event; 
        private OnThreadFinished _finish;
        private IVerifyStrategy _verifyStrategy;
        private bool _isRunning = false;

        private DateTime _begin;
        private DateTime _end;

        private int _totalFiles = 0;

        private string _log;
        public string Log => _log;

        public bool IsRunning => _isRunning;

        public VerifyDigitalSignature(Form owner, IVerifyStrategy strategy, UpdateProcessBarEventHandle eventHandle, OnThreadFinished finish)
        {
            _owner = owner;
            _verifyStrategy = strategy;
            _event = eventHandle;
            _finish = finish;
        }

        /// <summary>
        /// reset
        /// </summary>
        public void ResetCounter()
        {
            _totalFiles = 0;
        }

        /// <summary>
        /// Start to verify
        /// </summary>
        /// <param name="folder"></param>
        public void Start(string folder)
        {
            this.ResetCounter();

            // Create a new instance to verify          
            _thread = new Thread(this.DoProcessing) {IsBackground = true};
            _thread.Start(folder);
            _begin = DateTime.Now;
        }

        /// <summary>
        /// Stop
        /// </summary>
        public void Stop()
        {
            if (_thread == null) return;
            try
            {
                _isRunning = false;
                _thread.Abort();
                _thread.Join();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

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
                //this.DoCountDirs(new string[] { path });
                this.Verify(path);

                _end = DateTime.Now;
                _owner.Invoke(_finish);
            }
        }

        private void Verify(string path)
        {
            //实例化一个统计器，使用策略模式
            IVerifyStrategy strategy = new VerifyStrategy(true,
                true, true, true);
            SetStrategy(strategy); //设置统计策略

            #region check code
            try
            {
                var curIndex = 0;
                var listDllFiles = Directory.GetFiles(path, "*.dll");
                var listExeFiles = Directory.GetFiles(path, "*.exe");
                _totalFiles = listDllFiles.Length + listExeFiles.Length;
                
                // Output content
                var numWithSignatureDll = 0;
                var numWithSignatureExe = 0;
                var summaryContent = string.Empty;
                var exeFileWithValidSignatureList = new List<string>();
                var exeFileWithInvalidSignatureList = new List<string>();
                var exeFileWithoutSignatureList = new List<string>();
                var dllFileWithValidSignatureList = new List<string>();
                var dllFileWithInvalidSignatureList = new List<string>();
                var dllFileWithoutSignatureList = new List<string>();
                var fileWithoutSignatureList = new List<string>();

                // check signature for exe file
                foreach (var fileName in listExeFiles)
                {
                    try
                    {
                        X509Certificate cert = X509Certificate.CreateFromSignedFile(fileName);
                        X509Certificate2 theCertificate = new X509Certificate2(cert.Handle);
                        bool valid = theCertificate.Verify();
                        if (valid)
                        {
                            numWithSignatureExe = numWithSignatureExe + 1;
                            exeFileWithValidSignatureList.Add(fileName);
                        }
                        else
                        {
                            numWithSignatureExe = numWithSignatureExe + 1;
                            exeFileWithInvalidSignatureList.Add(fileName);
                        }
                    }
                    catch (Exception)
                    {
                        exeFileWithoutSignatureList.Add(fileName);
                        fileWithoutSignatureList.Add(fileName);
                    }

                    //显示消息    
                    curIndex++;
                    _owner.Invoke(_event, _totalFiles, 0, _totalFiles, curIndex, string.Empty);
                }

                // check signature for dll file
                foreach (var fileName in listDllFiles)
                {
                    try
                    {
                        X509Certificate cert = X509Certificate.CreateFromSignedFile(fileName);
                        X509Certificate2 theCertificate = new X509Certificate2(cert.Handle);
                        bool valid = theCertificate.Verify();
                        if (valid)
                        {
                            numWithSignatureDll = numWithSignatureDll + 1;
                            dllFileWithValidSignatureList.Add(fileName);
                        }
                        else
                        {
                            numWithSignatureDll = numWithSignatureDll + 1;
                            dllFileWithInvalidSignatureList.Add(fileName);
                        }
                    }
                    catch (Exception)
                    {
                        dllFileWithoutSignatureList.Add(fileName);
                        fileWithoutSignatureList.Add(fileName); 
                    }

                    //显示消息    
                    curIndex++;
                    _owner.Invoke(_event, _totalFiles, 0, _totalFiles, curIndex, string.Empty);
                    //VerifyLog.Instance.Write(file, count, DateTime.Now);
                }

                // Summary
                var numTotalFiles = listExeFiles.Count() + listDllFiles.Count();
                var numNoSigExe = listExeFiles.Count() - numWithSignatureExe;
                var numNoSigDll = listDllFiles.Count() - numWithSignatureDll;
                var numNoSig = numNoSigExe + numNoSigDll;
                summaryContent = summaryContent + "===================================================================" + "\r\n";
                summaryContent = summaryContent + "Directory:  " + path + "\r\n";
                summaryContent = summaryContent + "Total files (.exe and .dll file) need to check:  " + numTotalFiles + "\r\n";
                summaryContent = summaryContent + "Total files without signature:  " + numNoSig + "\r\n";
                summaryContent = summaryContent + "===================================================================" + "\r\n\r\n\r\n";

                summaryContent = summaryContent + "Total exe files: " + listExeFiles.Count() + "\r\n";
                summaryContent = summaryContent + "Total exe files with signature:  " + numWithSignatureExe + "\r\n";
                summaryContent = summaryContent + "Total exe files without signature:  " + numNoSigExe + "\r\n\r\n";

                summaryContent = summaryContent + "Total dll files: " + listDllFiles.Count() + "\r\n";
                summaryContent = summaryContent + "Total dll files with signature: " + numWithSignatureDll + "\r\n";
                summaryContent = summaryContent + "Total dll files without signature: " + numNoSigDll + "\r\n\r\n";

                if (fileWithoutSignatureList.Count() > 0)
                {
                    summaryContent = summaryContent + "\r\n\r\n" + "List of files without signature: " + "\r\n";
                    int i = 0;
                    foreach (var fileName in fileWithoutSignatureList)
                    {
                        i++;
                        summaryContent = summaryContent + i + "  " + Path.GetFileName(fileName) + "\r\n"; ;
                    }
                }

                if (exeFileWithValidSignatureList.Count > 0)
                {
                    summaryContent = summaryContent + "\r\n\r\n" + "List of exe files with valid signature:" + "\r\n";
                    int i = 0;
                    foreach (var fileName in exeFileWithValidSignatureList)
                    {
                        i++;
                        X509Certificate cert = X509Certificate.CreateFromSignedFile(fileName);
                        summaryContent = summaryContent + i + " " + Path.GetFileName(fileName) + ":  " + X509Certificate.CreateFromSignedFile(fileName).Subject.ToString() + "\r\n";
                    }
                }

                if (exeFileWithInvalidSignatureList.Count > 0)
                {
                    summaryContent = summaryContent + "\r\n\r\n" + "List of exe files with invalid signature:" + "\r\n";
                    int i = 0;
                    foreach (var fileName in exeFileWithInvalidSignatureList)
                    {
                        i++;
                        X509Certificate cert = X509Certificate.CreateFromSignedFile(fileName);
                        summaryContent = summaryContent + i + " " + Path.GetFileName(fileName) + ":  Chain Not Valid (certificate is self-signed)" + "\r\n";
                    }
                }

                if (dllFileWithValidSignatureList.Count > 0)
                {
                    summaryContent = summaryContent + "\r\n\r\n" + "List of dll files with valid signature:" + "\r\n";
                    int i = 0;
                    foreach (var fileName in dllFileWithValidSignatureList)
                    {
                        i++;
                        summaryContent = summaryContent + i + " " + Path.GetFileName(fileName) + ":  " + X509Certificate.CreateFromSignedFile(fileName).Subject.ToString() + "\r\n";
                    }
                }

                if (dllFileWithInvalidSignatureList.Count > 0)
                {
                    summaryContent = summaryContent + "\r\n\r\n" + "List of dll file with invalid signature:" + "\r\n";
                    int i = 0;
                    foreach (var fileName in dllFileWithInvalidSignatureList)
                    {
                        i++;
                        summaryContent = summaryContent + i + " " + Path.GetFileName(fileName) + ":  Chain Not Valid (certificate is self-signed)" + "\r\n";
                    }
                }

                _log = summaryContent;
                
            }

            catch (Exception ex)
            {
                _log = ex.Message;
            }
            #endregion
        }

        /// <summary>
        /// 获取统计结果
        /// </summary>
        /// <returns></returns>
        public string GetLastResult()
        {
            StringBuilder sb = new StringBuilder();

            TimeSpan ts = _end - _begin;

            sb.AppendLine("共统计文件: " + _totalFiles.ToString());
            sb.AppendLine("用时: " + ts.TotalSeconds + "秒!");
            return sb.ToString();
        }

        /// <summary>
        /// 设置统计策略
        /// </summary>
        /// <param name="strategy">策略</param>
        public void SetStrategy(IVerifyStrategy strategy)
        {
            _verifyStrategy = strategy;
        }
    }
}
