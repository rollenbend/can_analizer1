using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Diagnostics;
using System.Management;
using System.Timers;
using Newtonsoft.Json;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Thread ReadCOMThread;
        public delegate void delegates();
        delegates COM_ex, Read_COM_CAN;
        public delegate void delegates2(byte Ncan, int RowIndex);
        delegates2 sendusb;
        public Form1()
        {
            InitializeComponent();

            foreach (string s in System.IO.Ports.SerialPort.GetPortNames())
                comboBox1.Items.Add(s);
            serialPort1.PortName = comboBox1.Text;
            comboBox2.SelectedIndex = 3;
            comboBox3.SelectedIndex = 3;
        }

        void Read_CAN_Mess_From_COM()
        {
            if (canRxbuf[0] == CAN1_Number)
            {
                string rxmess = cntCAN1mess++.ToString() + "  ID = " + BitConverter.ToUInt32(canRxbuf, 2).ToString("X")
             + " DLC = " + canRxbuf[6].ToString("X") + "   DATA = " + BitConverter.ToString(canRxbuf, 7, canRxbuf[6] % 9) + "\r\n";
                richTextBox3.AppendText(rxmess);
                if (richTextBox3.Lines.GetLength(0) > 44) richTextBox3.Text = richTextBox3.Text.Remove(0, richTextBox3.Lines[0].Length + 1);
                richTextBox3.SelectionStart = richTextBox3.Text.Length;
                richTextBox3.ScrollToCaret();
            }
            else
            {
                string rxmess = cntCAN2mess++.ToString() + "  ID = " + BitConverter.ToUInt32(canRxbuf, 2).ToString("X")
             + " DLC = " + canRxbuf[6].ToString("X") + "   DATA = " + BitConverter.ToString(canRxbuf, 7, canRxbuf[6] % 9) + "\r\n";
                richTextBox4.AppendText(rxmess);
                if (richTextBox4.Lines.GetLength(0) > 44) richTextBox4.Text = richTextBox4.Text.Remove(0, richTextBox4.Lines[0].Length + 1);
                richTextBox4.SelectionStart = richTextBox4.Text.Length;
                richTextBox4.ScrollToCaret();
            }
        }

        int cntCAN1mess = 1;
        int cntCAN2mess = 1;
        byte[] canRxbuf = new byte[15];
        private void ThreadForCANReceiving()
        {
            while (true)
            {
                try
                {
                    if (serialPort1.IsOpen)
                    {
                        if (serialPort1.BytesToRead >= 15)
                        {
                            serialPort1.BaseStream.ReadAsync(canRxbuf, 0, 15);
                            Invoke(Read_COM_CAN);
                        }
                    }
                } catch (UnauthorizedAccessException)
                {
                    Invoke(COM_ex);
                }
            }
        }
        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            COM_ex = COM_exeption;
            Read_COM_CAN = Read_CAN_Mess_From_COM;
            sendusb = send_to_USB;

            string Can1SettingsJson, Can2SettingsJson;
            List<string[]> Can1Settings, Can2Settings;

            try
            {
                Can2SettingsJson = File.ReadAllText(@"..\can2settings.json");
                Can2Settings = JsonConvert.DeserializeObject<List<string[]>>(Can2SettingsJson);
            }
            catch (FileNotFoundException)
            {
                Can2Settings = new List<string[]>()
                {
                        new string[]{"11CE7304", "C00C 016C", " ", "angle otn +1 deg", "Start", "100"},
                        new string[]{"11CE7304", "C00C FD28", " ", "angle otn -1 deg", "Start", "100" },
                        new string[]{"11CE7304", "C00C 0E38", " ", "angle otn +10 deg", "Start", "100"},
                        new string[]{"11CE7304", "C00C F1C9", " ", "angle otn -10 deg", "Start", "100" },
                        new string[]{"11CE7304", "C00D 5555", " ", "angle otn 60 deg", "Start", "100" },
                        new string[]{"11CE7304", "C00D 1C71", " ", "angle otn 20 deg", "Start", "100" },
                };
            }
            for (int i = 0; i < Can2Settings.Count; i++)
            {
                dataGridView2.Rows.Add(Can2Settings[i]);
            }

            try
            {
                Can1SettingsJson = File.ReadAllText(@"..\can1settings.json");
                Can1Settings = JsonConvert.DeserializeObject<List<string[]>>(Can1SettingsJson);
            }
            catch (FileNotFoundException)
            {
                Can1Settings = new List<string[]>()
                {
                        new string[]{"11CE7304", "C00F FFFF", " ", "right time 32 sec", "Start", "100"},
                        new string[]{"11CE7304", "C00E FFFF", " ", "left time 32 sec", "Start", "100" },
                        new string[]{"11CE7304", "C00F 17FF", " ", "right time 3 sec", "Start", "100" },
                        new string[]{"11CE7304", "C00E 17FF", " ", "left time 3 sec", "Start", "100" },
                        new string[]{"11CE7304", "C00F 000F", " ", "right time 0.1 sec", "Start", "100" },
                        new string[]{"11CE7304", "C00E 000F", " ", "left time 0.1 sec", "Start", "100" },
                        new string[]{"11CE7304", "C011 0000", " ", "full right", "Start", "100" },
                        new string[]{"11CE7304", "C010 0000", " ", "full left", "Start", "100" },
                        new string[]{"11CE7304", "C001 0001", " ", "priority", "Start", "100" },
                        new string[]{"11CE7304", "C002 0001", " ", "BU addr=1", "Start", "100" },
                        new string[]{"1BEE7204", "0000 0000 0000 0000", " ", "PHSM BU main", "Start", "10" },
                        new string[]{"1BEE7204", "0000 0000 0000 0000", " ", "PHSM BU res", "Start", "10" },
                };
            }

            for (int i = 0; i < Can1Settings.Count; i++)
            {
                dataGridView1.Rows.Add(Can1Settings[i]);
            }

        }


        private void COM_exeption()
        {
            label1.Text = "PORT DOESN'T EXIST ANYMORE!";
            label1.ForeColor = Color.DeepPink;
            // if (ReadCOMThread.IsAlive) ReadCOMThread.Abort();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            comboBox1.Items.Clear();
            foreach (string s in System.IO.Ports.SerialPort.GetPortNames())
                comboBox1.Items.Add(s);
            comboBox1.Enabled = true;
        }

        byte CAN1_Number = 1;
        byte CAN2_Number = 2;
        void send_to_USB(byte Ncan, int RowIndex)
        {
            byte[] databuf = new byte[8];
            byte dlc = 0;
            string datastring;
            uint id;
            byte canmesstype;
            try
            {
                if (Ncan == CAN1_Number)
                {
                    datastring = dataGridView1.Rows[RowIndex].Cells[1].Value.ToString();
                    id = Convert.ToUInt32(dataGridView1.Rows[RowIndex].Cells[0].Value.ToString(), 16);
                }
                else
                {
                    datastring = dataGridView2.Rows[RowIndex].Cells[1].Value.ToString();
                    id = Convert.ToUInt32(dataGridView2.Rows[RowIndex].Cells[0].Value.ToString(), 16);
                }

                for (int i = 0; i < datastring.Length; i++) if (datastring[i] == ' ') datastring = datastring.Remove(i--, 1);
                for (int i = 0; i < (datastring.Length - 1); i += 2)
                {
                    databuf[i / 2] = Convert.ToByte(datastring.Substring(i, 2), 16);
                    dlc++;
                }

                if (id <= 0x7FF) canmesstype = 0; else canmesstype = 1;
                byte[] idbuf = BitConverter.GetBytes(id);
                byte[] canmess = { Ncan, canmesstype, idbuf[0], idbuf[1], idbuf[2], idbuf[3], dlc, 0, 0, 0, 0, 0, 0, 0, 0 };
                Array.ConstrainedCopy(databuf, 0, canmess, 7, 8);

                string txmess = "Transmit:  ID=" + id.ToString("X") + " DLC=" + dlc.ToString("X") +
                    "   DATA=" + BitConverter.ToString(databuf, 0, dlc) + "\r\n";
                if (Ncan == CAN1_Number && IsCAN1_connect == 1)
                {
                    serialPort1.BaseStream.WriteAsync(canmess, 0, 15);
                    richTextBox3.AppendText(txmess);
                    /*richTextBox3.Select(richTextBox3.TextLength - txmess.Length + 1, txmess.Length);
                    richTextBox3.SelectionColor = Color.Blue;
                    richTextBox3.ForeColor = Color.Black;*/

                }
                if (Ncan == CAN2_Number && IsCAN2_connect == 1)
                {
                    serialPort1.BaseStream.WriteAsync(canmess, 0, 15);
                    richTextBox4.AppendText(txmess);
                }
                if (richTextBox3.Lines.GetLength(0) > 44) richTextBox3.Text = richTextBox3.Text.Remove(0, richTextBox3.Lines[0].Length + 1);
                if (richTextBox4.Lines.GetLength(0) > 44) richTextBox4.Text = richTextBox4.Text.Remove(0, richTextBox4.Lines[0].Length + 1);
            }
            catch (NullReferenceException)
            {
            }
            catch (InvalidOperationException)
            {
                label1.Text = "PORT CLOSED!";
                label1.ForeColor = Color.SaddleBrown;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                if (Timer1Switch++ % 2 == 0)
                {
                    Timer1RowIndex = e.RowIndex;
                    dataGridView1.Rows[Timer1RowIndex].Cells[5].Style.BackColor = Color.Yellow;
                    uint interval = Convert.ToUInt32(dataGridView1.Rows[Timer1RowIndex].Cells[5].Value.ToString(), 10);
                    SetTimer1(interval);
                }
                else
                {
                    dataGridView1.Rows[Timer1RowIndex].Cells[5].Style.BackColor = Color.White;
                    aTimer1.Stop();
                    aTimer1.Dispose();
                }
            }
            if (e.ColumnIndex == 2)
            {
                if (sender == dataGridView1) send_to_USB(CAN1_Number, e.RowIndex);
                if (sender == dataGridView2) send_to_USB(CAN2_Number, e.RowIndex);
            }
        }
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                if (Timer2Switch++ % 2 == 0)
                {
                    Timer2RowIndex = e.RowIndex;
                    dataGridView2.Rows[Timer2RowIndex].Cells[5].Style.BackColor = Color.Yellow;
                    uint interval = Convert.ToUInt32(dataGridView2.Rows[Timer2RowIndex].Cells[5].Value.ToString(), 10);
                    SetTimer2(interval);
                }
                else
                {
                    dataGridView2.Rows[Timer2RowIndex].Cells[5].Style.BackColor = Color.White;
                    aTimer2.Stop();
                    aTimer2.Dispose();
                }
            }
            if (e.ColumnIndex == 2)
            {
                if (sender == dataGridView1) send_to_USB(CAN1_Number, e.RowIndex);
                if (sender == dataGridView2) send_to_USB(CAN2_Number, e.RowIndex);
            }
        }
        private static System.Timers.Timer aTimer1;
        private int Timer1RowIndex, Timer1Switch = 0;
        private static System.Timers.Timer aTimer2;
        private int Timer2RowIndex, Timer2Switch = 0;
        private void OnTimedEvent1(Object source, ElapsedEventArgs e)
        {
            Invoke(sendusb, CAN1_Number, Timer1RowIndex);
        }
        private void SetTimer1(uint interval)
        {
            // Create a timer with a one second interval.
            aTimer1 = new System.Timers.Timer(Convert.ToDouble(interval));
            // Hook up the Elapsed event for the timer. 
            aTimer1.Elapsed += OnTimedEvent1;
            aTimer1.AutoReset = true;
            aTimer1.Enabled = true;
        }

        private void OnTimedEvent2(Object source, ElapsedEventArgs e)
        {
            Invoke(sendusb, CAN2_Number, Timer2RowIndex);
        }
        private void SetTimer2(uint interval)
        {
            // Create a timer with a one second interval.
            aTimer2 = new System.Timers.Timer(Convert.ToDouble(interval));
            // Hook up the Elapsed event for the timer. 
            aTimer2.Elapsed += OnTimedEvent2;
            aTimer2.AutoReset = true;
            aTimer2.Enabled = true;
        }

        private void clear_CAN_Rx_bt_Click(object sender, EventArgs e)
        {
            richTextBox3.Clear();
            cntCAN1mess = 1;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (aTimer1 != null)
            {
                aTimer1.Stop();
                aTimer1.Dispose();
            }
            if (aTimer2 != null)
            {
                aTimer2.Stop();
                aTimer2.Dispose();
            }
            if (serialPort1.IsOpen)
            {
                ReadCOMThread.Abort();
                serialPort1.Close();
            }

            List<string[]> can1settings = new List<string[]>(dataGridView1.RowCount);
            List<string[]> can2settings = new List<string[]>(dataGridView2.RowCount);

            for (int i = 0; i < dataGridView1.RowCount - 1; i++)
            {
                can1settings.Add(new string[]
                {
                    dataGridView1[0, i].Value==null?"":dataGridView1[0, i].Value.ToString(),
                    dataGridView1[1, i].Value==null?"":dataGridView1[1, i].Value.ToString(),
                    dataGridView1[2, i].Value==null?"":dataGridView1[2, i].Value.ToString(),
                    dataGridView1[3, i].Value==null?"":dataGridView1[3, i].Value.ToString(),
                    dataGridView1[4, i].Value==null?"":dataGridView1[4, i].Value.ToString(),
                    dataGridView1[5, i].Value==null?"":dataGridView1[5, i].Value.ToString(),
                });
            }

            for (int i = 0; i < dataGridView2.RowCount - 1; i++)
            {
                can2settings.Add(new string[]
                {
                    dataGridView2[0, i].Value==null?"":dataGridView2[0, i].Value.ToString(),
                    dataGridView2[1, i].Value==null?"":dataGridView2[1, i].Value.ToString(),
                    dataGridView2[2, i].Value==null?"":dataGridView2[2, i].Value.ToString(),
                    dataGridView2[3, i].Value==null?"":dataGridView2[3, i].Value.ToString(),
                    dataGridView2[4, i].Value==null?"":dataGridView2[4, i].Value.ToString(),
                    dataGridView2[5, i].Value==null?"":dataGridView2[5, i].Value.ToString(),
                });
            }
            File.WriteAllText(@"..\can1settings.json", JsonConvert.SerializeObject(can1settings));
            File.WriteAllText(@"..\can2settings.json", JsonConvert.SerializeObject(can2settings));
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            serialPort1.PortName = comboBox1.Text;
        }

        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar != 8) && (e.KeyChar != ' ') && (e.KeyChar < 'a' || e.KeyChar > 'f') && (e.KeyChar < 'A' || e.KeyChar > 'F') && (e.KeyChar < '0' || e.KeyChar > '9'))
            {
                e.Handled = true;
            }

            if (e.KeyChar == 'a')
                e.KeyChar = 'A';
            if (e.KeyChar == 'b')
                e.KeyChar = 'B';
            if (e.KeyChar == 'c')
                e.KeyChar = 'C';
            if (e.KeyChar == 'd')
                e.KeyChar = 'D';
            if (e.KeyChar == 'e')
                e.KeyChar = 'E';
            if (e.KeyChar == 'f')
                e.KeyChar = 'F';
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex <= 1)
            {
                TextBox tb = (TextBox)e.Control;
                tb.KeyPress += new KeyPressEventHandler(dataGridView1_KeyPress);
            }
            else
            {
                TextBox tb = (TextBox)e.Control;
                tb.KeyPress -= dataGridView1_KeyPress;
            }
        }

        private void clear2_Click(object sender, EventArgs e)
        {
            richTextBox4.Clear();
            cntCAN2mess = 1;
        }

        void connect_disconnect_to_CANx(byte Ncan, int IsOpening)
        {
            try
            {
                if (!serialPort1.IsOpen && IsOpening == 1)
                {
                    serialPort1.PortName = comboBox1.Text;
                    serialPort1.Open();
                    label1.Text = "CONNECTED";
                    label1.ForeColor = Color.Green;
                    ReadCOMThread = new Thread(new ThreadStart(ThreadForCANReceiving));
                    ReadCOMThread.Start();
                    comboBox1.Enabled = false;
                }

                send_request(Ncan, IsOpening);
                if (receive_response(Ncan) == 1)
                {
                    if (Ncan == CAN1_Number && IsOpening == 1) { label2.ForeColor = Color.Green; comboBox2.Enabled = false; }
                    if (Ncan == CAN2_Number && IsOpening == 1) { label5.ForeColor = Color.Green; comboBox3.Enabled = false; }
                    if (Ncan == CAN1_Number && IsOpening == 0) { label2.ForeColor = Color.Red; comboBox2.Enabled = true; }
                    if (Ncan == CAN2_Number && IsOpening == 0) { label5.ForeColor = Color.Red; comboBox3.Enabled = true; }
                }

                if (serialPort1.IsOpen && IsOpening == 0 && (IsCAN1_connect * IsCAN2_connect == 0))
                {
                    ReadCOMThread.Abort();
                    serialPort1.Close();
                    label1.Text = "DISCONNECTED";
                    label1.ForeColor = Color.Red;
                    comboBox1.Enabled = true;
                }
            }
            catch (UnauthorizedAccessException)
            {
                label1.Text = "PORT DOESN'T EXIST!";
                label1.ForeColor = Color.DeepPink;
            }
            catch (IOException)
            {
                label1.Text = "DEVICE DOESN'T WORK!";
                label1.ForeColor = Color.DeepPink;
            }
        }

        void send_request(byte Ncan, int IsOpening)
        {
            if (IsOpening == 1)
            {
                byte Speed;
                if (Ncan == CAN1_Number) Speed = (byte)(comboBox2.SelectedIndex);
                else Speed = (byte)(comboBox3.SelectedIndex);
                byte[] concan = { Ncan, 0x53, Speed };
                serialPort1.BaseStream.WriteAsync(concan, 0, 3);
            }
            else
            {
                byte[] concan = { Ncan, 0x53, 5 };
                serialPort1.BaseStream.WriteAsync(concan, 0, 3);
            }
        }
        int receive_response(byte Ncan)
        {
            byte[] canresp = new byte[2];
            try
            {
                serialPort1.ReadTimeout = 300;
                for (int i = 0; i < 2; i++)
                    canresp[i] = (byte)serialPort1.ReadByte();
            }
            catch (TimeoutException)
            {
                richTextBox3.Text += "timeout!!\r\n";
                return 0;
            }
            if (canresp[0] == Ncan && canresp[1] == 0x79)
            {
                return 1;
            }
            return 0;
        }

        int IsCAN1_connect = 0;
        int IsCAN2_connect = 0;
        private void connectCAN1_Click(object sender, EventArgs e)
        {
            if (IsCAN1_connect == 0)
            {
                connect_disconnect_to_CANx(CAN1_Number, 1);
                connectCAN1.Text = "disconnect";
                IsCAN1_connect = 1;
            }
            else
            {
                connect_disconnect_to_CANx(CAN1_Number, 0);
                connectCAN1.Text = "connect";
                IsCAN1_connect = 0;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!serialPort1.IsOpen)
                {
                    serialPort1.PortName = comboBox1.Text;
                    serialPort1.Open();
                    label1.Text = "CONNECTED";
                    label1.ForeColor = Color.Green;
                    ReadCOMThread = new Thread(new ThreadStart(ThreadForCANReceiving));
                    ReadCOMThread.Start();
                    comboBox1.Enabled = false;
                    IsCAN1_connect = 1;
                }
            }
            catch (UnauthorizedAccessException)
            {
                label1.Text = "PORT DOESN'T EXIST!";
                label1.ForeColor = Color.DeepPink;
            }
            catch (IOException)
            {
                label1.Text = "DEVICE DOESN'T WORK!";
                label1.ForeColor = Color.DeepPink;
            }
        }

        private void connectCAN2_Click(object sender, EventArgs e)
        {
            if (IsCAN2_connect == 0)
            {
                connect_disconnect_to_CANx(CAN2_Number, 1);
                connectCAN2.Text = "disconnect";
                IsCAN2_connect = 1;
            }
            else
            {
                connect_disconnect_to_CANx(CAN2_Number, 0);
                connectCAN2.Text = "connect";
                IsCAN2_connect = 0;
            }
        }

    }
   
}

