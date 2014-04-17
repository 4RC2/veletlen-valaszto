using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace VéletlenVálasztó
{
    public partial class Form1 : Form
    {
        String újElem;
        Random véletlen;
        Int32 elemIndex;
        Int32 választásokSzáma;

        OpenFileDialog megnyitás;
        Int32 fájlAzonosító;
        String fájlNév;
        String fájlÚtvonal;
        Int32 fájlokSzáma;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            újElem = textBox1.Text;
            this.Text = "Véletlen választó program";
            button4.Text = "Választás";
            választásokSzáma = 0;
            label1.Text = "0";
            label2.Text = "";

            if (textBox1.Text != "")
            {
                listBox1.Items.Add(újElem);
                textBox1.Text = "";
            }
            else
            {
                MessageBox.Show("Írja be a hozzáadni kívánt elemet a szövegdobozba!", "Véletlen választó program", 
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1 && listBox1.Items.Count != 0)
            {
                this.Text = "Véletlen választó program";
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
                button4.Text = "Választás";
                választásokSzáma = 0;
                label1.Text = "0";
                label2.Text = "";
            }
            else if (listBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Jelölje meg az eltávolítani kívánt elemet!", "Véletlen választó program",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (listBox1.Items.Count == 0)
            {
                MessageBox.Show("Adjon hozzá elemeket a listához!", "Véletlen választó program",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count != 0)
            {
                this.Text = "Véletlen választó program";
                listBox1.Items.Clear();
                button4.Enabled = true;
                button4.Text = "Választás";
                választásokSzáma = 0;
                label1.Text = "0";
                label2.Text = "";
            }
            else
            {
                MessageBox.Show("Adjon hozzá elemeket a listához!", "Véletlen választó program",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count > 1)
            {
                véletlen = new Random();
                elemIndex = véletlen.Next(listBox1.Items.Count);

                if (választásokSzáma < 999)
                {
                    választásokSzáma += 1;
                    label1.Text = Convert.ToString(választásokSzáma);
                }
                else
                {
                    választásokSzáma = 999;
                    label1.Text = "999";
                    button4.Enabled = false;
                }

                label2.Visible = true;
                label2.Text = listBox1.Items[elemIndex].ToString();
                button4.Text = "Választás újra";
            }
            else if (listBox1.Items.Count == 0)
            {
                MessageBox.Show("Adjon hozzá elemeket a listához!", "Véletlen választó program",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (listBox1.Items.Count == 1)
            {
                MessageBox.Show("Adjon hozzá több elemet a listához!", "Véletlen választó program",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void megnyitásToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.listBox1.Items.Clear();
            megnyitás = new OpenFileDialog();
            megnyitás.Filter = "Szöveges dokumentum|*.txt";
            megnyitás.Title = "Lista megnyitása";

            try
            {
                megnyitás.ShowDialog();
                StreamReader Import = new StreamReader(megnyitás.FileName.ToString(), Encoding.Default);

                while (Import.Peek() >= 0)
                {
                    listBox1.Items.Add(Convert.ToString(Import.ReadLine()));
                }

                this.Text = "Véletlen választó program - Megnyitott fájl: " + megnyitás.SafeFileName;
                button4.Text = "Választás";
                választásokSzáma = 0;
                label1.Text = "0";
                label2.Text = "";
            }
            catch (Exception kivétel)
            {
                MessageBox.Show(kivétel.Message, "Véletlen választó program",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mentésToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fájlÚtvonal = @Application.StartupPath + "\\listák\\";
            Directory.CreateDirectory(Path.GetDirectoryName(fájlÚtvonal));
            fájlokSzáma = Directory.GetFiles(fájlÚtvonal, "*.txt", SearchOption.AllDirectories).Count();
            fájlAzonosító = fájlokSzáma + 1;
            fájlNév = "lista" + fájlAzonosító + ".txt";

            if (listBox1.Items.Count >= 1)
            {
                    try
                    {
                        using (TextWriter szövegÍró = new StreamWriter(fájlÚtvonal + fájlNév))
                        {
                            foreach (string újElem in listBox1.Items)
                            {
                                szövegÍró.WriteLine(újElem);
                            }

                            MessageBox.Show("A fájl mentése sikeres volt.", "Véletlen választó program",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            MessageBox.Show("A fájl útvonala: " + Path.GetFullPath(fájlÚtvonal + fájlNév),
                                "Véletlen választó program",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception kivétel)
                    {
                        MessageBox.Show(Convert.ToString(kivétel), "Véletlen választó program",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
            }
            else
            {
                MessageBox.Show("Adjon hozzá elemeket a listához!", "Véletlen választó program",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}

