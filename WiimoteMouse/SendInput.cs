using System;
using System.Runtime.InteropServices;

/// <summary>
/// SendInput allows a program to send keyboard and mouse actions
/// into the Windows input stream.
/// </summary>
public class WindowsInput
{
    public static int MAX_SCREEN_COORDINATE = 65535;

    public enum KEY_TYPE : int
    {
        DIK = 0,
        VK = 1
    }

    public enum KEY_POSITION : ushort
    {
        UP = 0,
        DOWN = 1
    }

    public enum MOUSE_BUTTON : ushort
    {
        LEFT = 0,
        MIDDLE = 1,
        RIGHT = 2
    }

    public enum CURSOR_POSITION : ushort
    {
        ABSOLUTE = 0,
        RELATIVE = 1
    }

    public enum WHEEL_TYPE : int
    {
        HORIZONTAL = 0,
        VERTICAL = 1
    }

    public enum DIK_KEYS : ushort 
    {   
        DIK_ESCAPE          = 0x01,
        DIK_1               = 0x02,
        DIK_2               = 0x03,
        DIK_3               = 0x04,
        DIK_4               = 0x05,
        DIK_5               = 0x06,
        DIK_6               = 0x07,
        DIK_7               = 0x08,
        DIK_8               = 0x09,
        DIK_9               = 0x0A,
        DIK_0               = 0x0B,
        DIK_MINUS           = 0x0C,   /* - on main keyboard */
        DIK_EQUALS          = 0x0D,
        DIK_BACK            = 0x0E,   /* backspace */
        DIK_TAB             = 0x0F,
        DIK_Q               = 0x10,
        DIK_W               = 0x11,
        DIK_E               = 0x12,
        DIK_R               = 0x13,
        DIK_T               = 0x14,
        DIK_Y               = 0x15,
        DIK_U               = 0x16,
        DIK_I               = 0x17,
        DIK_O               = 0x18,
        DIK_P               = 0x19,
        DIK_LBRACKET        = 0x1A,
        DIK_RBRACKET        = 0x1B,
        DIK_RETURN          = 0x1C,    /* Enter on main keyboard */
        DIK_LCONTROL        = 0x1D,
        DIK_A               = 0x1E,
        DIK_S               = 0x1F,
        DIK_D               = 0x20,
        DIK_F               = 0x21,
        DIK_G               = 0x22,
        DIK_H               = 0x23,
        DIK_J               = 0x24,
        DIK_K               = 0x25,
        DIK_L               = 0x26,
        DIK_SEMICOLON       = 0x27,
        DIK_APOSTROPHE      = 0x28,
        DIK_GRAVE           = 0x29,  /* accent grave */
        DIK_LSHIFT          = 0x2A,
        DIK_BACKSLASH       = 0x2B,
        DIK_Z               = 0x2C,
        DIK_X               = 0x2D,
        DIK_C               = 0x2E,
        DIK_V               = 0x2F,
        DIK_B               = 0x30,
        DIK_N               = 0x31,
        DIK_M               = 0x32,
        DIK_COMMA           = 0x33,
        DIK_PERIOD          = 0x34,    /* . on main keyboard */
        DIK_SLASH           = 0x35,    /* / on main keyboard */
        DIK_RSHIFT          = 0x36,
        DIK_MULTIPLY        = 0x37,    /* * on numeric keypad */
        DIK_LMENU           = 0x38,    /* left Alt */
        DIK_SPACE           = 0x39,
        DIK_CAPITAL         = 0x3A,
        DIK_F1              = 0x3B,
        DIK_F2              = 0x3C,
        DIK_F3              = 0x3D,
        DIK_F4              = 0x3E,
        DIK_F5              = 0x3F,
        DIK_F6              = 0x40,
        DIK_F7              = 0x41,
        DIK_F8              = 0x42,
        DIK_F9              = 0x43,
        DIK_F10             = 0x44,
        DIK_NUMLOCK         = 0x45,
        DIK_SCROLL          = 0x46,   /* Scroll Lock */
        DIK_NUMPAD7         = 0x47,
        DIK_NUMPAD8         = 0x48,
        DIK_NUMPAD9         = 0x49,
        DIK_SUBTRACT        = 0x4A,   /* - on numeric keypad */
        DIK_NUMPAD4         = 0x4B,
        DIK_NUMPAD5         = 0x4C,
        DIK_NUMPAD6         = 0x4D,
        DIK_ADD             = 0x4E,   /* + on numeric keypad */
        DIK_NUMPAD1         = 0x4F,
        DIK_NUMPAD2         = 0x50,
        DIK_NUMPAD3         = 0x51,
        DIK_NUMPAD0         = 0x52,
        DIK_DECIMAL         = 0x53,    /* . on numeric keypad */
        DIK_F11             = 0x57,
        DIK_F12             = 0x58,
        DIK_F13             = 0x64,    /* =       (NEC PC98) */
        DIK_F14             = 0x65,    /* =       (NEC PC98) */
        DIK_F15             = 0x66,    /* =       (NEC PC98) */
        DIK_KANA            = 0x70,    /* (Japanese keyboard) =    */
        DIK_CONVERT         = 0x79,    /* (Japanese keyboard) =    */
        DIK_NOCONVERT       = 0x7B,    /* (Japanese keyboard) =    */
        DIK_YEN             = 0x7D,    /* (Japanese keyboard) =    */
        DIK_NUMPADEQUALS    = 0x8D,    /* = on numeric keypad (NEC PC98) */
        DIK_CIRCUMFLEX      = 0x90,    /* (Japanese keyboard) =    */
        DIK_AT              = 0x91,    /* =       (NEC PC98) */
        DIK_COLON           = 0x92,    /* =       (NEC PC98) */
        DIK_UNDERLINE       = 0x93,    /* =       (NEC PC98) */
        DIK_KANJI           = 0x94,    /* (Japanese keyboard) =    */
        DIK_STOP            = 0x95,    /* =       (NEC PC98) */
        DIK_AX              = 0x96,    /* =       (Japan AX) */
        DIK_UNLABELED       = 0x97,    /* = =  (J3100) */
        DIK_NUMPADENTER     = 0x9C,    /* Enter on numeric keypad */
        DIK_RCONTROL        = 0x9D,
        DIK_NUMPADCOMMA     = 0xB3,    /* , on numeric keypad (NEC PC98) */
        DIK_DIVIDE          = 0xB5,    /* / on numeric keypad */
        DIK_SYSRQ           = 0xB7,
        DIK_RMENU           = 0xB8,    /* right Alt */
        DIK_PAUSE           = 0xC5,    /* Pause */
        DIK_HOME            = 0xC7,    /* Home on arrow keypad */
        DIK_UP              = 0xC8,    /* UpArrow on arrow keypad */
        DIK_PRIOR           = 0xC9,    /* PgUp on arrow keypad */
        DIK_LEFT            = 0xCB,    /* LeftArrow on arrow keypad */
        DIK_RIGHT           = 0xCD,    /* RightArrow on arrow keypad */
        DIK_END             = 0xCF,    /* End on arrow keypad */
        DIK_DOWN            = 0xD0,    /* DownArrow on arrow keypad */
        DIK_NEXT            = 0xD1,    /* PgDn on arrow keypad */
        DIK_INSERT          = 0xD2,    /* Insert on arrow keypad */
        DIK_DELETE          = 0xD3,    /* Delete on arrow keypad */
        DIK_LWIN            = 0xDB,    /* Left Windows key */
        DIK_RWIN            = 0xDC,    /* Right Windows key */
        DIK_APPS            = 0xDD,    /* AppMenu key */
        DIK_POWER           = 0xDE,
        DIK_SLEEP           = 0xDF,
        DIK_BACKSPACE       = DIK_BACK,         /* backspace */
        DIK_NUMPADSTAR      = DIK_MULTIPLY,     /* * on numeric keypad */
        DIK_LALT            = DIK_LMENU,        /* left Alt */
        DIK_CAPSLOCK        = DIK_CAPITAL,      /* CapsLock */
        DIK_NUMPADMINUS     = DIK_SUBTRACT,     /* - on numeric keypad */
        DIK_NUMPADPLUS      = DIK_ADD,          /* + on numeric keypad */
        DIK_NUMPADPERIOD    = DIK_DECIMAL,      /* . on numeric keypad */
        DIK_NUMPADSLASH     = DIK_DIVIDE,       /* / on numeric keypad */
        DIK_RALT            = DIK_RMENU,        /* right Alt */
        DIK_UPARROW         = DIK_UP,           /* UpArrow on arrow keypad */
        DIK_PGUP            = DIK_PRIOR,        /* PgUp on arrow keypad */
        DIK_LEFTARROW       = DIK_LEFT,         /* LeftArrow on arrow keypad */
        DIK_RIGHTARROW      = DIK_RIGHT,        /* RightArrow on arrow keypad */
        DIK_DOWNARROW       = DIK_DOWN,         /* DownArrow on arrow keypad */
        DIK_PGDN            = DIK_NEXT          /* PgDn on arrow keypad */
    }



