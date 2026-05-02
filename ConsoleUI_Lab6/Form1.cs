using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using System.IO;
using System.Xml.Linq;
using Core;

namespace ConsoleUI
{
    public partial class Form1 : Form
    {
        private BindingSource bindingSource;
        private List<EventBase> events;

        public Form1()
        {
            InitializeComponent();
            InitializeData();
        }

        private void InitializeData()
        {
            // Ініціалізація тестових даних
            events = new List<EventBase>
            {
                new ConcertEvent("Rock Festival", new DateTime(2026, 8, 15), 800, true),
                new ConcertEvent("Jazz Night", new DateTime(2026, 9, 20), 500, false),
                new ConferenceEvent("Tech Summit", new DateTime(2026, 10, 5), 1200, 3),
                new ConferenceEvent("Business Forum", new DateTime(2026, 11, 12), 900, 2)
            };

            // Налаштування BindingSource
            bindingSource = new BindingSource();
            bindingSource.DataSource = events;
            dataGridView1.DataSource = bindingSource;

            // Налаштування DataGridView
            ConfigureDataGridView();
        }

        private void ConfigureDataGridView()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Clear();

            // Колонка типу події
            DataGridViewTextBoxColumn typeColumn = new DataGridViewTextBoxColumn();
            typeColumn.Name = "Type";
            typeColumn.HeaderText = "Тип";
            typeColumn.DataPropertyName = "Type";
            dataGridView1.Columns.Add(typeColumn);

            // Колонка назви
            DataGridViewTextBoxColumn titleColumn = new DataGridViewTextBoxColumn();
            titleColumn.Name = "Title";
            titleColumn.HeaderText = "Назва";
            titleColumn.DataPropertyName = "Title";
            dataGridView1.Columns.Add(titleColumn);

            // Колонка дати
            DataGridViewTextBoxColumn dateColumn = new DataGridViewTextBoxColumn();
            dateColumn.Name = "Date";
            dateColumn.HeaderText = "Дата";
            dateColumn.DataPropertyName = "Date";
            dateColumn.DefaultCellStyle.Format = "dd.MM.yyyy";
            dataGridView1.Columns.Add(dateColumn);

            // Колонка ціни
            DataGridViewTextBoxColumn priceColumn = new DataGridViewTextBoxColumn();
            priceColumn.Name = "BasePrice";
            priceColumn.HeaderText = "Базова ціна";
            priceColumn.DataPropertyName = "BasePrice";
            priceColumn.DefaultCellStyle.Format = "N2";
            dataGridView1.Columns.Add(priceColumn);

            // Колонка фінальної ціни
            DataGridViewTextBoxColumn finalPriceColumn = new DataGridViewTextBoxColumn();
            finalPriceColumn.Name = "FinalPrice";
            finalPriceColumn.HeaderText = "Фінальна ціна";
            dataGridView1.Columns.Add(finalPriceColumn);

            // Оновлення значень після прив'язки
            dataGridView1.DataBindingComplete += DataGridView1_DataBindingComplete;
        }

