using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Timers;
using System.IO;

using WiimoteLib;

namespace WiimoteMouse
{
    public partial class MainForm : Form
    {
        Wiimote wm;
        MotionAndButtonController motionButtonControl;
        Bitmap b;
        Graphics g;
        Mutex m;

        String irFilePath = null;
        bool fixedScale = false;

        //static System.IO.StreamWriter log = null;

        public static int WII_WIDTH = 1024;
        public static int WII_HEIGHT = 768;
        public static int NUMBER_OF_WII_DOTS = 4;

        public static bool timeoutEnabled = false;

        public bool realClose = false;
        private bool firstConnectionAttempt = true;

        // variables for drawing the main window
        static Color bgcolor = Color.White;

        static int sensorCircleRadius = 5;
        static int groupBoxSize = sensorCircleRadius + 2;
        static int fontSize = 8;

        Brush generalBrush = Brushes.DarkBlue;
        Brush disabledBrush = Brushes.LightGray;
        Font generalFont;
        static float generalFontEmSize = 9;
        static String generalFontFamilyName = "Tahoma";

        // The position is in relation to the top left corner
        static int rawDataPositionX = 40;
        static int rawDataPositionY = 40;
        static int rawData2ndValueOffset = 50;
        static int rawDataLineHeightPx = 12;

        // The position is in relation to the bottom left corner
        static int optPositionX = 2;
        static int optPositionY = 2;
        static int optLineHeightPx = 12;

        // The position is in relation to the bottom left corner
        static int recoveryPositionX = 2;
        static int recoveryPositionY = 60;
        static int recoveryLineHeightPx = 12;

        // The position is in relation to the top left corner
        static int dynamicOffsetsPositionX = 2;
        static int dynamicOffsetsPositionY = 2;
        static int dynamicOffsetsLineHeightPx = 15;

        static Color irCamColor = Color.DarkGray;
        static int irCamWidth = 300;
        static int irCamHeight = 200;
        static int irCamDotRadius = 1;
        static float irCamHorPosPercent = 0.5f; // centred
        static float irCamVertPosPercent = 0.7f; // near the bottom

        static Color irGroupingColor = Color.DarkGray;
        static int irGroupingWidth = 300;
        static int irGroupingHeight = 200;
        static int irGroupingDotRadius = 5;
        static float irGroupingHorPosPercent = 0.5f; // centred
        static float irGroupingVertPosPercent = 0.7f; // near the bottom

        static int mappingsInsetFromRight = 220;
        static int mappingsFirstFigureWidth = 50;
        static int mappingsLineHeight = 12;

        static int functionsInsetFromRight = 220;
        static int functionsXHeightFromBottom = 60;
        static int functionsYHeightFromBottom = 30;
        static int functionsLineHeight = 12;

        // Srings used for the graphical representation
        static String EXTENSION_TEXT = "Extension Type: ";
        static String BATTERY_TEXT = "Battery Level: ";
        static String UNKNOWN = "unknown";
        static String NO_RAW_DATA = "-";

        // Config file strings
        private static String DOT_X_Y = "DOT-X-Y";
        private static String SCREEN_X_Y = "SCREEN-WIDTH-HEIGHT";
        private static String SCALE_X_Y = "SCALE-X-Y";
        private static String MAX_GROUPS = "MAX-GROUPS";
        private static String MAX_GROUP_SIZE = "MAX-GROUPING-SIZE";

        System.Timers.Timer batteryTimer, startupTimer, rumbleTimer, rumbleTimerLong, wiimoteTimeoutTimer;
        static int BATTERY_UPDATE_RATE_MILLIS = 60000; // every minute
        static int STARTUP_DELAY = 10; // a tenth of a second
        static int RUMBLE_DURATION_NORMAL = 150;
        static int RUMBLE_DURATION_LONG = 400;
        static int WIIMOTE_TIMEOUT = 5000; // 5 seconds

        bool inCritical = false;

        // if a dots jumps more than 50% of the screen, we consider it a new one
        float JUMP_THRESHOLD = 0.5f;

        // variables for computing the cursor position
        IRState irData;
        int maxGroupingSize = 2;
        int maxGroups = 0; // 0 for no limit
        int screenWidth;
        int screenHeight;
        StaticEnvironment env;
        AdditionalGroupingInfo optomise;
        List<DotGroup> latestDotGrouping;
        List<DotGroup> currentDotGrouping;
        GroupableDot dot1 = null; 
        GroupableDot dot2 = null; 
        GroupableDot dot3 = null;
        GroupableDot dot4 = null;
        int envOffset = 0;
        List<GroupOffsets> offsetsPairs;
        List<DotMatch> dotMapping;
        List<CursorLocation> cursors;
        int smoothing = 15;
        List<MappingOffsets> offsets;
        float scaleX;
        float scaleY;
        float cursorX;
        float cursorY;
        static int SCALING_RATE = 500;

        int CURSOR_STABILISATION_MINOR_SIZE = 10;
        //int CURSOR_STABILISATION_MAJOR_SIZE = 50;
        CursorLocation stabilisedCursor = new CursorLocation();
        //CursorLocation previousCursor = new CursorLocation();

        int irBorderSizeWidth = 250; // in IR units
        int irBorderSizeHeight = 250; // in IR units

        int visible = 0;
        int previouslyVisible = 0;
        bool offLeft = false;
        bool offRight = false;
        bool offTop = false;
        bool offBottom = false;
        bool returnedLeft = false;
        bool returnedRight = false;
        bool returnedTop = false;
        bool returnedBottom = false;
        bool recoverable = false;
        int recoveryOffset = 0;
        int previousRecoveryOffset = 0;

        bool noDotsFirstInform = false;

        // Errors and warnings
        bool error_tooManyGroups = false;
        bool warning_moreThanTwoGroups = false;
        bool centerReset = false;

        int environmentResets = 0;
        static String ENV_RESET_MENU = "Reset Model Resets";

        int recenters = 0;
        int unneededRecenters = 0;
        static String RECENTER_RESET_MENU = "Reset Recenter Count";

        struct GroupOffsets
        {
            public int currentGroupOffset;
            public int newGroupOffset;
        }

        struct CursorLocation
        {
            public int x;
            public int y;
        } 

        public MainForm()
        {
            InitializeComponent();
            generalFont = new Font(generalFontFamilyName, generalFontEmSize);
            m = new Mutex();

            this.Opacity = 0; // this.Visible does not work
            this.ShowInTaskbar = false;
            //systemTrayIcon.Visible = false;

            this.extensionMenuItem.Text = EXTENSION_TEXT + UNKNOWN;
            this.batteryMenuItem.Text = BATTERY_TEXT + UNKNOWN;

            // TO DO: alter this so that it takes different screen sizes into account
            screenHeight = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
            screenWidth = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;

            //this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Paint += new PaintEventHandler(MainForm_Paint);
            this.SizeChanged += new EventHandler(MainForm_SizeChanged);
            this.FormClosing += new FormClosingEventHandler(MainForm_FormClosing);

            this.loadIRConfigWindow.FileOk += new CancelEventHandler(loadIRConfigWindow_FileOk);
            this.loadButtonConfigWindow.FileOk += new CancelEventHandler(loadButtonConfigWindow_FileOk);

            this.showIRViewToolStripMenuItem.CheckedChanged += new EventHandler(showIRViewToolStripMenuItem_CheckedChanged);
            this.showIRGroupingToolStripMenuItem.CheckedChanged += new EventHandler(showIRGroupingToolStripMenuItem_CheckedChanged);
            irData = new IRState();
            irData.Found1 = false;
            irData.Found2 = false;
            irData.Found3 = false;
            irData.Found4 = false;

            currentDotGrouping = new List<DotGroup>();
            dot1 = new GroupableDot();
            dot2 = new GroupableDot();
            dot3 = new GroupableDot();
            dot4 = new GroupableDot();

            cursors = new List<CursorLocation>();
            dotMapping = new List<DotMatch>();

            // put a delay on the connection so that the form can finish loading
            // in the event that the Wiimote is unable to connect and we want to close
            startupTimer = new System.Timers.Timer();
            startupTimer.Interval = STARTUP_DELAY;
            startupTimer.Elapsed += new ElapsedEventHandler(setupWiimote);
            startupTimer.Start();

            rumbleTimer = new System.Timers.Timer();
            rumbleTimer.Interval = RUMBLE_DURATION_NORMAL;
            rumbleTimer.Elapsed += new ElapsedEventHandler(rumbleTimer_Elapsed);

            rumbleTimerLong = new System.Timers.Timer();
            rumbleTimerLong.Interval = RUMBLE_DURATION_LONG;
            rumbleTimerLong.Elapsed += new ElapsedEventHandler(rumbleTimerLong_Elapsed);

            wiimoteTimeoutTimer = new System.Timers.Timer();
            wiimoteTimeoutTimer.Interval = WIIMOTE_TIMEOUT;
            wiimoteTimeoutTimer.Elapsed += new ElapsedEventHandler(wiimoteTimeoutTimer_Elapsed);

            // Make multiply-defined controls consistent
            controlCursorToolStripMenuItem.Checked = moveCursorToolStripMenuItem.Checked;
            rumbleToolStripMenuItem.Checked = rumbleToolStripMenu.Checked;
            stabiliseCursorToolStripMenuItem.Checked = stabiliseCursorSystemToolStripMenuItem.Checked;

            errorStrip.Visible = showErrorsAndWarningsToolStripMenuItem.Checked;

            resetModelResetsToolStripMenuItem.Text = ENV_RESET_MENU + " (" + environmentResets + ")";
            this.resetRecenterCountToolStripMenuItem.Text = RECENTER_RESET_MENU + " (" + unneededRecenters + "/" + recenters + ") (unneeded/total)";

            intialiseGraphics();

            writeToLog("-- New session --");
        }

        void rumble()
        {
            if (rumbleToolStripMenuItem.Checked && !rumbleTimerLong.Enabled)
            {
                wm.SetRumble(true);
                rumbleTimer.Start();
            }
        }

