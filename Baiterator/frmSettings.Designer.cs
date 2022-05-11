namespace Baiterator
{
	partial class frmSettings
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
			this.components = new System.ComponentModel.Container();
			this.btnOK = new System.Windows.Forms.Button();
			this.chkSummon = new System.Windows.Forms.CheckBox();
			this.chkRemove = new System.Windows.Forms.CheckBox();
			this.chkBladebone = new System.Windows.Forms.CheckBox();
			this.tip = new System.Windows.Forms.ToolTip(this.components);
			this.txtGear = new System.Windows.Forms.TextBox();
			this.grpBaits = new System.Windows.Forms.GroupBox();
			this.chkAGE = new System.Windows.Forms.CheckBox();
			this.chkSS = new System.Windows.Forms.CheckBox();
			this.chkBW = new System.Windows.Forms.CheckBox();
			this.chkJS = new System.Windows.Forms.CheckBox();
			this.chkBLS = new System.Windows.Forms.CheckBox();
			this.chkFA = new System.Windows.Forms.CheckBox();
			this.chkFS = new System.Windows.Forms.CheckBox();
			this.chkRemoveFB = new System.Windows.Forms.CheckBox();
			this.lblKeep = new System.Windows.Forms.Label();
			this.grpMisc = new System.Windows.Forms.GroupBox();
			this.grpRemoval = new System.Windows.Forms.GroupBox();
			this.chkCS = new System.Windows.Forms.CheckBox();
			this.grpBaits.SuspendLayout();
			this.grpMisc.SuspendLayout();
			this.grpRemoval.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnOK
			// 
			this.btnOK.Location = new System.Drawing.Point(12, 313);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(272, 23);
			this.btnOK.TabIndex = 0;
			this.btnOK.Text = "&OK";
			this.tip.SetToolTip(this.btnOK, "Save & Close");
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// chkSummon
			// 
			this.chkSummon.AutoSize = true;
			this.chkSummon.Checked = true;
			this.chkSummon.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkSummon.Location = new System.Drawing.Point(6, 21);
			this.chkSummon.Name = "chkSummon";
			this.chkSummon.Size = new System.Drawing.Size(140, 17);
			this.chkSummon.TabIndex = 3;
			this.chkSummon.Text = "Summon Cave Dweller";
			this.tip.SetToolTip(this.chkSummon, "This changes wether to \r\nsummon the cave dwellers \r\nor not.");
			this.chkSummon.UseVisualStyleBackColor = true;
			this.chkSummon.CheckedChanged += new System.EventHandler(this.chkSummon_CheckedChanged);
			// 
			// chkRemove
			// 
			this.chkRemove.AutoSize = true;
			this.chkRemove.Checked = true;
			this.chkRemove.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkRemove.Location = new System.Drawing.Point(6, 44);
			this.chkRemove.Name = "chkRemove";
			this.chkRemove.Size = new System.Drawing.Size(128, 17);
			this.chkRemove.TabIndex = 4;
			this.chkRemove.Text = "Remove Excess Gear";
			this.tip.SetToolTip(this.chkRemove, "This changes wether to remove \r\nexcess gear from your bags that \r\nthe cave dwelle" +
        "rs drop.");
			this.chkRemove.UseVisualStyleBackColor = true;
			this.chkRemove.CheckedChanged += new System.EventHandler(this.chkRemove_CheckedChanged);
			// 
			// chkBladebone
			// 
			this.chkBladebone.AutoSize = true;
			this.chkBladebone.Checked = true;
			this.chkBladebone.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkBladebone.Location = new System.Drawing.Point(6, 67);
			this.chkBladebone.Name = "chkBladebone";
			this.chkBladebone.Size = new System.Drawing.Size(135, 17);
			this.chkBladebone.TabIndex = 5;
			this.chkBladebone.Text = "Use Bladebone Hook";
			this.tip.SetToolTip(this.chkBladebone, "FishingBuddy supports the hook, \r\nbut mine does it faster apparently.");
			this.chkBladebone.UseVisualStyleBackColor = true;
			this.chkBladebone.CheckedChanged += new System.EventHandler(this.chkBladebone_CheckedChanged);
			// 
			// tip
			// 
			this.tip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
			this.tip.ToolTipTitle = "Info";
			// 
			// txtGear
			// 
			this.txtGear.Location = new System.Drawing.Point(6, 90);
			this.txtGear.Name = "txtGear";
			this.txtGear.Size = new System.Drawing.Size(29, 22);
			this.txtGear.TabIndex = 6;
			this.txtGear.Text = "1";
			this.tip.SetToolTip(this.txtGear, "This is the amount of each\r\nitem to keep before removing \r\nthem.");
			this.txtGear.TextChanged += new System.EventHandler(this.txtGear_TextChanged);
			// 
			// grpBaits
			// 
			this.grpBaits.Controls.Add(this.chkCS);
			this.grpBaits.Controls.Add(this.chkAGE);
			this.grpBaits.Controls.Add(this.chkSS);
			this.grpBaits.Controls.Add(this.chkBW);
			this.grpBaits.Controls.Add(this.chkJS);
			this.grpBaits.Controls.Add(this.chkBLS);
			this.grpBaits.Controls.Add(this.chkFA);
			this.grpBaits.Controls.Add(this.chkFS);
			this.grpBaits.Location = new System.Drawing.Point(12, 12);
			this.grpBaits.Name = "grpBaits";
			this.grpBaits.Size = new System.Drawing.Size(272, 117);
			this.grpBaits.TabIndex = 15;
			this.grpBaits.TabStop = false;
			this.grpBaits.Text = "Baits";
			this.tip.SetToolTip(this.grpBaits, "Enable/Disable certain baits.");
			// 
			// chkAGE
			// 
			this.chkAGE.AutoSize = true;
			this.chkAGE.Checked = true;
			this.chkAGE.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkAGE.Location = new System.Drawing.Point(6, 21);
			this.chkAGE.Name = "chkAGE";
			this.chkAGE.Size = new System.Drawing.Size(120, 17);
			this.chkAGE.TabIndex = 8;
			this.chkAGE.Text = "Abyssal Gulper Eel";
			this.chkAGE.UseVisualStyleBackColor = true;
			this.chkAGE.CheckedChanged += new System.EventHandler(this.chkAGE_CheckedChanged);
			// 
			// chkSS
			// 
			this.chkSS.AutoSize = true;
			this.chkSS.Checked = true;
			this.chkSS.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkSS.Location = new System.Drawing.Point(6, 90);
			this.chkSS.Name = "chkSS";
			this.chkSS.Size = new System.Drawing.Size(93, 17);
			this.chkSS.TabIndex = 14;
			this.chkSS.Text = "Sea Scorpion";
			this.chkSS.UseVisualStyleBackColor = true;
			this.chkSS.CheckedChanged += new System.EventHandler(this.chkSS_CheckedChanged);
			// 
			// chkBW
			// 
			this.chkBW.AutoSize = true;
			this.chkBW.Checked = true;
			this.chkBW.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkBW.Location = new System.Drawing.Point(141, 21);
			this.chkBW.Name = "chkBW";
			this.chkBW.Size = new System.Drawing.Size(129, 17);
			this.chkBW.TabIndex = 9;
			this.chkBW.Text = "Blackwater Whiptail";
			this.chkBW.UseVisualStyleBackColor = true;
			this.chkBW.CheckedChanged += new System.EventHandler(this.chkBW_CheckedChanged);
			// 
			// chkJS
			// 
			this.chkJS.AutoSize = true;
			this.chkJS.Checked = true;
			this.chkJS.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkJS.Location = new System.Drawing.Point(141, 67);
			this.chkJS.Name = "chkJS";
			this.chkJS.Size = new System.Drawing.Size(105, 17);
			this.chkJS.TabIndex = 13;
			this.chkJS.Text = "Jawless Skulker";
			this.chkJS.UseVisualStyleBackColor = true;
			this.chkJS.CheckedChanged += new System.EventHandler(this.chkJS_CheckedChanged);
			// 
			// chkBLS
			// 
			this.chkBLS.AutoSize = true;
			this.chkBLS.Checked = true;
			this.chkBLS.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkBLS.Location = new System.Drawing.Point(6, 44);
			this.chkBLS.Name = "chkBLS";
			this.chkBLS.Size = new System.Drawing.Size(130, 17);
			this.chkBLS.TabIndex = 10;
			this.chkBLS.Text = "Blind Lake Sturgeon";
			this.chkBLS.UseVisualStyleBackColor = true;
			this.chkBLS.CheckedChanged += new System.EventHandler(this.chkBLS_CheckedChanged);
			// 
			// chkFA
			// 
			this.chkFA.AutoSize = true;
			this.chkFA.Checked = true;
			this.chkFA.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkFA.Location = new System.Drawing.Point(6, 67);
			this.chkFA.Name = "chkFA";
			this.chkFA.Size = new System.Drawing.Size(100, 17);
			this.chkFA.TabIndex = 12;
			this.chkFA.Text = "Fire Ammonite";
			this.chkFA.UseVisualStyleBackColor = true;
			this.chkFA.CheckedChanged += new System.EventHandler(this.chkFA_CheckedChanged);
			// 
			// chkFS
			// 
			this.chkFS.AutoSize = true;
			this.chkFS.Checked = true;
			this.chkFS.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkFS.Location = new System.Drawing.Point(141, 44);
			this.chkFS.Name = "chkFS";
			this.chkFS.Size = new System.Drawing.Size(83, 17);
			this.chkFS.TabIndex = 11;
			this.chkFS.Text = "Fat Sleeper";
			this.chkFS.UseVisualStyleBackColor = true;
			this.chkFS.CheckedChanged += new System.EventHandler(this.chkFS_CheckedChanged);
			// 
			// chkRemoveFB
			// 
			this.chkRemoveFB.AutoSize = true;
			this.chkRemoveFB.Location = new System.Drawing.Point(7, 22);
			this.chkRemoveFB.Name = "chkRemoveFB";
			this.chkRemoveFB.Size = new System.Drawing.Size(155, 17);
			this.chkRemoveFB.TabIndex = 0;
			this.chkRemoveFB.Text = "Remove FB Applied Buffs";
			this.tip.SetToolTip(this.chkRemoveFB, "Remove buffs that FB has applied if they are disabled in Baiterator.");
			this.chkRemoveFB.UseVisualStyleBackColor = true;
			this.chkRemoveFB.CheckedChanged += new System.EventHandler(this.chkRemoveFB_CheckedChanged);
			// 
			// lblKeep
			// 
			this.lblKeep.AutoSize = true;
			this.lblKeep.Location = new System.Drawing.Point(41, 93);
			this.lblKeep.Name = "lblKeep";
			this.lblKeep.Size = new System.Drawing.Size(131, 13);
			this.lblKeep.TabIndex = 7;
			this.lblKeep.Text = "Amount of Gear to Keep";
			// 
			// grpMisc
			// 
			this.grpMisc.Controls.Add(this.chkSummon);
			this.grpMisc.Controls.Add(this.chkRemove);
			this.grpMisc.Controls.Add(this.txtGear);
			this.grpMisc.Controls.Add(this.lblKeep);
			this.grpMisc.Controls.Add(this.chkBladebone);
			this.grpMisc.Location = new System.Drawing.Point(12, 135);
			this.grpMisc.Name = "grpMisc";
			this.grpMisc.Size = new System.Drawing.Size(272, 120);
			this.grpMisc.TabIndex = 16;
			this.grpMisc.TabStop = false;
			this.grpMisc.Text = "Misc.";
			// 
			// grpRemoval
			// 
			this.grpRemoval.Controls.Add(this.chkRemoveFB);
			this.grpRemoval.Location = new System.Drawing.Point(12, 261);
			this.grpRemoval.Name = "grpRemoval";
			this.grpRemoval.Size = new System.Drawing.Size(272, 46);
			this.grpRemoval.TabIndex = 17;
			this.grpRemoval.TabStop = false;
			this.grpRemoval.Text = "Removal";
			// 
			// chkCS
			// 
			this.chkCS.AutoSize = true;
			this.chkCS.Checked = true;
			this.chkCS.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkCS.Location = new System.Drawing.Point(141, 90);
			this.chkCS.Name = "chkCS";
			this.chkCS.Size = new System.Drawing.Size(121, 17);
			this.chkCS.TabIndex = 15;
			this.chkCS.Text = "Crescent Saberfish";
			this.chkCS.UseVisualStyleBackColor = true;
			this.chkCS.CheckedChanged += new System.EventHandler(this.chkSaber_CheckedChanged);
			// 
			// frmSettings
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(296, 347);
			this.Controls.Add(this.grpRemoval);
			this.Controls.Add(this.grpMisc);
			this.Controls.Add(this.grpBaits);
			this.Controls.Add(this.btnOK);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmSettings";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Settings";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmSettings_FormClosed);
			this.Load += new System.EventHandler(this.frmSettings_Load);
			this.grpBaits.ResumeLayout(false);
			this.grpBaits.PerformLayout();
			this.grpMisc.ResumeLayout(false);
			this.grpMisc.PerformLayout();
			this.grpRemoval.ResumeLayout(false);
			this.grpRemoval.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.CheckBox chkSummon;
		private System.Windows.Forms.CheckBox chkRemove;
		private System.Windows.Forms.CheckBox chkBladebone;
		private System.Windows.Forms.ToolTip tip;
		private System.Windows.Forms.TextBox txtGear;
		private System.Windows.Forms.Label lblKeep;
		private System.Windows.Forms.CheckBox chkAGE;
		private System.Windows.Forms.CheckBox chkBW;
		private System.Windows.Forms.CheckBox chkBLS;
		private System.Windows.Forms.CheckBox chkFS;
		private System.Windows.Forms.CheckBox chkFA;
		private System.Windows.Forms.CheckBox chkJS;
		private System.Windows.Forms.CheckBox chkSS;
		private System.Windows.Forms.GroupBox grpBaits;
		private System.Windows.Forms.GroupBox grpMisc;
		private System.Windows.Forms.GroupBox grpRemoval;
		private System.Windows.Forms.CheckBox chkRemoveFB;
		private System.Windows.Forms.CheckBox chkCS;
	}
}