        private void DataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            // Оновлюємо колонку фінальної ціни після прив'язки даних
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.DataBoundItem is EventBase eventItem)
                {
                    row.Cells["FinalPrice"].Value = $"{eventItem.CalculateFinalPrice():N2} UAH";
                    row.Cells["Type"].Value = eventItem is ConcertEvent ? "Концерт" : "Конференція";
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var editForm = new EventEditForm())
            {
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    events.Add(editForm.Event);
                    bindingSource.ResetBindings(false);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                var selectedEvent = dataGridView1.CurrentRow.DataBoundItem as EventBase;
                if (selectedEvent != null)
                {
                    var result = MessageBox.Show(
                        $"Видалити подію '{selectedEvent.Title}'?",
                        "Підтвердження видалення",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        events.Remove(selectedEvent);
                        bindingSource.ResetBindings(false);
                    }
                }
            }
            else
            {
                MessageBox.Show("Виберіть подію для видалення", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSaveJson_Click(object sender, EventArgs e)
        {
            using (var saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "JSON файли (*.json)|*.json|Всі файли (*.*)|*.*";
                saveDialog.DefaultExt = "json";
                saveDialog.FileName = "events.json";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var options = new JsonSerializerOptions { WriteIndented = true };
                        using (var stream = File.Create(saveDialog.FileName))
                        {
                            JsonSerializer.Serialize(stream, events, options);
                        }
                        MessageBox.Show("Дані збережено успішно!", "Збереження", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Помилка збереження: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnLoadJson_Click(object sender, EventArgs e)
        {
            using (var openDialog = new OpenFileDialog())
            {
                openDialog.Filter = "JSON файли (*.json)|*.json|Всі файли (*.*)|*.*";
                openDialog.DefaultExt = "json";

                if (openDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (var stream = File.OpenRead(openDialog.FileName))
                        {
                            var loadedEvents = JsonSerializer.Deserialize<List<EventBase>>(stream);
                            if (loadedEvents != null)
                            {
                                events.Clear();
                                events.AddRange(loadedEvents);
                                bindingSource.ResetBindings(false);
                                MessageBox.Show("Дані завантажено успішно!", "Завантаження", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Помилка завантаження: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnSaveXml_Click(object sender, EventArgs e)
        {
            using (var saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "XML файли (*.xml)|*.xml|Всі файли (*.*)|*.*";
                saveDialog.DefaultExt = "xml";
                saveDialog.FileName = "events.xml";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var xmlDoc = new XDocument(
                            new XElement("Events",
                                from ev in events
                                select new XElement(ev is ConcertEvent ? "Concert" : "Conference",
                                    new XElement("Title", ev.Title),
                                    new XElement("Date", ev.Date.ToShortDateString()),
                                    new XElement("BasePrice", ev.BasePrice),
                                    new XElement("FinalPrice", ev.CalculateFinalPrice()),
                                    ev is ConcertEvent concert ? new XElement("VipZone", concert.IsVipZoneAvailable) :
                                    ev is ConferenceEvent conference ? new XElement("CoffeeBreaks", conference.CoffeeBreaksCount) : null
                                )
                            )
                        );

                        xmlDoc.Save(saveDialog.FileName);
                        MessageBox.Show("XML експортовано успішно!", "Експорт", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Помилка експорту: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnLoadXml_Click(object sender, EventArgs e)
        {
            using (var openDialog = new OpenFileDialog())
            {
                openDialog.Filter = "XML файли (*.xml)|*.xml|Всі файли (*.*)|*.*";
                openDialog.DefaultExt = "xml";

                if (openDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var xmlDoc = XDocument.Load(openDialog.FileName);
                        var loadedEvents = new List<EventBase>();

                        foreach (var element in xmlDoc.Root.Elements())
                        {
                            string title = element.Element("Title")?.Value ?? "";
                            DateTime date = DateTime.Parse(element.Element("Date")?.Value ?? DateTime.Now.ToShortDateString());
                            double basePrice = double.Parse(element.Element("BasePrice")?.Value ?? "0");

                            if (element.Name == "Concert")
                            {
                                bool vipZone = bool.Parse(element.Element("VipZone")?.Value ?? "false");
                                loadedEvents.Add(new ConcertEvent(title, date, basePrice, vipZone));
                            }
                            else if (element.Name == "Conference")
                            {
                                int coffeeBreaks = int.Parse(element.Element("CoffeeBreaks")?.Value ?? "0");
                                loadedEvents.Add(new ConferenceEvent(title, date, basePrice, coffeeBreaks));
                            }
                        }

                        events.Clear();
                        events.AddRange(loadedEvents);
                        bindingSource.ResetBindings(false);
                        MessageBox.Show("XML імпортовано успішно!", "Імпорт", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Помилка імпорту: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}