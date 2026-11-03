//////////////////////////////////////////////////////////////////////////////////
//	Form1.cs
//	Managed Wiimote Library Tester
//	Written by Brian Peek (http://www.brianpeek.com/)
//  for MSDN's Coding4Fun (http://msdn.microsoft.com/coding4fun/)
//  Visit http://msdn.microsoft.com/coding4fun/hardware/article.aspx?articleid=1879033
//  for more information
//////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

using WiimoteLib;

namespace WiimoteTest
{
	public partial class Form1 : Form
	{
		Wiimote wm = new Wiimote();
		Bitmap b = new Bitmap(256, 192, PixelFormat.Format24bppRgb);
		Graphics g;
		Mutex m = new Mutex();

		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			wm.OnWiimoteChanged += new WiimoteChangedEventHandler(wm_OnWiimoteChanged);
			wm.OnWiimoteExtensionChanged += new WiimoteExtensionChanged(wm_OnWiimoteExtensionChanged);
			g = Graphics.FromImage(b);
			wm.Connect();
			wm.SetReportType(Wiimote.InputReport.IRAccel, true);
			wm.SetLEDs(false, false, false, false);
			wm.GetBatteryLevel();
		}

		void wm_OnWiimoteExtensionChanged(object sender, WiimoteExtensionChangedEventArgs args)
		{
			BeginInvoke((MethodInvoker)delegate(){chkExtension.Text = args.ExtensionType.ToString();});
			BeginInvoke((MethodInvoker)delegate(){chkExtension.Checked = args.Inserted;});

			if(args.Inserted)
				wm.SetReportType(Wiimote.InputReport.IRExtensionAccel, true);
			else
				wm.SetReportType(Wiimote.InputReport.IRAccel, true);
		}

