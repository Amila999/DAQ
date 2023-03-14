using System;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Globalization;

namespace DAQ
{
    public partial class Form1 : Form
    {
        private readonly Random random = new Random();
        StreamWriter writer = new StreamWriter("C:\\Users\\Public\\Documents\\data.csv");

        //ADC converter is 8bits
        private float analogResolution= 10f / 254f;
        private float temperatureResolution = 75f / 254f;
        private float analogValue;
        private int digitalValue;
        private string dataForFile;

        public Form1()
        {
            InitializeComponent();
            //Sampling time is 500ms
            timer1.Interval = 500;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            dataForFile = "";
            analogSensorValue();
            temperatureSensorValues();
            digitalSensorValues();
            writeToFile();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            writer.Close();
        }

        private void analogSensorValue() 
        {
            for (int i = 1; i < 5; i++) 
            {
                var adc = random.Next(0, 255);
                TextBox textBox = this.Controls.Find("txtAnalog"+i.ToString(),true).FirstOrDefault() as TextBox;
                analogValue = adc * analogResolution - 5;
                textBox.Text = analogValue.ToString();
                dataForFile = dataForFile + analogValue.ToString(CultureInfo.InvariantCulture) + ",";
            }
        }

        private void temperatureSensorValues() 
        {
            for (int i = 1; i < 3; i++)
            {
                var adc = random.Next(0, 255);
                TextBox textBox = this.Controls.Find("txtTemperature" + i.ToString(), true).FirstOrDefault() as TextBox;
                analogValue = adc * temperatureResolution;
                textBox.Text = analogValue.ToString();
                dataForFile = dataForFile + analogValue.ToString(CultureInfo.InvariantCulture) + ",";
            }
        }
        private void digitalSensorValues()
        {
            for (int i = 1; i < 11; i++)
            {
                var adc = random.Next(0, 2);
                TextBox textBox = this.Controls.Find("txtDigital" + i.ToString(), true).FirstOrDefault() as TextBox;
                digitalValue = adc*10-5;
                textBox.Text = digitalValue.ToString();
                if (i != 10)
                {
                    dataForFile = dataForFile + digitalValue.ToString(CultureInfo.InvariantCulture) + ",";
                }
                else 
                {
                    dataForFile = dataForFile + digitalValue.ToString(CultureInfo.InvariantCulture);
                }
            }
        }

        private void writeToFile() 
        {
            DateTime now = DateTime.Now;
            string formattedDate = now.ToString("yyyy-MM-dd HH:mm:ss");
            dataForFile = formattedDate + ","+ dataForFile;
            writer.WriteLine(dataForFile);
        }
    }
}
