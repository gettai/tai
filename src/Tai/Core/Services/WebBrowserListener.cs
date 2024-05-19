using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tai.Common;
using Tai.Core.Event;
using Tai.Core.Message;
using Tai.Core.Message.Types;
using WebSocketSharp.Server;

namespace Tai.Core.Services
{
    public class WebBrowserListener : WebSocketBehavior, IWebBrowserListener
    {
        private WebSocketServer _server;
        private const int _port = 8908;
        private const string _path = "/TaiWebSentry";

        public event WebBrowserListenerActiveEventHandler OnActive;
        public event WebBrowserListenerInactiveEventHandler OnInactive;
        public event WebBrowserListenerClosedEventHandler OnClosed;
        public event WebBrowserListenerExitEventHandler OnExit;

        public void Start()
        {
            Stop();

            try
            {
                _server = new WebSocketServer(_port, false);
                //_server.AddWebSocketService<WebBrowserListener>(_path);
                _server.AddWebSocketService(_path, () => this);
                _server.Start();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "WebBrowserListener Start Failed");
            }
        }

        public void Stop()
        {
            if (_server != null)
            {
                _server.Stop();
                _server = null;
            }
        }

        protected override void OnMessage(WebSocketSharp.MessageEventArgs e)
        {
            base.OnMessage(e);
            try
            {
                Debug.WriteLine("【浏览器消息】" + e.Data);
                var msgObj = JsonConvert.DeserializeObject<JObject>(e.Data);
                var typeStr = msgObj["Type"].ToString();
                if (typeStr == nameof(TaiWebBrowserMessageType.Active))
                {
                    var activeLog = JsonConvert.DeserializeObject<TaiWebBrowserActiveMessage>(e.Data);
                    OnActive?.Invoke(ID, activeLog);
                }
                else if (typeStr == nameof(TaiWebBrowserMessageType.Inactive))
                {
                    var inactiveLog = JsonConvert.DeserializeObject<TaiWebBrowserInactiveMessage>(e.Data);
                    OnInactive?.Invoke(ID, inactiveLog);
                }
                else if (typeStr == nameof(TaiWebBrowserMessageType.Closed))
                {
                    var closedLog = JsonConvert.DeserializeObject<TaiWebBrowserClosedMessage>(e.Data);
                    OnClosed?.Invoke(ID, closedLog.Data);
                }
            }
            catch { }
        }

        protected override void OnClose(WebSocketSharp.CloseEventArgs e)
        {
            base.OnClose(e);
            OnExit?.Invoke(ID);
        }
    }
}
