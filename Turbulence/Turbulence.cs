using System;
using System.Collections.Generic;
using System.Windows.Media;
using Styx;
using Styx.Common;
using Styx.Plugins;
using Styx.WoWInternals.WoWObjects;

namespace Turbulence
{
    public class Turbulence : HBPlugin
    {
        public override string Name { get { return "Turbulence"; } }

        public override string Author { get { return "p1xel8ted"; } }

        public override Version Version { get { return new Version(1, 0, 0); } }

        private static LocalPlayer Me { get { return StyxWoW.Me; } }

        private List<uint> gearList = new List<uint>(new uint[] {
        122626,122632,122633,122631,122624,122628,122625,114068,
        122626,122629,122622,122627,122623,122630,114110,114069,
        114078,114075,122621,114080,114071,114070,114100,114105,
        114097,114099,114094,114108,114096,114098,114101,114060,
        114066,114059,114057,114063,114058,114109
        });

        private void checkForGear()
        {
            foreach (WoWItem item in Me.BagItems)
            {
                if (gearList.Contains(item.ItemInfo.Id))
                {
#pragma warning disable CS0618 // Type or member is obsolete
					StyxWoW.Sleep(3000);
#pragma warning restore CS0618 // Type or member is obsolete
					item.Use();
                    Log("Using " + item.Name + "!", false);
                }
                continue;
            }
            return;
        }

        private void Log(String log, Boolean error)
        {
            if (error)
            {
                Logging.Write(Colors.Red, "Turbulence " + Version + ": " + log);
            }
            else
            {
                Logging.Write(Colors.Magenta, "Turbulence " + Version + ": " + log);
			}
        }

        public override void OnEnable()
        {
            Log("Enabled", false);
            base.OnEnable();
        }

        public override void OnDisable()
        {
            Log("Disabled", true);
            base.OnDisable();
        }

        public override void Pulse()
        {
            checkForGear();
        }
    }
}