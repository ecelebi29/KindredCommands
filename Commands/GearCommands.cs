using System.Text;
using KindredCommands.Commands.Converters;
using ProjectM;
using ProjectM.Shared;
using Unity.Entities;
using VampireCommandFramework;

namespace KindredCommands.Commands;
internal static class DurabilityCommands
{
	[CommandGroup("gear")]
	internal class GearCommands
	{
		[Command("repair", "r", description: "Repairs all gear.", adminOnly: true)]
		public static void RepairCommand(ChatCommandContext ctx, FoundPlayer player = null)
		{
			var targetEntity = player?.Value.CharEntity ?? ctx.Event.SenderCharacterEntity;

			Helper.RepairGear(targetEntity);
			ctx.Reply($"Gear repaired for {targetEntity.Read<PlayerCharacter>().Name}.");
		}

		[Command("break", "b", description: "Breaks all gear.", adminOnly: true)]
		public static void BreakGearCommand(ChatCommandContext ctx, FoundPlayer player = null)
		{
			var targetEntity = player?.Value.CharEntity ?? ctx.Event.SenderCharacterEntity;
			Helper.RepairGear(targetEntity, false);
			ctx.Reply($"Gear broken for {targetEntity.Read<PlayerCharacter>().Name}.");
		}

		[Command("headgear", "hg", description: "Toggles headgear loss on death.", adminOnly: true)]
		public static void HeadgearBloodBoundCommand(ChatCommandContext ctx)
		{
			if(Core.GearService.ToggleHeadgearBloodbound())
			{
				ctx.Reply("Headgear will not be lost on death.");
			}
			else
			{
				ctx.Reply("Headgear will be lost on death.");
			}
		}

		[Command("soulshardflight", "ssf", description: "Toggles soulshard flight restrictions.", adminOnly: true)]
		public static void SoulshardsFlightRestrictedCommand(ChatCommandContext ctx)
		{
			if (Core.GearService.ToggleShardsFlightRestricted())
			{
				ctx.Reply("Soulshards will not allowing flying.");
			}
			else
			{
				ctx.Reply("Soulshards will allow flying.");
			}
		}

		[Command("togglesoulsharddropmanagement", "tssdm", description: "Toggles whether KindredCommands will do soulshard drop management.", adminOnly: true)]
		public static void ToggleSoulshardDropManagementCommand(ChatCommandContext ctx)
		{
			if (Core.SoulshardService.ToggleShardDropManagement())
			{
				ctx.Reply("Soulshard drop management by KindredCommands enabled.");
			}
			else
			{
				ctx.Reply("Soulshard drop management by KindredCommands disabled.");
			}
		}

		[Command("soulshardlimit", "ssl", description: "How many soulshards can be dropped before a boss won't drop a new one if the relic Unique setting is active.", adminOnly: true)]
		public static void SoulshardLimitCommand(ChatCommandContext ctx, int limit, RelicType shardType=RelicType.None)
		{
			if (limit < 0)
			{
				throw ctx.Error("Limit must be zero or greater.");
			}
			Core.SoulshardService.SetShardDropLimit(limit, shardType);
			if (shardType == RelicType.None)
			{
				ctx.Reply($"Soulshard limit set to {limit} for all soulshards.");
			}
			else
			{
				ctx.Reply($"Soulshard limit set to {limit} for {shardType}.");
			}
		}