        void longRumble()
        {
            if (rumbleToolStripMenuItem.Checked)
            {
                wm.SetRumble(true);
                rumbleTimerLong.Start();
            }
        }

        void rumbleTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            m.WaitOne();
            rumbleTimer.Stop();
            wm.SetRumble(false);
            m.ReleaseMutex();
        }

        void rumbleTimerLong_Elapsed(object sender, ElapsedEventArgs e)
        {
            m.WaitOne();
            rumbleTimerLong.Stop();
            wm.SetRumble(false);
            m.ReleaseMutex();
        }

        void wiimoteTimeoutTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (timeoutEnabled)
            {
                wiimoteTimeoutTimer.Stop();
                DialogueWindow warning = new DialogueWindow("Wiimote Lost", this);
                warning.setText("Wiimote timed out. Is it still connected?");
                warning.ShowDialog();
            }
        }

        void showIRGroupingToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (showIRGroupingToolStripMenuItem.Checked && showIRViewToolStripMenuItem.Checked)
                showIRViewToolStripMenuItem.Checked = false;
            else if (!showIRGroupingToolStripMenuItem.Checked && !showIRViewToolStripMenuItem.Checked)
                showIRViewToolStripMenuItem.Checked = true;
        }

        void showIRViewToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (showIRGroupingToolStripMenuItem.Checked && showIRViewToolStripMenuItem.Checked)
                showIRGroupingToolStripMenuItem.Checked = false;
            else if (!showIRGroupingToolStripMenuItem.Checked && !showIRViewToolStripMenuItem.Checked)
                showIRGroupingToolStripMenuItem.Checked = true;
        }

        void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!realClose)
                e.Cancel = true;
            else
                systemTrayIcon.Visible = false;
            this.Hide();
        }

        private void intialiseGraphics()
        {
            int height = this.ClientSize.Height;

            if(menuStrip.Visible)
                height -= this.menuStrip.Height;
            if(errorStrip.Visible)
                height -= this.errorStrip.Height;

            b = new Bitmap(this.ClientSize.Width, height, PixelFormat.Format24bppRgb);
            g = Graphics.FromImage(b);
        }

        /**
         * Tries to connect to the Wiimote using two approaches.
         */
        public void setupWiimote(object sender, EventArgs args)
        {
            m.WaitOne();
            if (firstConnectionAttempt)
            {
                firstConnectionAttempt = false;
                startupTimer.Stop();
                startupTimer.Dispose();
                try
                {
                    wm = new Wiimote();

                    try
                    {
                        wm.AltWriteMethod = false;
                        wm.Connect();
                    }
                    catch
                    {
                        wm.AltWriteMethod = true;
                        wm.Connect();
                    }
                    wm.SetReportType(Wiimote.InputReport.IRAccel, true);
                    wm.SetLEDs(false, false, false, false);

                    wm.OnWiimoteChanged += new WiimoteChangedEventHandler(wm_OnWiimoteChanged);
                    wm.OnWiimoteExtensionChanged += new WiimoteExtensionChanged(wm_OnWiimoteExtensionChanged);

                    setupBatteryStatus();

                    Invoke(new EventHandler(showSystemMenu));

                    wiimoteTimeoutTimer.Start();
                }
                catch (Exception e)
                {
                    DialogueWindow warning = new DialogueWindow("Connection Error", this);
                    warning.setText(
                        e.Message + "\r\r" +
                        "Suggestions:\r\r" +
                        "- Ensure your bluetooth adapter is compatible with a Wiimote. Check www.wiibrew.org for a comprehensive compatibility list.\r\r" +
                        "- Install the latest bluetooth stack. The Toshiba or BlueSoleil drivers are recommended.\r\r" +
                        "- Go to Control Panel > System > Hardware > Device Manager and disable and then reenable any HID-compliant game controllers");
                    warning.ShowDialog();
                }
            }
            m.ReleaseMutex();
        }

        private void showSystemMenu(object sender, EventArgs e)
        {
            systemTrayIcon.Visible = true;
            PerformLayout();
        }

        /**
         * Set up a timer so that it will update the battery level
         * on regular intervals.
         */
        private void setupBatteryStatus()
        {
            batteryTimer = new System.Timers.Timer();
            batteryTimer.Interval = BATTERY_UPDATE_RATE_MILLIS;
            batteryTimer.Elapsed += new ElapsedEventHandler(updateBatteryStatus);
            batteryTimer.Start();
            updateBatteryStatus(null, null);
        }

        /**
         * Updates the battery level that will be returned.
         */
        public void updateBatteryStatus(object sender, EventArgs args)
        {
            m.WaitOne();
            wm.GetBatteryLevel();
            m.ReleaseMutex();
        }

        public void MainForm_FormClosed(object sender, System.EventArgs e)
        {
            m.WaitOne();
            wm.Disconnect();
            if (batteryTimer != null)
            {
                batteryTimer.Stop();
                batteryTimer.Dispose();
            }
            if (wm != null)
            {
                wm.Disconnect();
            }
            m.ReleaseMutex();

            //log.Close();
            //log = null;


            // allow time for any drawing processes to finish
            Thread.Sleep(100);
        }

        void wm_OnWiimoteExtensionChanged(object sender, WiimoteExtensionChangedEventArgs args)
        {
            if (args.Inserted)
                wm.SetReportType(Wiimote.InputReport.IRExtensionAccel, true);
            else
                wm.SetReportType(Wiimote.InputReport.IRAccel, true);
        }

        void wm_OnWiimoteChanged(object sender, WiimoteChangedEventArgs args)
        {
            if (inCritical)
                return;

            m.WaitOne();
            inCritical = true;
            try
            {
                wiimoteTimeoutTimer.Stop();
                wiimoteTimeoutTimer.Start();

                float batteryLevel = (((100.0f * 48.0f * (float)(args.WiimoteState.Battery / 48.0f))) / 192.0f);
                BeginInvoke((MethodInvoker)delegate() { batteryMenuItem.Text = BATTERY_TEXT + batteryLevel.ToString("F") + "%"; });
                BeginInvoke((MethodInvoker)delegate() { extensionMenuItem.Text = EXTENSION_TEXT + args.WiimoteState.ExtensionType.ToString(); });

                if (motionButtonControl != null)
                {
                    motionButtonControl.updateButtons(args.WiimoteState.ButtonState);
                    motionButtonControl.updateButtons(args.WiimoteState.NunchukState);
                    motionButtonControl.updateMotion(args.WiimoteState.AccelState, args.WiimoteState.NunchukState.AccelState);
                }

                IRState oldIRData = irData;
                irData = args.WiimoteState.IRState;
                if (env != null)
                {
                    List<GroupableDot> latestDots = new List<GroupableDot>();
                    bool newDot = false;

                    int previousEnvOffset = envOffset;
                    //int visible = 0;
                    visible = 0;

                    // logic to handle going off to the right, left or jumping between sensors
                    if (previouslyVisible > 0)
                    {
                        bool recoveryAlreadySet = false;

                        offRight = true;
                        offLeft = true;
                        offTop = true;
                        offBottom = true;

                        for (int i = 0; i < currentDotGrouping.Count; i++)
                        {
                            for (int j = 0; j < currentDotGrouping[i].Count; j++)
                            {
                                if (currentDotGrouping[i][j].active)
                                {
                                    if (recoveryAlreadySet)
                                    {
                                        recoveryOffset = Math.Min(i, recoveryOffset); // + envOffset;
                                    }
                                    else
                                    {
                                        recoveryOffset = i;
                                        recoveryAlreadySet = true;
                                    }
                                    offRight = offRight && onTheRight(currentDotGrouping[i][j]);
                                    offLeft = offLeft && onTheLeft(currentDotGrouping[i][j]);
                                    offTop = offTop && onTheTop(currentDotGrouping[i][j]);
                                    offBottom = offBottom && onTheBottom(currentDotGrouping[i][j]);
                                }
                            }
                        }
                    }

                    // old dot doesn't exist
                    if ((!oldIRData.Found1 && irData.Found1) ||
                        (oldIRData.Found1 && irData.Found1 && (majorJump(oldIRData.X1, irData.X1) || majorJump(oldIRData.Y1, irData.Y1))))
                    {
                        dot1.active = false;

                        dot1 = new GroupableDot();
                        dot1.x = irData.RawX1;
                        dot1.y = WII_HEIGHT - irData.RawY1;
                        latestDots.Add(dot1);
                        newDot = true;

                        visible++;
                    }
                    // update the old dot
                    else if (irData.Found1)
                    {
                        dot1.x = irData.RawX1;
                        dot1.y = WII_HEIGHT - irData.RawY1;
                        latestDots.Add(dot1);

                        visible++;
                    }
                    else
                    {
                        dot1.active = false;
                    }

                    // old dot doesn't exist
                    if ((!oldIRData.Found2 && irData.Found2) ||
                        (oldIRData.Found2 && irData.Found2 && (majorJump(oldIRData.X2, irData.X2) || majorJump(oldIRData.Y2, irData.Y2))))
                    {
                        dot2.active = false;

                        dot2 = new GroupableDot();
                        dot2.x = irData.RawX2;
                        dot2.y = WII_HEIGHT - irData.RawY2;
                        latestDots.Add(dot2);
                        newDot = true;

                        visible++;
                    }
                    // update the old dot
                    else if (irData.Found2)
                    {
                        dot2.x = irData.RawX2;
                        dot2.y = WII_HEIGHT - irData.RawY2;
                        latestDots.Add(dot2);

                        visible++;
                    }
                    else
                    {
                        dot2.active = false;
                    }

                    // old dot doesn't exist
                    if ((!oldIRData.Found3 && irData.Found3) ||
                        (oldIRData.Found3 && irData.Found3 && (majorJump(oldIRData.X3, irData.X3) || majorJump(oldIRData.Y3, irData.Y3))))
                    {
                        dot3.active = false;

                        dot3 = new GroupableDot();
                        dot3.x = irData.RawX3;
                        dot3.y = WII_HEIGHT - irData.RawY3;
                        latestDots.Add(dot3);
                        newDot = true;

                        visible++;
                    }
                    // update the old dot
                    else if (irData.Found3)
                    {
                        dot3.x = irData.RawX3;
                        dot3.y = WII_HEIGHT - irData.RawY3;
                        latestDots.Add(dot3);

                        visible++;
                    }
                    else
                    {
                        dot3.active = false;
                    }

                    // old dot doesn't exist
                    if ((!oldIRData.Found4 && irData.Found4) ||
                        (oldIRData.Found4 && irData.Found4 && (majorJump(oldIRData.X4, irData.X4) || majorJump(oldIRData.Y4, irData.Y4))))
                    {
                        dot4.active = false;

                        dot4 = new GroupableDot();
                        dot4.x = irData.RawX4;
                        dot4.y = WII_HEIGHT - irData.RawY4;
                        latestDots.Add(dot4);
                        newDot = true;

                        visible++;
                    }
                    // update the old dot
                    else if (irData.Found4)
                    {
                        dot4.x = irData.RawX4;
                        dot4.y = WII_HEIGHT - irData.RawY4;
                        latestDots.Add(dot4);

                        visible++;
                    }
                    else
                    {
                        dot4.active = false;
                    }

                    // Rumble when we go off screen
                    if (visible == 0 && (
                        (env.Count == currentDotGrouping.Count &&
                            ((offLeft && previousRecoveryOffset == (env.Count - 1))
                            || (offRight && previousRecoveryOffset == 0)))
                        || ((!offRight && !offLeft) && (offBottom || offTop))))
                    {
                        if (noDotsFirstInform)
                        {
                            String off = "Out of Range: off to the";
                            if (offTop)
                                off += " top";
                            if (offBottom)
                                off += " bottom";
                            if (offLeft)
                                off += " left";
                            if (offRight)
                                off += " right";
                            writeToLog(off);
                            rumble();
                            noDotsFirstInform = false;
                        }
                    }
                    else
                    {
                        noDotsFirstInform = true;
                    }

                    // logic to handle going off to the right, left or jumping between sensors
                    if (newDot)  // (visible > 0 && previouslyVisible == 0)
                    {
                        //bool returnedLeft = true;
                        //bool returnedRight = true;
                        returnedLeft = true;
                        returnedRight = true;
                        returnedTop = true;
                        returnedBottom = true;

                        for (int i = 0; i < latestDots.Count; i++)
                        {
                            returnedRight = returnedRight && onTheRight(latestDots[i]);
                            returnedLeft = returnedLeft && onTheLeft(latestDots[i]);
                            returnedTop = returnedTop && onTheTop(latestDots[i]);
                            returnedBottom = returnedBottom && onTheBottom(latestDots[i]);
                        }
                        if (offLeft && returnedLeft)
                        {
                            recoverable = true;
                        }
                        else if (offRight && returnedRight)
                        {
                            recoverable = true;
                        }
                        else if (offLeft && returnedRight)
                        {
                            if (previousRecoveryOffset < env.Count - 1)
                                previousRecoveryOffset++;
                            recoverable = true;
                        }
                        else if (offRight && returnedLeft)
                        {
                            if (previousRecoveryOffset > 0)
                                previousRecoveryOffset--;
                            else
                                if (envOffset > 0)
                                    envOffset--;
                            recoverable = true;
                        }

                        if ((offTop && returnedTop) || (offBottom && returnedBottom))
                        {
                            recoverable = true;
                        }

                        offRight = false;
                        offLeft = false;
                        offTop = false;
                        offBottom = false;
                    }

                    previouslyVisible = visible;

                    if (newDot || error_tooManyGroups || warning_moreThanTwoGroups || centerReset)
                    {
                        error_tooManyGroups = false;
                        warning_moreThanTwoGroups = false;
                        centerReset = false;

                        // create new grouping
                        latestDotGrouping = groupDots(latestDots, maxGroupingSize, maxGroups, optomise);

                        if (latestDotGrouping.Count > env.Count)
                        {
                            error_tooManyGroups = true;
                            int overshoot = latestDotGrouping.Count - env.Count;
                            for (int i = 0; i < overshoot; i++)
                                latestDotGrouping.RemoveAt(latestDotGrouping.Count - 1);
                        }
                        else if (latestDotGrouping.Count > 2)
                        {
                            warning_moreThanTwoGroups = true;
                        }

                        /*
                        bool moreSeperated = false;
                        bool moreMerged = false;
                    
                        // compare new and old model
                        for (int i = 0; i < latestDots.Count; i++)
                        {
                            for (int j = i + 1; j < latestDots.Count; j++)
                            {
                                if (latestDots[i].currentGrouping != null && latestDots[j].currentGrouping != null)
                                {
                                    if (latestDots[i].newGrouping != latestDots[j].newGrouping && latestDots[i].currentGrouping == latestDots[j].newGrouping)
                                        moreSeperated = true;
                                    if (latestDots[i].newGrouping == latestDots[j].newGrouping && latestDots[i].currentGrouping != latestDots[j].newGrouping)
                                        moreMerged = true;
                                }
                            }
                        }
                    
                        if (moreSeperated)
                        {
                            // old model is considered incorrect
                            makeLatestCurrent();
                        }
                        else if (!moreMerged)
                        {
                         */

                        // merge models
                        offsetsPairs = new List<GroupOffsets>();
                        for (int i = 0; i < latestDotGrouping.Count; i++)
                        {
                            for (int j = 0; j < latestDotGrouping[i].Count; j++)
                            {
                                if (latestDotGrouping[i][j].currentGrouping != null)
                                {
                                    GroupOffsets offsets;
                                    offsets.newGroupOffset = i;
                                    offsets.currentGroupOffset = currentDotGrouping.FindIndex(delegate(DotGroup dg1) { return dg1 == latestDotGrouping[i][j].currentGrouping; });
                                    offsetsPairs.Add(offsets);
                                }
                            }
                        }
                        if (offsetsPairs.Count == 0)
                        {
                            makeLatestCurrent();
                        }
                        else
                        {
                            bool allValid = true;
                            int offsetDiff = offsetsPairs[0].currentGroupOffset - offsetsPairs[0].newGroupOffset;
                            for (int i = 1; i < offsetsPairs.Count; i++)
                                if (offsetDiff != (offsetsPairs[i].currentGroupOffset - offsetsPairs[i].newGroupOffset))
                                    allValid = false;

                            if (!allValid)
                            {
                                makeLatestCurrent();
                            }
                            else
                            {
                                for (int i = 0; i < latestDotGrouping.Count; i++)
                                {
                                    for (int j = 0; j < latestDotGrouping[i].Count; j++)
                                    {
                                        if (latestDotGrouping[i][j].currentGrouping == null)
                                        {
                                            // add a group to the front
                                            if (offsetDiff + i < 0)
                                            {
                                                List<DotGroup> newCurrentDotGroup = new List<DotGroup>();
                                                DotGroup group = new DotGroup();
                                                group.Add(latestDotGrouping[i][j]);
                                                latestDotGrouping[i][j].currentGrouping = group;
                                                newCurrentDotGroup.Add(group);
                                                for (int h = 0; h < currentDotGrouping.Count; h++)
                                                    newCurrentDotGroup.Add(currentDotGrouping[h]);
                                                currentDotGrouping = newCurrentDotGroup;
                                                // we have a full model, so we need to remove the last to keep it the correct size
                                                currentDotGrouping.RemoveAt(currentDotGrouping.Count - 1);
                                                //invariants();
                                                offsetDiff++;
                                                if (envOffset > 0)
                                                    envOffset--;
                                            }
                                            // add a group to the end
                                            else if (i + offsetDiff >= currentDotGrouping.Count)
                                            {
                                                DotGroup group = new DotGroup();
                                                group.Add(latestDotGrouping[i][j]);
                                                latestDotGrouping[i][j].currentGrouping = group;
                                                currentDotGrouping.Add(group);
                                                // we have a full model, so we need to remove the first to keep it the correct size
                                                currentDotGrouping.RemoveAt(0);
                                                //invariants();
                                                if (envOffset + currentDotGrouping.Count > env.Count)
                                                    envOffset--;
                                                //invariants();
                                            }
                                            else
                                            {
                                                currentDotGrouping[i + offsetDiff].Add(latestDotGrouping[i][j]);
                                                //invariants();
                                                latestDotGrouping[i][j].currentGrouping = currentDotGrouping[i + offsetDiff];
                                            }

                                        }
                                    }
                                }
                            }
                        }
                        // clean up
                        for (int i = 0; i < currentDotGrouping.Count; i++)
                            for (int j = currentDotGrouping[i].Count - 1; 0 <= j; j--)
                                if (!currentDotGrouping[i][j].active)
                                    currentDotGrouping[i].RemoveAt(j);

                        // match to environment
                        bool match = matches(0);
                        if (!match)
                        {
                            for (int i = 0; i < (env.Count - currentDotGrouping.Count); i++)
                            {
                                match = matches(-i);
                                if (match)
                                {
                                    envOffset -= i;
                                    i = env.Count;
                                    //invariants();
                                }
                                else
                                {
                                    match = matches(i);
                                    if (match)
                                    {
                                        envOffset += i;
                                        i = env.Count;
                                    }
                                }
                            }
                        }

                        if (match)
                        {
                            // find a mapping between ir dots and screen dots
                            if (previousEnvOffset != envOffset)
                            {
                                dotMapping = new List<DotMatch>();
                            }
                            else
                            {
                                for (int i = dotMapping.Count - 1; 0 <= i; i--)
                                {
                                    if (!dotMapping[i].reliable || !dotMapping[i].irDot.active)
                                        dotMapping.RemoveAt(i);
                                }
                            }
                            for (int i = 0; i < currentDotGrouping.Count; i++)
                                env[i + envOffset].computeMatches(currentDotGrouping[i], dotMapping);
                        }
                    }

                    // compute cursor location
                    if (!fixedScale)
                    {
                        for (int i = 0; i < dotMapping.Count; i++)
                        {
                            for (int j = i + 1; j < dotMapping.Count; j++)
                            {
                                if (dotMapping[i].irDot.active && dotMapping[j].irDot.active)
                                {
                                    if (dotMapping[i].screenDot.x != dotMapping[j].screenDot.x && dotMapping[i].irDot.x != dotMapping[j].irDot.x)
                                    {
                                        float scale = (float)(dotMapping[i].screenDot.x - dotMapping[j].screenDot.x) / (dotMapping[i].irDot.x - dotMapping[j].irDot.x);
                                        if (scaleX == 0)
                                            scaleX = scale;
                                        else
                                            scaleX = (scaleX * (SCALING_RATE - 1) + scale) / SCALING_RATE;
                                    }
                                    if (dotMapping[i].screenDot.y != dotMapping[j].screenDot.y && dotMapping[i].irDot.y != dotMapping[j].irDot.y)
                                    {
                                        float scale = (float)(dotMapping[i].screenDot.y - dotMapping[j].screenDot.y) / (dotMapping[i].irDot.y - dotMapping[j].irDot.y);
                                        if (scaleY == 0)
                                            scaleY = scale;
                                        else
                                            scaleY = (scaleY * (SCALING_RATE - 1) + scale) / SCALING_RATE;
                                    }
                                }
                            }
                        }
                    }

                    this.offsets = new List<MappingOffsets>();
                    for (int i = 0; i < dotMapping.Count; i++)
                        if (dotMapping[i].irDot.active)
                        {
                            MappingOffsets offset = new MappingOffsets();
                            offset.x = (int)(dotMapping[i].screenDot.x - scaleX * dotMapping[i].irDot.x);
                            offset.y = (int)(dotMapping[i].screenDot.y - scaleY * dotMapping[i].irDot.y);
                            offsets.Add(offset);
                        }


                    // compute cursor
                    if (offsets.Count > 0)
                    {
                        int offX = 0;
                        int offY = 0;

                        for (int i = 0; i < offsets.Count; i++)
                        {
                            offX += offsets[i].x;
                            offY += offsets[i].y;
                        }
                        CursorLocation cursor;
                        cursor.x = (int)(scaleX * 512 + (float)(offX) / offsets.Count);
                        cursor.y = (int)(scaleY * 384 + (float)(offY) / offsets.Count);

                        // Requires a significant change in the cursor position to make it move.
                        if (stabiliseCursorToolStripMenuItem.Checked)
                        {

                            /*
                            if (Math.Abs(cursor.x - previousCursor.x) > CURSOR_STABILISATION_MAJOR_SIZE || Math.Abs(cursor.y - previousCursor.y) > CURSOR_STABILISATION_MAJOR_SIZE)
                            {
                                stabilisedCursor = cursor;
                                cursors.RemoveAll(delegate(CursorLocation location) { return true; });
                                cursors.Add(cursor);
                            }
                            else
                             */

                            if (Math.Abs(cursor.x - stabilisedCursor.x) > CURSOR_STABILISATION_MINOR_SIZE || Math.Abs(cursor.y - stabilisedCursor.y) > CURSOR_STABILISATION_MINOR_SIZE)
                            {
                                stabilisedCursor = cursor;
                                cursors.Add(cursor);
                            }
                            else
                            {
                                cursors.Add(stabilisedCursor);
                            }
                        }
                        else
                        {
                            cursors.Add(cursor);
                        }

                        // previousCursor = cursor;

                        if (cursors.Count > smoothing)
                            cursors.RemoveAt(0);

                        cursor.x = 0;
                        cursor.y = 0;

                        for (int i = 0; i < cursors.Count; i++)
                        {
                            cursor.x += cursors[i].x;
                            cursor.y += cursors[i].y;
                        }
                        cursor.x = cursor.x / cursors.Count;
                        cursor.y = cursor.y / cursors.Count;

                        if (moveCursorToolStripMenuItem.Checked)
                        {
                            cursorX = (float)(cursor.x) / screenWidth;
                            if (cursorX < 0)
                                cursorX = 0;
                            else if (cursorX > 1)
                                cursorX = 1;

                            cursorY = (float)(cursor.y) / screenHeight;
                            if (cursorY < 0)
                                cursorY = 0;
                            else if (cursorY > 1)
                                cursorY = 1;

                            WindowsInput.sendMousePosition((int)(cursorX * WindowsInput.MAX_SCREEN_COORDINATE), (int)(cursorY * WindowsInput.MAX_SCREEN_COORDINATE), WindowsInput.CURSOR_POSITION.ABSOLUTE);
                        }
                    }
                }

                wm.SetLEDs(irData.Found1, irData.Found2, irData.Found3, irData.Found4);

                if (visible > 0)
                {
                    previousRecoveryOffset = recoveryOffset;
                    recoverable = false;
                }
                returnedLeft = false;
                returnedRight = false;
                returnedTop = false;
                returnedBottom = false;

                Invalidate();
            }
            catch (Exception ex)
            {
                // This runs on WiimoteLib's read thread, where an unhandled
                // exception would end the application. Log it and start the
                // model afresh from the next report instead.
                writeToLog("Error: " + ex);
                resetModel();
            }
            finally
            {
                inCritical = false;
                m.ReleaseMutex();
            }
        }

        /**
         * Forgets what the tracking model has learnt, so the dots in view are
         * treated as new on the next report.
         */
        private void resetModel()
        {
            irData = new IRState();
            dot1 = new GroupableDot();
            dot2 = new GroupableDot();
            dot3 = new GroupableDot();
            dot4 = new GroupableDot();
            latestDotGrouping = null;
            currentDotGrouping = new List<DotGroup>();
            dotMapping = new List<DotMatch>();
            cursors = new List<CursorLocation>();
            envOffset = 0;
            previouslyVisible = 0;
            recoverable = false;
        }

        private bool onTheBottom(GroupableDot dot)
        {
            return dot.y <= irBorderSizeHeight;
        }

        private bool onTheTop(GroupableDot dot)
        {
            return dot.y >= (WII_HEIGHT - irBorderSizeHeight);
        }

        private bool onTheLeft(GroupableDot dot)
        {
            return dot.x <= irBorderSizeWidth;
        }

        private bool onTheRight(GroupableDot dot)
        {
            return dot.x >= (WII_WIDTH - irBorderSizeWidth);
        }

        private bool matches(int offset)
        {
            if ((envOffset + offset) >= 0 && (currentDotGrouping.Count + envOffset + offset) <= env.Count)
            {
                bool match = true;
                for (int i = 0; i < currentDotGrouping.Count; i++)
                {
                    match = match && env[i + envOffset + offset].accepts(currentDotGrouping[i]);
                }
                return match;
            }
            else
            {
                return false;
            }
        }

        private void makeLatestCurrent()
        {
            int previousModelSize = currentDotGrouping.Count;

            currentDotGrouping = new List<DotGroup>();

            // bulk the start of the model out
            if (recoverable)
            {
                // ensures the dynamic model can never expand bigger than the static one
                //
                // this arises when:
                // - last seen dot is in the last bar
                // - two dots appear at the same time
                int paddingSize = Math.Min(env.Count - latestDotGrouping.Count, previousRecoveryOffset);

                for (int i = 0; i < paddingSize; i++)
                {
                    currentDotGrouping.Add(new DotGroup());
                    //invariants();
                }
                if (envOffset + currentDotGrouping.Count > env.Count)
                {
                    // model pushes left
                    envOffset = env.Count - currentDotGrouping.Count;
                    //invariants();
                }
            }
            else
            {
                resetOffset();

                // avoid rumbling on first point at the screen
                if (previousModelSize > 0)
                {
                    longRumble();
                    environmentResets++;
                    updateEnvResetMenu();
                    writeToLog("Environment reset");
                }
            }

            int furtherPaddingSize = Math.Min(env.Count - latestDotGrouping.Count, envOffset);
            for (int i = 0; i < furtherPaddingSize; i++)
            {
                currentDotGrouping.Add(new DotGroup());
                //invariants();
            }
            envOffset = 0;

            currentDotGrouping.AddRange(latestDotGrouping);
            //invariants();

            // bulk the end of the model out
            int tmp = currentDotGrouping.Count;
            for (int i = 0; i < env.Count - tmp; i++)
                currentDotGrouping.Add(new DotGroup());
            //invariants();

            recoverable = false;

            for (int i = 0; i < latestDotGrouping.Count; i++)
                for (int j = 0; j < latestDotGrouping[i].Count; j++)
                    latestDotGrouping[i][j].currentGrouping = latestDotGrouping[i][j].newGrouping;
        }

        // for debugging purposes only
        private void invariants()
        {
            int i;
            if (envOffset < 0)
                i = 0;
            if(envOffset < 0 || currentDotGrouping.Count > env.Count)
                i = 0;
        }

        public void recenterIR()
        {
            m.WaitOne();
            String logMessage = "Recentered:";
            recenters++;

            // find the first group with a dot
            int first = -1;
            for (int i = currentDotGrouping.Count - 1; i >= 0; i--)
            {
                if (currentDotGrouping[i].Count > 0)
                    for (int j = 0; j < currentDotGrouping[i].Count; j++)
                        if(currentDotGrouping[i][j].active)
                            first = i;
            }
            int center = getCenter();
            if (first >= 0)
            {
                if (first > center)
                {
                    for (int i = 0; i < first - center; i++)
                    {
                        currentDotGrouping.RemoveAt(0);
                        currentDotGrouping.Add(new DotGroup());
                    }
                    logMessage += " off by " + (first - center) + " to the left";
                }
                else if (first < center)
                {
                    for (int i = 0; i < center - first; i++)
                    {
                        if (currentDotGrouping.Count >= env.Count)
                            currentDotGrouping.RemoveAt(currentDotGrouping.Count - 1);
                        currentDotGrouping.Insert(0, new DotGroup());
                    }
                    logMessage += " off by " + (center - first) + " to the right";
                }
                else
                {
                    unneededRecenters++;
                    logMessage += " unneeded";
                }
                centerReset = true;
            }
            else
            {
                logMessage += " no IR visible";
                longRumble();
                recoverable = true;
            }
            previousRecoveryOffset = center;
            updateRecenterMenu();
            writeToLog(logMessage);
            m.ReleaseMutex();
        }

        private void resetOffset()
        {
            envOffset = getCenter();
        }

        private int getCenter()
        {
            return (env.Count - 1) / 2;
        }

        private bool majorJump(float p1, float p2)
        {
            return Math.Abs(p1 - p2) > JUMP_THRESHOLD;
        }

        void loadIRConfigWindow_FileOk(object sender, CancelEventArgs cea)
        {
            try
            {
                m.WaitOne();
                try
                {
                    initiateLogic(this.loadIRConfigWindow.FileName);
                    resetModel();
                }
                finally
                {
                    m.ReleaseMutex();
                }
                irFilePath = this.loadIRConfigWindow.FileName;
                String irMenu = "IR: " + getFileName(this.loadIRConfigWindow.FileName) + "...";
                BeginInvoke((MethodInvoker)delegate() { this.loadIRConfigurationMenuItem.Text = irMenu; });
                BeginInvoke((MethodInvoker)delegate() { this.loadIRConfigurationToolStripMenuItem.Text = irMenu; });
            }
            catch (Exception e)
            {
                DialogueWindow warning = new DialogueWindow("Error");
                warning.setText(e.Message);
                warning.ShowDialog();
            }
        }

        private void initiateLogic(String filename)
        {
            int lineNumber = 0;
            float width = 0;
            float height = 0;

            // settings not given in this file revert to their defaults
            maxGroupingSize = 2;
            maxGroups = 0;
            fixedScale = false;
            scaleX = 0;
            scaleY = 0;

            FileStream file = new FileStream(filename, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(file);
            List<ConfigFileDot> dots = new List<ConfigFileDot>();

            while (!sr.EndOfStream)
            {
                String line;

                lineNumber++;
                line = sr.ReadLine().Trim().ToUpper();
                if (line.Equals(MAX_GROUP_SIZE))
                {
                    line = sr.ReadLine();
                    try
                    {
                        maxGroupingSize = System.Convert.ToInt32(line);
                        if(maxGroupingSize < 1)
                            throw new Exception("The maximum grouping size must be greater than 0");
                        else if(maxGroupingSize > 2)
                            throw new Exception("The program does not support groups larger than 2");
                    }
                    catch
                    {
                        throw new Exception("Line " + lineNumber +
                            " was meant to contain an integer specifying the maximum group size");
                    }
                }
                else if (line.Equals(MAX_GROUPS))
                {
                    line = sr.ReadLine();
                    try
                    {
                        maxGroups = System.Convert.ToInt32(line);
                    }
                    catch
                    {
                        throw new Exception("Line " + lineNumber + 
                            " was meant to contain an integer specifying the maximum group size");
                    }
                }
                else if (line.Equals(SCREEN_X_Y))
                {
                    line = sr.ReadLine();
                    try
                    {
                        width = System.Convert.ToSingle(line);
                        if (width <= 0)
                            throw new Exception("The screen width must be greater than 0");
                    }
                    catch
                    {
                        throw new Exception("Line " + lineNumber + 
                            " was meant to contain a number for the screen width");
                    }

                    line = sr.ReadLine();
                    try
                    {
                        height = System.Convert.ToSingle(line);
                        if (height <= 0)
                            throw new Exception("The screen height must be greater than 0");
                    }
                    catch
                    {
                        throw new Exception("Line " + lineNumber +
                            " was meant to contain a number for the screen height");
                    }
                }
                else if (line.Equals(DOT_X_Y))
                {
                    ConfigFileDot dot = new ConfigFileDot();
                    line = sr.ReadLine();
                    try
                    {
                        dot.x = System.Convert.ToSingle(line);
                    }
                    catch
                    {
                        throw new Exception("Line " + lineNumber +
                            " was meant to contain a number for the screen width");
                    }

                    line = sr.ReadLine();
                    try
                    {
                        dot.y = System.Convert.ToSingle(line);
                    }
                    catch
                    {
                        throw new Exception("Line " + lineNumber +
                            " was meant to contain a number for the screen height");
                    }
                    dots.Add(dot);
                }
                else if (line.Equals(SCALE_X_Y))
                {
                    line = sr.ReadLine();
                    try
                    {
                        scaleX = System.Convert.ToSingle(line);
                    }
                    catch
                    {
                        throw new Exception("Line " + lineNumber +
                            " was meant to contain a number for the x scale");
                    }

                    line = sr.ReadLine();
                    try
                    {
                        scaleY = System.Convert.ToSingle(line);
                    }
                    catch
                    {
                        throw new Exception("Line " + lineNumber +
                            " was meant to contain a number for the y scale");
                    }
                    fixedScale = true;
                }
            }
            if (height == 0 || width == 0)
                throw new Exception("No screen size specified. Please add \n" + SCREEN_X_Y + "\nx\ny\n\nto your config file");
            if (dots.Count == 0)
                throw new Exception("No dots specified. Please add at least one \n" + DOT_X_Y + "\nx\ny\n\nto your config file");

            // group the dots
            List<GroupableDot> screenDots = new List<GroupableDot>();
            for (int i = 0; i < dots.Count; i++)
            {
                GroupableDot dot = new GroupableDot();
                dot.x = (int)(dots[i].x / width * screenWidth);
                dot.y = (int)((1 - dots[i].y / height) * screenHeight);
                screenDots.Add(dot);
            }
            List<DotGroup> groupedDots = groupDots(screenDots, maxGroupingSize, maxGroups, new AdditionalGroupingInfo());

            // convert into a static environment
            env = new StaticEnvironment();
            optomise = new AdditionalGroupingInfo();
            int vertical = 0;
            int horizontal = 0;
            for(int i = 0; i < groupedDots.Count; i++)
            {
                if (i == 0)
                {
                    optomise.allSameSize = true;
                    optomise.size = groupedDots[i].Count;
                }
                else if (optomise.size != groupedDots[i].Count)
                {
                    optomise.allSameSize = false;
                }
                if (groupedDots[i].Count == 1)
                {
                    env.Add(new SingleDotSensor(new ScreenDot(groupedDots[i][0])));
                }
                else if (groupedDots[i].Count == 2)
                {
                    if (BasicDot.areVertical(groupedDots[i][0], groupedDots[i][1]))
                    {
                        env.Add(new VerticalSensorBar(new ScreenDot(groupedDots[i][0]), new ScreenDot(groupedDots[i][1])));
                        vertical++;
                    }
                    else
                    {
                        env.Add(new HorizontalSensorBar(new ScreenDot(groupedDots[i][0]), new ScreenDot(groupedDots[i][1])));
                        horizontal++;
                    }
                }
            }
            if (vertical == groupedDots.Count)
                optomise.allVertical = true;
            if (horizontal == groupedDots.Count)
                optomise.allHorizontal = true;
        }

        /**
         * Groups dots together in an attempt to identify sensor bars.
         */
        private List<DotGroup> groupDots(List<GroupableDot> dots, int maxGroupSize, int maxGroups, AdditionalGroupingInfo op)
        {
            List<DotSeperation> dotPairs = new List<DotSeperation>();
            List<DotGroup> groupedDots = new List<DotGroup>();
            int groups = 0;

            for(int i = 0; i < dots.Count; i++) 
            {
                DotGroup group = new DotGroup();
                group.Add(dots[i]);
                groupedDots.Add(group);
                dots[i].newGrouping = group;

                for(int j = i + 1; j < dots.Count; j++)
                {
                    dotPairs.Add(new DotSeperation(dots[i], dots[j]));
                }
            }

            dotPairs.Sort(delegate(DotSeperation ds1, DotSeperation ds2) { return (int)((ds1.seperation - ds2.seperation) * 1000); });
            
            bool done = false;
            while(
                dotPairs.Count > 0 && 
                (dotPairs[0].dot1.newGrouping.Count + dotPairs[0].dot2.newGrouping.Count) <= maxGroupingSize &&
                (maxGroups <= 0 || groups < maxGroups) &&
                !done)
            {
                bool vertical = BasicDot.areVertical(dotPairs[0].dot1, dotPairs[0].dot2);
                if (!(
                    (op.allHorizontal && vertical) || 
                    (op.allVertical && !vertical)))
                {
                    dotPairs[0].dot1.newGrouping.AddRange(dotPairs[0].dot2.newGrouping);
                    groupedDots.Remove(dotPairs[0].dot2.newGrouping);
                    dotPairs[0].dot2.newGrouping = dotPairs[0].dot1.newGrouping;
                    dotPairs.RemoveAt(0);
                    groups++;
                }
                else
                {
                    done = true;
                }
            }

            groupedDots.Sort(delegate(DotGroup dg1, DotGroup dg2) { return dg1.minX() - dg2.minX(); });
            return groupedDots;
        }

        void loadButtonConfigWindow_FileOk(object sender, CancelEventArgs cea)
        {
            try
            {
                motionButtonControl = new MotionAndButtonController(this.loadButtonConfigWindow.FileName, this);
                String buttonMenu = "Buttons: " + getFileName(this.loadButtonConfigWindow.FileName) + "...";
                BeginInvoke((MethodInvoker)delegate() { this.loadButtonConfigurationSystemMenuItem.Text = buttonMenu; });
                BeginInvoke((MethodInvoker)delegate() { this.loadButtonConfigurationToolStripMenuItem.Text = buttonMenu; });
            }
            catch (Exception e)
            {
                DialogueWindow warning = new DialogueWindow("Error");
                warning.setText(e.Message);
                warning.ShowDialog();
            }
        }

        public static String getFileName(String path)
        {
            char[] delim = new char[2];
            delim[0] = '.';
            delim[1] = '\\';

            String[] bits = path.Split(delim);
            if (bits.Length > 1)
                return bits[bits.Length - 2];
            else
                return path;
        }

        private void statusMenuItem_Click(object sender, EventArgs e)
        {
            this.Opacity = 100;
            this.Visible = true;
            this.ShowInTaskbar = true;
        }

        void MainForm_SizeChanged(object sender, EventArgs e)
        {
            intialiseGraphics();
            Invalidate();
        }

        void MainForm_Paint(object sender, PaintEventArgs e)
        {
            g.Clear(bgcolor);
            
            if (showRawDataToolStripMenuItem.Checked)
                drawRawData();

            if (showIRGroupingToolStripMenuItem.Checked)
                drawIRGrouping();

            if (showIRViewToolStripMenuItem.Checked)
                drawIRCamera();

            if (showSensorSetupToolStripMenuItem.Checked)
                drawSensorSetup();

            if (showOptomisationsToolStripMenuItem.Checked)
                drawOptomisations();

            if (showDynamicModelToolStripMenuItem.Checked)
                drawDynamicModel();

            if (showDotMappingsToolStripMenuItem.Checked)
                drawDotMappings();

            if (showCursorToolStripMenuItem.Checked)
                drawCursor();

            if (showIRToPixelFunctionsToolStripMenuItem.Checked)
                drawFunctions();

            if (showDynamicOffsetsToolStripMenuItem.Checked)
                drawDynamicOffsets();

            if (showRecoveryInformationToolStripMenuItem.Checked)
                drawRecoveryInformation();

            if (showErrorsAndWarningsToolStripMenuItem.Checked)
                drawErrorsAndWarnings();

            BeginInvoke((MethodInvoker)delegate() { pbIR.Image = b; });
        }

        private void drawErrorsAndWarnings()
        {
            String stripText = "";
            String errorText = "";
            String warningText = "";
            int errors = 0;
            int warnings = 0;

            // Errors:
            if(error_tooManyGroups)
            {
                errorText += " Too many IR groups, truncated.";
                errors++;
            }
            if(errors > 0)
                stripText = "Error(s):" + errorText;

            // Warnings:
            if(warning_moreThanTwoGroups)
            {
                warningText += " More than two IR groups.";
                warnings++;
            }
            if(warnings > 0)
                stripText += "Warning(s):" + warningText;


            BeginInvoke((MethodInvoker)delegate() { errorStripStatusLabel.Text = stripText; });
        }

        private void drawRecoveryInformation()
        {
            g.DrawString("Recoverable - " + recoverable, generalFont, generalBrush, recoveryPositionX, g.VisibleClipBounds.Height - (recoveryPositionY + (6 * recoveryLineHeightPx)));
            g.DrawString("Off Right - " + offRight, generalFont, generalBrush, recoveryPositionX, g.VisibleClipBounds.Height - (recoveryPositionY + (5 * recoveryLineHeightPx)));
            g.DrawString("Off Left - " + offLeft, generalFont, generalBrush, recoveryPositionX, g.VisibleClipBounds.Height - (recoveryPositionY + (4 * recoveryLineHeightPx)));
            g.DrawString("Off Top - " + offTop, generalFont, generalBrush, recoveryPositionX, g.VisibleClipBounds.Height - (recoveryPositionY + (3 * recoveryLineHeightPx)));
            g.DrawString("Off Bottom - " + offBottom, generalFont, generalBrush, recoveryPositionX, g.VisibleClipBounds.Height - (recoveryPositionY + (2 * recoveryLineHeightPx)));
            g.DrawString("Recovery Offset - " + recoveryOffset, generalFont, generalBrush, recoveryPositionX, g.VisibleClipBounds.Height - (recoveryPositionY + recoveryLineHeightPx));
        }

        private void drawCursor()
        {
            if (cursors != null && cursors.Count > 0)
            {
                try
                {
                    //String cursorsStr = "Cursors: "; //(" + cursorX + ", " + cursorY + ")";
                    CursorLocation cursor;
                    cursor.x = 0;
                    cursor.y = 0;
                    for (int i = 0; i < cursors.Count; i++)
                    {
                        g.DrawEllipse(new Pen(Color.Gray), (float)(cursors[i].x) / screenWidth * g.VisibleClipBounds.Width - 2, (float)(cursors[i].y) / screenHeight * g.VisibleClipBounds.Height - 2, 4, 4);
                        cursor.x += cursors[i].x;
                        cursor.y += cursors[i].y;
                        //cursorsStr += "(" + cursors[i].x + "," + cursors[i].y + ") ";
                    }
                    cursor.x = cursor.x / cursors.Count;
                    cursor.y = cursor.y / cursors.Count;
                    g.DrawEllipse(new Pen(Color.Red), (float)(cursor.x) / screenWidth * g.VisibleClipBounds.Width - 2, (float)(cursor.y) / screenHeight * g.VisibleClipBounds.Height - 2, 4, 4);
                    //g.DrawString(cursorsStr, generalFont, generalBrush, 0, g.VisibleClipBounds.Height - 15);
                    //g.DrawString((float)(cursor.x) / screenWidth + " " + (float)(cursor.y) / screenHeight, generalFont, generalBrush, 0, g.VisibleClipBounds.Height - 30);
                }
                catch (Exception)
                {
                }
            }
        }

        private void drawDotMappings()
        {
            if (dotMapping != null)
            {
                try
                {
                    g.DrawString("Mappings: ", generalFont, generalBrush, g.VisibleClipBounds.Width - mappingsInsetFromRight, 0);
                    for (int i = 0; i < dotMapping.Count; i++)
                    {
                        Brush brush;
                        if (dotMapping[i].irDot.active)
                            brush = generalBrush;
                        else
                            brush = disabledBrush;

                        g.DrawString(dotMapping[i].irDot.x + ", " + dotMapping[i].irDot.y, generalFont, brush, g.VisibleClipBounds.Width - mappingsInsetFromRight, (i + 1) * mappingsLineHeight);
                        g.DrawString("<=>  " + dotMapping[i].screenDot.x + ", " + dotMapping[i].screenDot.y, generalFont, brush, g.VisibleClipBounds.Width - mappingsInsetFromRight + mappingsFirstFigureWidth, (i + 1) * mappingsLineHeight);
                    }
                }
                catch (Exception)
                {
                    // caused by multithreading (probably). Easier to just catch and ignore
                }
            }
        }

        private void drawDynamicOffsets()
        {
            g.DrawString("Environment offset: " + envOffset, generalFont, generalBrush, dynamicOffsetsPositionX, dynamicOffsetsPositionY);
            if (offsetsPairs != null)
            {
                List<GroupOffsets> offsetCopy = offsetsPairs;

                String offsets = "Offsets:";
                for (int i = 0; i < offsetCopy.Count; i++)
                {
                    offsets += " (" + offsetCopy[i].newGroupOffset + ", " + offsetCopy[i].currentGroupOffset + ")";
                }
                g.DrawString(offsets, generalFont, generalBrush, dynamicOffsetsPositionX, dynamicOffsetsPositionY + dynamicOffsetsLineHeightPx);
            }
        }

        private void drawFunctions()
        {
            if (offsets != null)
            {
                try
                {
                    List<MappingOffsets> offsetCopy = offsets;
                    String offsetsX = "";
                    String offsetsY = "";

                    for (int i = 0; i < offsetCopy.Count; i++)
                    {
                        //g.DrawString(offsetCopy[i].x + "", generalFont, generalBrush, g.VisibleClipBounds.Width - 200, 150 + (i + 1) * 12);
                        //g.DrawString(offsetCopy[i].y + "", generalFont, generalBrush, g.VisibleClipBounds.Width - 200, 300 + (i + 1) * 12);
                        offsetsX += " " + offsetCopy[i].x;
                        offsetsY += " " + offsetCopy[i].y;
                    }

                    g.DrawString("X scale: " + scaleX, generalFont, generalBrush, g.VisibleClipBounds.Width - functionsInsetFromRight, g.VisibleClipBounds.Height - functionsXHeightFromBottom);
                    g.DrawString("X offsets:" + offsetsX, generalFont, generalBrush, g.VisibleClipBounds.Width - functionsInsetFromRight, g.VisibleClipBounds.Height - functionsXHeightFromBottom + functionsLineHeight);
                    g.DrawString("Y scale: " + scaleY, generalFont, generalBrush, g.VisibleClipBounds.Width - functionsInsetFromRight, g.VisibleClipBounds.Height - functionsYHeightFromBottom);
                    g.DrawString("Y offsets:" + offsetsY, generalFont, generalBrush, g.VisibleClipBounds.Width - functionsInsetFromRight, g.VisibleClipBounds.Height - functionsYHeightFromBottom + functionsLineHeight);
                    
                }
                catch (Exception)
                {
                    // caused by multithreading (probably). Easier to just catch and ignore
                }
            }
        }

        private void drawDynamicModel()
        {
            try
            {
                if (currentDotGrouping != null && env != null)
                {
                    List<DotGroup> currentDotGroupingCopy = currentDotGrouping;

                    int width = (int)(g.VisibleClipBounds.Width / (env.Count + 2));
                    int height = (int)(width * 0.7);

                    for (int i = 0; i < currentDotGroupingCopy.Count; i++)
                    {
                        int x = width * (i + 1 + envOffset) + 10;
                        int y = (int)((g.VisibleClipBounds.Height / 4) - (height / 2));

                        g.DrawRectangle(new Pen(Color.Blue), x, y, width - 20, height);
                        for (int j = 0; j < currentDotGroupingCopy[i].Count; j++)
                        {
                            if (currentDotGroupingCopy[i][j].active)
                                g.DrawEllipse(new Pen(Color.Red), x + ((float)(currentDotGroupingCopy[i][j].x) / WII_WIDTH) * (width - 20), y + ((float)(currentDotGroupingCopy[i][j].y) / WII_HEIGHT) * height, 4, 4);
                            else
                                g.DrawEllipse(new Pen(Color.Gray), x + ((float)(currentDotGroupingCopy[i][j].x) / WII_WIDTH) * (width - 20), y + ((float)(currentDotGroupingCopy[i][j].y) / WII_HEIGHT) * height, 4, 4);
                        }
                    }
                }
            }
            catch (Exception)
            {
                // do nothing
            }
        }

        private void drawOptomisations()
        {
            if (optomise != null)
            {
                g.DrawString("Characteristics:", generalFont, generalBrush, optPositionX, g.VisibleClipBounds.Height - (optPositionY + (4 * optLineHeightPx)));
                g.DrawString("All vertical - " + optomise.allVertical, generalFont, generalBrush, optPositionX, g.VisibleClipBounds.Height - (optPositionY + (3 * optLineHeightPx)));
                g.DrawString("All horizontal - " + optomise.allHorizontal, generalFont, generalBrush, optPositionX, g.VisibleClipBounds.Height - (optPositionY + (2 * optLineHeightPx)));
                if (optomise.allSameSize)
                    g.DrawString("All the same size: " + optomise.size, generalFont, generalBrush, optPositionX, g.VisibleClipBounds.Height - (optPositionY + optLineHeightPx));
                else
                    g.DrawString("All the same size - False", generalFont, generalBrush, optPositionX, g.VisibleClipBounds.Height - (optPositionY + optLineHeightPx));
            }
        }

        private void drawSensorSetup()
        {
            if (env != null)
            {
                for (int i = 0; i < env.Count ; i++)
                {
                    int minX = screenWidth;
                    int maxX = 0;
                    int minY = screenHeight;
                    int maxY = 0;

                    for (int j = 0; j < env[i].getDots().Count; j++)
                    {
                        ScreenDot dot = env[i].getDots()[j];
                        int x = (int)((dot.x / (float)screenWidth) * g.VisibleClipBounds.Width);
                        int y = (int)((dot.y / (float)screenHeight) * g.VisibleClipBounds.Height);

                        minX = System.Math.Min(x, minX);
                        maxX = System.Math.Max(x, maxX);
                        minY = System.Math.Min(y, minY);
                        maxY = System.Math.Max(y, maxY);

                        g.DrawEllipse(new Pen(Color.Red), x - sensorCircleRadius, y - sensorCircleRadius, (sensorCircleRadius * 2), (sensorCircleRadius * 2));
                    }

                    // draw a box around grouped dots
                    int groupWidth = maxX - minX + groupBoxSize * 2;
                    int groupHeight = maxY - minY + groupBoxSize * 2;
                    g.DrawRectangle(new Pen(Color.Blue), minX - groupBoxSize, minY - groupBoxSize, groupWidth, groupHeight);
                }
            }
        }

        private void drawRawData()
        {
            if (irData.Found1)
            {
                g.DrawString("1: " + irData.RawX1, generalFont, generalBrush, rawDataPositionX, rawDataPositionY);
                g.DrawString(irData.RawY1 + "", generalFont, generalBrush, rawDataPositionX + rawData2ndValueOffset, rawDataPositionY);
            }
            else
            {
                g.DrawString("1: " + NO_RAW_DATA, generalFont, generalBrush, rawDataPositionX, rawDataPositionY);
            }

            if (irData.Found2)
            {
                g.DrawString("2: " + irData.RawX2, generalFont, generalBrush, rawDataPositionX, rawDataPositionY + rawDataLineHeightPx);
                g.DrawString(irData.RawY2 + "", generalFont, generalBrush, rawDataPositionX + rawData2ndValueOffset, rawDataPositionY + rawDataLineHeightPx);
            }
            else
            {
                g.DrawString("2: " + NO_RAW_DATA, generalFont, generalBrush, rawDataPositionX, rawDataPositionY + rawDataLineHeightPx);
            }

            if (irData.Found3)
            {
                g.DrawString("3: " + irData.RawX3, generalFont, generalBrush, rawDataPositionX, rawDataPositionY + (2 * rawDataLineHeightPx));
                g.DrawString(irData.RawY3 + "", generalFont, generalBrush, rawDataPositionX + rawData2ndValueOffset, rawDataPositionY + (2 * rawDataLineHeightPx));
            }
            else
            {
                g.DrawString("3: " + NO_RAW_DATA, generalFont, generalBrush, rawDataPositionX, rawDataPositionY + (2 * rawDataLineHeightPx));
            }

            if (irData.Found4)
            {
                g.DrawString("4: " + irData.RawX4, generalFont, generalBrush, rawDataPositionX, rawDataPositionY + (3 * rawDataLineHeightPx));
                g.DrawString(irData.RawY4 + "", generalFont, generalBrush, rawDataPositionX + rawData2ndValueOffset, rawDataPositionY + (3 * rawDataLineHeightPx));
            }
            else
            {
                g.DrawString("4: " + NO_RAW_DATA, generalFont, generalBrush, rawDataPositionX, rawDataPositionY + (3 * rawDataLineHeightPx));
            }
        }

        private void drawIRCamera()
        {
            int irCamX = (int)((g.VisibleClipBounds.Width * irCamHorPosPercent) - (irCamWidth / 2));
            int irCamY = (int)((g.VisibleClipBounds.Height * irCamVertPosPercent) - (irCamHeight / 2));
            g.DrawRectangle(new Pen(irCamColor), irCamX, irCamY, irCamWidth, irCamHeight);
            
            if(irData.Found1)
                g.DrawEllipse(new Pen(irCamColor), ((float)(irData.RawX1) / WII_WIDTH) * irCamWidth + irCamX - irCamDotRadius, ((float)(irData.RawY1) / WII_HEIGHT) * irCamHeight + irCamY - irCamDotRadius, (irCamDotRadius * 2) * irData.Size1, (irCamDotRadius * 2) * irData.Size1);

            if (irData.Found2)
                g.DrawEllipse(new Pen(irCamColor), ((float)(irData.RawX2) / WII_WIDTH) * irCamWidth + irCamX - irCamDotRadius, ((float)(irData.RawY2) / WII_HEIGHT) * irCamHeight + irCamY - irCamDotRadius, (irCamDotRadius * 2) * irData.Size2, (irCamDotRadius * 2) * irData.Size2);

            if (irData.Found3)
                g.DrawEllipse(new Pen(irCamColor), ((float)(irData.RawX3) / WII_WIDTH) * irCamWidth + irCamX - irCamDotRadius, ((float)(irData.RawY3) / WII_HEIGHT) * irCamHeight + irCamY - irCamDotRadius, (irCamDotRadius * 2) * irData.Size3, (irCamDotRadius * 2) * irData.Size3);

            if (irData.Found4)
                g.DrawEllipse(new Pen(irCamColor), ((float)(irData.RawX4) / WII_WIDTH) * irCamWidth + irCamX - irCamDotRadius, ((float)(irData.RawY4) / WII_HEIGHT) * irCamHeight + irCamY - irCamDotRadius, (irCamDotRadius * 2) * irData.Size4, (irCamDotRadius * 2) * irData.Size4);
        }

        private void drawIRGrouping()
        {
            int irGroupingX = (int)((g.VisibleClipBounds.Width * irGroupingHorPosPercent) - (irGroupingWidth / 2));
            int irGroupingY = (int)((g.VisibleClipBounds.Height * irGroupingVertPosPercent) - (irGroupingHeight / 2));

            if(latestDotGrouping != null)
            {
                try
                {
                    g.DrawString("Number of Groups: " + latestDotGrouping.Count, generalFont, generalBrush, irGroupingX, irGroupingY + irGroupingHeight - 15);
                    g.DrawRectangle(new Pen(irGroupingColor), irGroupingX, irGroupingY, irGroupingWidth, irGroupingHeight);
                    for (int i = 0; i < latestDotGrouping.Count; i++)
                    {
                        int maxX = 0;
                        int minX = WII_WIDTH;
                        int maxY = 0;
                        int minY = WII_HEIGHT;

                        for (int j = 0; j < latestDotGrouping[i].Count; j++)
                        {
                            if (latestDotGrouping[i][j].active)
                            {
                                GroupableDot dot = latestDotGrouping[i][j];
                                maxX = Math.Max(maxX, dot.x);
                                minX = Math.Min(minX, dot.x);
                                maxY = Math.Max(maxY, dot.y);
                                minY = Math.Min(minY, dot.y);
                                g.DrawEllipse(new Pen(irGroupingColor), ((float)(dot.x) / WII_WIDTH) * irGroupingWidth + irGroupingX - irGroupingDotRadius, ((float)(dot.y) / WII_HEIGHT) * irGroupingHeight + irGroupingY - irGroupingDotRadius, irGroupingDotRadius * 2, irGroupingDotRadius * 2);
                            }
                        }
                        g.DrawRectangle(new Pen(irGroupingColor), ((float)(minX) / WII_WIDTH) * irGroupingWidth + irGroupingX - irGroupingDotRadius, ((float)(minY) / WII_HEIGHT) * irGroupingHeight + irGroupingY - irGroupingDotRadius, ((float)(maxX - minX) / WII_WIDTH) * irGroupingWidth + (irGroupingDotRadius * 2), ((float)(maxY - minY) / WII_HEIGHT) * irGroupingHeight + (irGroupingDotRadius * 2));
                    }
                }
                catch (Exception)
                {
                    // caused by multithreading (probably). Easier to just catch and ignore
                }
                
            }
        }

        private void moveCursorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            controlCursorToolStripMenuItem.Checked = moveCursorToolStripMenuItem.Checked;
        }

        private void controlCursorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            moveCursorToolStripMenuItem.Checked = controlCursorToolStripMenuItem.Checked;
        }

        private void rumbleToolStripMenu_Click(object sender, EventArgs e)
        {
            rumbleToolStripMenuItem.Checked = rumbleToolStripMenu.Checked;
        }

        private void rumbleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rumbleToolStripMenu.Checked = rumbleToolStripMenuItem.Checked;
        }

        private void loadIRConfigurationMenuItem_Click(object sender, EventArgs e)
        {
            this.loadIRConfigWindow.ShowDialog();
        }

        private void loadIRConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadIRConfigurationMenuItem_Click(sender, e);
        }

        private void loadButtonConfigurationMenuItem_Click(object sender, EventArgs e)
        {
            this.loadButtonConfigWindow.ShowDialog();
        }

        private void loadButtonConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadButtonConfigurationMenuItem_Click(sender, e);
        }

        private void exit_Click(object sender, EventArgs e)
        {
            realClose = true;
            this.Close();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            exit_Click(sender, e);
        }

        private void showErrorsAndWarningsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            errorStrip.Visible = showErrorsAndWarningsToolStripMenuItem.Checked;
            intialiseGraphics();
            Invalidate();
        }

        private void systemTrayIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            statusMenuItem_Click(sender, e);
        }

        private void stabiliseCursorSystemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stabiliseCursorToolStripMenuItem.Checked = stabiliseCursorSystemToolStripMenuItem.Checked;
        }

        private void stabiliseCursorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stabiliseCursorSystemToolStripMenuItem.Checked = stabiliseCursorToolStripMenuItem.Checked;
        }

        private void resetModelResetsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            environmentResets = 0;
            updateEnvResetMenu();
        }

        private void updateEnvResetMenu()
        {
            BeginInvoke((MethodInvoker)delegate() { resetModelResetsToolStripMenuItem.Text = ENV_RESET_MENU + " (" + environmentResets + ")"; });
        }

        private void resetRecenterCountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            recenters = 0;
            unneededRecenters = 0;
            updateRecenterMenu();
        }

        private void updateRecenterMenu()
        {
            BeginInvoke((MethodInvoker)delegate() { this.resetRecenterCountToolStripMenuItem.Text = RECENTER_RESET_MENU + " (" + unneededRecenters + "/" + recenters + ") (unneeded/total)"; });
        }

        public static void writeToLog(string Message)
        {
            System.IO.StreamWriter log = null;
            try
            {
                //if (log == null)
                log = System.IO.File.AppendText(AppDomain.CurrentDomain.BaseDirectory + "\\" + "wiimote_" + DateTime.Now.ToString("yyyy_MM_dd") + ".log");
                log.WriteLine(DateTime.Now.ToString("hh:mm:ss") + " " + Message);
                log.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        private void saveScaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m.WaitOne();
            if (irFilePath != null && fixedScale == false)
            {
                try
                {
                    fixedScale = true;
                    System.IO.StreamWriter save = System.IO.File.AppendText(irFilePath);
                    save.WriteLine("");
                    save.WriteLine("scale-x-y");
                    save.WriteLine(scaleX);
                    save.WriteLine(scaleY);
                    save.WriteLine("");
                    save.Close();
                }
                catch (Exception)
                {
                    DialogueWindow warning = new DialogueWindow("Error", this);
                    warning.setText("Unable to save the scales.");
                    warning.ShowDialog();
                }
            }
            m.ReleaseMutex();
        }

        private void saveScaleToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            saveScaleToolStripMenuItem_Click(sender, e);
        }
    }

    public class ConfigFileDot
    {
        public float x, y;
    }
    
    public class BasicDot
    {
        public int x, y;

        public bool above(BasicDot bd)
        {
            return y < bd.y;
        }

        public bool toLeft(BasicDot bd)
        {
            return x < bd.x;
        }

        public static bool areVertical(BasicDot bd1, BasicDot bd2)
        {
            return Math.Abs(bd1.y - bd2.y) > Math.Abs(bd1.x - bd2.x);
        }
    }

    public class ScreenDot : BasicDot
    {
        public ScreenDot() {}

        public ScreenDot(BasicDot bd)
        {
            x = bd.x;
            y = bd.y;
        }
    }

    public class GroupableDot : BasicDot
    {
        // reference to the new DotGroup
        public DotGroup newGrouping;

        // reference to the current DotGroup of the currentDotGrouping
        public DotGroup currentGrouping;

        public bool active = true;
    }

    public class DotGroup : List<GroupableDot>
    {

        public int minX()
        {
            int min = this[0].x;
            for (int i = 1; i < Count; i++)
                min = Math.Min(min, this[i].x);
            return min;
        }
    }

    public class DotMatch
    {
        public ScreenDot screenDot;
        public GroupableDot irDot;
        public bool reliable = false;
    }

    public abstract class StaticSensor
    {
        protected List<ScreenDot> dots = new List<ScreenDot>();
        public abstract String getName();
        public abstract bool accepts(DotGroup group);
        public abstract void computeMatches(DotGroup group, List<DotMatch> matches);
        
        public List<ScreenDot> getDots()
        {
            return dots;
        }
    }

    public class SingleDotSensor : StaticSensor
    {
        public ScreenDot dot;

        public SingleDotSensor(ScreenDot d)
        {
            dot = d;
            dots.Add(d);
        }

        public override bool accepts(DotGroup group)
        {
            return group.Count <= 1;
        }

        public override void computeMatches(DotGroup group, List<DotMatch> matches)
        {
            if (group.Count == 1)
            {
                for (int i = matches.Count - 1; i >= 0; i--)
                    if (matches[i].irDot == group[0])
                        matches.RemoveAt(i);

                DotMatch match = new DotMatch();
                match.irDot = group[0];
                match.screenDot = dot;
                match.reliable = true;
                matches.Add(match);
            }
        }

        public override String getName()
        {
            return "Single Dot Sensor";
        }
    }

    public class VerticalSensorBar : StaticSensor
    {
        public ScreenDot topDot, bottomDot;

        public VerticalSensorBar(ScreenDot sd1, ScreenDot sd2)
        {
            if(sd1.above(sd2))
            {
                topDot = sd1;
                bottomDot = sd2;
            }
            else
            {
                topDot = sd2;
                bottomDot = sd1;
            }
            dots.Add(sd1);
            dots.Add(sd2);
        }

        public override bool accepts(DotGroup group)
        {
            if (group.Count == 0 || group.Count == 1)
            {
                return true;
            }
            else if (group.Count == 2)
            {
                return BasicDot.areVertical(group[0], group[1]);
            }
            else
            {
                return false;
            }
        }

        public override void computeMatches(DotGroup group, List<DotMatch> matches)
        {
            if (group.Count == 1)
            {
                bool alreadyMatchedReliably = false;
                for (int i = 0; i < matches.Count; i++)
                    if (matches[i].irDot == group[0] && matches[i].reliable)
                        alreadyMatchedReliably = true;

                if (!alreadyMatchedReliably)
                {
                    DotMatch match = new DotMatch();
                    match.irDot = group[0];
                    if (group[0].y > (MainForm.WII_HEIGHT / 2))
                        match.screenDot = topDot;
                    else
                        match.screenDot = bottomDot;
                    matches.Add(match);
                }
            }
            else if (group.Count == 2)
            {
                for (int i = matches.Count - 1; i >= 0; i--)
                    if (matches[i].irDot == group[0] || matches[i].irDot == group[1])
                        matches.RemoveAt(i);

                DotMatch topMatch = new DotMatch();
                topMatch.screenDot = topDot;

                DotMatch bottomMatch = new DotMatch();
                bottomMatch.screenDot = bottomDot;

                if (group[0].y < group[1].y)
                {
                    topMatch.irDot = group[0];
                    bottomMatch.irDot = group[1];
                }
                else
                {
                    topMatch.irDot = group[1];
                    bottomMatch.irDot = group[0];
                }
                topMatch.reliable = true;
                bottomMatch.reliable = true;

                matches.Add(topMatch);
                matches.Add(bottomMatch);
            }
        }

        public override String getName()
        {
            return "Vertical Sensor";
        }
    }

    public class HorizontalSensorBar : StaticSensor
    {
        public ScreenDot leftDot, rightDot;

        public HorizontalSensorBar(ScreenDot sd1, ScreenDot sd2)
        {
            if(sd1.toLeft(sd2))
            {
                leftDot = sd1;
                rightDot = sd2;
            }
            else
            {
                leftDot = sd2;
                rightDot = sd1;
            }
            dots.Add(sd1);
            dots.Add(sd2);
        }

        public override bool accepts(DotGroup group)
        {
            if (group.Count == 0 || group.Count == 1)
            {
                return true;
            }
            else if (group.Count == 2)
            {
                return !BasicDot.areVertical(group[0], group[1]);
            }
            else
            {
                return false;
            }
        }

        public override void computeMatches(DotGroup group, List<DotMatch> matches)
        {
            if (group.Count == 1)
            {
                bool alreadyMatchedReliably = false;
                for (int i = 0; i < matches.Count; i++)
                    if (matches[i].irDot == group[0] && matches[i].reliable)
                        alreadyMatchedReliably = true;

                if (!alreadyMatchedReliably)
                {
                    DotMatch match = new DotMatch();
                    match.irDot = group[0];
                    if (group[0].x < (MainForm.WII_WIDTH / 2))
                        match.screenDot = rightDot;
                    else
                        match.screenDot = leftDot;
                    matches.Add(match);
                }
            }
            else if (group.Count == 2)
            {
                for (int i = matches.Count - 1; i >= 0; i--)
                    if (matches[i].irDot == group[0] || matches[i].irDot == group[1])
                        matches.RemoveAt(i);

                DotMatch leftMatch = new DotMatch();
                leftMatch.screenDot = leftDot;

                DotMatch rightMatch = new DotMatch();
                rightMatch.screenDot = rightDot;

                if (group[0].x < group[1].x)
                {
                    leftMatch.irDot = group[0];
                    rightMatch.irDot = group[1];
                }
                else
                {
                    leftMatch.irDot = group[1];
                    rightMatch.irDot = group[0];
                }

                leftMatch.reliable = true;
                rightMatch.reliable = true;

                matches.Add(leftMatch);
                matches.Add(rightMatch);
            }
        }

        public override String getName()
        {
            return "Horizontal Sensor";
        }
    }

    public class StaticEnvironment : List<StaticSensor>
    {
        // name wrapper
    }

    public class AdditionalGroupingInfo
    {
        public bool allVertical = false;
        public bool allHorizontal = false;
        public bool allSameSize = false;
        public int size;
    }

    public class DotSeperation
    {
        public GroupableDot dot1, dot2;
        public float seperation;

        public DotSeperation(GroupableDot d1, GroupableDot d2)
        {
            dot1 = d1;
            dot2 = d2;
            seperation = (float)(Math.Sqrt(Math.Pow(Math.Abs((double)(d1.x - d2.x)), 2) + Math.Pow(Math.Abs((double)(d1.y - d2.y)), 2)));
        }
    }

    public class MappingOffsets
    {
        public int x;
        public int y;
    }
}