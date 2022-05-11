using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Media;
using Styx.Common;

namespace Baiterator
{
	public partial class frmSettings : Form
	{
		private string SettingsFilePath { get; set; }

		public int KeepGear { get; set; }

		public bool RemoveGear { get; set; }

		public bool UseBladebone { get; set; }

		public bool SummonCave { get; set; }

		public bool useAGE { get; private set; }

		public bool useSS { get; private set; }

		public bool useJS { get; private set; }

		public bool useFA { get; private set; }

		public bool useFS { get; private set; }

		public bool useBLS { get; private set; }

		public bool useBW { get; private set; }

		public bool RemoveFB { get; private set; }

		public bool useCS { get; private set; }

		private string name = null;

		private const uint AGE = 110293;
		private const uint BW = 110294;
		private const uint BLS = 110290;
		private const uint FS = 110289;
		private const uint FA = 110291;
		private const uint JS = 110274;
		private const uint SS = 110292;

		private const uint AGE_Aura = 158038;
		private const uint BW_Aura = 158039;
		private const uint BLS_Aura = 158035;
		private const uint FS_Aura = 158034;
		private const uint FA_Aura = 158036;
		private const uint JS_Aura = 158031;
		private const uint SS_Aura = 158037;

		public List<uint> dontUse = new List<uint>(new uint[] {
		});

		public List<uint> dontUseAura = new List<uint>(new uint[] {
		});

		public frmSettings()
		{
			InitializeComponent();
			SettingsFilePath = "Settings\\Baiterator.txt";
			readSettings();
		}

		public void saveSettings()
		{
			try
			{
				System.IO.StreamWriter file = new System.IO.StreamWriter(SettingsFilePath);
				file.WriteLine(SummonCave.ToString());
				file.WriteLine(RemoveGear.ToString());
				file.WriteLine(UseBladebone.ToString());
				file.WriteLine(KeepGear.ToString());
				file.WriteLine(useAGE.ToString());
				file.WriteLine(useBW.ToString());
				file.WriteLine(useBLS.ToString());
				file.WriteLine(useFS.ToString());
				file.WriteLine(useFA.ToString());
				file.WriteLine(useJS.ToString());
				file.WriteLine(useSS.ToString());
				file.WriteLine(RemoveFB.ToString());
				file.WriteLine(useCS.ToString());
				file.Close();
				Logging.Write(Colors.OrangeRed, "Baiterator: Settings Updated.");
				updateDontUse();
				readSettings();
			}
			catch (Exception e)
			{
				Logging.Write(Colors.Red, "Baiterator: Error creating/saving settings.");
				Logging.Write(Colors.Red, "Baiterator: " + e.Message);
			}
		}

		public void readSettings()
		{
			if (System.IO.File.Exists(SettingsFilePath))
			{
				string[] file = System.IO.File.ReadAllLines(SettingsFilePath);
				if (file.Length < 13)
				{
					Logging.Write(Colors.Red, "Baiterator: Incomplete settings, writing defaults. Please re-set your settings.");
					saveSettings();
				}
				else
				{
					chkSummon.Checked = Convert.ToBoolean(file[0]);
					SummonCave = Convert.ToBoolean(file[0]);

					chkRemove.Checked = Convert.ToBoolean(file[1]);
					RemoveGear = Convert.ToBoolean(file[1]);

					chkBladebone.Checked = Convert.ToBoolean(file[2]);
					UseBladebone = Convert.ToBoolean(file[2]);

					txtGear.Text = file[3].ToString();
					KeepGear = Convert.ToInt16(file[3]);

					chkAGE.Checked = Convert.ToBoolean(file[4]);
					useAGE = Convert.ToBoolean(file[4]);

					chkBW.Checked = Convert.ToBoolean(file[5]);
					useBW = Convert.ToBoolean(file[5]);

					chkBLS.Checked = Convert.ToBoolean(file[6]);
					useBLS = Convert.ToBoolean(file[6]);

					chkFS.Checked = Convert.ToBoolean(file[7]);
					useFS = Convert.ToBoolean(file[7]);

					chkFA.Checked = Convert.ToBoolean(file[8]);
					useFA = Convert.ToBoolean(file[8]);

					chkJS.Checked = Convert.ToBoolean(file[9]);
					useJS = Convert.ToBoolean(file[9]);

					chkSS.Checked = Convert.ToBoolean(file[10]);
					useSS = Convert.ToBoolean(file[10]);

					chkRemoveFB.Checked = Convert.ToBoolean(file[11]);
					RemoveFB = Convert.ToBoolean(file[11]);

					chkCS.Checked = Convert.ToBoolean(file[12]);
					useCS = Convert.ToBoolean(file[12]);

					updateDontUse();
				}
			}
			else
			{
				Logging.Write(Colors.Red, "Baiterator: No settings file found, creating one.");
				saveSettings();
			}
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			saveSettings();
			Close();
		}

