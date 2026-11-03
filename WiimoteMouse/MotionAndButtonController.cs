using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Timers;

namespace WiimoteMouse
{
    public enum PRESS_TYPE : int
    {
        NORMAL = 0,
        SINGLE_TAP = 1,
        DOUBLE_TAP = 2
    }

    public enum AXIS : int
    {
        X = 0,
        Y = 1,
        Z = 2,
        //X_ROTATION = 3,
        //Y_ROTATION = 4,
        //Z_ROTATION = 5
    }

    public class AxisMap
    {
        public static System.Collections.Hashtable map;

        static AxisMap()
        {
            map = new System.Collections.Hashtable();
            map.Add("X", AXIS.X);
            map.Add("SIDEWAYS", AXIS.X);
            map.Add("Y", AXIS.Y);
            map.Add("FORWARDS", AXIS.Y);
            map.Add("Z", AXIS.Z);
            map.Add("UPWARDS", AXIS.Z);
            map.Add("VERTICALLY", AXIS.Z);
            //map.Add("X_ROTATION", AXIS.X_ROTATION);
            //map.Add("PITCH", AXIS.X_ROTATION);
            //map.Add("Y_ROTATION", AXIS.Y_ROTATION);
            //map.Add("ROLL", AXIS.Y_ROTATION);
            //map.Add("Z_ROTATION", AXIS.Z_ROTATION);
            //map.Add("YAW", AXIS.Z_ROTATION);
        }
    }

    public enum DEVICE : int
    {
        WIIMOTE = 0,
        NUNCHUK = 1
    }

    public class DeviceMap
    {
        public static System.Collections.Hashtable map;

        static DeviceMap()
        {
            map = new System.Collections.Hashtable();
            map.Add("WIIMOTE", DEVICE.WIIMOTE);
            map.Add("WII", DEVICE.WIIMOTE);
            map.Add("NUNCHUK", DEVICE.NUNCHUK);
            map.Add("NUNCHUCK", DEVICE.NUNCHUK);
        }
    }

    public class MouseClickMap
    {
        public static System.Collections.Hashtable map;

        static MouseClickMap()
        {
            map = new System.Collections.Hashtable();
            map.Add("NORMAL", PRESS_TYPE.NORMAL);
            map.Add("SINGLE", PRESS_TYPE.SINGLE_TAP);
            map.Add("SINGLE_TAP", PRESS_TYPE.SINGLE_TAP);
            map.Add("SINGLE_CLICK", PRESS_TYPE.SINGLE_TAP);
            map.Add("DOUBLE", PRESS_TYPE.DOUBLE_TAP);
            map.Add("DOUBLE_TAP", PRESS_TYPE.DOUBLE_TAP);
            map.Add("DOUBLE_CLICK", PRESS_TYPE.DOUBLE_TAP);
        }
    }

    public class MouseButton
    {
        public static System.Collections.Hashtable map;

        static MouseButton()
        {
            map = new System.Collections.Hashtable();
            map.Add("LEFT", WindowsInput.MOUSE_BUTTON.LEFT);
            map.Add("RIGHT", WindowsInput.MOUSE_BUTTON.RIGHT);
            map.Add("MIDDLE", WindowsInput.MOUSE_BUTTON.MIDDLE);
        }
    }

    public class WheelType
    {
        public static System.Collections.Hashtable map;

        static WheelType()
        {
            map = new System.Collections.Hashtable();
            map.Add("VERTICAL", WindowsInput.WHEEL_TYPE.VERTICAL);
            map.Add("VERT", WindowsInput.WHEEL_TYPE.VERTICAL);
            map.Add("HORIZONTAL", WindowsInput.WHEEL_TYPE.HORIZONTAL);
            map.Add("HOR", WindowsInput.WHEEL_TYPE.HORIZONTAL);
        }
    }

    public enum WII_BUTTONS : ushort
    {
        A = 0,
		B = 1,
		PLUS = 2,
		HOME = 3,
		MINUS = 4,
		ONE = 5,
		TWO = 6,
		UP = 7,
		DOWN = 8,
		LEFT = 9,
		RIGHT = 10
    }

