using MelonLoader;
using BoneLib;
using BoneLib.BoneMenu;
using System;
using UnityEngine;
using Il2CppSLZ.Marrow;
using Il2CppSLZ.Marrow.Warehouse;
using HarmonyLib;
using Harmony;

[assembly: MelonInfo(typeof(HolsterScaler.Core), "HolsterScaler", "1.0.0", "SonofForehead", null)]
[assembly: MelonGame("Stress Level Zero", "BONELAB")]

namespace HolsterScaler
{
    public class Core : MelonMod
    {
        public static bool modToggle;
        public static float holsterSize;
        public static void ModEnabled(bool toggle){ modToggle = toggle; }

        public override void OnInitializeMelon()
        {
            CreateBonemenu();
        }

        public static void CreateBonemenu()
        {
            Page mainPage = Page.Root.CreatePage("HolsterScaler", Color.black);
            mainPage.CreateBool("Mod Toggle", Color.white, true, ModEnabled);
        }

        public static void OnAvatarSwitch()
        {
            holsterSize = Player.Avatar.height / 2;

            Array slotContainer = Player.RigManager.physicsRig.m_spine.GetComponentsInChildren<SlotContainer>();

            if (modToggle)
            {
                foreach(SlotContainer container in slotContainer)
                {
                    container.gameObject.transform.localScale = new Vector3(holsterSize, holsterSize, holsterSize);
                    MelonLogger.Msg(holsterSize);
                }
            }
        }
    }

    [HarmonyLib.HarmonyPatch(typeof(RigManager), "SwitchAvatar")]
    public static class AvatarPatch
    {
        public static void Postfix(Avatar __instance)
        {
            {
                MelonLogger.Msg("Switched");
                HolsterScaler.Core.OnAvatarSwitch();
            }
        }
    }
}