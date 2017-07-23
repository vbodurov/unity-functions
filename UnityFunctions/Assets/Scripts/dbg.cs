using System;
using System.Linq;
using UnityEngine;

namespace UnityFunctions
{
    internal static class dbg
    {
        public static object Info = "";

        private static bool _appendInitialized;
        internal static readonly string logFilePath = Application.persistentDataPath + @"/__log.txt";
        internal static void log(params object[] args)
        {
            if (!_appendInitialized)
            {
                clear();
            }
            System.IO.File.AppendAllText(logFilePath, string.Join("\t", args.Select<object,string>(Convert.ToString).ToArray()) + "\r\n");
        }
        internal static void clear()
        {
            System.IO.File.WriteAllText(logFilePath, "");
            _appendInitialized = true;
        }
        internal static int int1;
        internal static Vector3 v1;
        internal static Vector3 v2;
        internal static Vector3 v3;
        internal static string st
        {
            get
            {
                System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace();
                return trace.ToString(); //StackTraceUtility.ExtractStackTrace();
            }
        }
        
    }
}