//public class CANTransmitter
//{
//    public string ID { get; set; }
//    public string Data { get; set; }
//    public string Send { get; set; }
//    public string Comment { get; set; }
//    public string Repeating { get; set; }
//    public string Period { get; set; }
//}
/* dataGridView1.Rows.Add("11CE7304", "C00F FFFF", "", "right time 32 sec", "Start", "100"); 
 dataGridView1.Rows.Add("11CE7304", "C00E FFFF", "", "left time 32 sec", "Start", "100");
 dataGridView1.Rows.Add("11CE7304", "C00F 17FF", "", "right time 3 sec", "Start", "100");
 dataGridView1.Rows.Add("11CE7304", "C00E 17FF", "", "left time 3 sec", "Start", "100");
 dataGridView1.Rows.Add("11CE7304", "C00F 000F", "", "right time 0.1 sec", "Start", "100");
 dataGridView1.Rows.Add("11CE7304", "C00E 000F", "", "left time 0.1 sec", "Start", "100");
 dataGridView1.Rows.Add("11CE7304", "C011 0000", "", "full right", "Start", "100");
 dataGridView1.Rows.Add("11CE7304", "C010 0000", "", "full left", "Start", "100");
 dataGridView1.Rows.Add("11CE7304", "C001 0001", "", "priority", "Start", "100");
 dataGridView1.Rows.Add("11CE7304", "C002 0001", "", "BU addr=1", "Start", "100");
 dataGridView1.Rows.Add("1BEE7204", "0000 0000 0000 0000", "", "PHSM BU main", "Start", "10");
 dataGridView1.Rows.Add("1BEE7204", "0000 0000 0000 0000", "", "PHSM BU res", "Start", "10");

 dataGridView2.Rows.Add("11CE7304", "C00C 016C", "angle otn", "+1 deg");
 dataGridView2.Rows.Add("11CE7304", "C00C FD28", "angle otn", "-1 deg");
 dataGridView2.Rows.Add("11CE7304", "C00C 0E38", "angle otn", "+10 deg");
 dataGridView2.Rows.Add("11CE7304", "C00C F1C9", "angle otn", "-10 deg");
 dataGridView2.Rows.Add("11CE7304", "C00D 5555", "angle abs", "60 deg");
 dataGridView2.Rows.Add("11CE7304", "C00D 1C71", "angle abs", "20 deg");
 dataGridView2.Rows.Add("11CE7304", "C012 0E38 17FF", "angle+time", "+10; 3sec");
 dataGridView2.Rows.Add("11CE7304", "C012 F1C9 17FF", "angle+time", "-10; 3sec");
 dataGridView2.Rows.Add("11CE7304", "C003 4FFF", "valve speed", "10sec");*/

