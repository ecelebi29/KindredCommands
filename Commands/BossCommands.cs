using System.Collections.Generic;
using System.Linq;
using KindredCommands.Commands.Converters;
using ProjectM;
using ProjectM.Network;
using Stunlock.Core;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using VampireCommandFramework;

namespace KindredCommands.Commands;
[CommandGroup("boss")]
internal class BossCommands
{
	[Command("modify", "m", description: "Modify the level of the specified nearest boss.", adminOnly: true)]
	public static void ModifyBossCommand(ChatCommandContext ctx, FoundVBlood boss, int level)
	{
		var entityManager = Core.EntityManager;
		var playerEntity = ctx.Event.SenderCharacterEntity;
        var playerPos = playerEntity.Read<LocalToWorld>().Position;
        var closestVBlood = Entity.Null;
        var closestDistance = float.MaxValue;
		
		foreach (var entity in Helper.GetEntitiesByComponentType<VBloodUnit>(includeDisabled: true).ToArray().Where(x => Vector3.Distance(x.Read<Translation>().Value, Vector3.zero) > 1))
		{
			if (entity.Read<PrefabGUID>().GuidHash != boss.Value.GuidHash)
				continue;

			if (Vector3.Distance(entity.Read<Translation>().Value, playerPos) < closestDistance)
			{
				closestDistance = Vector3.Distance(entity.Read<Translation>().Value, playerPos);
				closestVBlood = entity;
			}
		}

		if (closestVBlood.Equals(Entity.Null))
        {
            ctx.Reply($"Couldn't find '{boss.Name}' to modify");
            return;
        }
		
		var unitLevel = closestVBlood.Read<UnitLevel>();
		var previousLevel = unitLevel.Level;
		unitLevel.Level._Value = level;
        closestVBlood.Write<UnitLevel>(unitLevel);

		ctx.Reply($"Changed the nearest {boss.Name} to level {level} from level {previousLevel}");
	}

	[Command("teleportto", "tt", description: "Teleports you to the named boss. (If multiple specify the number of which one)", adminOnly: true)]
    public static void TeleportToBossCommand(ChatCommandContext ctx, FoundVBlood boss, int whichOne=0)
    {
		var foundBosses = new List<Entity>();
        foreach (var entity in Helper.GetEntitiesByComponentType<VBloodUnit>(includeDisabled: true).ToArray().Where(x => Vector3.Distance(x.Read<Translation>().Value, Vector3.zero)>1))
        {
			if(entity.Read<PrefabGUID>().GuidHash != boss.Value.GuidHash)
				continue;

			foundBosses.Add(entity);
		}

		if(!foundBosses.Any())
		{
			ctx.Reply($"Couldn't find {boss.Name}");
		}
		else if (foundBosses.Count > 1 && whichOne==0)
		{
			ctx.Reply($"Found {foundBosses.Count} {boss.Name}. Please specify the number of which one to teleport to.");
		}
		else
		{
			var index = whichOne == 0 ? 0 : Mathf.Clamp(whichOne, 1, foundBosses.Count) - 1;
			var bossEntity = foundBosses[index];
			var pos = bossEntity.Read<Translation>().Value;

			var entity = Core.EntityManager.CreateEntity(
					ComponentType.ReadWrite<FromCharacter>(),
					ComponentType.ReadWrite<PlayerTeleportDebugEvent>()
				);

			Core.EntityManager.SetComponentData<FromCharacter>(entity, new()
			{
				User = ctx.Event.SenderUserEntity,
				Character = ctx.Event.SenderCharacterEntity
			});

			Core.EntityManager.SetComponentData<PlayerTeleportDebugEvent>(entity, new()
			{
				Position = new float3(pos.x, pos.y, pos.z),
				Target = PlayerTeleportDebugEvent.TeleportTarget.Self
			});

			ctx.Reply($"Teleporting to {boss.Name} at {pos}");
		}
    }

	[Command("lock", "l", description: "Locks the specified boss from spawning.", adminOnly: true)]
	public static void LockBossCommand(ChatCommandContext ctx, FoundVBlood boss)
	{
		if (Core.Boss.LockBoss(boss))
			ctx.Reply($"Locked {boss.Name}");
		else
			ctx.Reply($"{boss.Name} is already locked");
	}

	[Command("unlock", "u", description: "Unlocks the specified boss allowing it to spawn.", adminOnly: true)]
	public static void UnlockBossCommand(ChatCommandContext ctx, FoundVBlood boss)
	{
		if(Core.Boss.UnlockBoss(boss))
			ctx.Reply($"Unlocked {boss.Name}");
		else
			ctx.Reply($"{boss.Name} is already unlocked");
	}

	[Command("list", "ls", description: "Lists all locked bosses.", adminOnly: true)]
    public static void ListLockedBossesCommand(ChatCommandContext ctx)
    {
        var lockedBosses = Core.Boss.LockedBossNames;
        if (lockedBosses.Any())
        {
            ctx.Reply($"Locked bosses: {string.Join(", ", lockedBosses)}");
        }
        else
        {
            ctx.Reply("No bosses are currently locked.");
        }
    }
}
