using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        DataTable table = new DataTable();
        List<ProgressBarEx> progressBar = new List<ProgressBarEx>();
        Random r = new Random();
        double memorycount = 0;
        int memory = 5000;
        int datagridrows = 0;
        int segmentsintotal = 0;
        int segmentscount = 10;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            double segmentsize = Convert.ToInt32(numericUpDown2.Value);
            int memory1 = Convert.ToInt32(numericUpDown1.Value);
            segmentscount = memory1 / Convert.ToInt32(segmentsize);
            segmentsize = (segmentsize / memory1) * panel1.Size.Height;
            
            int width = 0;
            datagridrows = Convert.ToInt32(dataGridView1.Rows.Count);
            for (int i = 0; i < datagridrows; i++)
            {
                int segmentamount = Convert.ToInt32(numericUpDown2.Value);   
                int x = (Convert.ToInt32(dataGridView1["Размер", i].Value) % segmentamount);
                if (x != 0) segmentamount = ((Convert.ToInt32(dataGridView1["Размер", i].Value) / segmentamount) + 1);
                else segmentamount = ((Convert.ToInt32(dataGridView1["Размер", i].Value) / segmentamount));

                if (Convert.ToString(dataGridView1["Статус", i].Value) == "В ожидании" && (segmentamount + segmentsintotal) <= segmentscount)
                {
                    ProgressBarEx progress = new ProgressBarEx();
                    progress.Step = 1;
                    progress.Minimum = 0;
                    progress.Maximum = Convert.ToInt32(dataGridView1["Время", i].Value);

                    memorycount = (Convert.ToDouble(dataGridView1["Размер", i].Value) / memory1) * panel1.Size.Height;
                    progress.Size = new Size(300, Convert.ToInt32(memorycount));

                    panel1.Controls.Add(progress);
                    progressBar.Add(progress);
                    progress.BringToFront();

                    dataGridView1["Статус", i].Value = "В процессе";
                    segmentsintotal += segmentamount;
                }
                else if ((segmentamount + segmentsintotal) > segmentscount) break;

            }
            
            for (int i = 0; i < progressBar.Count; i++)
            {
                int segmentamount = Convert.ToInt32(numericUpDown2.Value);
                int y = (Convert.ToInt32(dataGridView1["Размер", i].Value) % segmentamount);
                
                if (y != 0) segmentamount = ((Convert.ToInt32(dataGridView1["Размер", i].Value) / segmentamount) + 1);
                else segmentamount = ((Convert.ToInt32(dataGridView1["Размер", i].Value) / segmentamount));
                if (progressBar[i].Value < progressBar[i].Maximum && Convert.ToString(dataGridView1["Статус", i].Value) == "В процессе")
                {
                    string text = Convert.ToString(dataGridView1["Имя", i].Value);
                    progressBar[i].CustomText = text;

                    progressBar[i].Location = new Point(0, width);
                    width = width + (segmentamount * Convert.ToInt32(segmentsize));
                    progressBar[i].Value = progressBar[i].Value + 1;
                }
                else if (progressBar[i].Value == progressBar[i].Maximum && Convert.ToString(dataGridView1["Статус", i].Value) == "В процессе")
                {
                    segmentsintotal -= segmentamount;
                    panel1.Controls.Remove(progressBar[i]);
                    int x = r.Next(100);
                    
                    if (x < numericUpDown3.Value)
                    {
                        dataGridView1["Статус", i].Value = "ОК";
                    }
                    else if (x >= numericUpDown3.Value) dataGridView1["Статус", i].Value = "Ошибка";        
                }

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != String.Empty && textBox3.Text != String.Empty) // проверка корректности ввода данных 
            {
                int memory1 = Convert.ToInt32(numericUpDown1.Value);
                bool isTimeInt = Int32.TryParse(textBox2.Text, out int timevalue);
                bool isSizeInt = Int32.TryParse(textBox3.Text, out int sizevalue);
                int i = Convert.ToInt32(textBox2.Text);
                if (i <= memory1)
                {
                    if (isTimeInt && isSizeInt)
                    {
                        string result = textBox1.Text;
                        if (textBox1.Text == String.Empty)
                        {
                            int x = r.Next(10000);
                            string[] task = new string[2];
                            task[0] = "Задача ";
                            task[1] = x.ToString();
                            result = String.Concat(task);

                        }
                        table.Rows.Add(result, Convert.ToInt32(textBox2.Text), Convert.ToInt32(textBox3.Text), "В ожидании"); // добавление записи в таблицу
                    }

                }
                else listBox1.Items.Add("Недостаточно ОП для выполнения задачи");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int rowIndex = dataGridView1.CurrentCell.RowIndex;
            if (dataGridView1.Rows.Count > 1 && Convert.ToString(dataGridView1["Статус", rowIndex].Value) == "В процессе")  // удаление выделенной строки
            {
                listBox1.Items.Add("Нельзя удалить задачу, которая уже выполняется");
            }
            else 
            {
                dataGridView1.Rows.RemoveAt(rowIndex);
                progressBar.RemoveAt(rowIndex);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            while (dataGridView1.Rows.Count > 1) // очистка таблицы
            { 
                dataGridView1.Rows.Remove(dataGridView1.Rows[0]);
            }
            progressBar.Clear(); 
            panel1.Controls.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count != 0)
            {
                timer1.Enabled = true;
                int segmentsize = ((Convert.ToInt32(numericUpDown2.Value) / memory) * panel1.Size.Height);

            }
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int segmentsize = Convert.ToInt32(numericUpDown2.Value);
            int memory1 = Convert.ToInt32(numericUpDown1.Value);

            int x = memory1 % segmentsize;
            int m = memory1 / segmentsize;
            if (x != 0)
            {
                memory = m * segmentsize;
                listBox1.Items.Add(memory);
                numericUpDown1.Value = memory;
                segmentscount = m;
            }
            panel1.Controls.Clear();
            for (int i = 1; i < m; i++)
            { 
                int s = 400 / m;
                PictureBox pic = new PictureBox();
                panel1.Controls.Add(pic);
                pic.Location = new Point(0, i * s);
                pic.Size = new Size(300, 1);
                pic.BackColor = Color.Black;
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            table.Columns.Add("Имя", typeof(string));
            table.Columns.Add("Размер", typeof(int));
            table.Columns.Add("Время", typeof(int));
            table.Columns.Add("Статус", typeof(string));
            dataGridView1.DataSource = table;

            dataGridView1.Columns[0].Width = 100;
            dataGridView1.Columns[1].Width = 75;
            dataGridView1.Columns[2].Width = 75;
            dataGridView1.Columns[3].Width = 150;
            dataGridView1.RowHeadersVisible = false;

            numericUpDown1.Maximum = int.MaxValue;
            numericUpDown1.Minimum = 0;
            numericUpDown1.Value = 5000;

            numericUpDown2.Maximum = int.MaxValue;
            numericUpDown2.Minimum = 0;
            numericUpDown2.Value = 1000;

            numericUpDown3.Maximum = 100;
            numericUpDown3.Minimum = 0;
            numericUpDown3.Value = 50;

            for (int i = 1; i < 5; i++)
            { 
                PictureBox pic = new PictureBox();
                panel1.Controls.Add(pic);
                pic.Location = new Point(0, i * 80);
                pic.Size = new Size(300, 1);
                pic.BackColor = Color.Black;
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }
    }
}