/*private void disconnect(object sender, EventArgs e)
{
    try
    {
        if (serialPort1.IsOpen)
        {
            ReadCOMThread.Abort();
            serialPort1.Close();
            label1.Text = "DISCONNECTED";
            label1.ForeColor = Color.Red;
            comboBox1.Enabled = true;
        }
    }
    catch
    {
        COM_exeption();
    }
}
*/

/*private void try_connect(object sender, EventArgs e)
{
    try
    {
        if (!serialPort1.IsOpen)
        {
            serialPort1.PortName = comboBox1.Text;
            serialPort1.Open();
            label1.Text = "CONNECTED";
            label1.ForeColor = Color.Green;
            ReadCOMThread = new Thread(new ThreadStart(ThreadForCANReceiving));
            ReadCOMThread.Start();
            comboBox1.Enabled = false;
        }
    }
    catch (UnauthorizedAccessException)
    {
        label1.Text = "PORT DON'T EXIST!";
        label1.ForeColor = Color.DeepPink;
    }
    catch (IOException)
    {
        label1.Text = "DEVICE DON'T WORK!";
        label1.ForeColor = Color.DeepPink;
    }
}*/



/* byte[] databuf = new byte[8];
                   try
                   {
                       databuf = BitConverter.GetBytes(Convert.ToUInt64((string)dataGridView1.Rows[e.RowIndex].Cells[1].Value, 16));
                   } catch(ArgumentOutOfRangeException) {; }

                   byte dlc = (byte)(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString().Length/2);
                   /* byte dlc = 8; 
                    while (databuf[dlc-1] == 0)
                    {
                        dlc--;
                        if (dlc == 0) break;
                    }*/

