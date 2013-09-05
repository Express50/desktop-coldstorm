﻿using System;
using DesktopCS.Helpers;
using DesktopCS.Models;
using DesktopCS.MVVM;
using NetIRC;
using NetIRC.Messages.Send;

namespace DesktopCS.Services.IRC
{
    public class IRCUser : UIInvoker, IDisposable
    {
        private readonly IRCClient _ircClient;
        private readonly Tab _tab;
        private readonly User _user;

        public IRCUser(IRCClient ircClient, Tab tab, User user)
        {
            this._ircClient = ircClient;
            this._ircClient.Input += _ircClient_Input;
            this._ircClient.Ping += _ircClient_Ping;
            this._ircClient.Message += _ircClient_Message;

            this._tab = tab;
            this._tab.Close += _tab_Close;

            this._user = user;
            this._user.OnNickNameChange += _user_OnNickNameChange;
            this._user.OnQuit += _user_OnQuit;
        }

        #region Event Handlers

        private void _tab_Close(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void _ircClient_Ping(object sender, PingEventArgs e)
        {
            if (e.Target == _user.NickName)
            {
                e.Handled = true;
            }
        }

        void _ircClient_Message(object sender, User user, string message)
        {
            if (user == this._user)
            {
                _tab.AddChat(user, message);
            }
        }

        private void _ircClient_Input(object sender, string text)
        {
            if (this._tab.IsSelected)
            {
                this._ircClient.Send(new UserPrivate(this._user, text));
            }
        }

        private void _user_OnQuit(User user, string reason)
        {
            Run(this.Dispose);
        }

        private void _user_OnNickNameChange(User user, string original)
        {
            Run(() => { this._tab.Header = _user.NickName; });
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            this._ircClient.Input -= this._ircClient_Input;
            this._ircClient.Ping -= this._ircClient_Ping;

            this._tab.Close -= this._tab_Close;

            this._user.OnNickNameChange -= _user_OnNickNameChange;
            this._user.OnQuit -= _user_OnQuit;
        }

        #endregion
    }
}