    public enum NUNCHUCK_BUTTONS : ushort
    {
        X_POS = 11,
        X_NEG = 12,
        Y_POS = 13,
        Y_NEG = 14,
        C = 15,
        Z = 16
    }

    public class WiiButtonMap
    {
        public static System.Collections.Hashtable map;

        static WiiButtonMap()
        {
            map = new System.Collections.Hashtable();

            map.Add("A", WII_BUTTONS.A);
            map.Add("B", WII_BUTTONS.B);
            map.Add("PLUS", WII_BUTTONS.PLUS);
            map.Add("+", WII_BUTTONS.PLUS);
            map.Add("HOME", WII_BUTTONS.HOME);
            map.Add("MINUS", WII_BUTTONS.MINUS);
            map.Add("-", WII_BUTTONS.MINUS);
            map.Add("ONE", WII_BUTTONS.ONE);
            map.Add("1", WII_BUTTONS.ONE);
            map.Add("TWO", WII_BUTTONS.TWO);
            map.Add("2", WII_BUTTONS.TWO);
            map.Add("UP", WII_BUTTONS.UP);
            map.Add("DOWN", WII_BUTTONS.DOWN);
            map.Add("LEFT", WII_BUTTONS.LEFT);
            map.Add("RIGHT", WII_BUTTONS.RIGHT);

            map.Add("X", NUNCHUCK_BUTTONS.X_POS);
            map.Add("X_POS", NUNCHUCK_BUTTONS.X_POS);
            map.Add("POS_X", NUNCHUCK_BUTTONS.X_POS);
            map.Add("+X", NUNCHUCK_BUTTONS.X_POS);

            map.Add("X_NEG", NUNCHUCK_BUTTONS.X_NEG);
            map.Add("NEG_X", NUNCHUCK_BUTTONS.X_NEG);
            map.Add("-X", NUNCHUCK_BUTTONS.X_NEG);

            map.Add("Y", NUNCHUCK_BUTTONS.Y_POS); 
            map.Add("Y_POS", NUNCHUCK_BUTTONS.Y_POS);
            map.Add("POS_Y", NUNCHUCK_BUTTONS.Y_POS);
            map.Add("+Y", NUNCHUCK_BUTTONS.Y_POS);

            map.Add("Y_NEG", NUNCHUCK_BUTTONS.Y_NEG);
            map.Add("NEG_Y", NUNCHUCK_BUTTONS.Y_NEG);
            map.Add("-Y", NUNCHUCK_BUTTONS.Y_NEG);

            map.Add("C", NUNCHUCK_BUTTONS.C);
            map.Add("Z", NUNCHUCK_BUTTONS.Z);
        }
    }

    public class MotionAndButtonController
    {
        WiimoteLib.ButtonState previousWiiState;
        WiimoteLib.NunchukState previousNunchukState;
        System.Collections.Hashtable buttonMap;
        System.Collections.ArrayList motionWatchers;

        // the parameters indicies, based on line position 
        public static int WII_ACTION = 0;
        public static int WII_KEY = 1;
        public static int WII_KEY_THRESHOLD = 2;

        public static int ACTION = 0;
        public static int MOTION_DEVICE = 1;
        public static int MOTION_AXIS = 2;
        public static int KEY_PRESS_LENGTH = 1;
        public static int MOUSE_BUTTON = 1;
        public static int MOUSE_CLICK_TYPE = 2;
        public static int WHEEL_TYPE = 1;
        public static int WHEEL_INCREMENTS = 2;