		void wm_OnWiimoteChanged(object sender, WiimoteChangedEventArgs args)
		{
			m.WaitOne();

			WiimoteState ws = args.WiimoteState;

			BeginInvoke((MethodInvoker)delegate(){clbButtons.SetItemChecked(0, ws.ButtonState.A);});
			BeginInvoke((MethodInvoker)delegate(){clbButtons.SetItemChecked(1, ws.ButtonState.B);});
			BeginInvoke((MethodInvoker)delegate(){clbButtons.SetItemChecked(2, ws.ButtonState.Minus);});
			BeginInvoke((MethodInvoker)delegate(){clbButtons.SetItemChecked(3, ws.ButtonState.Home);});
			BeginInvoke((MethodInvoker)delegate(){clbButtons.SetItemChecked(4, ws.ButtonState.Plus);});
			BeginInvoke((MethodInvoker)delegate(){clbButtons.SetItemChecked(5, ws.ButtonState.One);});
			BeginInvoke((MethodInvoker)delegate(){clbButtons.SetItemChecked(6, ws.ButtonState.Two);});
			BeginInvoke((MethodInvoker)delegate(){clbButtons.SetItemChecked(7, ws.ButtonState.Up);});
			BeginInvoke((MethodInvoker)delegate(){clbButtons.SetItemChecked(8, ws.ButtonState.Down);});
			BeginInvoke((MethodInvoker)delegate(){clbButtons.SetItemChecked(9, ws.ButtonState.Left);});
			BeginInvoke((MethodInvoker)delegate(){clbButtons.SetItemChecked(10, ws.ButtonState.Right);});
			BeginInvoke((MethodInvoker)delegate(){clbButtons.SetItemChecked(11, ws.NunchukState.C);});
			BeginInvoke((MethodInvoker)delegate(){clbButtons.SetItemChecked(12, ws.NunchukState.Z);});

			BeginInvoke((MethodInvoker)delegate(){lblX.Text = ws.AccelState.X.ToString();});
			BeginInvoke((MethodInvoker)delegate(){lblY.Text = ws.AccelState.Y.ToString();});
			BeginInvoke((MethodInvoker)delegate(){lblZ.Text = ws.AccelState.Z.ToString();});

			if(ws.ExtensionType == ExtensionType.Nunchuk)
			{
				BeginInvoke((MethodInvoker)delegate(){lblChukX.Text = ws.NunchukState.AccelState.X.ToString();});
				BeginInvoke((MethodInvoker)delegate(){lblChukY.Text = ws.NunchukState.AccelState.Y.ToString();});
				BeginInvoke((MethodInvoker)delegate(){lblChukZ.Text = ws.NunchukState.AccelState.Z.ToString();});

				BeginInvoke((MethodInvoker)delegate(){lblChukJoyX.Text = ws.NunchukState.X.ToString();});
				BeginInvoke((MethodInvoker)delegate(){lblChukJoyY.Text = ws.NunchukState.Y.ToString();});
			}

			if(ws.ExtensionType == ExtensionType.ClassicController)
			{
				BeginInvoke((MethodInvoker)delegate(){clbCCButtons.SetItemChecked(0, ws.ClassicControllerState.ButtonState.A);});
				BeginInvoke((MethodInvoker)delegate(){clbCCButtons.SetItemChecked(1, ws.ClassicControllerState.ButtonState.B);});
				BeginInvoke((MethodInvoker)delegate(){clbCCButtons.SetItemChecked(2, ws.ClassicControllerState.ButtonState.X);});
				BeginInvoke((MethodInvoker)delegate(){clbCCButtons.SetItemChecked(3, ws.ClassicControllerState.ButtonState.Y);});
				BeginInvoke((MethodInvoker)delegate(){clbCCButtons.SetItemChecked(4, ws.ClassicControllerState.ButtonState.Minus);});
				BeginInvoke((MethodInvoker)delegate(){clbCCButtons.SetItemChecked(5, ws.ClassicControllerState.ButtonState.Home);});
				BeginInvoke((MethodInvoker)delegate(){clbCCButtons.SetItemChecked(6, ws.ClassicControllerState.ButtonState.Plus);});
				BeginInvoke((MethodInvoker)delegate(){clbCCButtons.SetItemChecked(7, ws.ClassicControllerState.ButtonState.Up);});
				BeginInvoke((MethodInvoker)delegate(){clbCCButtons.SetItemChecked(8, ws.ClassicControllerState.ButtonState.Down);});
				BeginInvoke((MethodInvoker)delegate(){clbCCButtons.SetItemChecked(9, ws.ClassicControllerState.ButtonState.Left);});
				BeginInvoke((MethodInvoker)delegate(){clbCCButtons.SetItemChecked(10, ws.ClassicControllerState.ButtonState.Right);});
				BeginInvoke((MethodInvoker)delegate(){clbCCButtons.SetItemChecked(11, ws.ClassicControllerState.ButtonState.ZL);});
				BeginInvoke((MethodInvoker)delegate(){clbCCButtons.SetItemChecked(12, ws.ClassicControllerState.ButtonState.ZR);});
				BeginInvoke((MethodInvoker)delegate(){clbCCButtons.SetItemChecked(13, ws.ClassicControllerState.ButtonState.TriggerL);});
				BeginInvoke((MethodInvoker)delegate(){clbCCButtons.SetItemChecked(14, ws.ClassicControllerState.ButtonState.TriggerR);});

				BeginInvoke((MethodInvoker)delegate(){lblXL.Text = ws.ClassicControllerState.XL.ToString();});
				BeginInvoke((MethodInvoker)delegate(){lblYL.Text = ws.ClassicControllerState.YL.ToString();});
				BeginInvoke((MethodInvoker)delegate(){lblXR.Text = ws.ClassicControllerState.XR.ToString();});
				BeginInvoke((MethodInvoker)delegate(){lblYR.Text = ws.ClassicControllerState.YR.ToString();});

				BeginInvoke((MethodInvoker)delegate(){lblTriggerL.Text = ws.ClassicControllerState.TriggerL.ToString();});
				BeginInvoke((MethodInvoker)delegate(){lblTriggerR.Text = ws.ClassicControllerState.TriggerR.ToString();});
			}

			if (ws.IRState.Found1)
				BeginInvoke((MethodInvoker)delegate(){lblIR1.Text = ws.IRState.RawX1.ToString() + ", " + ws.IRState.RawY1.ToString() + ", " + ws.IRState.Size1;});
			if (ws.IRState.Found2)
				BeginInvoke((MethodInvoker)delegate(){lblIR2.Text = ws.IRState.RawX2.ToString() + ", " + ws.IRState.RawY2.ToString() + ", " + ws.IRState.Size2;});
            if (ws.IRState.Found3)
                BeginInvoke((MethodInvoker)delegate() { lblIR3.Text = ws.IRState.RawX3.ToString() + ", " + ws.IRState.RawY3.ToString() + ", " + ws.IRState.Size3; });
            if (ws.IRState.Found4)
                BeginInvoke((MethodInvoker)delegate() { lblIR4.Text = ws.IRState.RawX4.ToString() + ", " + ws.IRState.RawY4.ToString() + ", " + ws.IRState.Size4; });

			BeginInvoke((MethodInvoker)delegate(){chkFound1.Checked = ws.IRState.Found1;});
			BeginInvoke((MethodInvoker)delegate(){chkFound2.Checked = ws.IRState.Found2;});
            BeginInvoke((MethodInvoker)delegate() { chkFound3.Checked = ws.IRState.Found3; });
            BeginInvoke((MethodInvoker)delegate() { chkFound4.Checked = ws.IRState.Found4; });

			BeginInvoke((MethodInvoker)delegate(){pbBattery.Value = (ws.Battery > 0xc8 ? 0xc8 : (int)ws.Battery);});
			float f = (((100.0f * 48.0f * (float)(ws.Battery / 48.0f))) / 192.0f);
			BeginInvoke((MethodInvoker)delegate(){lblBattery.Text = f.ToString("F");});

			g.Clear(Color.Black);
			if(ws.IRState.Found1)
				g.DrawEllipse(new Pen(Color.Red), ws.IRState.RawX1 / 4, ws.IRState.RawY1 / 4, ws.IRState.Size1+1, ws.IRState.Size1+1);
			if(ws.IRState.Found2)
                g.DrawEllipse(new Pen(Color.Blue), ws.IRState.RawX2 / 4, ws.IRState.RawY2 / 4, ws.IRState.Size2 + 1, ws.IRState.Size2 + 1);
            if (ws.IRState.Found3)
                g.DrawEllipse(new Pen(Color.Red), ws.IRState.RawX3 / 4, ws.IRState.RawY3 / 4, ws.IRState.Size3 + 1, ws.IRState.Size3 + 1);
            if (ws.IRState.Found4)
                g.DrawEllipse(new Pen(Color.Blue), ws.IRState.RawX4 / 4, ws.IRState.RawY4 / 4, ws.IRState.Size4 + 1, ws.IRState.Size4 + 1);
			BeginInvoke((MethodInvoker)delegate(){pbIR.Image = b;});

			m.ReleaseMutex();
		}

		private void chkLED1_CheckedChanged(object sender, EventArgs e)
		{
			wm.SetLEDs(chkLED1.Checked, chkLED2.Checked, chkLED3.Checked, chkLED4.Checked);
		}

		private void chkRumble_CheckedChanged(object sender, EventArgs e)
		{
			wm.SetRumble(chkRumble.Checked);
		}

		private void Form1_FormClosed(object sender, FormClosedEventArgs e)
		{
			wm.Disconnect();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			wm.ReadData(0x0016, 5);
		}
	}
}