		private void chkSummon_CheckedChanged(object sender, EventArgs e)
		{
			SummonCave = chkSummon.Checked;
		}

		private void chkRemove_CheckedChanged(object sender, EventArgs e)
		{
			RemoveGear = chkRemove.Checked;
		}

		private void chkBladebone_CheckedChanged(object sender, EventArgs e)
		{
			UseBladebone = chkBladebone.Checked;
		}

		private void txtGear_TextChanged(object sender, EventArgs e)
		{
			KeepGear = Convert.ToInt16(txtGear.Text);
		}

		private void frmSettings_Load(object sender, EventArgs e)
		{
			chkSummon.Checked = SummonCave;
			chkRemove.Checked = RemoveGear;
			chkBladebone.Checked = UseBladebone;
			chkAGE.Checked = useAGE;
			chkBW.Checked = useBW;
			chkBLS.Checked = useBLS;
			chkFS.Checked = useFS;
			chkFA.Checked = useFA;
			chkJS.Checked = useJS;
			chkSS.Checked = useSS;
			chkCS.Checked = useCS;
			chkRemoveFB.Checked = RemoveFB;
			updateDontUse();
		}

		public void updateDontUse()
		{
			dontUse.Clear();
			dontUseAura.Clear();

			if (!chkAGE.Checked)
			{
				dontUse.Add(AGE);
				dontUseAura.Add(AGE_Aura);
			}
			if (!chkBW.Checked)
			{
				dontUse.Add(BW);
				dontUseAura.Add(BW_Aura);
			}
			if (!chkBLS.Checked)
			{
				dontUse.Add(BLS);
				dontUseAura.Add(BLS_Aura);
			}
			if (!chkFS.Checked)
			{
				dontUse.Add(FS);
				dontUseAura.Add(FS_Aura);
			}
			if (!chkFA.Checked)
			{
				dontUse.Add(FA);
				dontUseAura.Add(FA_Aura);
			}
			if (!chkJS.Checked)
			{
				dontUse.Add(JS);
				dontUseAura.Add(JS_Aura);
			}
			if (!chkSS.Checked)
			{
				dontUse.Add(SS);
				dontUseAura.Add(SS_Aura);
			}

			//if (!chkCS.Checked)
			//{
			//	dontUse.Add(CS);
			//	dontUse.Add(CS_Aura);
			//}
			//if (chkCS.Checked)
			//{
			//	dontUse.Remove(CS);
			//	dontUse.Remove(CS_Aura);
			//}
		}

		private void chkAGE_CheckedChanged(object sender, EventArgs e)
		{
			useAGE = chkAGE.Checked;
			updateDontUse();
		}

		private void chkBW_CheckedChanged(object sender, EventArgs e)
		{
			useBW = chkBW.Checked;
			updateDontUse();
		}

		private void chkBLS_CheckedChanged(object sender, EventArgs e)
		{
			useBLS = chkBLS.Checked;
			updateDontUse();
		}

		private void chkFS_CheckedChanged(object sender, EventArgs e)
		{
			useFS = chkFS.Checked;
			updateDontUse();
		}

		private void chkFA_CheckedChanged(object sender, EventArgs e)
		{
			useFA = chkFA.Checked;
			updateDontUse();
		}

		private void chkJS_CheckedChanged(object sender, EventArgs e)
		{
			useJS = chkJS.Checked;
			updateDontUse();
		}

		private void chkSS_CheckedChanged(object sender, EventArgs e)
		{
			useSS = chkSS.Checked;
			updateDontUse();
		}

		private void frmSettings_FormClosed(object sender, FormClosedEventArgs e)
		{
			saveSettings();
		}

		private void chkRemoveFB_CheckedChanged(object sender, EventArgs e)
		{
			RemoveFB = chkRemoveFB.Checked;
		}

		private void chkSaber_CheckedChanged(object sender, EventArgs e)
		{
			useCS = chkCS.Checked;
			updateDontUse();
		}
	}
}