    public class DIKButtonMap 
    {
        public static System.Collections.Hashtable map;

        static DIKButtonMap()
        {
            map = new System.Collections.Hashtable();

            map.Add("ESCAPE", DIK_KEYS.DIK_ESCAPE);
            map.Add("1", DIK_KEYS.DIK_1);
            map.Add("2", DIK_KEYS.DIK_2);
            map.Add("3", DIK_KEYS.DIK_3);
            map.Add("4", DIK_KEYS.DIK_4);
            map.Add("5", DIK_KEYS.DIK_5);
            map.Add("6", DIK_KEYS.DIK_6);
            map.Add("7", DIK_KEYS.DIK_7);
            map.Add("8", DIK_KEYS.DIK_8);
            map.Add("9", DIK_KEYS.DIK_9);
            map.Add("0", DIK_KEYS.DIK_0);
            map.Add("MINUS", DIK_KEYS.DIK_MINUS);
            map.Add("EQUALS", DIK_KEYS.DIK_EQUALS);
            map.Add("BACK", DIK_KEYS.DIK_BACK);
            map.Add("TAB", DIK_KEYS.DIK_TAB);
            map.Add("Q", DIK_KEYS.DIK_Q);
            map.Add("W", DIK_KEYS.DIK_W);
            map.Add("E", DIK_KEYS.DIK_E);
            map.Add("R", DIK_KEYS.DIK_R);
            map.Add("T", DIK_KEYS.DIK_T);
            map.Add("Y", DIK_KEYS.DIK_Y);
            map.Add("U", DIK_KEYS.DIK_U);
            map.Add("I", DIK_KEYS.DIK_I);
            map.Add("O", DIK_KEYS.DIK_O);
            map.Add("P", DIK_KEYS.DIK_P);
            map.Add("LBRACKET", DIK_KEYS.DIK_LBRACKET);
            map.Add("RBRACKET", DIK_KEYS.DIK_RBRACKET);
            map.Add("RETURN", DIK_KEYS.DIK_RETURN);
            map.Add("LCONTROL", DIK_KEYS.DIK_LCONTROL);
            map.Add("CONTROL", DIK_KEYS.DIK_LCONTROL);
            map.Add("CTRL", DIK_KEYS.DIK_LCONTROL);
            map.Add("A", DIK_KEYS.DIK_A);
            map.Add("S", DIK_KEYS.DIK_S);
            map.Add("D", DIK_KEYS.DIK_D);
            map.Add("F", DIK_KEYS.DIK_F);
            map.Add("G", DIK_KEYS.DIK_G);
            map.Add("H", DIK_KEYS.DIK_H);
            map.Add("J", DIK_KEYS.DIK_J);
            map.Add("K", DIK_KEYS.DIK_K);
            map.Add("L", DIK_KEYS.DIK_L);
            map.Add("SEMICOLON", DIK_KEYS.DIK_SEMICOLON);
            map.Add("APOSTROPHE", DIK_KEYS.DIK_APOSTROPHE);
            map.Add("GRAVE", DIK_KEYS.DIK_GRAVE);
            map.Add("LSHIFT", DIK_KEYS.DIK_LSHIFT);
            map.Add("SHIFT", DIK_KEYS.DIK_LSHIFT);
            map.Add("BACKSLASH", DIK_KEYS.DIK_BACKSLASH);
            map.Add("Z", DIK_KEYS.DIK_Z);
            map.Add("X", DIK_KEYS.DIK_X);
            map.Add("C", DIK_KEYS.DIK_C);
            map.Add("V", DIK_KEYS.DIK_V);
            map.Add("B", DIK_KEYS.DIK_B);
            map.Add("N", DIK_KEYS.DIK_N);
            map.Add("M", DIK_KEYS.DIK_M);
            map.Add("COMMA", DIK_KEYS.DIK_COMMA);
            map.Add("PERIOD", DIK_KEYS.DIK_PERIOD);
            map.Add("SLASH", DIK_KEYS.DIK_SLASH);
            map.Add("RSHIFT", DIK_KEYS.DIK_RSHIFT);
            map.Add("MULTIPLY", DIK_KEYS.DIK_MULTIPLY);
            map.Add("LMENU", DIK_KEYS.DIK_LMENU);
            map.Add("SPACE", DIK_KEYS.DIK_SPACE);
            map.Add("CAPITAL", DIK_KEYS.DIK_CAPITAL);
            map.Add("CAPS", DIK_KEYS.DIK_CAPITAL);
            map.Add("F1", DIK_KEYS.DIK_F1);
            map.Add("F2", DIK_KEYS.DIK_F2);
            map.Add("F3", DIK_KEYS.DIK_F3);
            map.Add("F4", DIK_KEYS.DIK_F4);
            map.Add("F5", DIK_KEYS.DIK_F5);
            map.Add("F6", DIK_KEYS.DIK_F6);
            map.Add("F7", DIK_KEYS.DIK_F7);
            map.Add("F8", DIK_KEYS.DIK_F8);
            map.Add("F9", DIK_KEYS.DIK_F9);
            map.Add("F10", DIK_KEYS.DIK_F10);
            map.Add("NUMLOCK", DIK_KEYS.DIK_NUMLOCK);
            map.Add("SCROLL", DIK_KEYS.DIK_SCROLL);
            map.Add("NUMPAD7", DIK_KEYS.DIK_NUMPAD7);
            map.Add("NUMPAD8", DIK_KEYS.DIK_NUMPAD8);
            map.Add("NUMPAD9", DIK_KEYS.DIK_NUMPAD9);
            map.Add("SUBTRACT", DIK_KEYS.DIK_SUBTRACT);
            map.Add("NUMPAD4", DIK_KEYS.DIK_NUMPAD4);
            map.Add("NUMPAD5", DIK_KEYS.DIK_NUMPAD5);
            map.Add("NUMPAD6", DIK_KEYS.DIK_NUMPAD6);
            map.Add("ADD", DIK_KEYS.DIK_ADD);
            map.Add("NUMPAD1", DIK_KEYS.DIK_NUMPAD1);
            map.Add("NUMPAD2", DIK_KEYS.DIK_NUMPAD2);
            map.Add("NUMPAD3", DIK_KEYS.DIK_NUMPAD3);
            map.Add("NUMPAD0", DIK_KEYS.DIK_NUMPAD0);
            map.Add("DECIMAL", DIK_KEYS.DIK_DECIMAL);
            map.Add("F11", DIK_KEYS.DIK_F11);
            map.Add("F12", DIK_KEYS.DIK_F12);
            map.Add("F13", DIK_KEYS.DIK_F13);
            map.Add("F14", DIK_KEYS.DIK_F14);
            map.Add("F15", DIK_KEYS.DIK_F15);
            map.Add("KANA", DIK_KEYS.DIK_KANA);
            map.Add("CONVERT", DIK_KEYS.DIK_CONVERT);
            map.Add("NOCONVERT", DIK_KEYS.DIK_NOCONVERT);
            map.Add("YEN", DIK_KEYS.DIK_YEN);
            map.Add("NUMPADEQUALS", DIK_KEYS.DIK_NUMPADEQUALS);
            map.Add("CIRCUMFLEX", DIK_KEYS.DIK_CIRCUMFLEX);
            map.Add("AT", DIK_KEYS.DIK_AT);
            map.Add("COLON", DIK_KEYS.DIK_COLON);
            map.Add("UNDERLINE", DIK_KEYS.DIK_UNDERLINE);
            map.Add("KANJI", DIK_KEYS.DIK_KANJI);
            map.Add("STOP", DIK_KEYS.DIK_STOP);
            map.Add("AX", DIK_KEYS.DIK_AX);
            map.Add("UNLABELED", DIK_KEYS.DIK_UNLABELED);
            map.Add("NUMPADENTER", DIK_KEYS.DIK_NUMPADENTER);
            map.Add("RCONTROL", DIK_KEYS.DIK_RCONTROL);
            map.Add("NUMPADCOMMA", DIK_KEYS.DIK_NUMPADCOMMA);
            map.Add("DIVIDE", DIK_KEYS.DIK_DIVIDE);
            map.Add("SYSRQ", DIK_KEYS.DIK_SYSRQ);
            map.Add("RMENU", DIK_KEYS.DIK_RMENU);
            map.Add("PAUSE", DIK_KEYS.DIK_PAUSE);
            map.Add("HOME", DIK_KEYS.DIK_HOME);
            map.Add("UP", DIK_KEYS.DIK_UP);
            map.Add("PRIOR", DIK_KEYS.DIK_PRIOR);
            map.Add("LEFT", DIK_KEYS.DIK_LEFT);
            map.Add("RIGHT", DIK_KEYS.DIK_RIGHT);
            map.Add("END", DIK_KEYS.DIK_END);
            map.Add("DOWN", DIK_KEYS.DIK_DOWN);
            map.Add("NEXT", DIK_KEYS.DIK_NEXT);
            map.Add("INSERT", DIK_KEYS.DIK_INSERT);
            map.Add("DELETE", DIK_KEYS.DIK_DELETE);
            map.Add("DEL", DIK_KEYS.DIK_DELETE);
            map.Add("LWIN", DIK_KEYS.DIK_LWIN);
            map.Add("WIN", DIK_KEYS.DIK_LWIN);
            map.Add("WINDOWS", DIK_KEYS.DIK_LWIN);
            map.Add("RWIN", DIK_KEYS.DIK_RWIN);
            map.Add("APPS", DIK_KEYS.DIK_APPS);
            map.Add("POWER", DIK_KEYS.DIK_POWER);
            map.Add("SLEEP", DIK_KEYS.DIK_SLEEP);
            map.Add("BACKSPACE", DIK_KEYS.DIK_BACKSPACE);
            map.Add("NUMPADSTAR", DIK_KEYS.DIK_NUMPADSTAR);
            map.Add("LALT", DIK_KEYS.DIK_LALT);
            map.Add("ALT", DIK_KEYS.DIK_LALT);
            map.Add("CAPSLOCK", DIK_KEYS.DIK_CAPSLOCK);
            map.Add("NUMPADMINUS", DIK_KEYS.DIK_NUMPADMINUS);
            map.Add("NUMPADPLUS", DIK_KEYS.DIK_NUMPADPLUS);
            map.Add("NUMPADPERIOD", DIK_KEYS.DIK_NUMPADPERIOD);
            map.Add("NUMPADSLASH", DIK_KEYS.DIK_NUMPADSLASH);
            map.Add("RALT", DIK_KEYS.DIK_RALT);
            map.Add("UPARROW", DIK_KEYS.DIK_UPARROW);
            map.Add("PGUP", DIK_KEYS.DIK_PGUP);
            map.Add("LEFTARROW", DIK_KEYS.DIK_LEFTARROW);
            map.Add("RIGHTARROW", DIK_KEYS.DIK_RIGHTARROW);
            map.Add("DOWNARROW", DIK_KEYS.DIK_DOWNARROW);
            map.Add("PGDN", DIK_KEYS.DIK_PGDN);
        }
    }

