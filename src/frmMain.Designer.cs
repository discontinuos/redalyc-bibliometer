namespace Biblio
{
	partial class frmMain
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.txtQuery = new System.Windows.Forms.TextBox();
			this.lstTopics = new System.Windows.Forms.CheckedListBox();
			this.txtSearch = new System.Windows.Forms.TextBox();
			this.txtTopics = new System.Windows.Forms.TextBox();
			this.cmdGet = new System.Windows.Forms.Button();
			this.lstResults = new System.Windows.Forms.ListView();
			this.colTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader14 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.cmdGetPdfs = new System.Windows.Forms.Button();
			this.cmdSaveAs = new System.Windows.Forms.Button();
			this.cmbOpen = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.lstYears = new System.Windows.Forms.CheckedListBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.txtMatch1 = new System.Windows.Forms.TextBox();
			this.cmdMatch1 = new System.Windows.Forms.Button();
			this.cmdMatch2 = new System.Windows.Forms.Button();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.lblCount = new System.Windows.Forms.Label();
			this.cmdClear = new System.Windows.Forms.Button();
			this.lblStatus = new System.Windows.Forms.Label();
			this.txtMatch2 = new System.Windows.Forms.TextBox();
			this.cmdSave = new System.Windows.Forms.Button();
			this.cmdSaveCSV = new System.Windows.Forms.Button();
			this.cmdGetTotals = new System.Windows.Forms.Button();
			this.txtFind = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.cmdNext = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// txtQuery
			// 
			this.txtQuery.Location = new System.Drawing.Point(62, 62);
			this.txtQuery.Name = "txtQuery";
			this.txtQuery.ReadOnly = true;
			this.txtQuery.Size = new System.Drawing.Size(653, 20);
			this.txtQuery.TabIndex = 0;
			this.txtQuery.Text = "http://www.redalyc.org/busquedaArticuloFiltros.oa?q={{ búsqueda }}&a={{ años }}&i" +
    "=1&d={{ disciplinas }}&cvePais=&p=t&idp={{ página }}";
			this.txtQuery.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
			// 
			// lstTopics
			// 
			this.lstTopics.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.lstTopics.FormattingEnabled = true;
			this.lstTopics.Location = new System.Drawing.Point(62, 252);
			this.lstTopics.Name = "lstTopics";
			this.lstTopics.Size = new System.Drawing.Size(226, 214);
			this.lstTopics.TabIndex = 2;
			this.lstTopics.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lstTopics_ItemCheck);
			// 
			// txtSearch
			// 
			this.txtSearch.Location = new System.Drawing.Point(156, 25);
			this.txtSearch.Name = "txtSearch";
			this.txtSearch.Size = new System.Drawing.Size(198, 20);
			this.txtSearch.TabIndex = 3;
			// 
			// txtTopics
			// 
			this.txtTopics.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.txtTopics.Location = new System.Drawing.Point(62, 489);
			this.txtTopics.Name = "txtTopics";
			this.txtTopics.ReadOnly = true;
			this.txtTopics.Size = new System.Drawing.Size(226, 20);
			this.txtTopics.TabIndex = 4;
			// 
			// cmdGet
			// 
			this.cmdGet.Location = new System.Drawing.Point(360, 19);
			this.cmdGet.Name = "cmdGet";
			this.cmdGet.Size = new System.Drawing.Size(147, 30);
			this.cmdGet.TabIndex = 5;
			this.cmdGet.Text = "Obtener listado";
			this.cmdGet.UseVisualStyleBackColor = true;
			this.cmdGet.Click += new System.EventHandler(this.cmdGet_Click);
			// 
			// lstResults
			// 
			this.lstResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lstResults.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colTitle,
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader11,
            this.columnHeader12,
            this.columnHeader13,
            this.columnHeader14});
			this.lstResults.FullRowSelect = true;
			this.lstResults.HideSelection = false;
			this.lstResults.Location = new System.Drawing.Point(347, 196);
			this.lstResults.Name = "lstResults";
			this.lstResults.Size = new System.Drawing.Size(344, 250);
			this.lstResults.TabIndex = 6;
			this.lstResults.UseCompatibleStateImageBehavior = false;
			this.lstResults.View = System.Windows.Forms.View.Details;
			this.lstResults.SelectedIndexChanged += new System.EventHandler(this.lstResults_SelectedIndexChanged);
			this.lstResults.DoubleClick += new System.EventHandler(this.lstResults_DoubleClick);
			this.lstResults.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstResults_KeyPress);
			// 
			// colTitle
			// 
			this.colTitle.Text = "Título";
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Autores";
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Revista";
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Número";
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Resumen";
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "PDF";
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "Año";
			// 
			// columnHeader7
			// 
			this.columnHeader7.Text = "Disciplina";
			this.columnHeader7.Width = 90;
			// 
			// columnHeader8
			// 
			this.columnHeader8.Text = "Match1Título";
			this.columnHeader8.Width = 90;
			// 
			// columnHeader9
			// 
			this.columnHeader9.Text = "Match1Resumen";
			this.columnHeader9.Width = 90;
			// 
			// columnHeader10
			// 
			this.columnHeader10.Text = "Match1Pdf";
			this.columnHeader10.Width = 90;
			// 
			// columnHeader11
			// 
			this.columnHeader11.Text = "Match2Título";
			this.columnHeader11.Width = 90;
			// 
			// columnHeader12
			// 
			this.columnHeader12.Text = "Match2Resumen";
			this.columnHeader12.Width = 90;
			// 
			// columnHeader13
			// 
			this.columnHeader13.Text = "Match2Pdf";
			this.columnHeader13.Width = 90;
			// 
			// columnHeader14
			// 
			this.columnHeader14.Text = "Tiene archivo";
			this.columnHeader14.Width = 90;
			// 
			// cmdGetPdfs
			// 
			this.cmdGetPdfs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.cmdGetPdfs.Location = new System.Drawing.Point(347, 472);
			this.cmdGetPdfs.Name = "cmdGetPdfs";
			this.cmdGetPdfs.Size = new System.Drawing.Size(147, 30);
			this.cmdGetPdfs.TabIndex = 5;
			this.cmdGetPdfs.Text = "Obtener PDFs";
			this.cmdGetPdfs.UseVisualStyleBackColor = true;
			this.cmdGetPdfs.Click += new System.EventHandler(this.cmdGetPdfs_Click);
			// 
			// cmdSaveAs
			// 
			this.cmdSaveAs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.cmdSaveAs.Location = new System.Drawing.Point(62, 541);
			this.cmdSaveAs.Name = "cmdSaveAs";
			this.cmdSaveAs.Size = new System.Drawing.Size(147, 30);
			this.cmdSaveAs.TabIndex = 5;
			this.cmdSaveAs.Text = "Guardar como ...";
			this.cmdSaveAs.UseVisualStyleBackColor = true;
			this.cmdSaveAs.Click += new System.EventHandler(this.cmdSaveAs_Click);
			// 
			// cmbOpen
			// 
			this.cmbOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmbOpen.Location = new System.Drawing.Point(590, 530);
			this.cmbOpen.Name = "cmbOpen";
			this.cmbOpen.Size = new System.Drawing.Size(147, 30);
			this.cmbOpen.TabIndex = 5;
			this.cmbOpen.Text = "Abrir";
			this.cmbOpen.UseVisualStyleBackColor = true;
			this.cmbOpen.Click += new System.EventHandler(this.cmbOpen_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(59, 28);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(43, 13);
			this.label1.TabIndex = 7;
			this.label1.Text = "Buscar:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(59, 116);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(31, 13);
			this.label2.TabIndex = 7;
			this.label2.Text = "Años";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(344, 150);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(63, 13);
			this.label3.TabIndex = 7;
			this.label3.Text = "Resultados:";
			// 
			// lstYears
			// 
			this.lstYears.FormattingEnabled = true;
			this.lstYears.Location = new System.Drawing.Point(62, 132);
			this.lstYears.Name = "lstYears";
			this.lstYears.Size = new System.Drawing.Size(226, 94);
			this.lstYears.TabIndex = 8;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(59, 236);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(57, 13);
			this.label4.TabIndex = 9;
			this.label4.Text = "Disciplinas";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(344, 94);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(52, 13);
			this.label5.TabIndex = 11;
			this.label5.Text = "Etiquetar:";
			// 
			// txtMatch1
			// 
			this.txtMatch1.Location = new System.Drawing.Point(441, 91);
			this.txtMatch1.Name = "txtMatch1";
			this.txtMatch1.Size = new System.Drawing.Size(198, 20);
			this.txtMatch1.TabIndex = 10;
			// 
			// cmdMatch1
			// 
			this.cmdMatch1.Location = new System.Drawing.Point(645, 91);
			this.cmdMatch1.Name = "cmdMatch1";
			this.cmdMatch1.Size = new System.Drawing.Size(70, 20);
			this.cmdMatch1.TabIndex = 5;
			this.cmdMatch1.Text = "Match1";
			this.cmdMatch1.UseVisualStyleBackColor = true;
			this.cmdMatch1.Click += new System.EventHandler(this.cmdMatch1_Click);
			// 
			// cmdMatch2
			// 
			this.cmdMatch2.Location = new System.Drawing.Point(645, 114);
			this.cmdMatch2.Name = "cmdMatch2";
			this.cmdMatch2.Size = new System.Drawing.Size(70, 20);
			this.cmdMatch2.TabIndex = 5;
			this.cmdMatch2.Text = "Match2";
			this.cmdMatch2.UseVisualStyleBackColor = true;
			this.cmdMatch2.Click += new System.EventHandler(this.cmdMatch2_Click);
			// 
			// textBox3
			// 
			this.textBox3.Location = new System.Drawing.Point(62, 87);
			this.textBox3.Name = "textBox3";
			this.textBox3.ReadOnly = true;
			this.textBox3.Size = new System.Drawing.Size(192, 20);
			this.textBox3.TabIndex = 12;
			this.textBox3.Text = "idioma=español";
			// 
			// lblCount
			// 
			this.lblCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblCount.AutoSize = true;
			this.lblCount.Location = new System.Drawing.Point(344, 454);
			this.lblCount.Name = "lblCount";
			this.lblCount.Size = new System.Drawing.Size(16, 13);
			this.lblCount.TabIndex = 13;
			this.lblCount.Text = "0.";
			// 
			// cmdClear
			// 
			this.cmdClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.cmdClear.Location = new System.Drawing.Point(500, 472);
			this.cmdClear.Name = "cmdClear";
			this.cmdClear.Size = new System.Drawing.Size(147, 30);
			this.cmdClear.TabIndex = 14;
			this.cmdClear.Text = "Vaciar";
			this.cmdClear.UseVisualStyleBackColor = true;
			this.cmdClear.Click += new System.EventHandler(this.cmdClear_Click);
			// 
			// lblStatus
			// 
			this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblStatus.AutoSize = true;
			this.lblStatus.Location = new System.Drawing.Point(344, 505);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(0, 13);
			this.lblStatus.TabIndex = 13;
			// 
			// txtMatch2
			// 
			this.txtMatch2.Location = new System.Drawing.Point(441, 114);
			this.txtMatch2.Name = "txtMatch2";
			this.txtMatch2.Size = new System.Drawing.Size(198, 20);
			this.txtMatch2.TabIndex = 10;
			// 
			// cmdSave
			// 
			this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.cmdSave.Location = new System.Drawing.Point(215, 541);
			this.cmdSave.Name = "cmdSave";
			this.cmdSave.Size = new System.Drawing.Size(147, 30);
			this.cmdSave.TabIndex = 5;
			this.cmdSave.Text = "Guardar";
			this.cmdSave.UseVisualStyleBackColor = true;
			this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
			// 
			// cmdSaveCSV
			// 
			this.cmdSaveCSV.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.cmdSaveCSV.Location = new System.Drawing.Point(674, 472);
			this.cmdSaveCSV.Name = "cmdSaveCSV";
			this.cmdSaveCSV.Size = new System.Drawing.Size(147, 30);
			this.cmdSaveCSV.TabIndex = 14;
			this.cmdSaveCSV.Text = "Guardar CSV";
			this.cmdSaveCSV.UseVisualStyleBackColor = true;
			this.cmdSaveCSV.Click += new System.EventHandler(this.cmdSaveCSV_Click);
			// 
			// cmdGetTotals
			// 
			this.cmdGetTotals.Location = new System.Drawing.Point(568, 19);
			this.cmdGetTotals.Name = "cmdGetTotals";
			this.cmdGetTotals.Size = new System.Drawing.Size(147, 30);
			this.cmdGetTotals.TabIndex = 5;
			this.cmdGetTotals.Text = "Obtener totales";
			this.cmdGetTotals.UseVisualStyleBackColor = true;
			this.cmdGetTotals.Click += new System.EventHandler(this.cmdGetTotals_Click);
			// 
			// txtFind
			// 
			this.txtFind.Location = new System.Drawing.Point(441, 170);
			this.txtFind.Name = "txtFind";
			this.txtFind.Size = new System.Drawing.Size(198, 20);
			this.txtFind.TabIndex = 10;
			this.txtFind.TextChanged += new System.EventHandler(this.txtFind_TextChanged);
			this.txtFind.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFind_KeyPress);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(344, 173);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(91, 13);
			this.label6.TabIndex = 11;
			this.label6.Text = "Buscar (tít/autor):";
			// 
			// cmdNext
			// 
			this.cmdNext.Location = new System.Drawing.Point(645, 170);
			this.cmdNext.Name = "cmdNext";
			this.cmdNext.Size = new System.Drawing.Size(92, 20);
			this.cmdNext.TabIndex = 5;
			this.cmdNext.Text = "Siguiente";
			this.cmdNext.UseVisualStyleBackColor = true;
			this.cmdNext.Click += new System.EventHandler(this.cmdNext_Click);
			// 
			// frmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(806, 589);
			this.Controls.Add(this.cmdSaveCSV);
			this.Controls.Add(this.cmdClear);
			this.Controls.Add(this.lblStatus);
			this.Controls.Add(this.lblCount);
			this.Controls.Add(this.textBox3);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.txtMatch2);
			this.Controls.Add(this.txtFind);
			this.Controls.Add(this.txtMatch1);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.lstYears);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.lstResults);
			this.Controls.Add(this.cmbOpen);
			this.Controls.Add(this.cmdSave);
			this.Controls.Add(this.cmdSaveAs);
			this.Controls.Add(this.cmdGetTotals);
			this.Controls.Add(this.cmdGetPdfs);
			this.Controls.Add(this.cmdMatch2);
			this.Controls.Add(this.cmdNext);
			this.Controls.Add(this.cmdMatch1);
			this.Controls.Add(this.cmdGet);
			this.Controls.Add(this.txtTopics);
			this.Controls.Add(this.txtSearch);
			this.Controls.Add(this.lstTopics);
			this.Controls.Add(this.txtQuery);
			this.Name = "frmMain";
			this.Text = "Cuenta artículos";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Load += new System.EventHandler(this.frmMain_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txtQuery;
		private System.Windows.Forms.CheckedListBox lstTopics;
		private System.Windows.Forms.TextBox txtSearch;
		private System.Windows.Forms.TextBox txtTopics;
		private System.Windows.Forms.Button cmdGet;
		private System.Windows.Forms.ListView lstResults;
		private System.Windows.Forms.Button cmdGetPdfs;
		private System.Windows.Forms.Button cmdSaveAs;
		private System.Windows.Forms.Button cmbOpen;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ColumnHeader colTitle;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.ColumnHeader columnHeader8;
		private System.Windows.Forms.ColumnHeader columnHeader9;
		private System.Windows.Forms.ColumnHeader columnHeader10;
		private System.Windows.Forms.ColumnHeader columnHeader11;
		private System.Windows.Forms.ColumnHeader columnHeader12;
		private System.Windows.Forms.ColumnHeader columnHeader13;
		private System.Windows.Forms.CheckedListBox lstYears;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox txtMatch1;
		private System.Windows.Forms.Button cmdMatch1;
		private System.Windows.Forms.Button cmdMatch2;
		private System.Windows.Forms.TextBox textBox3;
		private System.Windows.Forms.Label lblCount;
		private System.Windows.Forms.Button cmdClear;
		private System.Windows.Forms.Label lblStatus;
		private System.Windows.Forms.ColumnHeader columnHeader14;
		private System.Windows.Forms.TextBox txtMatch2;
		private System.Windows.Forms.Button cmdSave;
		private System.Windows.Forms.Button cmdSaveCSV;
		private System.Windows.Forms.Button cmdGetTotals;
		private System.Windows.Forms.TextBox txtFind;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button cmdNext;
	}
}

