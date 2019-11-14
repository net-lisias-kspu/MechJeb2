using System;
using KSPe.Util.Log;
using System.Diagnostics;
using UnityToolbag;

#if DEBUG
using System.Collections.Generic;
#endif

namespace MechJeb2
{
    internal static class Log
    {
        private static readonly Logger log = Logger.CreateForType<Startup>();

        internal static void init()
        {
            log.level =
#if DEBUG
                Level.TRACE
#else
                GameSettings.VERBOSE_DEBUG_LOG ? Level.DETAIL : Level.INFO
#endif
                ;
        }

        internal static void force (string msg, params object [] @params)
        {
            log.force (msg, @params);
        }

        internal static void info(string msg, params object[] @params)
        {
            log.info(msg, @params);
        }

        internal static void warn(string msg, params object[] @params)
        {
            log.warn(msg, @params);
        }

        internal static void detail(string msg, params object[] @params)
        {
            log.detail(msg, @params);
        }

        internal static void err(Exception e, object offended)
        {
            log.error(offended, e);
        }

        internal static void err(string msg, params object[] @params)
        {
            log.error(msg, @params);
        }

        internal static void err(Exception e, string msg, params object[] @params)
        {
            log.error(e, msg, @params);
        }

        internal static void aerr(Exception e, string msg, params object[] @params)
        {
            Dispatcher.InvokeAsync(() => log.error(e, msg, @params));
        }

        [ConditionalAttribute("DEBUG")]
        internal static void dbg(string msg, params object[] @params)
        {
            log.trace(msg, @params);
        }

        [ConditionalAttribute("DEBUG")]
        internal static void adbg (string msg, params object [] @params)
        {
            Dispatcher.InvokeAsync(() => log.trace (msg, @params));
        }

        #if DEBUG
        private static readonly HashSet<string> DBG_SET = new HashSet<string>();
        #endif

        [ConditionalAttribute("DEBUG")]
        internal static void dbgOnce(string msg, params object[] @params)
        {
            string new_msg = string.Format(msg, @params);
            #if DEBUG
            if (DBG_SET.Contains(new_msg)) return;
            DBG_SET.Add(new_msg);
            #endif
            log.trace(new_msg);
        }
    }
}