    public enum VK_KEYS : ushort
    {
        /* Virtual key codes */
        VK_LBUTTON = 0x01,
        VK_RBUTTON = 0x02,
        VK_CANCEL = 0x03,
        VK_MBUTTON = 0x04,
        /* 0x05-0x07    Undefined */
        VK_BACK = 0x08,
        VK_TAB = 0x09,
        /* 0x0A-0x0B    Undefined */
        VK_CLEAR = 0x0C,
        VK_RETURN = 0x0D,
        /* 0x0E-0x0F    Undefined */
        VK_SHIFT = 0x10,
        VK_CONTROL = 0x11,
        VK_MENU = 0x12,
        VK_PAUSE = 0x13,
        VK_CAPITAL = 0x14,
        /* 0x15-0x19    Reserved for Kanji systems */
        /* 0x1A         Undefined */
        VK_ESCAPE = 0x1B,
        /* 0x1C-0x1F    Reserved for Kanji systems */
        VK_SPACE = 0x20,
        VK_PRIOR = 0x21,
        VK_NEXT = 0x22,
        VK_END = 0x23,
        VK_HOME = 0x24,
        VK_LEFT = 0x25,
        VK_UP = 0x26,
        VK_RIGHT = 0x27,
        VK_DOWN = 0x28,
        VK_SELECT = 0x29,
        VK_PRINT = 0x2A, /* OEM specific in Windows 3.1 SDK */
        VK_EXECUTE = 0x2B,
        VK_SNAPSHOT = 0x2C,
        VK_INSERT = 0x2D,
        VK_DELETE = 0x2E,
        VK_HELP = 0x2F,
        VK_0 = 0x30,
        VK_1 = 0x31,
        VK_2 = 0x32,
        VK_3 = 0x33,
        VK_4 = 0x34,
        VK_5 = 0x35,
        VK_6 = 0x36,
        VK_7 = 0x37,
        VK_8 = 0x38,
        VK_9 = 0x39,
        /* 0x3A-0x40  Undefined */
        VK_A = 0x41,
        VK_B = 0x42,
        VK_C = 0x43,
        VK_D = 0x44,
        VK_E = 0x45,
        VK_F = 0x46,
        VK_G = 0x47,
        VK_H = 0x48,
        VK_I = 0x49,
        VK_J = 0x4A,
        VK_K = 0x4B,
        VK_L = 0x4C,
        VK_M = 0x4D,
        VK_N = 0x4E,
        VK_O = 0x4F,
        VK_P = 0x50,
        VK_Q = 0x51,
        VK_R = 0x52,
        VK_S = 0x53,
        VK_T = 0x54,
        VK_U = 0x55,
        VK_V = 0x56,
        VK_W = 0x57,
        VK_X = 0x58,
        VK_Y = 0x59,
        VK_Z = 0x5A,

