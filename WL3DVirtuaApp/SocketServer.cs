using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WL3DVirtuaApp
{
    public class SokcetServer
    {
        public delegate void DelegateReceive(AgreementInfo agreement);
        public event DelegateReceive ReceiveEvent;
        private SocketError SError;
        private Socket AsyncSocketListener;
        public ManualResetEvent allDone;
        private IPAddress _ip;
        public IPAddress Ip
        {
            get { return _ip; }
            set { _ip = value; }
        }
        private int _port;
        public int Port
        {
            get { return _port; }
            set { _port = value; }
        }
        private Thread threadListener;
        private bool flagListener;
        private List<ConnectClient> _listClient;
        public List<ConnectClient> GetListClient
        {
            get
            {
                ConnectClient[] list;
                lock (lockObj)
                {
                    list = new ConnectClient[_listClient.Count];
                    _listClient.CopyTo(list, 0);
                }
                return list.ToList();
            }
        }
        private static object lockObj = new object();
        private Queue<AgreementInfo> QueueAgreement;
        private Thread threadHandle;
        private bool flagHandle;
        private Thread threadCheckStatus;
        private bool flagCheck;

        public SokcetServer(IPAddress ip, int Port)
        {
            this._ip = ip;
            this._port = Port;
            this.allDone = new ManualResetEvent(false);
            this._listClient = new List<ConnectClient>();
            this.QueueAgreement = new Queue<AgreementInfo>();
        }

        public bool Start()
        {
            try
            {
                this._listClient.Clear();
                this.QueueAgreement.Clear();
                this.AsyncSocketListener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                this.AsyncSocketListener.Bind(new IPEndPoint(this._ip, this._port));
                this.AsyncSocketListener.Listen(100);
                this.threadListener = new Thread(new ThreadStart(() =>
                {
                    while (this.flagListener)
                    {
                        this.allDone.Reset();
                        this.AsyncSocketListener.BeginAccept(new AsyncCallback(AcceptCallback), this.AsyncSocketListener);
                        this.allDone.WaitOne();
                    }
                }));
                this.threadListener.IsBackground = true;
                this.flagListener = true;
                this.threadListener.Start();

                this.threadHandle = new Thread(new ThreadStart(() =>
                {
                    AgreementInfo obj;
                    while (this.flagHandle)
                    {
                        try
                        {
                            if (this.QueueAgreement.Count > 0)
                            {
                                lock (lockObj)
                                {
                                    obj = this.QueueAgreement.Dequeue();
                                }
                                this.ReceiveEvent(obj);
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                        finally
                        {
                            Thread.Sleep(1);
                        }
                    }
                }));
                this.threadHandle.IsBackground = true;
                this.flagHandle = true;
                this.threadHandle.Start();

                this.threadCheckStatus = new Thread(new ThreadStart(() =>
                {
                    List<ConnectClient> objs;
                    while (this.flagCheck)
                    {
                        try
                        {
                            lock (lockObj)
                            {
                                objs = this._listClient.Where(a => a.LastTime.AddMinutes(1) < DateTime.Now).ToList();
                                if (objs != null && objs.Count > 0)
                                {
                                    foreach (var v in objs)
                                    {
                                        v.Customer.Close();
                                        this._listClient.Remove(v);
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                        finally
                        {
                            Thread.Sleep(10000);
                        }
                    }
                }));
                this.threadCheckStatus.IsBackground = true;
                this.flagCheck = true;
                this.threadCheckStatus.Start();

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("server-Start-" + ex.Message);
                return false;
            }
        }

        public void Stop()
        {
            try
            {
                this.flagHandle = false;
                this.flagListener = false;
                this.flagCheck = false;
                if (this.AsyncSocketListener != null)
                {
                    this.AsyncSocketListener.Close();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("server-Stop-" + ex.Message);
            }
        }

        private void AcceptCallback(IAsyncResult ar)
        {
            try
            {
                this.allDone.Set();
                Socket listener = (Socket)ar.AsyncState;
                Socket handler = listener.EndAccept(ar);
                lock (lockObj)
                {
                    if (this._listClient.Find(a => a.IpEnd == handler.RemoteEndPoint.ToString()) == null)
                    {
                        ConnectClient obj = new ConnectClient() { Customer = handler, LastTime = DateTime.Now, IpEnd = handler.RemoteEndPoint.ToString() };
                        this._listClient.Add(obj);
                    }
                }
                StateObject state = new StateObject();
                state.workSocket = handler;
                handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReadCallback), state);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("server-AcceptCallback-" + ex.Message);
            }
        }

        private void ReadCallback(IAsyncResult ar)
        {
            try
            {
                StateObject state = (StateObject)ar.AsyncState;
                Socket handler = state.workSocket;
                int bytesRead = handler.EndReceive(ar, out this.SError);
                //Debug.WriteLine("server-Receive-" + bytesRead.ToString());
                if (bytesRead > 0)
                {
                    ConnectClient conc = null;
                    lock (lockObj)
                    {
                        conc = this._listClient.Find(a => a.IpEnd == handler.RemoteEndPoint.ToString());
                    }
                    if (conc != null)
                    {
                        if (conc.LackCount == 0)
                        {
                            if (state.buffer[0] == 0xc5 && state.buffer[1] == 0x5c)
                            {
                                #region
                                int baglength = BitConverter.ToInt32(new byte[] { state.buffer[2], state.buffer[3], state.buffer[4], state.buffer[5] }, 0);
                                conc.Bycommd = new byte[baglength];
                                if (bytesRead == 6)
                                {
                                    conc.LackCount = baglength;
                                    conc.CopyIndex = 0;
                                }
                                else if (bytesRead - 6 > baglength)
                                {
                                    Array.Copy(state.buffer, 6, conc.Bycommd, 0, baglength);
                                    conc.LackCount = 0;
                                    this.HandlePack(conc);
                                    conc.RecurrenceIndex = baglength + 6;
                                    //Debug.WriteLine("server-RecurrenceIndex-" + conc.RecurrenceIndex.ToString());
                                    this.RecurrenceRead(conc, bytesRead, state.buffer);
                                }
                                else
                                {
                                    Array.Copy(state.buffer, 6, conc.Bycommd, 0, bytesRead - 6);
                                    conc.LackCount = baglength - (bytesRead - 6);
                                    if (conc.LackCount == 0)
                                    {
                                        conc.CopyIndex = 0;
                                        this.HandlePack(conc);
                                    }
                                    else
                                    {
                                        conc.CopyIndex = bytesRead - 6;
                                    }
                                }
                                #endregion
                            }
                        }
                        else
                        {
                            if (bytesRead < conc.LackCount)
                            {
                                Array.Copy(state.buffer, 0, conc.Bycommd, conc.CopyIndex, bytesRead);
                                conc.LackCount = conc.LackCount - bytesRead;
                                conc.CopyIndex = conc.CopyIndex + bytesRead;
                            }
                            else if (bytesRead == conc.LackCount)
                            {
                                Array.Copy(state.buffer, 0, conc.Bycommd, conc.CopyIndex, bytesRead);
                                conc.CopyIndex = 0;
                                conc.LackCount = 0;
                                this.HandlePack(conc);
                            }
                            else if (bytesRead > conc.LackCount)
                            {
                                Array.Copy(state.buffer, 0, conc.Bycommd, conc.CopyIndex, conc.LackCount);
                                this.HandlePack(conc);
                                conc.RecurrenceIndex = conc.LackCount;
                                //Debug.WriteLine("server-再次-" + conc.RecurrenceIndex.ToString());
                                this.RecurrenceRead(conc, bytesRead, state.buffer);
                            }
                        }
                    }
                    handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReadCallback), state);
                }
                else
                {
                    lock (lockObj)
                    {
                        ConnectClient ccc = this._listClient.Find(a => a.IpEnd == handler.RemoteEndPoint.ToString());
                        this._listClient.Remove(ccc);
                    }
                    handler.Close();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("server-ReadCallback-" + ex.Message);
            }
        }

        private void RecurrenceRead(ConnectClient conc, int bytesRead, byte[] buffer)
        {
            try
            {
                if (buffer[conc.RecurrenceIndex] == 0xc5 && buffer[conc.RecurrenceIndex + 1] == 0x5c)
                {
                    int ageinlength = BitConverter.ToInt32(new byte[] { buffer[conc.RecurrenceIndex + 2], buffer[conc.RecurrenceIndex + 3], buffer[conc.RecurrenceIndex + 4], buffer[conc.RecurrenceIndex + 5] }, 0);
                    conc.Bycommd = new byte[ageinlength];
                    if (bytesRead - conc.RecurrenceIndex - 6 > ageinlength)
                    {
                        Array.Copy(buffer, conc.RecurrenceIndex + 6, conc.Bycommd, 0, ageinlength);
                        conc.LackCount = 0;
                        this.HandlePack(conc);
                        conc.RecurrenceIndex = conc.RecurrenceIndex + ageinlength + 6;
                        //Debug.WriteLine("server-RecurrenceIndex-" + conc.RecurrenceIndex.ToString());
                        this.RecurrenceRead(conc, bytesRead, buffer);
                    }
                    else if (bytesRead - conc.RecurrenceIndex == 6)
                    {
                        conc.LackCount = ageinlength;
                        conc.CopyIndex = 0;
                    }
                    else
                    {
                        Array.Copy(buffer, conc.RecurrenceIndex + 6, conc.Bycommd, 0, bytesRead - conc.RecurrenceIndex - 6);
                        conc.LackCount = ageinlength - (bytesRead - conc.RecurrenceIndex - 6);
                        //Debug.WriteLine("server-LackCount-" + conc.LackCount.ToString());
                        if (conc.LackCount == 0)
                        {
                            conc.CopyIndex = 0;
                            this.HandlePack(conc);
                        }
                        else
                        {
                            conc.CopyIndex = bytesRead - conc.RecurrenceIndex - 6;
                            //Debug.WriteLine("server-CopyIndex-" + conc.CopyIndex.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("server-RecurrenceRead-" + ex.Message);
            }
        }

        private void HandlePack(ConnectClient conc)
        {
            try
            {
                if (conc.Bycommd.Length == 9 && Encoding.GetEncoding("GB2312").GetString(conc.Bycommd) == "HeartBeat")//心跳包
                {
                    conc.LastTime = DateTime.Now;
                }
                else
                {
                    conc.LastTime = DateTime.Now;
                    //Debug.WriteLine("server-Receive-" + conc.Bycommd[0].ToString("X"));
                    //Debug.WriteLine("server-Receive-" + conc.Bycommd[conc.Bycommd.Length - 1].ToString("X"));
                    //进行包处理
                    string commd = Encoding.GetEncoding("GB2312").GetString(conc.Bycommd);
                    Debug.WriteLine(commd);
                    lock (lockObj)
                    {
                        this.QueueAgreement.Enqueue(new AgreementInfo() { Ipend = conc.IpEnd, Content = commd });
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("server-HandlePack-" + ex.Message);
            }
        }
        public void SendAgreement1(string str, string iped)
        {
            //Thread thread = new Thread(ReceiveMessage);
            // thread.Start();
            try
            {
                //List<byte[]> commd = this.MakeBag(Encoding.GetEncoding("GB2312").GetBytes(str));
                //List<byte[]> commd = this.MakeBag(Encoding.Default.GetBytes(str));
                //List<byte[]> commd = Encoding.GetEncoding("GB2312").GetBytes(str);
                List<ConnectClient> list = new List<ConnectClient>();
                ConnectClient client = this._listClient.Find(a => a.IpEnd == iped);
                if (client != null)
                {
                    if (client.Customer.Connected)
                    {
                        Debug.WriteLine("回复 " + iped);

                            //Debug.WriteLine("server-" + commd[i][0].ToString("X"));
                            //Debug.WriteLine("server-" + commd[i][commd[i].Length - 1].ToString("X"));
                            try
                            {
                                //client.Customer.Send(Encoding.GetEncoding("GB2312").GetBytes(StringToHexString(str,Encoding.GetEncoding("GB2312"))));
                                client.Customer.Send(Encoding.GetEncoding("GB2312").GetBytes(str));
                            }
                            catch (Exception sk)
                            {
                                Debug.WriteLine("server-ClientSend-" + sk.Message);
                            }                        
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("server-SendAgreement-" + ex.Message);
            }
        }

        public void SendAgreement(string str, string iped)
        {
            try
            {
                List<byte[]> commd = this.MakeBag(Encoding.GetEncoding("GB2312").GetBytes(str));
                //List<byte[]> commd = this.MakeBag(Encoding.Default.GetBytes(str));
                //List<byte[]> commd = Encoding.GetEncoding("GB2312").GetBytes(str);
                if (commd == null || commd.Count == 0)
                    return;
                List<ConnectClient> list = new List<ConnectClient>();
                ConnectClient client = this._listClient.Find(a => a.IpEnd == iped);
                if (client != null)
                {
                    if (client.Customer.Connected)
                    {
                        Debug.WriteLine("回复 " + iped);
                        for (int i = 0; i < commd.Count; i++)
                        {
                            //Debug.WriteLine("server-" + commd[i][0].ToString("X"));
                            //Debug.WriteLine("server-" + commd[i][commd[i].Length - 1].ToString("X"));
                            try
                            {
                                client.Customer.BeginSend(commd[i], 0, commd[i].Length, 0, callback =>
                                {
                                    try
                                    {
                                        Socket handler = (Socket)callback.AsyncState;
                                        handler.EndSend(callback);
                                    }
                                    catch { }
                                }, client.Customer);
                            }
                            catch (Exception sk)
                            {
                                Debug.WriteLine("server-ClientSend-" + sk.Message);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("server-SendAgreement-" + ex.Message);
            }
        }

        /// <summary>
        /// Get bytes with head(byte length and character 5c,c5)
        /// </summary>
        /// <param name="bys"></param>
        /// <returns></returns>
        private List<byte[]> MakeBag(byte[] bys)
        {
            byte[] byhead = new byte[]{ 0x5c, 0xc5 };
            byte[] bylength = BitConverter.GetBytes(bys.Length);
            byte[] bybag = new byte[byhead.Length + bylength.Length + bys.Length];
            Array.Copy(byhead, 0, bybag, 0, byhead.Length);
            Array.Copy(bylength, 0, bybag, byhead.Length, bylength.Length);
            Array.Copy(bys, 0, bybag, byhead.Length + bylength.Length, bys.Length);
            List<byte[]> lists = new List<byte[]>();
            if (bybag.Length <= 1024)
            {
                lists.Add(bybag);
            }
            else
            {
                int count = bybag.Length / 1024;
                for (int i = 0; i < count; i++)
                {
                    byte[] cby = new byte[1024];
                    Array.Copy(bybag, i * 1024, cby, 0, 1024);
                    lists.Add(cby);
                }
                int yu = bybag.Length % 1024;
                if (yu > 0)
                {
                    byte[] yuby = new byte[yu];
                    Array.Copy(bybag, count * 1024, yuby, 0, yu);
                    lists.Add(yuby);
                }
            }
            return lists;
        }

        /// <summary>
        /// Get bytes without head
        /// </summary>
        /// <param name="bys"></param>
        /// <returns></returns>
        private List<byte[]> MakeBagWithNoHead(byte[] bys)
        {
            byte[] bybag = new byte[bys.Length];
            Array.Copy(bys, 0, bybag, 0, bys.Length);
            List<byte[]> lists = new List<byte[]>();

            if (bybag.Length <= 1024)
            {
                lists.Add(bybag);
            }
            else
            {
                int count = bybag.Length / 1024;
                for (int i = 0; i < count; i++)
                {
                    byte[] cby = new byte[1024];
                    Array.Copy(bybag, i * 1024, cby, 0, 1024);
                    lists.Add(cby);
                }
                int yu = bybag.Length % 1024;
                if (yu > 0)
                {
                    byte[] yuby = new byte[yu];
                    Array.Copy(bybag, count * 1024, yuby, 0, yu);
                    lists.Add(yuby);
                }
            }
            return lists;
        }

        public string StringToHexString(string s, Encoding encode)
        {
            byte[] b = encode.GetBytes(s);//按照指定编码将string编程字节数组
            string result = string.Empty;
            for (int i = 0; i < b.Length; i++)//逐字节变为16进制字符，以%隔开
            {
                result += " " + Convert.ToString(b[i], 16);
            }
            return result;
        }
    }

    public class AgreementInfo
    {
        private string ipend;

        public string Ipend
        {
            get { return ipend; }
            set { ipend = value; }
        }

        private string content;

        public string Content
        {
            get { return content; }
            set { content = value; }
        }
    }

    public class StateObject
    {
        public Socket workSocket = null;

        public const int BufferSize = 1024;

        public byte[] buffer = new byte[BufferSize];
    }

}
