using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Windows.Forms;
using System.Windows.Media;
using Bots.Grind;
using Styx;
using Styx.Common;
using Styx.CommonBot;
using Styx.Plugins;
using Styx.WoWInternals;
using Styx.WoWInternals.WoWObjects;

namespace Baiterator
{
    public class Baiterator : HBPlugin
    {
        public override string Name { get { return "Baiterator"; } }

        public override string Author { get { return "p1xel8ted"; } }

        public override string ButtonText { get { return "Settings"; } }

        public override bool WantButton { get { return true; } }

        public override Version Version { get { return new Version(1, 0, 9); } }

        private static LocalPlayer Me { get { return StyxWoW.Me; } }

        private int baitCount = 0;

        private System.Timers.Timer csBaitTimer = new System.Timers.Timer(600000);

        //This is the list of baits, at the moment its just the WoD baits, because it's all I designed it for. Feel free to add others that you want in the rotation
        private List<uint> baits = new List<uint>(new uint[] {
            110293, //Abyssal Gulper Eel Bait
            110294, //Blackwater Whiptail Bait
            110290, //Blind Lake Sturgeon Bait
            110289, //Fat Sleeper Bait
            110291, //Fire Ammonite Bait
            110274, //Jawless Skulker Bait
            110292, //Sea Scorpion Bait
        });

        //This is the aura for bait once its applied
        private List<uint> baitAuras = new List<uint>(new uint[] {
            158038, //Abyssal Gulper Eel Bait Aura
            158039, //Blackwater Whiptail Bait Aura
            158035, //Blind Lake Sturgeon Bait Aura
            158034, //Fat Sleeper Bait Aura
            158036, //Fire Ammonite Bait Aura
            158031, //Jawless Skulker Bait Aura
            158037, //Sea Scorpion Bait Aura
        });

        //Don't touch this list
        private List<uint> usedBaits = new List<uint>(new uint[] { });

        private List<uint> usedBaitCounter = new List<uint>(new uint[] { });

        //Add/remove items in here to delete them/keep them
        private List<uint> deleteList = new List<uint>(new uint[] {
            118380, //Hightfish Cap
            118393, //Tentacled Hat
            118381 //Ephemeral Fishing Pole
        });

        //This is the ids of the dweller items, no need to touch anything in here
        private List<uint> dwellerList = new List<uint>(new uint[] {
            116158, //Lunarfall Carp
            112633 //Frostdeep Minnow
        });

        private List<uint> lootArangList = new List<uint>(new uint[] {
            109167, //Findle's Loot-A-Rang
            60854 //Loot-A-Rang (Eng Only)
        });

        private const int FPItemId = 118381; //Ephemeral Fishing Pole Item
        private const int bHookItem = 122742; //Bladebone Hook Item
        private const int bHookAura = 182226; //Bladebone Hook Aura
        private const int draenicPoleItem = 116826; //Draenic Fishing Pole Item

        private frmSettings settings = null;

        private int dwellersSummoned = 0;
        private bool useCsBait;
        private int percentDone;
        private int csBaitUsed;
        private DateTime startTime;
        private DateTime endTime;
        private TimeSpan duration = new TimeSpan(0, 0, 30);

        public Baiterator()
        {
            settings = new frmSettings();
            settings.updateDontUse();
        }

        public override void OnButtonPress()
        {
            settings.ShowDialog();
            settings.saveSettings();
            updateStatus();
        }

        //big thanks to Lacrymae - I barely understand this linq stuff.
        private IOrderedEnumerable<WoWUnit> LootableCorpses
        {
            get
            {
                var lootList = ObjectManager.GetObjectsOfType<WoWUnit>(false, false).Where(b => b.Lootable && b.Distance <= 100).OrderBy(l => l.Distance);
                return lootList;
            }
        }

        private void Log(string log, bool error)
        {
            if (error)
            {
                Logging.Write(Colors.Red, Name + " " + Version + " - " + log);
            }
            else
            {
                Logging.Write(Colors.OrangeRed, Name + " " + Version + " - " + log);
            }
        }

        private void updateStatus()
        {
            Log("Summon Dweller: " + settings.SummonCave, false);
            Log("Remove Excess Gear: " + settings.RemoveGear + ", Keep: " + settings.KeepGear.ToString(), false);
            Log("Use Bladebone Hook: " + settings.UseBladebone, false);
            Log("Remove Buffs Not Applied By " + Name + ": " + settings.RemoveFB, false);
            Log("-------------------", false);
            reportUsage();
        }

        private void reportUsage()
        {
            Log("Dwellers Summoned: " + dwellersSummoned, false);
        }

