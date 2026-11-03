namespace WiimoteMouse
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.systemTrayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.systemTrayMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.controlCursorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stabiliseCursorSystemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rumbleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.statusMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.extensionMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.batteryMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.loadIRConfigurationMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadButtonConfigurationSystemMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveScaleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadButtonConfigWindow = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadIRConfigurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadButtonConfigurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
            this.saveScaleToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mouseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveCursorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stabiliseCursorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.rumbleToolStripMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showIRViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showIRGroupingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.showSensorSetupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showOptomisationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.showDynamicModelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.showCursorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showErrorsAndWarningsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showRawDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.showDotMappingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showIRToPixelFunctionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.showDynamicOffsetsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showRecoveryInformationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.resetModelResetsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetRecenterCountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadIRConfigWindow = new System.Windows.Forms.OpenFileDialog();
            this.errorStrip = new System.Windows.Forms.StatusStrip();
            this.errorStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.pbIR = new System.Windows.Forms.PictureBox();
            this.toolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
            this.systemTrayMenu.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.errorStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbIR)).BeginInit();
            this.SuspendLayout();
            // 
            // systemTrayIcon
            // 
            this.systemTrayIcon.ContextMenuStrip = this.systemTrayMenu;
            this.systemTrayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("systemTrayIcon.Icon")));
            this.systemTrayIcon.Text = "Wiimote Mouse";
            this.systemTrayIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.systemTrayIcon_MouseDoubleClick);
            // 
            // systemTrayMenu
            // 
            this.systemTrayMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.controlCursorToolStripMenuItem,
            this.stabiliseCursorSystemToolStripMenuItem,
            this.rumbleToolStripMenuItem,
            this.toolStripSeparator10,
            this.statusMenuItem,
            this.toolStripSeparator5,
            this.extensionMenuItem,
            this.batteryMenuItem,
            this.toolStripSeparator4,
            this.loadIRConfigurationMenuItem,
            this.loadButtonConfigurationSystemMenuItem,
            this.toolStripSeparator14,
            this.saveScaleToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitMenuItem});
            this.systemTrayMenu.Name = "systemTrayMenu";
            this.systemTrayMenu.Size = new System.Drawing.Size(224, 254);
            // 
            // controlCursorToolStripMenuItem
            // 
            this.controlCursorToolStripMenuItem.Checked = true;
            this.controlCursorToolStripMenuItem.CheckOnClick = true;
            this.controlCursorToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.controlCursorToolStripMenuItem.Name = "controlCursorToolStripMenuItem";
            this.controlCursorToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.controlCursorToolStripMenuItem.Text = "Control Cursor";
            this.controlCursorToolStripMenuItem.Click += new System.EventHandler(this.controlCursorToolStripMenuItem_Click);
            // 
            // stabiliseCursorSystemToolStripMenuItem
            // 
            this.stabiliseCursorSystemToolStripMenuItem.Checked = true;
            this.stabiliseCursorSystemToolStripMenuItem.CheckOnClick = true;
            this.stabiliseCursorSystemToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.stabiliseCursorSystemToolStripMenuItem.Name = "stabiliseCursorSystemToolStripMenuItem";
            this.stabiliseCursorSystemToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.stabiliseCursorSystemToolStripMenuItem.Text = "Stabilise Cursor";
            this.stabiliseCursorSystemToolStripMenuItem.Click += new System.EventHandler(this.stabiliseCursorSystemToolStripMenuItem_Click);
            // 
            // rumbleToolStripMenuItem
            // 
            this.rumbleToolStripMenuItem.Checked = true;
            this.rumbleToolStripMenuItem.CheckOnClick = true;
            this.rumbleToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.rumbleToolStripMenuItem.Name = "rumbleToolStripMenuItem";
            this.rumbleToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.rumbleToolStripMenuItem.Text = "Rumble";
            this.rumbleToolStripMenuItem.Click += new System.EventHandler(this.rumbleToolStripMenuItem_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(220, 6);
            // 
            // statusMenuItem
            // 
            this.statusMenuItem.Name = "statusMenuItem";
            this.statusMenuItem.Size = new System.Drawing.Size(223, 22);
            this.statusMenuItem.Text = "Show Status";
            this.statusMenuItem.Click += new System.EventHandler(this.statusMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(220, 6);
            // 
            // extensionMenuItem
            // 
            this.extensionMenuItem.Enabled = false;
            this.extensionMenuItem.Name = "extensionMenuItem";
            this.extensionMenuItem.Size = new System.Drawing.Size(223, 22);
            this.extensionMenuItem.Text = "extension type";
            // 
            // batteryMenuItem
            // 
            this.batteryMenuItem.Enabled = false;
            this.batteryMenuItem.Name = "batteryMenuItem";
            this.batteryMenuItem.Size = new System.Drawing.Size(223, 22);
            this.batteryMenuItem.Text = "battery level";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(220, 6);
            // 
            // loadIRConfigurationMenuItem
            // 
            this.loadIRConfigurationMenuItem.Name = "loadIRConfigurationMenuItem";
            this.loadIRConfigurationMenuItem.Size = new System.Drawing.Size(223, 22);
            this.loadIRConfigurationMenuItem.Text = "Load IR Configuration...";
            this.loadIRConfigurationMenuItem.Click += new System.EventHandler(this.loadIRConfigurationMenuItem_Click);
            // 
            // loadButtonConfigurationSystemMenuItem
            // 
            this.loadButtonConfigurationSystemMenuItem.Name = "loadButtonConfigurationSystemMenuItem";
            this.loadButtonConfigurationSystemMenuItem.Size = new System.Drawing.Size(223, 22);
            this.loadButtonConfigurationSystemMenuItem.Text = "Load Button Configuration...";
            this.loadButtonConfigurationSystemMenuItem.Click += new System.EventHandler(this.loadButtonConfigurationMenuItem_Click);
            // 
            // saveScaleToolStripMenuItem
            // 
            this.saveScaleToolStripMenuItem.Name = "saveScaleToolStripMenuItem";
            this.saveScaleToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.saveScaleToolStripMenuItem.Text = "Save Scale";
            this.saveScaleToolStripMenuItem.Click += new System.EventHandler(this.saveScaleToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(220, 6);
            // 
            // exitMenuItem
            // 
            this.exitMenuItem.Name = "exitMenuItem";
            this.exitMenuItem.Size = new System.Drawing.Size(223, 22);
            this.exitMenuItem.Text = "Exit";
            this.exitMenuItem.Click += new System.EventHandler(this.exit_Click);
            // 
            // loadButtonConfigWindow
            // 
            this.loadButtonConfigWindow.FileName = "configFile";
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.mouseToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.debugToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(692, 24);
            this.menuStrip.TabIndex = 13;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadIRConfigurationToolStripMenuItem,
            this.loadButtonConfigurationToolStripMenuItem,
            this.toolStripSeparator13,
            this.saveScaleToolStripMenuItem1,
            this.toolStripSeparator11,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadIRConfigurationToolStripMenuItem
            // 
            this.loadIRConfigurationToolStripMenuItem.Name = "loadIRConfigurationToolStripMenuItem";
            this.loadIRConfigurationToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.loadIRConfigurationToolStripMenuItem.Text = "Load IR Configuration...";
            this.loadIRConfigurationToolStripMenuItem.Click += new System.EventHandler(this.loadIRConfigurationToolStripMenuItem_Click);
            // 
            // loadButtonConfigurationToolStripMenuItem
            // 
            this.loadButtonConfigurationToolStripMenuItem.Name = "loadButtonConfigurationToolStripMenuItem";
            this.loadButtonConfigurationToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.loadButtonConfigurationToolStripMenuItem.Text = "Load Button Configuration...";
            this.loadButtonConfigurationToolStripMenuItem.Click += new System.EventHandler(this.loadButtonConfigurationToolStripMenuItem_Click);
            // 
            // toolStripSeparator13
            // 
            this.toolStripSeparator13.Name = "toolStripSeparator13";
            this.toolStripSeparator13.Size = new System.Drawing.Size(220, 6);
            // 
            // saveScaleToolStripMenuItem1
            // 
            this.saveScaleToolStripMenuItem1.Name = "saveScaleToolStripMenuItem1";
            this.saveScaleToolStripMenuItem1.Size = new System.Drawing.Size(223, 22);
            this.saveScaleToolStripMenuItem1.Text = "Save Scale";
            this.saveScaleToolStripMenuItem1.Click += new System.EventHandler(this.saveScaleToolStripMenuItem1_Click);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(220, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // mouseToolStripMenuItem
            // 
            this.mouseToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.moveCursorToolStripMenuItem,
            this.stabiliseCursorToolStripMenuItem,
            this.toolStripSeparator12,
            this.rumbleToolStripMenu});
            this.mouseToolStripMenuItem.Name = "mouseToolStripMenuItem";
            this.mouseToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.mouseToolStripMenuItem.Text = "Mouse";
            // 
            // moveCursorToolStripMenuItem
            // 
            this.moveCursorToolStripMenuItem.Checked = true;
            this.moveCursorToolStripMenuItem.CheckOnClick = true;
            this.moveCursorToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.moveCursorToolStripMenuItem.Name = "moveCursorToolStripMenuItem";
            this.moveCursorToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.moveCursorToolStripMenuItem.Text = "Control Cursor";
            this.moveCursorToolStripMenuItem.Click += new System.EventHandler(this.moveCursorToolStripMenuItem_Click);
            // 
            // stabiliseCursorToolStripMenuItem
            // 
            this.stabiliseCursorToolStripMenuItem.Checked = true;
            this.stabiliseCursorToolStripMenuItem.CheckOnClick = true;
            this.stabiliseCursorToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.stabiliseCursorToolStripMenuItem.Name = "stabiliseCursorToolStripMenuItem";
            this.stabiliseCursorToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.stabiliseCursorToolStripMenuItem.Text = "Stabilise Cursor";
            this.stabiliseCursorToolStripMenuItem.Click += new System.EventHandler(this.stabiliseCursorToolStripMenuItem_Click);
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size(156, 6);
            // 
            // rumbleToolStripMenu
            // 
            this.rumbleToolStripMenu.Checked = true;
            this.rumbleToolStripMenu.CheckOnClick = true;
            this.rumbleToolStripMenu.CheckState = System.Windows.Forms.CheckState.Checked;
            this.rumbleToolStripMenu.Name = "rumbleToolStripMenu";
            this.rumbleToolStripMenu.Size = new System.Drawing.Size(159, 22);
            this.rumbleToolStripMenu.Text = "Rumble";
            this.rumbleToolStripMenu.Click += new System.EventHandler(this.rumbleToolStripMenu_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showIRViewToolStripMenuItem,
            this.showIRGroupingToolStripMenuItem,
            this.toolStripSeparator6,
            this.showSensorSetupToolStripMenuItem,
            this.showOptomisationsToolStripMenuItem,
            this.toolStripSeparator3,
            this.showDynamicModelToolStripMenuItem,
            this.toolStripSeparator7,
            this.showCursorToolStripMenuItem,
            this.showErrorsAndWarningsToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // showIRViewToolStripMenuItem
            // 
            this.showIRViewToolStripMenuItem.CheckOnClick = true;
            this.showIRViewToolStripMenuItem.Name = "showIRViewToolStripMenuItem";
            this.showIRViewToolStripMenuItem.Size = new System.Drawing.Size(252, 22);
            this.showIRViewToolStripMenuItem.Text = "Show IR Camera";
            // 
            // showIRGroupingToolStripMenuItem
            // 
            this.showIRGroupingToolStripMenuItem.Checked = true;
            this.showIRGroupingToolStripMenuItem.CheckOnClick = true;
            this.showIRGroupingToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showIRGroupingToolStripMenuItem.Name = "showIRGroupingToolStripMenuItem";
            this.showIRGroupingToolStripMenuItem.Size = new System.Drawing.Size(252, 22);
            this.showIRGroupingToolStripMenuItem.Text = "Show IR Grouping";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(249, 6);
            // 
            // showSensorSetupToolStripMenuItem
            // 
            this.showSensorSetupToolStripMenuItem.Checked = true;
            this.showSensorSetupToolStripMenuItem.CheckOnClick = true;
            this.showSensorSetupToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showSensorSetupToolStripMenuItem.Name = "showSensorSetupToolStripMenuItem";
            this.showSensorSetupToolStripMenuItem.Size = new System.Drawing.Size(252, 22);
            this.showSensorSetupToolStripMenuItem.Text = "Show Sensor Setup";
            // 
            // showOptomisationsToolStripMenuItem
            // 
            this.showOptomisationsToolStripMenuItem.CheckOnClick = true;
            this.showOptomisationsToolStripMenuItem.Name = "showOptomisationsToolStripMenuItem";
            this.showOptomisationsToolStripMenuItem.Size = new System.Drawing.Size(252, 22);
            this.showOptomisationsToolStripMenuItem.Text = "Show Sensor Setup Characteristics";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(249, 6);
            // 
            // showDynamicModelToolStripMenuItem
            // 
            this.showDynamicModelToolStripMenuItem.Checked = true;
            this.showDynamicModelToolStripMenuItem.CheckOnClick = true;
            this.showDynamicModelToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showDynamicModelToolStripMenuItem.Name = "showDynamicModelToolStripMenuItem";
            this.showDynamicModelToolStripMenuItem.Size = new System.Drawing.Size(252, 22);
            this.showDynamicModelToolStripMenuItem.Text = "Show Dynamic Model";
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(249, 6);
            // 
            // showCursorToolStripMenuItem
            // 
            this.showCursorToolStripMenuItem.Checked = true;
            this.showCursorToolStripMenuItem.CheckOnClick = true;
            this.showCursorToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showCursorToolStripMenuItem.Name = "showCursorToolStripMenuItem";
            this.showCursorToolStripMenuItem.Size = new System.Drawing.Size(252, 22);
            this.showCursorToolStripMenuItem.Text = "Show Cursor";
            // 
            // showErrorsAndWarningsToolStripMenuItem
            // 
            this.showErrorsAndWarningsToolStripMenuItem.CheckOnClick = true;
            this.showErrorsAndWarningsToolStripMenuItem.Name = "showErrorsAndWarningsToolStripMenuItem";
            this.showErrorsAndWarningsToolStripMenuItem.Size = new System.Drawing.Size(252, 22);
            this.showErrorsAndWarningsToolStripMenuItem.Text = "Show Errors and Warnings";
            this.showErrorsAndWarningsToolStripMenuItem.Click += new System.EventHandler(this.showErrorsAndWarningsToolStripMenuItem_Click);
            // 
            // debugToolStripMenuItem
            // 
            this.debugToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showRawDataToolStripMenuItem,
            this.toolStripSeparator8,
            this.showDotMappingsToolStripMenuItem,
            this.showIRToPixelFunctionsToolStripMenuItem,
            this.toolStripSeparator9,
            this.showDynamicOffsetsToolStripMenuItem,
            this.showRecoveryInformationToolStripMenuItem,
            this.toolStripSeparator1,
            this.resetModelResetsToolStripMenuItem,
            this.resetRecenterCountToolStripMenuItem});
            this.debugToolStripMenuItem.Name = "debugToolStripMenuItem";
            this.debugToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.debugToolStripMenuItem.Text = "Debug";
            // 
            // showRawDataToolStripMenuItem
            // 
            this.showRawDataToolStripMenuItem.CheckOnClick = true;
            this.showRawDataToolStripMenuItem.Name = "showRawDataToolStripMenuItem";
            this.showRawDataToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.showRawDataToolStripMenuItem.Text = "Show Raw IR Data";
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(216, 6);
            // 
            // showDotMappingsToolStripMenuItem
            // 
            this.showDotMappingsToolStripMenuItem.CheckOnClick = true;
            this.showDotMappingsToolStripMenuItem.Name = "showDotMappingsToolStripMenuItem";
            this.showDotMappingsToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.showDotMappingsToolStripMenuItem.Text = "Show IR to Pixel Mappings";
            // 
            // showIRToPixelFunctionsToolStripMenuItem
            // 
            this.showIRToPixelFunctionsToolStripMenuItem.CheckOnClick = true;
            this.showIRToPixelFunctionsToolStripMenuItem.Name = "showIRToPixelFunctionsToolStripMenuItem";
            this.showIRToPixelFunctionsToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.showIRToPixelFunctionsToolStripMenuItem.Text = "Show IR to Pixel Functions";
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(216, 6);
            // 
            // showDynamicOffsetsToolStripMenuItem
            // 
            this.showDynamicOffsetsToolStripMenuItem.CheckOnClick = true;
            this.showDynamicOffsetsToolStripMenuItem.Name = "showDynamicOffsetsToolStripMenuItem";
            this.showDynamicOffsetsToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.showDynamicOffsetsToolStripMenuItem.Text = "Show Dynamic Offsets";
            // 
            // showRecoveryInformationToolStripMenuItem
            // 
            this.showRecoveryInformationToolStripMenuItem.CheckOnClick = true;
            this.showRecoveryInformationToolStripMenuItem.Name = "showRecoveryInformationToolStripMenuItem";
            this.showRecoveryInformationToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.showRecoveryInformationToolStripMenuItem.Text = "Show Recovery Information";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(216, 6);
            // 
            // resetModelResetsToolStripMenuItem
            // 
            this.resetModelResetsToolStripMenuItem.Name = "resetModelResetsToolStripMenuItem";
            this.resetModelResetsToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.resetModelResetsToolStripMenuItem.Text = "Reset Model Resets";
            this.resetModelResetsToolStripMenuItem.Click += new System.EventHandler(this.resetModelResetsToolStripMenuItem_Click);
            // 
            // resetRecenterCountToolStripMenuItem
            // 
            this.resetRecenterCountToolStripMenuItem.Name = "resetRecenterCountToolStripMenuItem";
            this.resetRecenterCountToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.resetRecenterCountToolStripMenuItem.Text = "Reset Recenter Count";
            this.resetRecenterCountToolStripMenuItem.Click += new System.EventHandler(this.resetRecenterCountToolStripMenuItem_Click);
            // 
            // loadIRConfigWindow
            // 
            this.loadIRConfigWindow.FileName = "configFile";
            // 
            // errorStrip
            // 
            this.errorStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.errorStripStatusLabel});
            this.errorStrip.Location = new System.Drawing.Point(0, 401);
            this.errorStrip.Name = "errorStrip";
            this.errorStrip.Size = new System.Drawing.Size(692, 22);
            this.errorStrip.TabIndex = 14;
            this.errorStrip.Tag = "";
            this.errorStrip.Text = "statusStrip1";
            this.errorStrip.Visible = false;
            // 
            // errorStripStatusLabel
            // 
            this.errorStripStatusLabel.Name = "errorStripStatusLabel";
            this.errorStripStatusLabel.Size = new System.Drawing.Size(109, 17);
            this.errorStripStatusLabel.Text = "toolStripStatusLabel1";
            // 
            // pbIR
            // 
            this.pbIR.Location = new System.Drawing.Point(0, 24);
            this.pbIR.Margin = new System.Windows.Forms.Padding(0);
            this.pbIR.Name = "pbIR";
            this.pbIR.Size = new System.Drawing.Size(400, 300);
            this.pbIR.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbIR.TabIndex = 15;
            this.pbIR.TabStop = false;
            // 
            // toolStripSeparator14
            // 
            this.toolStripSeparator14.Name = "toolStripSeparator14";
            this.toolStripSeparator14.Size = new System.Drawing.Size(220, 6);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 423);
            this.Controls.Add(this.pbIR);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.errorStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.ShowInTaskbar = false;
            this.Text = "Wiimote Mouse";
            this.systemTrayMenu.ResumeLayout(false);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.errorStrip.ResumeLayout(false);
            this.errorStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbIR)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon systemTrayIcon;
        private System.Windows.Forms.ContextMenuStrip systemTrayMenu;
        private System.Windows.Forms.ToolStripMenuItem exitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadIRConfigurationMenuItem;
        private System.Windows.Forms.OpenFileDialog loadButtonConfigWindow;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem loadButtonConfigurationSystemMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showIRViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem statusMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem extensionMenuItem;
        private System.Windows.Forms.ToolStripMenuItem batteryMenuItem;
        private System.Windows.Forms.OpenFileDialog loadIRConfigWindow;
        private System.Windows.Forms.ToolStripMenuItem showSensorSetupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showOptomisationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showIRGroupingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showDynamicModelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showCursorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mouseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveCursorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem debugToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showRawDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showDotMappingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showIRToPixelFunctionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showDynamicOffsetsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripMenuItem controlCursorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rumbleToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripMenuItem rumbleToolStripMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadIRConfigurationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadButtonConfigurationToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showRecoveryInformationToolStripMenuItem;
        private System.Windows.Forms.StatusStrip errorStrip;
        private System.Windows.Forms.ToolStripMenuItem showErrorsAndWarningsToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel errorStripStatusLabel;
        private System.Windows.Forms.ToolStripMenuItem stabiliseCursorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stabiliseCursorSystemToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem resetModelResetsToolStripMenuItem;
        private System.Windows.Forms.PictureBox pbIR;
        private System.Windows.Forms.ToolStripMenuItem resetRecenterCountToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveScaleToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
        private System.Windows.Forms.ToolStripMenuItem saveScaleToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator14;
    }
}

