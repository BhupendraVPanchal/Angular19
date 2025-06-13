namespace OTAGenerator
{
    partial class Form1
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
            label1 = new Label();
            txt_from_name = new TextBox();
            txt_table_name = new TextBox();
            txt_module_name = new TextBox();
            label2 = new Label();
            label3 = new Label();
            btn_generate = new Button();
            panel1 = new Panel();
            dataGridView1 = new DataGridView();
            btn_get_column_info = new Button();
            label4 = new Label();
            txt_schema_name = new TextBox();
            button1 = new Button();
            button2 = new Button();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(277, 9);
            label1.Name = "label1";
            label1.Size = new Size(70, 15);
            label1.TabIndex = 0;
            label1.Text = "From Name";
            label1.Click += label1_Click;
            // 
            // txt_from_name
            // 
            txt_from_name.Location = new Point(261, 28);
            txt_from_name.Name = "txt_from_name";
            txt_from_name.Size = new Size(100, 23);
            txt_from_name.TabIndex = 1;
            txt_from_name.TextChanged += txt_from_name_TextChanged;
            // 
            // txt_table_name
            // 
            txt_table_name.Location = new Point(126, 28);
            txt_table_name.Name = "txt_table_name";
            txt_table_name.Size = new Size(100, 23);
            txt_table_name.TabIndex = 2;
            // 
            // txt_module_name
            // 
            txt_module_name.Location = new Point(386, 28);
            txt_module_name.Name = "txt_module_name";
            txt_module_name.Size = new Size(100, 23);
            txt_module_name.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(144, 9);
            label2.Name = "label2";
            label2.Size = new Size(69, 15);
            label2.TabIndex = 4;
            label2.Text = "Table Name";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(403, 9);
            label3.Name = "label3";
            label3.Size = new Size(83, 15);
            label3.TabIndex = 5;
            label3.Text = "Module Name";
            // 
            // btn_generate
            // 
            btn_generate.Location = new Point(678, 27);
            btn_generate.Name = "btn_generate";
            btn_generate.Size = new Size(75, 23);
            btn_generate.TabIndex = 6;
            btn_generate.Text = "Generate";
            btn_generate.UseVisualStyleBackColor = true;
            btn_generate.Click += btn_generate_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(dataGridView1);
            panel1.Location = new Point(48, 72);
            panel1.Name = "panel1";
            panel1.Size = new Size(1024, 366);
            panel1.TabIndex = 7;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToOrderColumns = true;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(0, 0);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(1024, 366);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // btn_get_column_info
            // 
            btn_get_column_info.Location = new Point(527, 27);
            btn_get_column_info.Name = "btn_get_column_info";
            btn_get_column_info.Size = new Size(118, 23);
            btn_get_column_info.TabIndex = 8;
            btn_get_column_info.Text = "Get Column Info";
            btn_get_column_info.UseVisualStyleBackColor = true;
            btn_get_column_info.Click += button1_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(27, 9);
            label4.Name = "label4";
            label4.Size = new Size(49, 15);
            label4.TabIndex = 9;
            label4.Text = "Schema";
            // 
            // txt_schema_name
            // 
            txt_schema_name.Location = new Point(12, 28);
            txt_schema_name.Name = "txt_schema_name";
            txt_schema_name.Size = new Size(100, 23);
            txt_schema_name.TabIndex = 10;
            txt_schema_name.Text = "marketplace";
            txt_schema_name.TextChanged += txt_schema_name_TextChanged;
            // 
            // button1
            // 
            button1.Location = new Point(938, 27);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 11;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click_1;
            // 
            // button2
            // 
            button2.Location = new Point(785, 27);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 12;
            button2.Text = "Generate 2 ";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1130, 450);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(txt_schema_name);
            Controls.Add(label4);
            Controls.Add(btn_get_column_info);
            Controls.Add(panel1);
            Controls.Add(btn_generate);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(txt_module_name);
            Controls.Add(txt_table_name);
            Controls.Add(txt_from_name);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txt_from_name;
        private TextBox txt_table_name;
        private TextBox txt_module_name;
        private Label label2;
        private Label label3;
        private Button btn_generate;
        private Panel panel1;
        private DataGridView dataGridView1;
        private Button btn_get_column_info;
        private Label label4;
        private TextBox textBox1;
        private TextBox txt_schema;
        private TextBox txt_schema_name;
        private Button button1;
        private Button button2;
    }
}
