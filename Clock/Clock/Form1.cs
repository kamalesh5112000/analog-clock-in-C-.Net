using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PA5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

      


        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string hourString = numericUpDown2.Value.ToString() + ":";
            string minuteString = ":" + numericUpDown3.Value.ToString() + ":";
            if(numericUpDown3.Value < 10) minuteString = ":0" + numericUpDown3.Value.ToString() + ":";
            string secondString = ":" + numericUpDown4.Value.ToString() + " ";
            if (numericUpDown4.Value < 10) secondString = ":0" + numericUpDown4.Value.ToString() + " ";
            if (textBox1.Text.Contains(hourString) 
                && textBox1.Text.Contains(minuteString) 
                && textBox1.Text.Contains(secondString) 
                && textBox1.Text.Contains(domainUpDown1.Text))
            {
                textBox3.Text = "Active";

            }
            if (listBox2.Items.Contains(textBox1.Text))
            {

                SystemSounds.Beep.Play();
                listBox2.SetSelected(listBox2.FindString(textBox1.Text), true);
                pictureBox1.BringToFront();
                pictureBox1.Focus();
                
            }


        }

        private void timer1_Tick(object sender, EventArgs e)//Seconds
        {
            textBox1.Text = DateTime.Now.ToString();
            if(textBox3.Text == "Active")
                SystemSounds.Beep.Play();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void Snooze_Tick(object sender, EventArgs e)
        {
            textBox3.Text = "Active";
            Snooze.Enabled = false;
            
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e) //Snooze
        {
            if (textBox3.Text == "Active")
            {
                Snooze.Interval = (int)numericUpDown1.Value * 1000;
                textBox3.Text = "Inactive";
                Snooze.Enabled = true;
            }

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e) //Stop Alarm
        {
            textBox3.Text = "Inactive";
        }

        private void button1_Click_1(object sender, EventArgs e)//Create event
        {
            listBox1.Items.Add(dateTimePicker1.Text);
            richTextBox1.Text = richTextBox1.Text + "\r\n\r\n" + dateTimePicker1.Text + ":\r\n" +
                "--------------------------------------\r\n";
            richTextBox1.Focus();
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
        }

        private void textBox5_Enter(object sender, EventArgs e)
        {
            
        }

        private void numericUpDown7_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)//Reminder
        {
            if (listBox1.SelectedIndex != -1)
            {
                listBox2.Items.Add(dateTimePicker1.Text);//reminder date
                listBox3.Items.Add(listBox1.SelectedItem);//reminder subject
            }
            
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            richTextBox1.BringToFront();
            richTextBox1.Focus();
            richTextBox1.SelectionStart = 62+ richTextBox1.Find(listBox1.SelectedItem.ToString());
            richTextBox1.SelectionLength = 0;
                   
        }

        private void richTextBox1_Leave(object sender, EventArgs e)
        {
            listBox1.BringToFront();
        }

        private void button5_Click(object sender, EventArgs e)//back
        {
            listBox1.BringToFront();
        }

        private void button6_Click(object sender, EventArgs e)//delete
        {
            if (listBox1.SelectedIndex != -1)
            {
                richTextBox1.Text = richTextBox1.Text.Remove
                    (richTextBox1.Find(listBox1.SelectedItem.ToString()), listBox1.SelectedItem.ToString().Length);
                richTextBox1.Text = richTextBox1.Text.Remove
                    (richTextBox1.Text.IndexOf(":"), richTextBox1.Text.IndexOf("/")-4);
                listBox1.Items.Remove(listBox1.SelectedItem);
            }

        }

        private void list_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            richTextBox1.BringToFront();
            richTextBox1.Focus();
            richTextBox1.SelectionStart = 65 + richTextBox1.Find(listBox2.SelectedItem.ToString());
            richTextBox1.SelectionLength = 0;
        }
    }
}