        public override void OnEnable()
        {
            settings.readSettings();
            usedBaitCounter.Clear();
            usedBaits.Clear();
            Log("Enabled", false);
            updateStatus();
            HotkeysManager.Register("Baiterator_Remove", Keys.E, ModifierKeys.Alt, hk => { BaiteratorRemove(); });
            HotkeysManager.Register("Baiterator_Summon", Keys.Q, ModifierKeys.Alt, hk => { BaiteratorSummon(); });
            BotEvents.OnBotStarted += OnBotStarted;
            BotEvents.OnBotStopped += OnBotStopped;
            base.OnEnable();
        }

        private void OnBotStarted(EventArgs args)
        {
            dwellersSummoned = 0;
            settings.updateDontUse();
            Log("Beginning Baiteration Process.", false);
        }

        public override void OnDisable()
        {
            settings.saveSettings();
            Log("Disabled", false);

            HotkeysManager.Unregister("Baiterator_Summon");
            HotkeysManager.Unregister("Baiterator_Remove");
            BotEvents.OnBotStarted -= OnBotStarted;
            BotEvents.OnBotStopped -= OnBotStopped;
            base.OnDisable();
        }

        private void OnBotStopped(EventArgs args)
        {
            Log("Stopping Baiteration Process.", false);
            reportUsage();
            reportBaitUsage();
        }

        private void BaiteratorSummon()
        {
            if (settings.SummonCave == true)
            {
                settings.SummonCave = false;
                Log("Summon Dweller: False", true);
            }
            else
            {
                settings.SummonCave = true;
                Log("Summon Dweller: True", false);
            }
            settings.saveSettings();
        }

        private void BaiteratorRemove()
        {
            if (settings.RemoveGear == true)
            {
                settings.RemoveGear = false;
                Log("Remove Excess Gear: False", true);
            }
            else
            {
                settings.RemoveGear = true;
                Log("Remove Excess Gear: True", false);
            }
            settings.saveSettings();
        }

        private bool hasFishingPoleEquipped() //check if we have a fishing pole equipped or not
        {
            try
            {
                if (StyxWoW.Me.Inventory.Equipped.MainHand.ItemInfo.WeaponClass == WoWItemWeaponClass.FishingPole)
                {
                    return true;
                }
                else
                {
                    if (!Me.Combat)
                    {
                        Log("A fishing pole must be equipped for " + Name + " to function.", true);
                    }
                    return false;
                }
            }
            catch (Exception)
            {
                if (!Me.Combat)
                {
                    Log("A fishing pole must be equipped for " + Name + " to function.", true);
                }
                return false;
            }
        }

        private WoWItem getDweller()
        {
            foreach (uint i in dwellerList)
            {
                WoWItem dwellar = StyxWoW.Me.BagItems.FirstOrDefault(r => r.Entry == i);
                if (dwellar != null)
                {
                    return dwellar;
                }
                continue;
            }
            return null;
        }

        private void reportBaitUsage()
        {
            var bait0 = usedBaitCounter.Count(item => item == baits[0]);
            var bait0Name = ItemInfo.FromId(baits[0]).Name;
            var bait1 = usedBaitCounter.Count(item => item == baits[1]);
            var bait1Name = ItemInfo.FromId(baits[1]).Name;
            var bait2 = usedBaitCounter.Count(item => item == baits[2]);
            var bait2Name = ItemInfo.FromId(baits[2]).Name;
            var bait3 = usedBaitCounter.Count(item => item == baits[3]);
            var bait3Name = ItemInfo.FromId(baits[3]).Name;
            var bait4 = usedBaitCounter.Count(item => item == baits[4]);
            var bait4Name = ItemInfo.FromId(baits[4]).Name;
            var bait5 = usedBaitCounter.Count(item => item == baits[5]);
            var bait5Name = ItemInfo.FromId(baits[5]).Name;
            var bait6 = usedBaitCounter.Count(item => item == baits[6]);
            var bait6Name = ItemInfo.FromId(baits[6]).Name;
            Log("----------", false);
            Log("Baits Used", false);
            Log(bait0Name + ": " + bait0.ToString(), false);
            Log(bait1Name + ": " + bait1.ToString(), false);
            Log(bait2Name + ": " + bait2.ToString(), false);
            Log(bait3Name + ": " + bait3.ToString(), false);
            Log(bait4Name + ": " + bait4.ToString(), false);
            Log(bait5Name + ": " + bait5.ToString(), false);
            Log(bait6Name + ": " + bait6.ToString(), false);
            Log("Crescent Saberfish 'Bait': " + csBaitUsed.ToString(), false);
        }

