using System;
using System.Linq;
using System.Windows.Media;
using Styx;
using Styx.Common;
using Styx.Plugins;
using Styx.WoWInternals.WoWObjects;

namespace Safarinator
{
    public class Safarinator : HBPlugin
    {
        public override string Name { get { return "Safarinator"; } }

        public override string Author { get { return "p1xel8ted"; } }

        public override Version Version { get { return new Version(1, 0, 0); } }

        private static LocalPlayer Me { get { return StyxWoW.Me; } }

        private void Log(String log, Boolean error)
        {
            if (error)
            {
                Logging.Write(Colors.Red, "Safarinator " + Version + ": " + log);
            }
            else
            {
                Logging.Write(Colors.Magenta, "Safarinator " + Version + ": " + log);
			}
        }

        public override void OnEnable()
        {
            Log("Enabled", false);
            base.OnEnable();
        }

        public override void OnDisable()
        {
            Log("Disabled", false);
            base.OnDisable();
        }

        private void useHat()
        {
            if (!Me.HasAura(158486))
            {
                WoWItem hat = StyxWoW.Me.BagItems.FirstOrDefault(r => r.Entry == 92738);
                if (hat != null)
                {
                    Log("Used " + hat.ItemInfo.Name + "!", false);
                    hat.Use();
#pragma warning disable CS0618 // Type or member is obsolete
					StyxWoW.Sleep(1000);
#pragma warning restore CS0618 // Type or member is obsolete
				}
            }
        }

        public override void Pulse()
        {
            try
            {
                useHat();
            }
            catch (Exception e)
            {
                Log("Exception: " + e, true);
            }
        }
    }
}