        public MotionAndButtonController(String confFile, MainForm form)
        {
            String line = null;
            int lineNumber = 0;

            try
            {
                FileStream file = new FileStream(confFile, FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(file);
                buttonMap = new System.Collections.Hashtable();
                motionWatchers = new System.Collections.ArrayList();

                char[] delimeters = new char[1];
                delimeters[0] = ' ';

                while (!sr.EndOfStream)
                {
                    String[] parameters;
                    MotionWatcher motion = null;
                    ushort wiiKey = 0;
                    float wiiKeyThreshold = 0.0F;
                    bool set = false;

                    line = sr.ReadLine().Trim().ToUpper();
                    lineNumber++;
                    parameters = line.Split(delimeters, StringSplitOptions.RemoveEmptyEntries);

                    if (parameters.Length > 0 && parameters[WII_ACTION].Equals("WII-KEY"))
                    {
                        wiiKey = (ushort)WiiButtonMap.map[parameters[WII_KEY]];
                        if (
                                wiiKey == (ushort)NUNCHUCK_BUTTONS.X_POS || 
                                wiiKey == (ushort)NUNCHUCK_BUTTONS.X_NEG ||
                                wiiKey == (ushort)NUNCHUCK_BUTTONS.Y_POS ||
                                wiiKey == (ushort)NUNCHUCK_BUTTONS.Y_NEG)
                        {
                            wiiKeyThreshold = System.Convert.ToSingle(parameters[WII_KEY_THRESHOLD]);
                            if (wiiKeyThreshold < 0)
                            {
                                if (wiiKey == (ushort)NUNCHUCK_BUTTONS.X_POS)
                                    wiiKey = (ushort)NUNCHUCK_BUTTONS.X_NEG;
                                else if (wiiKey == (ushort)NUNCHUCK_BUTTONS.X_NEG)
                                    wiiKey = (ushort)NUNCHUCK_BUTTONS.X_POS;
                                else if (wiiKey == (ushort)NUNCHUCK_BUTTONS.Y_POS)
                                    wiiKey = (ushort)NUNCHUCK_BUTTONS.Y_NEG;
                                else if (wiiKey == (ushort)NUNCHUCK_BUTTONS.Y_NEG)
                                    wiiKey = (ushort)NUNCHUCK_BUTTONS.Y_POS;
                            }
                        }
                        set = true;
                    }
                    else if (parameters.Length > 0 && parameters[WII_ACTION].Equals("FLICK-MOTION"))
                    {
                        DEVICE device = (DEVICE)DeviceMap.map[parameters[MOTION_DEVICE]];
                        AXIS axis = (AXIS)AxisMap.map[parameters[MOTION_AXIS]];
                        float motionThreshold = System.Convert.ToSingle(parameters[3]);
                        motion = new FlickMotionWatcher(motionThreshold, device, axis);
                        set = true;
                    }
                    else if (parameters.Length > 0 && parameters[WII_ACTION].Equals("LONG-MOTION"))
                    {
                        DEVICE device = (DEVICE)DeviceMap.map[parameters[MOTION_DEVICE]];
                        AXIS axis = (AXIS)AxisMap.map[parameters[MOTION_AXIS]];
                        float motionThreshold = System.Convert.ToSingle(parameters[3]);
                        int durationMillis = System.Convert.ToInt32(parameters[4]);
                        bool once = false;
                        if (parameters.Length > 5)
                            if (parameters[5].Equals("ONCE"))
                                once = true;
                        motion = new LongMotionWatcher(motionThreshold, durationMillis, device, axis, once);
                        set = true;
                    }

                    if (set)
                    {
                        InputAction action = null;
                        line = sr.ReadLine().Trim().ToUpper();
                        lineNumber++;
                        parameters = line.Split(delimeters, StringSplitOptions.RemoveEmptyEntries);

                        if (parameters[ACTION].Equals("KEY-PRESS"))
                        {
                            ushort[] keyCodes = new ushort[parameters.Length - 1];
                            for (int i = 1; i < parameters.Length; i++)
                            {
                                keyCodes[i - 1] = (ushort)WindowsInput.DIKButtonMap.map[parameters[i]];
                            }
                            action = new KeyStrokeAction(wiiKeyThreshold, keyCodes);
                        }
                        else if (parameters[ACTION].Equals("LONG-KEY-PRESS"))
                        {
                            int length = System.Convert.ToInt32(parameters[KEY_PRESS_LENGTH]);
                            if (length <= 0)
                                throw new UnexpectedDataFormatException("Length must be greater than 0");
                            ushort[] keyCodes = new ushort[parameters.Length - 2];
                            for (int i = 2; i < parameters.Length; i++)
                            {
                                keyCodes[i - 2] = (ushort)WindowsInput.DIKButtonMap.map[parameters[i]];
                            }
                            action = new LongKeyStrokeAction(wiiKeyThreshold, keyCodes, length);
                        }
                        else if (parameters[ACTION].Equals("MOUSE-CLICK"))
                        {
                            WindowsInput.MOUSE_BUTTON button = (WindowsInput.MOUSE_BUTTON)MouseButton.map[parameters[MOUSE_BUTTON]];
                            PRESS_TYPE type = (PRESS_TYPE)MouseClickMap.map[parameters[MOUSE_CLICK_TYPE]];
                            action = new MouseAction(wiiKeyThreshold, button, type);
                        }
                        else if (parameters[ACTION].Equals("MOUSE-WHEEL"))
                        {
                            WindowsInput.WHEEL_TYPE type = (WindowsInput.WHEEL_TYPE)WheelType.map[parameters[WHEEL_TYPE]];
                            int increments = System.Convert.ToInt32(parameters[WHEEL_INCREMENTS]);
                            action = new RotateWheelAction(wiiKeyThreshold, type, increments);
                        }
                        else if (parameters[ACTION].Equals("RECENTER-IR") || parameters[ACTION].Equals("RECENTRE-IR"))
                        {
                            action = new ResetIRAction(wiiKeyThreshold, form);
                        }
                        else
                        {
                            throw new UnexpectedDataFormatException("Expecting an action on line " + lineNumber);
                        }

                        // create action and add to map
                        if (motion != null)
                        {
                            motion.setAction(action);
                            motionWatchers.Add(motion);
                        }
                        else
                        {
                            buttonMap.Add(wiiKey, action);
                        }
                    }
                }
            }
            catch (UnexpectedDataFormatException e)
            {
                throw e;
            }
            catch (System.ArgumentException e)
            {
                throw new Exception("The wii-key on line " + lineNumber + " has already been specified", e);
            }
            catch (Exception e)
            {
                if (line == null)
                    throw new Exception("Unable to read the file:\r" + confFile, e);
                else
                    throw new Exception("Unable to understand line " + lineNumber + ":\r" + line, e);
            }

            if (motionWatchers.Count == 0 && buttonMap.Count == 0)
            {
                throw new Exception("File contained no configuration information");
            }
        }

        public void updateMotion(WiimoteLib.AccelState wiiAccel, WiimoteLib.AccelState nunAccel)
        {
            for (int i = 0; i < motionWatchers.Count; i++)
            {
                ((MotionWatcher)motionWatchers[i]).asses(wiiAccel, nunAccel);
            }
        }
        
        public void updateButtons(WiimoteLib.NunchukState newState)
        {
            if (newState.X != previousNunchukState.X && buttonMap.ContainsKey((ushort)NUNCHUCK_BUTTONS.X_POS))
                ((InputAction)buttonMap[(ushort)NUNCHUCK_BUTTONS.X_POS]).perform(newState.X);

            if (newState.X != previousNunchukState.X && buttonMap.ContainsKey((ushort)NUNCHUCK_BUTTONS.X_NEG))
                ((InputAction)buttonMap[(ushort)NUNCHUCK_BUTTONS.X_NEG]).perform(newState.X);

            if (newState.Y != previousNunchukState.Y && buttonMap.ContainsKey((ushort)NUNCHUCK_BUTTONS.Y_POS))
                ((InputAction)buttonMap[(ushort)NUNCHUCK_BUTTONS.Y_POS]).perform(newState.Y);

            if (newState.Y != previousNunchukState.Y && buttonMap.ContainsKey((ushort)NUNCHUCK_BUTTONS.Y_NEG))
                ((InputAction)buttonMap[(ushort)NUNCHUCK_BUTTONS.Y_NEG]).perform(newState.Y);

            if (newState.C != previousNunchukState.C && buttonMap.ContainsKey((ushort)NUNCHUCK_BUTTONS.C))
                ((InputAction)buttonMap[(ushort)NUNCHUCK_BUTTONS.C]).perform(newState.C);

            if (newState.Z != previousNunchukState.Z && buttonMap.ContainsKey((ushort)NUNCHUCK_BUTTONS.Z))
                ((InputAction)buttonMap[(ushort)NUNCHUCK_BUTTONS.Z]).perform(newState.Z);

            previousNunchukState = newState;
        }

        public void updateButtons(WiimoteLib.ButtonState newState)
        {
            if (newState.A != previousWiiState.A && buttonMap.ContainsKey((ushort)WII_BUTTONS.A))
                ((InputAction)buttonMap[(ushort)WII_BUTTONS.A]).perform(newState.A);

            if (newState.B != previousWiiState.B && buttonMap.ContainsKey((ushort)WII_BUTTONS.B))
                ((InputAction)buttonMap[(ushort)WII_BUTTONS.B]).perform(newState.B);

            if (newState.Plus != previousWiiState.Plus && buttonMap.ContainsKey((ushort)WII_BUTTONS.PLUS))
                ((InputAction)buttonMap[(ushort)WII_BUTTONS.PLUS]).perform(newState.Plus);

            if (newState.Home != previousWiiState.Home && buttonMap.ContainsKey((ushort)WII_BUTTONS.HOME))
                ((InputAction)buttonMap[(ushort)WII_BUTTONS.HOME]).perform(newState.Home);

            if (newState.Minus != previousWiiState.Minus && buttonMap.ContainsKey((ushort)WII_BUTTONS.MINUS))
                ((InputAction)buttonMap[(ushort)WII_BUTTONS.MINUS]).perform(newState.Minus);

            if (newState.One != previousWiiState.One && buttonMap.ContainsKey((ushort)WII_BUTTONS.ONE))
                ((InputAction)buttonMap[(ushort)WII_BUTTONS.ONE]).perform(newState.One);

            if (newState.Two != previousWiiState.Two && buttonMap.ContainsKey((ushort)WII_BUTTONS.TWO))
                ((InputAction)buttonMap[(ushort)WII_BUTTONS.TWO]).perform(newState.Two);

            if(newState.Up != previousWiiState.Up && buttonMap.ContainsKey((ushort) WII_BUTTONS.UP))
                ((InputAction)buttonMap[(ushort)WII_BUTTONS.UP]).perform(newState.Up);

            if (newState.Down != previousWiiState.Down && buttonMap.ContainsKey((ushort)WII_BUTTONS.DOWN))
                ((InputAction)buttonMap[(ushort)WII_BUTTONS.DOWN]).perform(newState.Down);

            if (newState.Left != previousWiiState.Left && buttonMap.ContainsKey((ushort)WII_BUTTONS.LEFT))
                ((InputAction)buttonMap[(ushort)WII_BUTTONS.LEFT]).perform(newState.Left);

            if (newState.Right != previousWiiState.Right && buttonMap.ContainsKey((ushort)WII_BUTTONS.RIGHT))
                ((InputAction)buttonMap[(ushort)WII_BUTTONS.RIGHT]).perform(newState.Right);

            previousWiiState = newState;
        }
    }

