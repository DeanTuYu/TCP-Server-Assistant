using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WL3DVirtuaApp
{
    public class ConnectClient
    {

        public ConnectClient()
        {
            this._lastTime = DateTime.Now;
            this._lackCount = 0;
            this._copyIndex = 0;
            this._recurrenceIndex = 0;
        }

        private DateTime _lastTime;

        public DateTime LastTime
        {
            get { return _lastTime; }
            set { _lastTime = value; }
        }

        private string _ipEnd;

        public string IpEnd
        {
            get { return _ipEnd; }
            set { _ipEnd = value; }
        }

        private string _clientID;

        public string ClientID
        {
            get { return _clientID; }
            set { _clientID = value; }
        }

        private Socket _customer;

        public Socket Customer
        {
            get { return _customer; }
            set { _customer = value; }
        }

        private int _copyIndex;

        public int CopyIndex
        {
            get { return _copyIndex; }
            set { _copyIndex = value; }
        }

        private int _lackCount;

        public int LackCount
        {
            get { return _lackCount; }
            set { _lackCount = value; }
        }

        private int _recurrenceIndex;

        public int RecurrenceIndex
        {
            get { return _recurrenceIndex; }
            set { _recurrenceIndex = value; }
        }

        private byte[] _bycommd;

        public byte[] Bycommd
        {
            get { return _bycommd; }
            set { _bycommd = value; }
        }

    }
}
