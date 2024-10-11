using MelonLoader;
using BoneLib;
using UnityEngine;
using Il2CppSLZ.Marrow;

[assembly: MelonInfo(typeof(HolsterScaler.Core), "HolsterScaler", "1.0.0", "SonofForehead", null)]
[assembly: MelonGame("Stress Level Zero", "BONELAB")]

namespace HolsterScaler
{
    public class Core : MelonMod
    {
        public static float holsterSize;

        public static void OnAvatarSwitch()
        {
            holsterSize = Player.Avatar.height / 2;

            Array slotContainer = Player.RigManager.physicsRig.GetComponentsInChildren<SlotContainer>();

            foreach (SlotContainer container in slotContainer)
            {
                container.gameObject.transform.localScale = new Vector3(holsterSize, holsterSize, holsterSize);
            }
        }
    }

    [HarmonyLib.HarmonyPatch(typeof(RigManager), "SwitchAvatar")]
    public static class AvatarPatch
    {
        public static void Postfix(Avatar __instance)
        {
            {
                HolsterScaler.Core.OnAvatarSwitch();
            }
        }
    }
}