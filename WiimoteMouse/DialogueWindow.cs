using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WiimoteMouse
{
    public partial class DialogueWindow : Form
    {
        MainForm parent = null;

        /**
         * A simple, standalone dialogue window.
         */
        public DialogueWindow(String title)
        {
            InitializeComponent();
            this.Text = title;
        }

        /**
         * On close, this window shall also close the parent.
         */
        public DialogueWindow(String title, MainForm parent)
        {
            InitializeComponent();
            this.Text = title;
            this.parent = parent;
        }

        private void close_Click(object sender, EventArgs e)
        {
            if (this.parent != null)
            {
                parent.realClose = true;
                this.Invoke(new EventHandler(closeMainForm));

                // This should work, but doesn't
                //if (InvokeRequired)
                //    this.Invoke(new EventHandler(close_Click));
                //else
                //    parent.Close();
            }
            else
            {
                this.Close();
            }
        }

        private void closeMainForm(object sender, EventArgs e)
        {
            parent.Close();
        }

        public void setText(String text)
        {
            char[] newlineDelim = new char[2];
            newlineDelim[0] = '\n';
            newlineDelim[1] = '\r';
            this.dialogueText.Lines = text.Split(newlineDelim);
        }
    }
}