    // EXCEPTION CLASSES

    public class UnexpectedDataFormatException : Exception
    {
        public UnexpectedDataFormatException(String message)
            : base(message)
        {
        }
    }

    // MOTION CLASSES:

    public abstract class MotionWatcher
    {
        protected InputAction action;

        public void setAction(InputAction action)
        {
            this.action = action;
        }

        public abstract void asses(WiimoteLib.AccelState wiiAccel, WiimoteLib.AccelState nunAccel);
    }

    public class FlickMotionWatcher : MotionWatcher
    {
        DEVICE device;
        AXIS axis;
        float threshold;
        float previousAxisValue = 0.0F;
        bool negative = false;
        bool previousState = false;

        public FlickMotionWatcher(float threshold, DEVICE device, AXIS axis)
        {
            this.device = device;
            this.axis = axis;
            if(threshold < 0)
                negative = true;
            this.threshold = System.Math.Abs(threshold);
        }

        public override void asses(WiimoteLib.AccelState wiiAccel, WiimoteLib.AccelState nunAccel)
        {
            float axisValue = 0.0F;

            switch (device)
            {
                case DEVICE.WIIMOTE:
                    switch (axis)
                    {
                        case AXIS.X:
                            axisValue = wiiAccel.X;
                            break;
                        case AXIS.Y:
                            axisValue = wiiAccel.Y;
                            break;
                        case AXIS.Z:
                            axisValue = wiiAccel.Z;
                            break;
                    }
                    break;
                case DEVICE.NUNCHUK:
                    switch (axis)
                    {
                        case AXIS.X:
                            axisValue = nunAccel.X;
                            break;
                        case AXIS.Y:
                            axisValue = nunAccel.Y;
                            break;
                        case AXIS.Z:
                            axisValue = nunAccel.Z;
                            break;
                    }
                    break;
            }

            if (negative)
            {
                if (!previousState && (previousAxisValue - axisValue) > threshold)
                {
                    action.perform(true);
                    previousState = true;
                }
                else if (previousState && (previousAxisValue - axisValue) <= threshold)
                {
                    action.perform(false);
                    previousState = false;
                }
            }
            else
            {
                if (!previousState && (axisValue - previousAxisValue) > threshold)
                {
                    action.perform(true);
                    previousState = true;
                }
                else if (previousState && (axisValue - previousAxisValue) <= threshold)
                {
                    action.perform(false);
                    previousState = false;
                }
            }
            previousAxisValue = axisValue;
        }
    }

