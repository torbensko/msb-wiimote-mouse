using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace WiimoteMouse
{
    public class IRInterpreter
    {
        public const int ERROR_FREE = 0;
        public const int ERROR_NO_IR_DATA = -1;
        public const int ERROR_UNABLE_TO_MATCH_TO_ENV = -2;
        public const int ERROR_NOT_ENOUGH_DATA_FOR_SCALING = -3;
        public const int ERROR_UNABLE_TO_READ_CONFIG = -4;
        public const int ERROR_LOGIC = -5;
        public const int ERROR_DOT = -6;

        public const int MODEL_END = 100000;

        [StructLayout(LayoutKind.Sequential)]
        public struct RawDot 
        {
            public int x;
            public int y;
            public bool visible;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Cursor 
        {
            public int x;
            public int y;
            public int error;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RawData
        {
            public RawDot dot1;
            public RawDot dot2;
            public RawDot dot3;
            public RawDot dot4;
        }

        // Import the methods that perform the main logic
        [DllImport("WiimoteLogic.dll", SetLastError = true, EntryPoint="init", ExactSpelling=false)]
        public static extern int init(StringBuilder envConfFileName, int screenWidth, int screenHeight);
        //public static extern int init(int screenWidth, int screenHeight);

        [DllImport("WiimoteLogic.dll", SetLastError = true)]
        public static extern Cursor update(RawData data);

        [DllImport("WiimoteLogic.dll", SetLastError = true)]
        public static extern int getError();

        [DllImport("WiimoteLogic.dll", SetLastError = true)]
        public static extern int getProblemDot();

        [DllImport("WiimoteLogic.dll", SetLastError = true)]
        public static extern int getEnvModel();

        [DllImport("WiimoteLogic.dll", SetLastError = true)]
        public static extern int getMapping();

        [DllImport("WiimoteLogic.dll", SetLastError = true)]
        public static extern double getScalingX();

        [DllImport("WiimoteLogic.dll", SetLastError = true)]
        public static extern double getScalingY();

        // tests:

        [DllImport("WiimoteLogic.dll", SetLastError = true)]
        public static extern int dot1x(RawData data);

        [DllImport("WiimoteLogic.dll", SetLastError = true)]
        public static extern int dot1y(RawData data);

        [DllImport("WiimoteLogic.dll", SetLastError = true)]
        public static extern int dot4x(RawData data);

        [DllImport("WiimoteLogic.dll", SetLastError = true)]
        public static extern int dot4y(RawData data);

        [DllImport("WiimoteLogic.dll", SetLastError = true)]
        public static extern Cursor cursor1();

        [DllImport("WiimoteLogic.dll", SetLastError = true)]
        public static extern Cursor cursor2();

        [DllImport("WiimoteLogic.dll", SetLastError = true)]
        public static extern void setNewCursors();

        [DllImport("WiimoteLogic.dll", SetLastError = true)]
        public static extern void throwException();

        [DllImport("WiimoteLogic.dll", SetLastError = true)]
        public static extern int getTestValue();


    }
}
