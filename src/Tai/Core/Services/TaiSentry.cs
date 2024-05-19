using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Tai.Common;
using Tai.Core.Event;
using Tai.Core.Message;
using Tai.Core.Message.Types;
using WebSocketSharp;

namespace Tai.Core.Services
{
    public class TaiSentry : ITaiSentry
    {
        public event TaiSentryActiveAppDataEventHandler OnActiveAppDataUpdated;
        public event TaiSentryInactiveAppDataEventHandler OnInactiveAppDataUpdated;
        public event TaiSentryStatusEventHandler OnStatusChanged;

        private readonly IPolling _polling;

        private readonly string _taiSentryName = "TaiSentryService.exe";
        private readonly string _taiSentryUrl = "ws://127.0.0.1:21123/TaiSentry";
        //  websocket
        private WebSocket _ws;
        private WebSocketState _wsState = WebSocketState.Closed;
        private bool _isTaiSentryRunning = false;
        public TaiSentry(IPolling polling_)
        {
            _polling = polling_;
        }

        public void Run()
        {
            ProcessHelper.Kill(_taiSentryName);
            RunTaiSentry();
            Connect();
            _polling.PollingTimeElapsed += _polling_PollingTimeElapsed;
        }

        private void _polling_PollingTimeElapsed()
        {
            RunTaiSentry();
            Connect();
        }

        public void Shutdown()
        {
            Disconnect();
            ProcessHelper.Kill(_taiSentryName);
            _polling.PollingTimeElapsed -= _polling_PollingTimeElapsed;
        }

        private void InitWebSocket()
        {
            _ws = new WebSocket(_taiSentryUrl);
            _ws.OnMessage += _ws_OnMessage;
            _ws.OnOpen += _ws_OnOpen;
            _ws.OnClose += _ws_OnClose;
        }
        private void Connect()
        {
            if (_ws == null)
            {
                InitWebSocket();
                _ws.Close();
            }
            if (_ws.ReadyState == WebSocketState.Open)
            {
                return;
            }
            _ws.Connect();
        }

        private void _ws_OnClose(object sender, CloseEventArgs e)
        {
            _wsState = WebSocketState.Closed;
            Common.Logger.Warn("websocket断开！！！");
        }

        private void _ws_OnOpen(object sender, EventArgs e)
        {
            _wsState = WebSocketState.Open;
            Common.Logger.Info("websocket已成功连接！");
        }

        private void _ws_OnMessage(object sender, MessageEventArgs e)
        {
            Common.Logger.Debug("收到消息：" + e.Data);
            var msgObj = JsonConvert.DeserializeObject<JObject>(e.Data);
            var typeStr = msgObj["Type"].ToString();
            if (typeStr == nameof(TaiSentryMessageType.ActiveAppTime))
            {
                var data = JsonConvert.DeserializeObject<TaiSentryActiveAppTimeMessage>(e.Data);
                OnActiveAppDataUpdated?.Invoke(this, data);
            }
            else if (typeStr == nameof(TaiSentryMessageType.InactiveAppTime))
            {
                var data = JsonConvert.DeserializeObject<TaiSentryInactiveAppTimeMessage>(e.Data);
                OnInactiveAppDataUpdated?.Invoke(this, data);
            }
            else if (typeStr == nameof(TaiSentryMessageType.Status))
            {
                var status = JsonConvert.DeserializeObject<TaiSentryStatusMessage>(e.Data);
                OnStatusChanged?.Invoke(this, status);
            }
        }

        private void Disconnect()
        {
            if (_ws != null)
            {
                _ws.Close();
                _ws = null;
            }
        }

        private bool RunTaiSentry()
        {
            try
            {
                string taiSentryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _taiSentryName);
                if (ProcessHelper.IsProcessRuning(_taiSentryName)) return true;
                return ProcessHelper.Start(taiSentryPath, true);
            }
            catch (Exception ex_)
            {
                Common.Logger.Error(ex_);
                return false;
            }
        }
    }
}