		[Command("soulshardstatus", "sss", description: "Reports the current status of soulshards.", adminOnly: false)]
		public static void SoulshardStatusCommand(ChatCommandContext ctx)
		{
			var sb = new StringBuilder();
			var soulshardStatus = Core.SoulshardService.GetSoulshardStatus();
			sb.AppendLine("\nSoulshard Status");
			sb.AppendLine($"Can Fly: {(Core.ConfigSettings.SoulshardsFlightRestricted ? "<color=red>No</color>" : "<color=green>Yes</color>")}");

			var notPlentiful = Core.ServerGameSettingsSystem._Settings.RelicSpawnType == RelicSpawnType.Unique;

			var managesDrops = notPlentiful && Core.ConfigSettings.ShardDropManagementEnabled;
			var theMonster = soulshardStatus[(int)RelicType.TheMonster];
			var solarus = soulshardStatus[(int)RelicType.Solarus];
			var wingedHorror = soulshardStatus[(int)RelicType.WingedHorror];
			var dracula = soulshardStatus[(int)RelicType.Dracula];
			sb.Append($"The Monster: <color=white>{theMonster.droppedCount}</color>x");
			if (managesDrops) sb.Append($" out of <color=white>{Core.ConfigSettings.ShardMonsterDropLimit}</color>x");
			sb.AppendLine($" dropped <color=white>{theMonster.spawnedCount}</color>x spawned{(notPlentiful ? (theMonster.willDrop ? " <color=green>Will</color> drop" : " <color=red>Won't</color> drop") : "")}");

			sb.Append($"Solarus: <color=white>{solarus.droppedCount}</color>x ");
			if (managesDrops) sb.Append($"out of <color=white>{Core.ConfigSettings.ShardSolarusDropLimit}</color>x");
			sb.AppendLine($" dropped <color=white>{solarus.spawnedCount}</color>x spawned{(notPlentiful ? (solarus.willDrop ? " <color=green>Will</color> drop" : " <color=red>Won't</color> drop") : "")}");

			ctx.Reply(sb.ToString());
			sb.Clear();

			sb.Append($"Winged Horror: <color=white>{wingedHorror.droppedCount}</color>x");
			if (managesDrops) sb.Append($" out of <color=white>{Core.ConfigSettings.ShardWingedHorrorDropLimit}</color>x");
			sb.AppendLine($" dropped <color=white>{wingedHorror.spawnedCount}</color>x spawned{(notPlentiful ? (wingedHorror.willDrop ? " <color=green>Will</color> drop" : " <color=red>Won't</color> drop") : "")}");

			sb.Append($"Dracula: <color=white>{dracula.droppedCount}</color>x");
			if (managesDrops) sb.Append($" out of <color=white>{Core.ConfigSettings.ShardDraculaDropLimit}</color>x");
			sb.AppendLine($" dropped <color=white>{dracula.spawnedCount}</color>x spawned{(notPlentiful ? (dracula.willDrop ? " <color=green>Will</color> drop" : " <color=red>Won't</color> drop") : "")}");
			ctx.Reply(sb.ToString());
		}

		[Command("soulsharddurability", "ssd", description: "Sets the durability on all soulshards in the player's inventory to a specified amount", adminOnly: true)]
		public static void ShardDurabilityCommand(ChatCommandContext ctx, float durability, FoundPlayer player = null)
		{
			var charEntity = player?.Value.CharEntity ?? ctx.Event.SenderCharacterEntity;

			if (durability < 0)
			{
				throw ctx.Error("Durability must be zero or greater.");
			}

			var shardsChanged = 0;
			foreach(var shard in Helper.GetEntitiesByComponentType<Relic>())
			{
				var relic = shard.Read<Relic>();
				if (relic.RelicType == RelicType.None) continue;

				if (!shard.Has<InventoryItem>()) continue;

			    var itemContainer = shard.Read<InventoryItem>().ContainerEntity;
				if (itemContainer.Equals(Entity.Null)) continue;

				if (!itemContainer.Has<InventoryConnection>()) continue;
				
				var inventoryOwner = itemContainer.Read<InventoryConnection>().InventoryOwner;
				if (inventoryOwner != charEntity) continue;

				var durabilityData = shard.Read<Durability>();
				durabilityData.Value = durability;
				shard.Write(durabilityData);

				shardsChanged += 1;
			}

			var playerName = charEntity.Read<PlayerCharacter>().Name;
			if (shardsChanged > 0)
				ctx.Reply($"<color=white>{shardsChanged}</color>x soulshards had their durability set to {durability} for <color=yellow>{playerName}</color>.");
			else
				ctx.Reply($"No soulshards found in <color=yellow>{playerName}</color>'s inventory.");
		}

		[Command("soulsharddurabilitytime", "ssdt", description: "How many seconds will soulshards last before they break", adminOnly: true)]
		public static void ShardDurabilityTimeCommand(ChatCommandContext ctx, int? seconds=null)
		{
			if (seconds.HasValue && seconds < 0)
			{
				throw ctx.Error("Time must be zero or greater.");
			}
			Core.SoulshardService.SetShardDurabilityTime(seconds);
			if (seconds == null)
			{
				ctx.Reply("Soulshard durability time restored to default.");
			}
			else
			{
				ctx.Reply($"Soulshard durability time set to {seconds} seconds.");
			}
		}
     /*
		[Command("showhair", "sh", description: "Toggles hair visibility.")]
		public static void ShowHairCommand(ChatCommandContext ctx)
		{
			var charEntity = ctx.Event.SenderCharacterEntity;
			var equipment = charEntity.Read<Equipment>();

			if (equipment.ArmorHeadgearSlotEntity.Equals(Entity.Null))
			{
				ctx.Reply("No headgear equipped.");
				return;
			}

			var headgear = equipment.ArmorHeadgearSlotEntity.GetEntityOnServer();
			var headgearToggleData = headgear.Read<HeadgearToggleData>();
			headgearToggleData.HideCharacterHairOnEquip = !headgearToggleData.HideCharacterHairOnEquip;
			headgear.Write(headgearToggleData);

			ctx.Reply("Hair is " + (headgearToggleData.HideCharacterHairOnEquip ? " hidden" : "visible") + " with current headgear");
		}

	*/

	}
}