/* uint idcan = Convert.ToUInt32((string)dataGridView1.Rows[e.RowIndex].Cells[0].Value, 16);
 byte[] idcanbuf = BitConverter.GetBytes(idcan);
 byte[] canframe = { idcanbuf[0], idcanbuf[1], idcanbuf[2], idcanbuf[3], dlc, 0, 0, 0, 0, 0, 0, 0, 0 };
 Array.ConstrainedCopy(databuf, 0, canframe, 5, databuf.Length);
 if (idcan <= 0x7FF)
 {
     var tmp = new List<byte>(canframe); // Преобразование в список
     tmp.RemoveAt(2); tmp.RemoveAt(2);// Удаление элемента 2 raza
     byte[] canframe2 = tmp.ToArray(); // Преобразование в массив
     serialPort1.BaseStream.WriteAsync(canframe2, 0, canframe2.Length);
 }
 else
 {
     serialPort1.BaseStream.WriteAsync(canframe, 0, canframe.Length);
 }*/



/*     enum Request : int { erase, write, crccheck, reboot, updateinfo, ping };
            private int offset_loading = 0;
            private int opening_file_lenght = 0;
            uint CRC = 0;
            private int firmware_version = 1;
            Stopwatch sw;//private long timecode;
            byte dev_type;

            private void loadprog_button_Click(object sender, EventArgs e)
            {
                dev_type = Convert.ToByte(textBox1.Text);
                button3.PerformClick(); // connecting
                if (ReadCOMThread.IsAlive) ReadCOMThread.Abort();
                progressBar1.Value = 0;
                offset_loading = Convert.ToInt32( offset_loading_textBox.Text); // offset for program

                openFileDialog1.Filter = "Binary file(*.bin)|*.bin";
                if (openFileDialog1.ShowDialog() == DialogResult.OK) // next we opening file
                {
                    int flen = (int)openFileDialog1.OpenFile().Length;
                    if (flen < 114686)
                    {
                        opening_file_lenght = flen;
                        byte[] buf = new byte[flen];
                        openFileDialog1.OpenFile().ReadAsync(buf, 0, flen); // while eras we copy to ram this file

                        sw = Stopwatch.StartNew();
                        if (serialPort1.IsOpen) serialPort1.DiscardInBuffer(); // clear receive buffer
                        send_request((int)Request.erase);
                        if (receive_response((int)Request.erase) == 1)
                        {
                            send_request((int)Request.write);
                            if (receive_response((int)Request.write) == 1)
                            {
                                byte[] databuf = { 0x31, dev_type, 8, 0, 0, 0, 0, 0, 0, 0, 0 };
                                byte size = 0;
                                int mess = 0;
                                for (int i = 0; i < flen; i += 8)
                                {
                                    if (flen - i >= 8) size = 8; else size = (byte)(flen - i);
                                    databuf[2] = size;
                                    Array.ConstrainedCopy(buf, i, databuf, 3 , size); 
                                    serialPort1.BaseStream.WriteAsync(databuf, 0, 11);
                                    if (receive_response((int)Request.write) == 1) mess++;
                                    else break;
                                    progressBar1.Value = (i * 100) / flen;
                                }
                                if ( mess == (flen / 8 + (flen % 8)/size) )
                                {
                                    richTextBox_load.Text += $"programming succsess in {mess} CAN messages in {sw.ElapsedMilliseconds} ms!\r\n"; sw.Restart();
                                    send_request((int)Request.crccheck);
                                    CRC = Crc32(buf);
                                    if (receive_response((int)Request.crccheck) == 1)
                                    {
                                        richTextBox_load.Text += $"verify succsess in {sw.ElapsedMilliseconds} ms!\r\n"; sw.Restart();
                                        byte[] bdata = get_BCD_date();
                                        byte[] bvers = BitConverter.GetBytes(firmware_version);
                                        byte[] correctcrcbuf = { 0x99, dev_type, 8, bdata[0], bdata[1], bdata[2], bdata[3], bvers[0], bvers[1], bvers[2], bvers[3] };
                                        serialPort1.BaseStream.WriteAsync(correctcrcbuf, 0, 11);
                                        if (receive_response((int)Request.updateinfo) == 1)
                                        {
                                            send_request((int)Request.reboot);
                                            richTextBox_load.Text += $"Reboot \r\nUpdate firmware finished!\r\n";
                                            richTextBox_load.SelectionStart = richTextBox_load.Text.Length;
                                            richTextBox_load.ScrollToCaret();
                                            progressBar1.Value = 100;
                                        }
                                    }
                                    else
                                    {
                                        byte[] wrongcrcbuf = { 0x99, dev_type, 1, 0x1F, 0, 0, 0, 0, 0, 0, 0 };
                                        serialPort1.BaseStream.WriteAsync(wrongcrcbuf, 0, 11);
                                    }

                                }
                            }
                        }
                        buf = null;
                        sw.Stop();
                    }
                    else
                    {
                        richTextBox_load.Text += $"File must be less then 114 kb!\r\n";
                    }
                    openFileDialog1.Reset();
                    openFileDialog1.Dispose();
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (serialPort1.BytesToRead > 0) serialPort1.DiscardInBuffer();
                //if (!ReadCOMThread.IsAlive) ReadCOMThread.Resume();
            }




            // enum ResponseID : int { erase = 0x43, writereq = 0x31, writing = 0x2, crccheck = 0x99, reboot = 0xA3, ACK = 0x79 };
            private void send_request(int request)
            {
                if (request == (int)Request.erase)
                {
                    byte CountPagesErase = (byte)((opening_file_lenght / 4096) + 1);
                    byte StartPageToErase = (byte)((offset_loading / 4096) + 4);
                    byte[] erasebuf = { 0x43, dev_type, 2, CountPagesErase , StartPageToErase, 0,0,  0, 0, 0, 0 };
                    serialPort1.BaseStream.WriteAsync(erasebuf, 0, 11);
                }
                if (request == (int)Request.write)
                {
                    byte[] writebuf = { 0x31, dev_type, 8, 0, 0, 0, 0, 0, 0, 0, 0 };
                    byte[] boff = BitConverter.GetBytes(offset_loading);
                    byte[] bsize = BitConverter.GetBytes(opening_file_lenght);
                    Array.ConstrainedCopy(bsize, 0, writebuf, 3, bsize.Length);
                    Array.ConstrainedCopy(boff, 0, writebuf, 7, boff.Length);
                    serialPort1.BaseStream.WriteAsync(writebuf, 0, 11);
                }
                if (request == (int)Request.crccheck)
                {
                    byte[] crcbuf = { 0x99, dev_type, 8, 0, 0, 0, 0, 0, 0, 0, 0 };
                    byte[] boff = BitConverter.GetBytes(offset_loading);
                    byte[] bsize = BitConverter.GetBytes(opening_file_lenght);
                    Array.ConstrainedCopy(bsize, 0, crcbuf, 3, bsize.Length);
                    Array.ConstrainedCopy(boff, 0, crcbuf, 7, boff.Length);
                    serialPort1.BaseStream.WriteAsync(crcbuf, 0, 11);
                }
                if (request == (int)Request.reboot)
                {
                    byte[] rebootbuf = { 0xA3, dev_type, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                    serialPort1.BaseStream.WriteAsync(rebootbuf, 0, 11);
                }
                if (request == (int)Request.ping)
                {
                    byte[] speedbuf = { 0x00, 0x01, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                    serialPort1.BaseStream.WriteAsync(speedbuf, 0, 11);
                }
            }

            private int receive_response(int response)
            {
                byte[] respbuf = new byte[12];
                try
                {
                    serialPort1.ReadTimeout = 1000;
                    for (int i = 0; i < respbuf.Length; i++)
                        respbuf[i] = (byte)serialPort1.ReadByte();
                } 
                catch (TimeoutException)
                {
                    richTextBox_load.Text += "timeout!!\r\n";
                    return 0;
                }

                if (response == (int)Request.erase)
                {
                    if (respbuf[0] == 0x43 && respbuf[2] == 1 && respbuf[3] == 0x79) // if erase succsess
                    {
                        richTextBox_load.Text += $"erase succsess in {sw.ElapsedMilliseconds} ms! \r\n"; sw.Restart();
                        return 1;
                    }
                    else
                    {
                        richTextBox_load.Text += "erase failed!:";
                        string s = BitConverter.ToString(respbuf, 0, 11);
                        richTextBox_load.Text += s + "\r\n";
                    }
                }
                if (response == (int)Request.write)
                {
                    if (respbuf[0] == 0x31 && respbuf[2] == 1 && respbuf[3] == 0x79) // if writing succsess
                    {
                        return 1;
                    }
                    else
                    {
                        richTextBox_load.Text += "write failed!:";
                        string s = BitConverter.ToString(respbuf, 0, 11);
                        richTextBox_load.Text += s + "\r\n";
                    }
                }
                if (response == (int)Request.crccheck)
                {
                    uint CRC_MCU = BitConverter.ToUInt32(respbuf, 3);
                    if (respbuf[0] == 0x99 && respbuf[2] == 4 && CRC == CRC_MCU) // if crc check succsess
                    {
                        return 1;
                    }
                    else
                    {
                        richTextBox_load.Text += "CRC wrong!:";
                        string s = BitConverter.ToString(respbuf, 0, 11); 
                        richTextBox_load.Text += s + "\r\n";
                    }
                }
                if (response == (int)Request.updateinfo)
                {
                    if (respbuf[0] == 0x99 && respbuf[2] == 1 && respbuf[3] == 0x79) // if update info finish
                    {
                        return 1;
                    }
                }
                return 0;

            }

            uint Crc32(byte[] buf)
            {
                uint[] crc_table = new uint[256];
                uint crc;
                uint i, j;

                for (i = 0; i < 256; i++)
                {
                    crc = i;
                    for (j = 0; j < 8; j++)
                        crc = (crc & 1) == 1 ? (crc >> 1) ^ 0xEDB88320 : crc >> 1;

                    crc_table[i] = crc;
                };

                crc = 0xFFFFFFFF;

                foreach (byte b in buf) crc = crc_table[(crc ^ b) & 0xFF] ^ (crc >> 8);

                return crc ^ 0xFFFFFFFF;
            }



       private byte[] get_BCD_date()
       {
           string sd = DateTime.Now.ToString(); // full string datatime
           sd = sd.Remove(10);
           sd = sd.Remove(5, 1); // delete time and points
           sd = sd.Remove(2, 1);
           int dd = Convert.ToInt32(sd, 16); // from string to hex
           return BitConverter.GetBytes(dd); // from hex to bytes
       }

       private void clr_load_textbox_bt_Click(object sender, EventArgs e)
       {
           richTextBox_load.Clear();
       }

       private void bRead_Click_1(object sender, EventArgs e)
       {
           uint[] _info = new uint[6];
           button3.PerformClick();
           byte[] metabuf = { 0x1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
           serialPort1.BaseStream.WriteAsync(metabuf, 0, 11);
           // send_request((int)Request.read);
           byte[] respbuf = new byte[11];

           for (int i = 0; i < 3; i++)
           {
               try
               {
                   serialPort1.ReadTimeout = 1000;
                   for (int j = 0; j < 11; j++)
                       respbuf[j] = (byte)serialPort1.ReadByte();
                   _info[i * 2] = BitConverter.ToUInt32(respbuf, 3);
                   _info[i * 2 + 1] = BitConverter.ToUInt32(respbuf, 7);

               }
               catch (TimeoutException)
               {
                   MessageBox.Show("timeout!!!\r\n");
               }
           }
           string _mes = $"Стартовый адрес прошивки: {_info[0]} \r\n";
           _mes += $"Количество байт прошивки:       {_info[1]} \r\n";
           _mes += $"Контрольная сумма:              {_info[2]} \r\n";
           _mes += $"Версия прошивки:                {_info[3]} \r\n";
           _mes += $"Дата загрузки прошивки:         {_info[4]} \r\n";
           _mes += $"SPU ID:                         {_info[5]} \r\n";
           MessageBox.Show(_mes);

       }*/