        VK_LWIN = 0x5B,
        VK_RWIN = 0x5C,
        VK_APPS = 0x5D,
        /* 0x5E-0x5F Unassigned */
        VK_NUMPAD0 = 0x60,
        VK_NUMPAD1 = 0x61,
        VK_NUMPAD2 = 0x62,
        VK_NUMPAD3 = 0x63,
        VK_NUMPAD4 = 0x64,
        VK_NUMPAD5 = 0x65,
        VK_NUMPAD6 = 0x66,
        VK_NUMPAD7 = 0x67,
        VK_NUMPAD8 = 0x68,
        VK_NUMPAD9 = 0x69,
        VK_MULTIPLY = 0x6A,
        VK_ADD = 0x6B,
        VK_SEPARATOR = 0x6C,
        VK_SUBTRACT = 0x6D,
        VK_DECIMAL = 0x6E,
        VK_DIVIDE = 0x6F,
        VK_F1 = 0x70,
        VK_F2 = 0x71,
        VK_F3 = 0x72,
        VK_F4 = 0x73,
        VK_F5 = 0x74,
        VK_F6 = 0x75,
        VK_F7 = 0x76,
        VK_F8 = 0x77,
        VK_F9 = 0x78,
        VK_F10 = 0x79,
        VK_F11 = 0x7A,
        VK_F12 = 0x7B,
        VK_F13 = 0x7C,
        VK_F14 = 0x7D,
        VK_F15 = 0x7E,
        VK_F16 = 0x7F,
        VK_F17 = 0x80,
        VK_F18 = 0x81,
        VK_F19 = 0x82,
        VK_F20 = 0x83,
        VK_F21 = 0x84,
        VK_F22 = 0x85,
        VK_F23 = 0x86,
        VK_F24 = 0x87,
        /* 0x88-0x8F    Unassigned */
        VK_NUMLOCK = 0x90,
        VK_SCROLL = 0x91,
        /* 0x92-0x9F    Unassigned */
        /*
         * differencing between right and left shift/control/alt key.
         * Used only by GetAsyncKeyState() and GetKeyState().
         */
        VK_LSHIFT = 0xA0,
        VK_RSHIFT = 0xA1,
        VK_LCONTROL = 0xA2,
        VK_RCONTROL = 0xA3,
        VK_LMENU = 0xA4,
        VK_RMENU = 0xA5,
        /* 0xA6-0xB9    Unassigned */
        VK_OEM_1 = 0xBA,
        VK_OEM_PLUS = 0xBB,
        VK_OEM_COMMA = 0xBC,
        VK_OEM_MINUS = 0xBD,
        VK_OEM_PERIOD = 0xBE,
        VK_OEM_2 = 0xBF,
        VK_OEM_3 = 0xC0,
        /* 0xC1-0xDA    Unassigned */
        VK_OEM_4 = 0xDB,
        VK_OEM_5 = 0xDC,
        VK_OEM_6 = 0xDD,
        VK_OEM_7 = 0xDE,
        /* 0xDF-0xE4    OEM specific */

