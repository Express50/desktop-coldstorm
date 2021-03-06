﻿using System;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace DesktopCS.MVVM
{
    public abstract class UIInvoker
    {
        private readonly Dispatcher _dispatcher = Application.Current.Dispatcher;

        protected bool InvokeRequired
        {
            get
            {
               return this._dispatcher.Thread != Thread.CurrentThread;
            }
        }

        protected void Invoke(Action action)
        {
            this._dispatcher.Invoke(action);
        }

        protected void Run(Action action)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(action);
            }
            else
            {
                action();
            }
        }
    }
}