/* string sn = addzero(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString().ToUpper(),
                                                 dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString().ToUpper());*/

/*byte[] erasebuf = { 0x43,0,   2, CountPagesErase,4,0,0,    0,0,0,0 };
uint ii = 0x11223344;
byte[] ib = BitConverter.GetBytes(ii);
Array.Reverse(ib);
uint jj = 0x55667788;
byte[] jb = BitConverter.GetBytes(jj);
Array.Reverse(jb);
Array.ConstrainedCopy(ib, 0, erasebuf, 3, 4);
Array.ConstrainedCopy(jb, 0, erasebuf, 7, 4);*/
/*  ulong iii = 0x1122334455667788;
byte[] iiib = BitConverter.GetBytes(iii);
Array.Reverse(iiib);
Array.ConstrainedCopy(iiib, 0, erasebuf, 3, iiib.Length); */
//byte[] iiib = BitConverter.GetBytes(iii);
//byte[] erasebuf = { 0x43, 0, 1, 0x88, 0x77, 0x66, 0x55, 0x44, 0x33, 0x22, 0x11 };
// byte[] data = BitConverter.GetBytes(Int32.Parse((String)dataGridView1.Rows[e.RowIndex].Cells[1].Value));

/*byte[] data = new byte[sn.Length / 2];
for (int i = 0; i < sn.Length; i += 2)
{
    data[i / 2] = Convert.ToByte(sn.Substring(i, 2), 16);
}*/
//serialPort1.BaseStream.WriteAsync(data, 0, sn.Length / 2);

