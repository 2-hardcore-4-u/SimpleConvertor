using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime;

namespace SimpleConvertor
{
    public partial class Form1 : Form
    {
        delegate void Convertation(string inputString, object presentCurrencyBox, object neededCurrencyBox);
        event Convertation Convert;

        List<Currency> currencies1 = new List<Currency>();
        List<Currency> currencies2 = new List<Currency>();

        public Form1()
        {
            InitializeComponent();

            currencies1.Add(new Currency() { Name = "₴", Value = "UAH", RelationToUAH = 1});
            currencies1.Add(new Currency() { Name = "₽", Value = "RUB", RelationToUAH = 27.80M});
            currencies1.Add(new Currency() { Name = "$", Value = "USD", RelationToUAH = 0.37M});

            currencies2.Add(new Currency() { Name = "₴", Value = "UAH", RelationToUAH = 1 });
            currencies2.Add(new Currency() { Name = "₽", Value = "RUB", RelationToUAH = 27.80M });
            currencies2.Add(new Currency() { Name = "$", Value = "USD", RelationToUAH = 0.37M });

            this.comboBox1.DataSource = currencies1;
            this.comboBox1.DisplayMember = "Name";
            this.comboBox1.ValueMember = "Value";

            this.comboBox2.DataSource = currencies2;
            this.comboBox2.DisplayMember = "Name";
            this.comboBox2.ValueMember = "Value";

            this.comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;

            Convert += ConvertationHandler;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Convert?.Invoke(textBox1.Text, comboBox1.SelectedItem , comboBox2.SelectedItem);
        }

        private void ConvertationHandler(string inputString, object presentCurrencyBox, object neededCurrencyBox)
        {
            decimal inputNumber = 0;
            try
            {
                inputNumber = Decimal.Parse(inputString);
            }
            catch (FormatException)
            {
                string message = "Input is not formatted right";
                string title = "Error";
                MessageBox.Show(message, title);
                textBox1.Text = "0";
                return;
            }
            (Currency c1, Currency c2) = (currencies1[comboBox1.SelectedIndex], currencies2[comboBox2.SelectedIndex]);
            decimal coef1 = c1.RelationToUAH;
            decimal coef2 = c2.RelationToUAH;
            decimal outputNumber = inputNumber / coef1 * coef2;
            textBox2.Text = outputNumber.ToString();
        }
    }
}