        VK_PROCESSKEY = 0xE5,

        /* 0xE6         OEM specific */
        /* 0xE7-0xE8    Unassigned */
        /* 0xE9-0xF5    OEM specific */
        VK_ATTN = 0xF6,
        VK_CRSEL = 0xF7,
        VK_EXSEL = 0xF8,
        VK_EREOF = 0xF9,
        VK_PLAY = 0xFA,
        VK_ZOOM = 0xFB,
        VK_NONAME = 0xFC,
        VK_PA1 = 0xFD,
        VK_OEM_CLEAR = 0xFE
    }


    public enum MOUSE_FLAGS : int
    {
        MOUSEEVENTF_MOVE = 0x0001,
        MOUSEEVENTF_LEFTDOWN = 0x0002,
        MOUSEEVENTF_LEFTUP = 0x0004,
        MOUSEEVENTF_RIGHTDOWN = 0x0008,
        MOUSEEVENTF_RIGHTUP = 0x0010,
        MOUSEEVENTF_MIDDLEDOWN = 0x0020,
        MOUSEEVENTF_MIDDLEUP = 0x0040,
        MOUSEEVENTF_ABSOLUTE = 0x8000,
        MOUSEEVENTF_WHEEL = 0x0800,
        MOUSEEVENTF_HWHEEL = 0x1000
    }