/*string s1 = sn.Insert(4, "    ");
string s2 = s1.Insert(10, "    ");
string s3 = s2.Insert(22, " ");*/

/* public static UInt64 ReverseBytes(UInt64 value)
        {
            return (value & 0x00000000000000FFUL) << 56 | (value & 0x000000000000FF00UL) << 40 |
                   (value & 0x0000000000FF0000UL) << 24 | (value & 0x00000000FF000000UL) << 8 |
                   (value & 0x000000FF00000000UL) >> 8 | (value & 0x0000FF0000000000UL) >> 24 |
                   (value & 0x00FF000000000000UL) >> 40 | (value & 0xFF00000000000000UL) >> 56;
        }

        public static string addzero(string pole0, string pole1)
        {
            string coms = pole0;
            string sp1 = pole1;
            if (sp1.Length > 16) sp1 = sp1.Remove(16);
            if (coms.Length > 8) coms = coms.Remove(8);

            uint idcan = Convert.ToUInt32(coms, 16);
            if (idcan < 0x7FF)
            {
                if (coms.Length == 1) coms = "0" + coms + "00"; // for id
                if (coms.Length == 2) coms = coms + "00";
                if (coms.Length == 3) coms = "0" + coms[2] + coms[0] + coms[1];
            }
            else
            {
                byte[] idcanbuf = BitConverter.GetBytes(idcan);
                Array.Reverse(idcanbuf);
                idcan = BitConverter.ToUInt32(idcanbuf, 0);
                coms = Convert.ToString(idcan, 16);
                while (coms.Length < 8) coms = "0" + coms;
            }

            coms = coms + "0";
            int len = sp1.Length / 2;
            coms = coms + (len.ToString("X")); // for bts cnt

            ulong data = 0;
            string s1 = string.Empty;
            try
            {
                data = Convert.ToUInt64(sp1, 16);
                data = ReverseBytes(data); // for data
                s1 = data.ToString("X");
                if (s1.Length == 15) s1 = "0" + s1;
            }
            catch (ArgumentOutOfRangeException) { s1 = "0000000000000000"; }

            coms = coms + s1;
            return coms;
        }*/



