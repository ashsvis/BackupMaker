namespace BackupMaker
{
    partial class frmBackupMaker
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.cbSchema = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbHost = new System.Windows.Forms.ComboBox();
            this.cbPort = new System.Windows.Forms.ComboBox();
            this.cbUser = new System.Windows.Forms.ComboBox();
            this.cbPassword = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.clbTables = new System.Windows.Forms.CheckedListBox();
            this.btnExport = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.cbAll = new System.Windows.Forms.CheckBox();
            this.btnImport = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.backgroundExportWorker = new System.ComponentModel.BackgroundWorker();
            this.label5 = new System.Windows.Forms.Label();
            this.lbTableName = new System.Windows.Forms.Label();
            this.backgroundImportWorker = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 96);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Текущая база данных:";
            // 
            // cbSchema
            // 
            this.cbSchema.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbSchema.FormattingEnabled = true;
            this.cbSchema.Location = new System.Drawing.Point(12, 116);
            this.cbSchema.Name = "cbSchema";
            this.cbSchema.Size = new System.Drawing.Size(145, 23);
            this.cbSchema.TabIndex = 1;
            this.cbSchema.TextChanged += new System.EventHandler(this.cbHost_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(12, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(135, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Расположение и порт:";
            // 
            // cbHost
            // 
            this.cbHost.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbHost.FormattingEnabled = true;
            this.cbHost.Location = new System.Drawing.Point(12, 27);
            this.cbHost.Name = "cbHost";
            this.cbHost.Size = new System.Drawing.Size(145, 23);
            this.cbHost.TabIndex = 1;
            this.cbHost.TextChanged += new System.EventHandler(this.cbHost_TextChanged);
            // 
            // cbPort
            // 
            this.cbPort.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbPort.FormattingEnabled = true;
            this.cbPort.Location = new System.Drawing.Point(163, 27);
            this.cbPort.Name = "cbPort";
            this.cbPort.Size = new System.Drawing.Size(55, 23);
            this.cbPort.TabIndex = 1;
            this.cbPort.TextChanged += new System.EventHandler(this.cbHost_TextChanged);
            // 
            // cbUser
            // 
            this.cbUser.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbUser.FormattingEnabled = true;
            this.cbUser.Location = new System.Drawing.Point(12, 71);
            this.cbUser.Name = "cbUser";
            this.cbUser.Size = new System.Drawing.Size(98, 23);
            this.cbUser.TabIndex = 1;
            this.cbUser.TextChanged += new System.EventHandler(this.cbHost_TextChanged);
            // 
            // cbPassword
            // 
            this.cbPassword.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbPassword.FormattingEnabled = true;
            this.cbPassword.Location = new System.Drawing.Point(116, 71);
            this.cbPassword.Name = "cbPassword";
            this.cbPassword.Size = new System.Drawing.Size(102, 23);
            this.cbPassword.TabIndex = 1;
            this.cbPassword.TextChanged += new System.EventHandler(this.cbHost_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(12, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(147, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Пользователь и пароль:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(12, 142);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 15);
            this.label4.TabIndex = 0;
            this.label4.Text = "Таблицы в базе:";
            // 
            // clbTables
            // 
            this.clbTables.FormattingEnabled = true;
            this.clbTables.Location = new System.Drawing.Point(15, 161);
            this.clbTables.Name = "clbTables";
            this.clbTables.Size = new System.Drawing.Size(203, 100);
            this.clbTables.TabIndex = 3;
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(25, 267);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(85, 23);
            this.btnExport.TabIndex = 4;
            this.btnExport.Text = "Экспорт";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // cbAll
            // 
            this.cbAll.AutoSize = true;
            this.cbAll.Location = new System.Drawing.Point(168, 142);
            this.cbAll.Name = "cbAll";
            this.cbAll.Size = new System.Drawing.Size(47, 19);
            this.cbAll.TabIndex = 5;
            this.cbAll.Text = "Все";
            this.cbAll.UseVisualStyleBackColor = true;
            this.cbAll.Click += new System.EventHandler(this.cbAll_Click);
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(116, 267);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(85, 23);
            this.btnImport.TabIndex = 4;
            this.btnImport.Text = "Импорт";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(15, 312);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(205, 15);
            this.progressBar1.TabIndex = 6;
            // 
            // backgroundExportWorker
            // 
            this.backgroundExportWorker.WorkerReportsProgress = true;
            this.backgroundExportWorker.WorkerSupportsCancellation = true;
            this.backgroundExportWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundExportWorker_DoWork);
            this.backgroundExportWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundExportWorker_ProgressChanged);
            this.backgroundExportWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundExportWorker_RunWorkerCompleted);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(12, 293);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(123, 15);
            this.label5.TabIndex = 0;
            this.label5.Text = "Обработка таблицы:";
            // 
            // lbTableName
            // 
            this.lbTableName.AutoEllipsis = true;
            this.lbTableName.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbTableName.Location = new System.Drawing.Point(139, 293);
            this.lbTableName.Name = "lbTableName";
            this.lbTableName.Size = new System.Drawing.Size(79, 15);
            this.lbTableName.TabIndex = 0;
            this.lbTableName.Text = "table";
            // 
            // backgroundImportWorker
            // 
            this.backgroundImportWorker.WorkerReportsProgress = true;
            this.backgroundImportWorker.WorkerSupportsCancellation = true;
            this.backgroundImportWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundImportWorker_DoWork);
            this.backgroundImportWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundImportWorker_ProgressChanged);
            this.backgroundImportWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundImportWorker_RunWorkerCompleted);
            // 
            // frmBackupMaker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(232, 339);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.cbAll);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.clbTables);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbPassword);
            this.Controls.Add(this.cbPort);
            this.Controls.Add(this.cbUser);
            this.Controls.Add(this.cbHost);
            this.Controls.Add(this.cbSchema);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lbTableName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBackupMaker";
            this.Text = "Создание резервных копий баз данных";
            this.Load += new System.EventHandler(this.frmBackupMaker_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbSchema;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbHost;
        private System.Windows.Forms.ComboBox cbPort;
        private System.Windows.Forms.ComboBox cbUser;
        private System.Windows.Forms.ComboBox cbPassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckedListBox clbTables;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.CheckBox cbAll;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.ComponentModel.BackgroundWorker backgroundExportWorker;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbTableName;
        private System.ComponentModel.BackgroundWorker backgroundImportWorker;
    }
}