    public enum KEY_FLAGS : int 
    {
        KEYEVENTF_EXTENDEDKEY = 0x0001,
        KEYEVENTF_KEYUP = 0x0002,
        KEYEVENTF_UNICODE = 0x0004,
        KEYEVENTF_SCANCODE = 0x0008,
        KEYEVENTF_WINE_FORCEEXTENDED = 0x8000
    }

    public const int INPUT_MOUSE = 0;
    public const int INPUT_KEYBOARD = 1;
    public const int INPUT_HARDWARE = 2;

    public const int WHEEL_DELTA = 120;

    // SendInput structures
    [StructLayout(LayoutKind.Sequential)]
    public struct MOUSEINPUT
    {
        public int dx;
        public int dy;
        public int mouseData;
        public int dwFlags;
        public int time;
        public IntPtr dwExtraInfo;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct KEYBDINPUT
    {
        public short wVk;
        public short wScan;
        public int dwFlags;
        public int time;
        public IntPtr dwExtraInfo;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct HARDWAREINPUT
    {
        public int uMsg;
        public short wParamL;
        public short wParamH;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct INPUT
    {
        [FieldOffset(0)]
        public int type;
        [FieldOffset(4)]
        public MOUSEINPUT mi;
        [FieldOffset(4)]
        public KEYBDINPUT ki;
        [FieldOffset(4)]
        HARDWAREINPUT hi;
    }

    // Import SendInput
    [DllImport("user32.dll", SetLastError = true)]
    public static extern int SendInput(int nInputs, ref INPUT pInputs, int cbSize);

    [DllImport("user32.dll", SetLastError = true)]
    public static extern IntPtr GetMessageExtraInfo();

    public static int sendKey(KEY_TYPE type, ushort key, KEY_POSITION pos)
    {
        WindowsInput.INPUT keyInput = new WindowsInput.INPUT();
        keyInput.type = WindowsInput.INPUT_KEYBOARD;

        if (type == KEY_TYPE.VK)
        {
            keyInput.ki.wVk = (short) key;
            keyInput.ki.wScan = 0;
            keyInput.ki.dwFlags = 0;
        }
        else if(type == KEY_TYPE.DIK)
        {
            keyInput.ki.wVk = 0;
            keyInput.ki.wScan = (short) key;
            keyInput.ki.dwFlags = (int)WindowsInput.KEY_FLAGS.KEYEVENTF_SCANCODE;
        }
        if (pos == KEY_POSITION.UP)
            keyInput.ki.dwFlags |= (int)WindowsInput.KEY_FLAGS.KEYEVENTF_KEYUP;

        keyInput.ki.time = 0;
        keyInput.ki.dwExtraInfo = WindowsInput.GetMessageExtraInfo();

        int error = WindowsInput.SendInput(1, ref keyInput, Marshal.SizeOf(keyInput));
        return error;
    }

    public static int sendKey(KEY_TYPE type, ushort[] keys, KEY_POSITION pos)
    {
        int errors = 0;
        for (int i = 0; i < keys.Length; i++)
        {
            errors += sendKey(type, keys[i], pos);
        }
        return errors;
    }

    public static int sendMouseClick(KEY_POSITION pos, MOUSE_BUTTON button)
    {
        WindowsInput.INPUT input = new WindowsInput.INPUT();
        input.type = WindowsInput.INPUT_MOUSE;

        input.mi.mouseData = 0;
        input.mi.dx = 0;
        input.mi.dy = 0;
        input.mi.dwFlags = 0;
        input.mi.time = 0;
        input.mi.dwExtraInfo = WindowsInput.GetMessageExtraInfo();

        if (pos == KEY_POSITION.UP && button == MOUSE_BUTTON.LEFT)
            input.mi.dwFlags |= (int)WindowsInput.MOUSE_FLAGS.MOUSEEVENTF_LEFTUP;
        else if (pos == KEY_POSITION.DOWN && button == MOUSE_BUTTON.LEFT)
            input.mi.dwFlags |= (int)WindowsInput.MOUSE_FLAGS.MOUSEEVENTF_LEFTDOWN;
        else if (pos == KEY_POSITION.UP && button == MOUSE_BUTTON.RIGHT)
            input.mi.dwFlags |= (int)WindowsInput.MOUSE_FLAGS.MOUSEEVENTF_RIGHTUP;
        else if (pos == KEY_POSITION.DOWN && button == MOUSE_BUTTON.RIGHT)
            input.mi.dwFlags |= (int)WindowsInput.MOUSE_FLAGS.MOUSEEVENTF_RIGHTDOWN;
        else if (pos == KEY_POSITION.UP && button == MOUSE_BUTTON.MIDDLE)
            input.mi.dwFlags |= (int)WindowsInput.MOUSE_FLAGS.MOUSEEVENTF_MIDDLEUP;
        else if (pos == KEY_POSITION.DOWN && button == MOUSE_BUTTON.MIDDLE)
            input.mi.dwFlags |= (int)WindowsInput.MOUSE_FLAGS.MOUSEEVENTF_MIDDLEDOWN;

        return WindowsInput.SendInput(1, ref input, System.Runtime.InteropServices.Marshal.SizeOf(input));
    }

    public static int sendMousePosition(int x, int y, CURSOR_POSITION pos)
    {
        INPUT input = new INPUT();
        input.type = INPUT_MOUSE;
        input.mi.dx = x;
        input.mi.dy = y;
        input.mi.mouseData = 0;
        input.mi.dwFlags = (int)MOUSE_FLAGS.MOUSEEVENTF_MOVE;
        if (pos == CURSOR_POSITION.ABSOLUTE)
            input.mi.dwFlags |= (int)(MOUSE_FLAGS.MOUSEEVENTF_ABSOLUTE);
        input.mi.time = 0;
        input.mi.dwExtraInfo = GetMessageExtraInfo();

        int result = SendInput(1, ref input, System.Runtime.InteropServices.Marshal.SizeOf(input));
        return result;
    }

    public static int sendWheelScroll(int increments, WHEEL_TYPE orientation)
    {   
        WindowsInput.INPUT input = new WindowsInput.INPUT();
        input.type = WindowsInput.INPUT_MOUSE;

        input.mi.mouseData = increments * WHEEL_DELTA;
        input.mi.dx = 0;
        input.mi.dy = 0;
        if (orientation == WHEEL_TYPE.VERTICAL)
            input.mi.dwFlags = (int)WindowsInput.MOUSE_FLAGS.MOUSEEVENTF_WHEEL;
        else if (orientation == WHEEL_TYPE.HORIZONTAL)
            input.mi.dwFlags = (int)WindowsInput.MOUSE_FLAGS.MOUSEEVENTF_HWHEEL;
        input.mi.time = 0;
        input.mi.dwExtraInfo = WindowsInput.GetMessageExtraInfo();

        int error = WindowsInput.SendInput(1, ref input, System.Runtime.InteropServices.Marshal.SizeOf(input));
        return error;
    }
}
