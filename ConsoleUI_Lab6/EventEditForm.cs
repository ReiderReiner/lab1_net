using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Core;

namespace ConsoleUI
{
    public partial class EventEditForm : Form
    {
        public EventBase Event { get; private set; }

        public EventEditForm()
        {
            InitializeComponent();
            InitializeForm();
        }

        public EventEditForm(EventBase existingEvent) : this()
        {
            LoadEventData(existingEvent);
        }

        private void InitializeForm()
        {
            // Налаштування ComboBox для типу події
            comboBoxType.Items.AddRange(new string[] { "Концерт", "Конференція" });
            comboBoxType.SelectedIndex = 0;

            // Налаштування DateTimePicker
            dateTimePickerEvent.Value = DateTime.Now;

            // Обробники подій
            comboBoxType.SelectedIndexChanged += ComboBoxType_SelectedIndexChanged;
            UpdateFormForEventType();
        }

        private void ComboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateFormForEventType();
        }

        private void UpdateFormForEventType()
        {
            bool isConcert = comboBoxType.SelectedItem?.ToString() == "Концерт";

            labelVipZone.Visible = isConcert;
            checkBoxVipZone.Visible = isConcert;

            labelCoffeeBreaks.Visible = !isConcert;
            numericUpDownCoffeeBreaks.Visible = !isConcert;
        }

        private void LoadEventData(EventBase existingEvent)
        {
            textBoxTitle.Text = existingEvent.Title;
            dateTimePickerEvent.Value = existingEvent.Date;
            numericUpDownBasePrice.Value = (decimal)existingEvent.BasePrice;

            if (existingEvent is ConcertEvent concert)
            {
                comboBoxType.SelectedItem = "Концерт";
                checkBoxVipZone.Checked = concert.IsVipZoneAvailable;
            }
            else if (existingEvent is ConferenceEvent conference)
            {
                comboBoxType.SelectedItem = "Конференція";
                numericUpDownCoffeeBreaks.Value = conference.CoffeeBreaksCount;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxTitle.Text))
            {
                MessageBox.Show("Введіть назву події", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string title = textBoxTitle.Text.Trim();
                DateTime date = dateTimePickerEvent.Value;
                double basePrice = (double)numericUpDownBasePrice.Value;

                if (comboBoxType.SelectedItem?.ToString() == "Концерт")
                {
                    Event = new ConcertEvent(title, date, basePrice, checkBoxVipZone.Checked);
                }
                else
                {
                    Event = new ConferenceEvent(title, date, basePrice, (int)numericUpDownCoffeeBreaks.Value);
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка створення події: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}