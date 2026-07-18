using HarmonyLib;
using ProjectM;
using ProjectM.Network;
using Unity.Collections;

namespace KindredCommands.Patches;

// Blocks the unstuck self-kill escape for players held by .freeze
[HarmonyPatch(typeof(KillEventSystem), nameof(KillEventSystem.OnUpdate))]
internal static class KillEventSystemPatch
{
	public static void Prefix(KillEventSystem __instance)
	{
		if (__instance._Query.IsEmptyIgnoreFilter)
			return;

		var entities = __instance._Query.ToEntityArray(Allocator.Temp);
		try
		{
			foreach (var entity in entities)
			{
				var charEntity = entity.Read<FromCharacter>().Character;

				if (!Core.EntityManager.Exists(charEntity))
					continue;

				if (entity.Read<KillEvent>().Who != KillWho.Self)
					continue;

				if (!BuffUtility.TryGetBuff(Core.EntityManager, charEntity, Data.Prefabs.Buff_General_LockRotation, out var buffEntity))
					continue;

				if ((buffEntity.Read<BuffModificationFlagData>().ModificationTypes & (long)BuffModificationTypes.MovementImpair) == 0)
					continue;

				Core.EntityManager.DestroyEntity(entity);
			}
		}
		finally
		{
			entities.Dispose();
		}
	}
}