    public class LongMotionWatcher : MotionWatcher
    {
        DEVICE device;
        AXIS axis;
        float threshold;
        bool negative = false;
        Timer keyTimer;
        bool once;
        bool performedAlready = false;

        public LongMotionWatcher(float threshold, int durationMillis, DEVICE device, AXIS axis, bool once)
        {
            this.device = device;
            this.axis = axis;
            this.once = once;
            if (threshold < 0)
                negative = true;
            this.threshold = System.Math.Abs(threshold);
            keyTimer = new Timer();
            keyTimer.Interval = durationMillis; // milliseconds
            keyTimer.Elapsed += new ElapsedEventHandler(performAction);
        }

        public override void asses(WiimoteLib.AccelState wiiAccel, WiimoteLib.AccelState nunAccel)
        {
            float axisValue = 0.0F;

            switch (device)
            {
                case DEVICE.WIIMOTE:
                    switch (axis)
                    {
                        case AXIS.X:
                            axisValue = wiiAccel.X;
                            break;
                        case AXIS.Y:
                            axisValue = wiiAccel.Y;
                            break;
                        case AXIS.Z:
                            axisValue = wiiAccel.Z;
                            break;
                    }
                    break;
                case DEVICE.NUNCHUK:
                    switch (axis)
                    {
                        case AXIS.X:
                            axisValue = nunAccel.X;
                            break;
                        case AXIS.Y:
                            axisValue = nunAccel.Y;
                            break;
                        case AXIS.Z:
                            axisValue = nunAccel.Z;
                            break;
                    }
                    break;
            }

            if (negative)
            {
                if ((-1 * axisValue) > threshold)
                {
                    if(!keyTimer.Enabled)
                        keyTimer.Start();
                }
                else
                {
                    keyTimer.Stop();
                    performedAlready = false;
                }
            }
            else
            {
                if (axisValue > threshold)
                {
                    if (!keyTimer.Enabled)
                        keyTimer.Start();
                }
                else
                {
                    keyTimer.Stop();
                    performedAlready = false;
                }
            }
        }

