namespace ConsoleUI
{
    partial class EventEditForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.labelVipZone = new System.Windows.Forms.Label();
            this.labelCoffeeBreaks = new System.Windows.Forms.Label();
            this.textBoxTitle = new System.Windows.Forms.TextBox();
            this.comboBoxType = new System.Windows.Forms.ComboBox();
            this.dateTimePickerEvent = new System.Windows.Forms.DateTimePicker();
            this.numericUpDownBasePrice = new System.Windows.Forms.NumericUpDown();
            this.checkBoxVipZone = new System.Windows.Forms.CheckBox();
            this.numericUpDownCoffeeBreaks = new System.Windows.Forms.NumericUpDown();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBasePrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCoffeeBreaks)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Тип:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Назва:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Дата:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 105);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 15);
            this.label4.TabIndex = 3;
            this.label4.Text = "Базова ціна:";
            // 
            // labelVipZone
            // 
            this.labelVipZone.AutoSize = true;
            this.labelVipZone.Location = new System.Drawing.Point(12, 135);
            this.labelVipZone.Name = "labelVipZone";
            this.labelVipZone.Size = new System.Drawing.Size(58, 15);
            this.labelVipZone.TabIndex = 4;
            this.labelVipZone.Text = "VIP-зона:";
            // 
            // labelCoffeeBreaks
            // 
            this.labelCoffeeBreaks.AutoSize = true;
            this.labelCoffeeBreaks.Location = new System.Drawing.Point(12, 135);
            this.labelCoffeeBreaks.Name = "labelCoffeeBreaks";
            this.labelCoffeeBreaks.Size = new System.Drawing.Size(95, 15);
            this.labelCoffeeBreaks.TabIndex = 5;
            this.labelCoffeeBreaks.Text = "Кава-брейків:";
            this.labelCoffeeBreaks.Visible = false;
            // 
            // textBoxTitle
            // 
            this.textBoxTitle.Location = new System.Drawing.Point(120, 42);
            this.textBoxTitle.Name = "textBoxTitle";
            this.textBoxTitle.Size = new System.Drawing.Size(200, 23);
            this.textBoxTitle.TabIndex = 6;
            // 
            // comboBoxType
            // 
            this.comboBoxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxType.Location = new System.Drawing.Point(120, 12);
            this.comboBoxType.Name = "comboBoxType";
            this.comboBoxType.Size = new System.Drawing.Size(121, 23);
            this.comboBoxType.TabIndex = 7;
            // 
            // dateTimePickerEvent
            // 
            this.dateTimePickerEvent.Location = new System.Drawing.Point(120, 72);
            this.dateTimePickerEvent.Name = "dateTimePickerEvent";
            this.dateTimePickerEvent.Size = new System.Drawing.Size(200, 23);
            this.dateTimePickerEvent.TabIndex = 8;
            // 
            // numericUpDownBasePrice
            // 
            this.numericUpDownBasePrice.DecimalPlaces = 2;
            this.numericUpDownBasePrice.Location = new System.Drawing.Point(120, 102);
            this.numericUpDownBasePrice.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDownBasePrice.Name = "numericUpDownBasePrice";
            this.numericUpDownBasePrice.Size = new System.Drawing.Size(120, 23);
            this.numericUpDownBasePrice.TabIndex = 9;
            // 
            // checkBoxVipZone
            // 
            this.checkBoxVipZone.AutoSize = true;
            this.checkBoxVipZone.Location = new System.Drawing.Point(120, 133);
            this.checkBoxVipZone.Name = "checkBoxVipZone";
            this.checkBoxVipZone.Size = new System.Drawing.Size(15, 14);
            this.checkBoxVipZone.TabIndex = 10;
            this.checkBoxVipZone.UseVisualStyleBackColor = true;
            // 
            // numericUpDownCoffeeBreaks
            // 
            this.numericUpDownCoffeeBreaks.Location = new System.Drawing.Point(120, 132);
            this.numericUpDownCoffeeBreaks.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericUpDownCoffeeBreaks.Name = "numericUpDownCoffeeBreaks";
            this.numericUpDownCoffeeBreaks.Size = new System.Drawing.Size(120, 23);
            this.numericUpDownCoffeeBreaks.TabIndex = 11;
            this.numericUpDownCoffeeBreaks.Visible = false;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(120, 170);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 12;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(201, 170);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "Скасувати";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // EventEditForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(334, 205);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.numericUpDownCoffeeBreaks);
            this.Controls.Add(this.checkBoxVipZone);
            this.Controls.Add(this.numericUpDownBasePrice);
            this.Controls.Add(this.dateTimePickerEvent);
            this.Controls.Add(this.comboBoxType);
            this.Controls.Add(this.textBoxTitle);
            this.Controls.Add(this.labelCoffeeBreaks);
            this.Controls.Add(this.labelVipZone);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EventEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Редагування події";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBasePrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCoffeeBreaks)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelVipZone;
        private System.Windows.Forms.Label labelCoffeeBreaks;
        private System.Windows.Forms.TextBox textBoxTitle;
        private System.Windows.Forms.ComboBox comboBoxType;
        private System.Windows.Forms.DateTimePicker dateTimePickerEvent;
        private System.Windows.Forms.NumericUpDown numericUpDownBasePrice;
        private System.Windows.Forms.CheckBox checkBoxVipZone;
        private System.Windows.Forms.NumericUpDown numericUpDownCoffeeBreaks;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}