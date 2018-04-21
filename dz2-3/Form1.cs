using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dz2_3
{
    public partial class Form1 : Form
    {
        List<KeyValuePair<string, double>> comboList;
        double resultSumm = 0;

         Timer myTimer = new Timer();
         bool exitFlag = false;

        public Form1()
        {
            InitializeComponent();
            myTimer.Tick += new EventHandler(TimerEventProcessor);

            comboList = new List<KeyValuePair<string, double>>();
            comboList.Add(new KeyValuePair<string, double>("А97", 16.40));
            comboList.Add(new KeyValuePair<string, double>("А97 Премиум", 10.10));
            comboList.Add(new KeyValuePair<string, double>("А95", 9.20));
            comboList.Add(new KeyValuePair<string, double>("А92", 8.50));
            comboList.Add(new KeyValuePair<string, double>("А86", 5.80));

            
            foreach (KeyValuePair<string, double> pair in comboList)
            {
                comboBox1.Items.Add(pair.Key);
            }
            comboBox1.SelectedIndex = 0;
            textBox11.Text = Convert.ToString(this.comboList[0].Value);
            comboBox1.SelectedIndexChanged += new EventHandler(ComboBox1_SelectedIndexChanged);
            radioButton1.CheckedChanged += new EventHandler(radioButton1_Chacked);
            checkBox1.CheckStateChanged += new EventHandler(checkBox_Checked);
            checkBox2.CheckStateChanged += new EventHandler(checkBox_Checked);
            checkBox3.CheckStateChanged += new EventHandler(checkBox_Checked);
            checkBox4.CheckStateChanged += new EventHandler(checkBox_Checked);
            this.FormClosing += new FormClosingEventHandler(Form_FormClosing);
        }

        private void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            MessageBox.Show("Общая выручка - " + this.resultSumm);
        }

        private  void TimerEventProcessor(Object myObject,
                                            EventArgs myEventArgs)
        {
            myTimer.Stop();
            if (MessageBox.Show("Следующий...", "Очистить форму?",
               MessageBoxButtons.YesNo) == DialogResult.No)
            {
                myTimer.Enabled = true;
            }
            else
            {
                exitFlag = true;
                label3.Text = "0.00";
                label5.Text = "0.00";
                label1.Text = "0.00";

                radioButton1.Checked = true;
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                checkBox4.Checked = false;

                textBox4.Text = "";
                textBox5.Text = "";
                textBox9.Text = "";
                textBox7.Text = "";

                textBox1.Text = "";
                textBox2.Text = "";
                comboBox1.SelectedIndex = 0;

            }
        }

        private void checkBox_Checked(object sender, System.EventArgs e)
        {
           
            if (checkBox1.Checked)
            {
                textBox4.ReadOnly = false;
            }
            else
            {
                textBox4.ReadOnly = true;
            }
            //////
            if (checkBox2.Checked)
            {
                textBox5.ReadOnly = false;
            }
            else
            {
                textBox5.ReadOnly = true;
            }
            /////
            if (checkBox3.Checked)
            {
                textBox9.ReadOnly = false;
            }
            else
            {
                textBox9.ReadOnly = true;
            }
            if (checkBox4.Checked)
            {
                textBox7.ReadOnly = false;
            }
            else
            {
                textBox7.ReadOnly = true;
            }
        }

        private void radioButton1_Chacked(object sender, System.EventArgs e)
        {
            if(radioButton1.Checked)
            {
                textBox1.ReadOnly = false;
                textBox2.ReadOnly = true;
                groupBox4.Text = "К оплате";
                label4.Text = "грн.";
            }
            else if(radioButton2.Checked)
            {
                textBox1.ReadOnly = true;
                textBox2.ReadOnly = false;
                groupBox4.Text = "К выдаче";
                label4.Text = "л.";
            }
        }

        private void ComboBox1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            int tmpKey = comboBox1.SelectedIndex;
            textBox11.Text = Convert.ToString(this.comboList[tmpKey].Value);

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            double az = 0.00, cafe = 0.00, summ = 0.00;
            int tmpKey = 0;

            tmpKey = comboBox1.SelectedIndex;

            if (checkBox1.Checked && textBox4.Text != "")
            {
                cafe += Convert.ToDouble(textBox4.Text, System.Globalization.CultureInfo.InvariantCulture) * Convert.ToDouble(textBox3.Text, System.Globalization.CultureInfo.InvariantCulture);
            }
            //////
            if (checkBox2.Checked)
            {
                cafe += (Convert.ToDouble(textBox5.Text, System.Globalization.CultureInfo.InvariantCulture) * Convert.ToDouble(textBox6.Text, System.Globalization.CultureInfo.InvariantCulture));
            }
            /////
            if (checkBox3.Checked)
            {
                cafe += (Convert.ToDouble(textBox10.Text, System.Globalization.CultureInfo.InvariantCulture) * Convert.ToDouble(textBox9.Text, System.Globalization.CultureInfo.InvariantCulture));
            }
            if (checkBox4.Checked)
            {
                cafe += (Convert.ToDouble(textBox8.Text, System.Globalization.CultureInfo.InvariantCulture) * Convert.ToDouble(textBox7.Text, System.Globalization.CultureInfo.InvariantCulture));
            }

            if(radioButton1.Checked)
            {

                if(textBox1.Text != "")
                {
                    az = (Convert.ToDouble(this.comboList[tmpKey].Value, System.Globalization.CultureInfo.InvariantCulture) * Convert.ToDouble(textBox1.Text, System.Globalization.CultureInfo.InvariantCulture));
                    
                }
                label3.Text = Convert.ToString(az);
            }
            else
            {
                if (textBox2.Text != "")
                {
                    az = Convert.ToDouble(textBox2.Text);
                }
                
                label3.Text = Convert.ToString(az/ this.comboList[tmpKey].Value);
            }
            
            label5.Text = Convert.ToString(cafe);
            label1.Text = Convert.ToString(cafe + az);

            this.resultSumm += cafe + az;
            myTimer.Interval = 10000;
            myTimer.Start();
        }
    }
}