        public void performAction(object sender, EventArgs args)
        {
            
            keyTimer.Stop();
            if (!performedAlready)
            {
                action.perform(true);
                action.perform(false);
            }
            if (once)
                performedAlready = true;
        }
    }

    // INPUT CLASSES:

    public abstract class InputAction
    {
        public static int POSITIVE = 1;
        public static int NEGATIVE = -1;

        protected float threshold;
        protected int direction;

        public InputAction(float threshold)
        {
            if (threshold < 0)
                direction = NEGATIVE;
            else
                direction = POSITIVE;
            this.threshold = System.Math.Abs(threshold);
        }

        public void perform(bool state)
        {
            if (state)
                perform(1.0F);
            else
                perform(0.0F);
        }

        public abstract void perform(float state);
    }

    public class KeyStrokeAction : InputAction
    {
        private ushort[] keys;
        private bool previousState = false;

        public KeyStrokeAction(float threshold, ushort[] keys) : base(threshold)
        {
            this.keys = keys;
        }

        public override void perform(float state)
        {
            if (!previousState && (direction * state) > threshold)
            {
                WindowsInput.sendKey(WindowsInput.KEY_TYPE.DIK, keys, WindowsInput.KEY_POSITION.DOWN);
                previousState = true;
            }
            else if (previousState && (direction * state) <= threshold)
            {
                WindowsInput.sendKey(WindowsInput.KEY_TYPE.DIK, keys, WindowsInput.KEY_POSITION.UP);
                previousState = false;
            }
        }
    }

    