/*Encoding enc = Encoding.GetEncoding("windows-1251"); // Подставь нужную ASCII-совместимую кодировку
                   StringBuilder sb = new StringBuilder(sn.Length / 2);
                   byte[] b = new byte[1];
                   for (int i = 0; i < sn.Length; i += 2)
                   {
                       b[0] = Convert.ToByte(sn.Substring(i, 2), 16);
                       sb.Append(enc.GetString(b));
                   }
                   string sci = sb.ToString();
                   serialPort1.Encoding.
                    serialPort1.Write(sci);*/




/*string s = "awdioawhnodnawndawdfyufuyyufuykfuykfvfvftjfufufyufuyfuykfuyf237827328827";
              COM_TO_CAN_FRAME frame = new COM_TO_CAN_FRAME();
               frame.id = Convert.ToUInt16( dataGridView1.Rows[e.RowIndex].Cells[0].Value);
               frame.data_l = Convert.ToUInt32(dataGridView1.Rows[e.RowIndex].Cells[1].Value);
               frame.bts_cnt = (char)((float)(frame.data_l.ToString("X").Length + frame.data_h.ToString("X").Length + 1) / 2);
               int bts_cnt = frame.bts_cnt;
               //s = $"Transmit: ID: {frame.id,-20:X} DATA: {frame.data,-30:X} BYTES: {bts_cnt,-20:X}\r\n";
               s = $"Trans: id={frame.id:X4} data={frame.data_l:X8} {frame.data_h:X8} bts={bts_cnt:X1}\r\n";
             //  richTextBox3.Text += s;
               ulong a = 0xf4a3;
               string sa = a.ToString("X");
               string ssa = Convert.ToString(a);
               string s1 = "f";
               int aa = 70;
               richTextBox3.Text += System.Convert.ToSByte(aa);
               /*foreach (char c in s1)
               {
                   richTextBox3.Text += System.Convert.ToInt32(c);
               }
               richTextBox3.Text += "\r\n";*/
