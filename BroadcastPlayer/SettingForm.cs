using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BroadcastPlayer
{
    public partial class SettingForm : Form
    {
        public event EventHandler OnSubmit;

        public SettingForm()
        {
            InitializeComponent();
            this.LostFocus += SettingForm_Leave;
        }

        public SettingForm(BPRuntime Runtime)
        {
            InitializeComponent();
            this.LostFocus += SettingForm_Leave;

            if (Runtime.UsingAllDevice)
            {
                radioButton1.Checked = true;
            }
            else
            {
                radioButton2.Checked = true;
                textBox1.Text = Runtime.CurrentDevice + "";
            }
        }

        private void SettingForm_Leave(object sender, EventArgs e)
        {
        //    this.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                if (null != OnSubmit)
                    OnSubmit.Invoke(null, null);

                button1_Click(null,null);
            }
            else
            {
                String DeviceID = textBox1.Text;
                if (String.IsNullOrEmpty(DeviceID.Trim()))
                {
                    textBox1.Focus();
                }
                else
                {
                    int result = 0;
                    if (int.TryParse(DeviceID, out result))
                    {
                        String UID = Models.TestPrintEntity.GetDeviceUUIDByUniqueID(result);
                        if (null != UID)
                        {
                            if (null != OnSubmit)
                                OnSubmit.Invoke(DeviceID + "|" + UID, null);
                            button1_Click(null, null);
                        }
                        else
                        { 
                            DialogResult dr = MessageBox.Show("Not found device！", "Err", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            if (dr == DialogResult.OK)
                            {
                                this.Focus();
                                textBox1.Focus();
                            }
                           
                        }
                    }
                    else
                    {
                        DialogResult dr = MessageBox.Show("Input Device Number！", "Err", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        
                        if (dr == DialogResult.OK)
                        {
                            this.Focus();
                            textBox1.Focus();
                        }
                    }
                }
            }
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                panel1.Visible = false;
            }
            else
            {
                panel1.Visible = true;
            }
        }
    }
}