    public class LongKeyStrokeAction : InputAction
    {

        private ushort[] keys;
        private bool down = false;
        Timer keyTimer;

        public LongKeyStrokeAction(float threshold, ushort[] keys, int durationMillis)
            : base(threshold)
        {
            this.keys = keys;
            keyTimer = new Timer();
            keyTimer.Interval = durationMillis; // milliseconds
            keyTimer.Elapsed += new ElapsedEventHandler(keyUp);
        }

        public override void perform(float state)
        {
            if (!down && (direction * state) > threshold)
            {
                WindowsInput.sendKey(WindowsInput.KEY_TYPE.DIK, keys, WindowsInput.KEY_POSITION.DOWN);
                down = true;
                keyTimer.Start();
            }
            else if (down && (direction * state) > threshold)
            {
                
                keyTimer.Stop();
                keyTimer.Start();
            }
        }

        public void keyUp(object sender, EventArgs args)
        {
            if (down)
            {
                WindowsInput.sendKey(WindowsInput.KEY_TYPE.DIK, keys, WindowsInput.KEY_POSITION.UP);
                keyTimer.Stop();
                down = false;
            }
        }
    }

    public class MouseAction : InputAction
    {
        private PRESS_TYPE type;
        private WindowsInput.MOUSE_BUTTON button;

        public MouseAction(float threshold, WindowsInput.MOUSE_BUTTON button, PRESS_TYPE type) : base(threshold)
        {
            this.button = button;
            this.type = type;
        }

        public override void perform(float state)
        {
            if (type == PRESS_TYPE.NORMAL)
            {
                if (state > threshold)
                    WindowsInput.sendMouseClick(WindowsInput.KEY_POSITION.DOWN, button);
                else
                    WindowsInput.sendMouseClick(WindowsInput.KEY_POSITION.UP, button);
            }
            else if (type == PRESS_TYPE.SINGLE_TAP || type == PRESS_TYPE.DOUBLE_TAP)
            {
                for (int i = 0; i < (ushort) type; i++)
                {
                    WindowsInput.sendMouseClick(WindowsInput.KEY_POSITION.DOWN, button);
                    WindowsInput.sendMouseClick(WindowsInput.KEY_POSITION.UP, button);
                }
            }
        }
    }

    public class RotateWheelAction : InputAction
    {
        WindowsInput.WHEEL_TYPE type;
        int increments;

        public RotateWheelAction(float threshold, WindowsInput.WHEEL_TYPE type, int increments) : base(threshold)
        {
            this.type = type;
            this.increments = increments;
        }

        public override void perform(float state)
        {
            if (state > threshold)
                WindowsInput.sendWheelScroll(increments, type);
        }
    }

    public class ResetIRAction : InputAction
    {
        MainForm form;

        public ResetIRAction(float threshold, MainForm form)
            : base(threshold)
        {
            this.form = form;
        }

        public override void perform(float state)
        {
            if (state > threshold && form != null)
                form.recenterIR();
        }
    }
}