//  string s = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();



//serialPort1.Write(bts_cnt);
/*switch (e.RowIndex)
{
    case 0:
        frame.bts_cnt = (char)4;
        s = $"Transmit: ID: {frame.id,-7:X} DATA: {frame.data:X}\r\n";
        richTextBox3.Text += s;
        //serialPort1.Write(frame.ToString());
        break;
    case 1:
        frame.bts_cnt = (char)5;
        s = $"Transmit: ID: {frame.id,-7:X} DATA: {frame.data:X}\r\n";
        richTextBox3.Text += s;
        //serialPort1.Write(frame.ToString());
        break;
    case 2:
        frame.bts_cnt = (char)5;
        s = $"Transmit: ID: {frame.id,-7:X} DATA: {frame.data:X}\r\n";
        richTextBox3.Text += s;
        //serialPort1.Write(frame.ToString());
        break;
    case 3:
        frame.bts_cnt = (char)5;
        s = $"Transmit: ID: {frame.id,-7:X} DATA: {frame.data:X}\r\n";
        richTextBox3.Text += s;
        serialPort1.Write(frame.ToString());
        break;
    default:
        richTextBox3.Text += "nope\r\n";
        break;


}*/
/*  string s1 = pole1;
              if (s1.Length > 8) s1.Remove(8);
              if (s1.Length % 2 != 0) s1 += "0"; // for data
              string s2 = string.Empty;
              for (int i = 0; i < s1.Length; i = i + 2)
              {
                  char cl = s1[s1.Length - 2 - i];
                  char ch = s1[s1.Length - 1 - i];
                  s2 += cl.ToString() + ch.ToString();
              }*/
/*   public static string ConvertHex(String hexString)
{
try
{
 string ascii = string.Empty;

 for (int i = 0; i < hexString.Length; i += 2)
 {
     String hs = string.Empty;

     hs = hexString.Substring(i, 2);
     uint decval = System.Convert.ToUInt32(hs, 16);
     char character = System.Convert.ToChar(decval);
     ascii += character;

 }

 return ascii;
}
catch (Exception ex) { Console.WriteLine(ex.Message); }

return string.Empty;
}*/