        private void recordBaitUsage(WoWItem bait)
        {
            usedBaitCounter.Add(bait.ItemInfo.Id);
        }

        private void throwTheBoomerang()
        {
            var lootTarget = LootableCorpses.FirstOrDefault(); //Thank you to Lacrymae.
            if (getLootArang() != null & lootTarget != null)
            {
                if (lootTarget.Lootable)
                {
                    Log("Using " + getLootArang().SafeName + "!", false);
                    getLootArang().UseContainerItem();
                }
            }
        }

        private WoWItem getLootArang()
        {
            foreach (uint i in lootArangList)
            {
                WoWItem lootArang = StyxWoW.Me.BagItems.FirstOrDefault(r => r.Entry == i);
                if (lootArang != null)
                {
                    var lootCD = new TimeSpan(0, 0, 0);
                    if (lootArang.CooldownTimeLeft <= lootCD)
                    {
                        return lootArang;
                    }
                }
                continue;
            }
            return null;
        }

        //check if we are capable of fishing etc
        private bool IfCapable()
        {
            if (!hasFishingPoleEquipped() || Me.Stunned || Me.IsDead || Me.OnTaxi || Me.IsGhost || Me.IsOnTransport || Me.Combat || Me.IsMoving || (Me.Mounted && Me.IsFlying))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //check if we have any existing bait auras
        private bool checkAura()
        {
            foreach (int i in baitAuras)
            {
                if (Me.HasAura(i))
                {
                    return true;
                }
                else
                {
                    continue;
                }
            }
            return false;
        }

        private void RemoveFbBuffs()
        {
            if (Me.HasAura(bHookAura) & !settings.UseBladebone)
            {
                Me.GetAuraById(bHookAura).TryCancelAura();
                Log("Removing " + Me.GetAuraById(bHookAura).Name + " as it is disabled.", false);
#pragma warning disable CS0618 // Type or member is obsolete
                StyxWoW.Sleep(1000);
#pragma warning restore CS0618 // Type or member is obsolete
            }
            foreach (int i in settings.dontUseAura)
            {
                if (Me.HasAura(i))
                {
                    Me.GetAuraById(i).TryCancelAura();
                    Log("Removing " + Me.GetAuraById(i).Name + " as it is disabled.", false);
#pragma warning disable CS0618 // Type or member is obsolete
                    StyxWoW.Sleep(1000);
#pragma warning restore CS0618 // Type or member is obsolete
                }
                else
                {
                    //Log("Allowing " + Me.GetAuraById(i).Name + " as it is enabled.", false);
                    continue;
                }
            }
        }

        private WoWItem getBait()
        {
            if (usedBaits.Count == (baits.Count - settings.dontUse.Count))
            {
                if (settings.useCS)
                {
                    useCsBait = true;
                    return null;
                }
                else
                {
                    usedBaits.Clear();
                    useCsBait = false;
                }
            }
            foreach (uint i in baits)
            {
                WoWItem bait = StyxWoW.Me.BagItems.FirstOrDefault(r => r.Entry == i);
                if (bait == null)
                {
                    continue;
                }
                else
                {
                    if (settings.dontUse.Contains(i))
                    {
                        //Log("Skipping " + bait.SafeName + " as it is not enabled.", true);
                        continue;
                    }
                    else
                    {
                        if (!InGarrison())
                        {
                            return bait;
                        }
                        else
                        {
                            if (!usedBaits.Contains(i))
                            {
                                usedBaits.Add(i);
                                return bait;
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                }
            }
            return null;
        }

        private int getBaitCount()
        {
            baitCount = 0;
            foreach (uint i in baits)
            {
                WoWItem bait = StyxWoW.Me.BagItems.FirstOrDefault(r => r.Entry == i);
                if (bait == null)
                {
                    continue;
                }
                else
                {
                    baitCount += 1;
                }
            }
            return baitCount;
        }

        //remove any excess hats/poles. should always leave you with whatever you KEEP_AMOUNT to
        private void removeExcessGear()
        {
            foreach (WoWItem item in StyxWoW.Me.BagItems)
            {
                if (deleteList.Contains(item.ItemInfo.Id))
                {
                    while (StyxWoW.Me.GetCarriedItemCount(item.ItemInfo.Id) > settings.KeepGear)
                    {
                        try
                        {
                            if (StyxWoW.Me.Inventory.Equipped.MainHand != item | StyxWoW.Me.Inventory.Equipped.Head != item)
                            {
                                item.PickUp();
#pragma warning disable CS0618 // Type or member is obsolete
                                StyxWoW.Sleep(1000);
                                Lua.DoString("DeleteCursorItem()");
                                StyxWoW.Sleep(1000);
#pragma warning restore CS0618 // Type or member is obsolete
                            }
                            Log("Removed extra gear! Keeping " + settings.KeepGear + " of each item!", false);
                        }
                        catch (Exception e)
                        {
                            Log("Exception: " + e, true);
                            return;
                        }
                    }
                    continue;
                }
                continue;
            }
            return;
        }

        //check if we are in either the horde or alliance garrison
        private bool InGarrison()
        {
            if (Me.ZoneId == 7004 | Me.ZoneId == 7078)
            {
                return true;
            }
            return false;
        }

        private bool isFishing()
        {
            if (hasFishingPoleEquipped() & Me.IsChanneling)
            {
                return true;
            }
            return false;
        }

        private void applyBladebone()
        {
            if (!Me.HasAura(bHookAura))
            {
                WoWItem hook = StyxWoW.Me.BagItems.FirstOrDefault(r => r.Entry == bHookItem);
                if (hook != null)
                {
                    Log("Using " + hook.ItemInfo.Name + "!", false);
                    hook.Use();
#pragma warning disable CS0618 // Type or member is obsolete
                    StyxWoW.Sleep(1000);
#pragma warning restore CS0618 // Type or member is obsolete
                }
            }
        }

        private bool okToSummonCaveDwellar()
        {
            if (InGarrison() & settings.SummonCave & isFishing() & !Me.IsMoving & getDweller() != null)
            {
                return true;
            }
            return false;
        }

        private void killDweller()
        {
#pragma warning disable CS0618 // Type or member is obsolete
            StyxWoW.Sleep(2000);
            LevelBot.BehaviorFlags = BehaviorFlags.All;
            Lua.DoString("TargetNearestEnemy()");
            Log("Summoned Dweller, attempting to initiate combat!", false);
            dwellersSummoned += 1;
            Lua.DoString("AttackTarget()");
#pragma warning restore CS0618 // Type or member is obsolete
        }

        public override void Pulse()
        {
            getBaitCount();
            settings.updateDontUse();
            if (useCsBait)
            {
                RemoveAllBaitBuffs();
                Log("Time Remaining: " + endTime.Subtract(DateTime.Now).ToString(), false);
            }
            if (settings.RemoveFB)
            {
                RemoveFbBuffs();
            }
            throwTheBoomerang();
            if (IfCapable())
            {
                if (settings.RemoveGear)
                {
                    removeExcessGear();
                }
                if (okToSummonCaveDwellar())
                {
                    getDweller().Use();
                    killDweller();
                }

                if (!Me.Combat) //if im not in combat, start fishing
                {
                    if (isFishing() & !Me.IsMoving)
                    {
                        //checkForDraenicPole();
                        if (settings.UseBladebone)
                        {
                            applyBladebone();
                        }

                        if (!checkAura()) //check if we have any active bait buffs
                        {
                            WoWItem bait = getBait(); //get next available bait
                            if (bait != null & !useCsBait)
                            {
                                bait.Use();
                                Log("Used " + bait.Name + "!", false);
                                recordBaitUsage(bait);
                                reportBaitUsage();
#pragma warning disable CS0618 // Type or member is obsolete
                                StyxWoW.Sleep(1000); //necessary, otherwise plugin was using next bait before aura was applied from previous bait
#pragma warning restore CS0618 // Type or member is obsolete
                            }
                            else if (bait == null & useCsBait & !csBaitTimer.Enabled)
                            {
                                csBaitUsed += 1;
                                RemoveAllBaitBuffs();
                                endTime = DateTime.Now.AddMinutes(10);
                                csBaitTimer.AutoReset = true;
                                csBaitTimer.Elapsed += new ElapsedEventHandler(checkTime);
                                csBaitTimer.Enabled = true;
                                Log("Beginning Crescent Saberfish Hunt. 10 Minutes Remaining.", false);
                            }
                        }
                    }
                }
            }
        }

        private void RemoveAllBaitBuffs()
        {
            foreach (int aura in baitAuras)
            {
                if (Me.HasAura(aura))
                {
                    Me.GetAuraById(aura).TryCancelAura();
                }
            }
        }

        private void checkTime(object sender, ElapsedEventArgs e)
        {
            Log("10 minutes has passed. Time to resume Baiteration.", false);
            useCsBait = false;
            usedBaits.Clear();
            csBaitTimer.Enabled = false;
        }
    }
}