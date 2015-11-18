using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WL3DVirtuaApp
{
    public partial class TCPServer : Form
    {
        private SokcetServer _socketServer;
        private Queue<VirtualPackage> _queue = new Queue<VirtualPackage>();
        private Thread _sendTask;
        private readonly object _thisLocker = new object();
        private bool _isRunning;
        private bool _inited;
        private bool _repeatSend = false;
        private bool _singleSend = false;

        public TCPServer()
        {
            InitializeComponent();
            RefreshControl();
            _sendTask = new Thread(new ThreadStart(WhileSendHandle));
            _sendTask.IsBackground = true;
            _sendTask.Start();

            //Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void WhileSendHandle()
        {
            while (true)
            {
                while (_isRunning)
                {
                    try
                    {
                        Monitor.Enter(_thisLocker);
                        if (_queue.Count > 0)
                        {
                            VirtualPackage package = _queue.Dequeue();

                            if (txtSendContent.InvokeRequired)
                            {                                
                                Action<string> actionDelegate = (x) => { txtSendContent.AppendText(package.Data.Trim() + "\r\n"); };
                                // Action<string> actionDelegate = delegate(string txt) { txtSendContent.Text = txt; };
                                txtSendContent.Invoke(actionDelegate, package.Data.Trim());
                            }
                            else
                            {
                                txtSendContent.AppendText(package.Data.Trim() + "\r\n");
                            }                           

                            List<ConnectClient> clients = _socketServer.GetListClient;
                            foreach (ConnectClient client in clients)
                            {
                                _socketServer.SendAgreement(package.Data, client.IpEnd);
                            }
                            if (package.SleepTime > 0)
                            {
                                Thread.Sleep(package.SleepTime);
                            }

                            if (_repeatSend)
                            {
                                _queue.Enqueue(package);
                            }
                            if(_singleSend)
                            {
                                _isRunning = false;
                            }
                        }
                        else if(_queue.Count <= 0)
                        {
                            string msgShow = "Send Completed";
                            if (txtSendContent.InvokeRequired)
                            {
                                Action<string> actionDelegate = (x) => { txtSendContent.AppendText(msgShow + "\r\n"); };
                                txtSendContent.Invoke(actionDelegate, msgShow);
                            }
                            else
                            {
                                txtSendContent.AppendText(msgShow + "\r\n");
                            }
                            _isRunning = false;
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                    finally
                    {
                        Monitor.Exit(_thisLocker);
                    }
                }
                Thread.Sleep(5);
            }
        }

        private void btnCreateServer_Click(object sender, EventArgs e)
        {
            try
            {
                _socketServer = new SokcetServer(IPAddress.Parse(txtServerIP.Text.Trim()), Convert.ToInt32(numServerPort.Value));
                _socketServer.ReceiveEvent += _socketServer_ReceiveEvent;
                _socketServer.Start();
                _inited = true;
                RefreshControl();
            }
            catch (Exception ex)
            {
            }
        }

        private void btnStopServer_Click(object sender, EventArgs e)
        {
            try
            {
                _socketServer.Stop();
                _inited = false;
                _isRunning = false;
                RefreshControl();
            }
            catch (Exception ex)
            {
            }
        }

        private void RefreshControl()
        {
            btnCreateServer.Enabled = !_inited;
            btnStopServer.Enabled = _inited;
            btnSet.Enabled = _inited && !_isRunning;
            btnClear.Enabled = _inited && !_isRunning;
            btnWhileSend.Enabled = _inited && !_isRunning;
            btnStop.Enabled = _inited && _isRunning;
            btnSendFileContent.Enabled = _inited && !_isRunning;
            btnSingleSend.Enabled = _inited && !_isRunning;
        }

        void _socketServer_ReceiveEvent(AgreementInfo agreement)
        {
            this.WriteInfo(richReceive, agreement.Content);
        }

        private void WriteInfo(RichTextBox control, string msg)
        {
            if (control.InvokeRequired)
            {
                Action<string> action = (str) =>
                {
                    this.WriteInfo(control, str);
                };
                control.Invoke(action, msg);
            }
            else
            {
                control.AppendText(msg + " ---" + DateTime.Now.ToString() + "\r\n");
            }
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            Monitor.Enter(_thisLocker);
            _queue.Enqueue(new VirtualPackage() { Data = txtVirualData.Text, SleepTime = Convert.ToInt32(numVirtualInterval.Value) });
            Monitor.Exit(_thisLocker);
            richSend.AppendText(txtVirualData.Text + "\r");
            txtVirualData.Text = String.Empty;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Monitor.Enter(_thisLocker);
            _queue.Clear();
            richSend.Clear();
            Monitor.Exit(_thisLocker);
        }

        private void btnWhileSend_Click(object sender, EventArgs e)
        {
            _isRunning = true;
            _repeatSend = true;
            RefreshControl();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            _isRunning = false;
            _repeatSend = false;

            _repeatSend = false;
            _singleSend = false;

            _queue.Clear();

            RefreshControl();
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openLog = new OpenFileDialog();
            if (openLog.ShowDialog() == DialogResult.OK)
            {
                txtFilePath.Text = openLog.FileName;
            }
        }

        private void btnSendFileContent_Click(object sender, EventArgs e)
        {
            FileOperation fileOp = new FileOperation();
            FileObject fileObj = new FileObject();
            string[] logLine = new string[0];
            if (txtFilePath.Text != "")
            {
                fileObj.FilePath = txtFilePath.Text;
                logLine = fileOp.PopulateList(fileObj.FilePath).ToArray();
                for (int i = 0; i < logLine.Length; i++)
                {
                    string log = logLine[i];
                    string logNext = i + 1 == logLine.Length ? logLine[i] : logLine[i + 1];
                    int seconds = 1;
                    if (log != "")
                    {
                        if (logNext != "")
                        {
                            DateTime logTime = Convert.ToDateTime(log.Substring(1, log.IndexOf("]") - 1));
                            DateTime logNextTime = Convert.ToDateTime(logNext.Substring(1, logNext.IndexOf("]") - 1));
                            seconds = Convert.ToInt32((logNextTime - logTime).TotalMilliseconds);
                        }
                        int res = log.IndexOf("[Res]") + 4;
                        int req = log.IndexOf("[Req]") + 4;
                        int index = res >= req ? res : req;
                        log = log.Replace("\r\n", "").Remove(0, index+1);                       

                        Monitor.Enter(_thisLocker);
                        _queue.Enqueue(new VirtualPackage() { Data = log.Trim(), SleepTime = seconds });                       
                        Monitor.Exit(_thisLocker);
                        
                    }
                }
                _isRunning = true;
                RefreshControl();
            }
        }

        private void btnSingleSend_Click(object sender, EventArgs e)
        {
            Monitor.Enter(_thisLocker);
            _queue.Enqueue(new VirtualPackage() { Data = richSend.Text, SleepTime = Convert.ToInt32(numVirtualInterval.Value) });
            Monitor.Exit(_thisLocker);
            _isRunning = true;
            _repeatSend = false;
            _singleSend = true;
            RefreshControl();
            btnSingleSend.Enabled = true;
        }

        private void btnClearSentLog_Click(object sender, EventArgs e)
        {
            txtSendContent.Text = string.Empty;
        }
    }
}
