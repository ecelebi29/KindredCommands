![](logo.png)
# KindredCommands for V Rising
KindredCommands is a server modification for V Rising that adds chat commands for the goal of server administration.
This is built upon CommunityCommands by deca with some fixes/tweaks and new commands added. Consultations by zfolmt. Credits to all of them for their work and inspiration. 
Also, thanks to the V Rising modding and server communities for ideas and requests! Commands now log to the server log for accountability and tracking.

Updated for V rising 1.0 as of v1.9.0!

[Territory ID Map](https://i.imgur.com/VkXoKwB.jpeg)

Feel free to reach out to me on discord (odjit) if you have any questions or need help with the mod.

[V Rising Modding Discord](https://vrisingmods.com/discord)

## Staff Commands
### Administration Commands
- `.toggleadmin (player)`
  - will add or remove a player from the admin list, authing and deauthing
- `.reloadadmin `
  - will reload the admin list
- `.stealthadmin`
  - will toggle stealth mode for the user as admin. This will enable you to still use all chat admin commands, but your name not go green. You cannnot adminauth or you will show as green again. Will persist through relog. You will get kicked, don't freak.
- `.reloadstaff`
  - reloads the staff list config file
- `.setstaff (Player) (Rank)`
  - Adds a player to the staff list, with whatever rank for listing in the online staff command return. This isn't the same as admin, as it allows for non-admin staff to be listed as well. It is merely a listing, and does not grant any powers.
  - Example: *.setstaff Joe King*
- `.removestaff (Player)`
  - Removes a player from the staff list, so they will no longer show in the staff command return when online.
  - Example: *.removestaff Joe*
- `.whereami`
  - will tell you your current location in the world as well as the territory ID of whatever plot you may be on (non plot reports -1)
  - Shortcut: *.wai*
- `.announce add (Name) (Message) (Time) (OneTime: True/False)`
  - Adds an announcement to the list of announcements. Time is server time. OneTime true will only do it once, false will repeat the announcement every day at the same time. (Default False)
  - Example: *.announce add Spooky “It is the spooky hour!” 12:00AM false*
  - Shortcut: *.announce a*
- `.announce remove (Name)`
  - Removes an announcement from the list of announcements.
  - Example: *.announce remove Spooky*
  - Shortcut: *.announce r*
- `.announce list`
  - Lists all announcements. Soonest upcoming announcements are at the start of the list.
  - Shortcut: *.announce l*
- `.announce change (Name) (Message) (Time) (OneTime: True/False)`
  - Changes an announcement in the list of announcements. Time is server time. OneTime true will only do it once, false will repeat the announcement every day at the same time. (Default False)
  - Example: *.announce change Spooky “It is the spookiest hour!” 12:00AM false*
  - Shortcut: *.announce c*
- `.unbindplayer (Name)`
  - Unbinds a steamID from a character. Useful for "deleting" a character. Old body, name, territory etc will still exist, but the character will be unplayable. Kicks affected player. When they log back, they will be prompted to create a new character.
  - Example:: *.unbindplayer Bob*
- `.swapplayers (Name1) (Name2)`
  - Swaps steamIDs between two characters. Useful for "changing" a character. You can swap two active players, or swap back into a previously unbound body. Kicks affected players.
  - Example: *.swapplayers Bob Joe*
- `.playerinfo (Player)`
  - will list the player's steamID, online status, clan name, Position, and list all of their castles (with index ID, region, and time remaining on heart).
  - Example: *.playerinfo Bob*
- `.idcheck (steamID)`
  - will see if a steamID is registered to a character, and if so, what character it is.
  - Example: *.idcheck 1234567890*
- `.settime (day) (hour)`
  - will set the in game time to a day and hour. Basically controls what the sun is doing. Careful of effects on hearts and horses.
  - Example: *.settime 1 8*
- `.gear headgear`
  - will toggle Headgear being bloodbound or not on the server. (Whether or not it drops on death). Saves out to config file to persist through restarts.
  - Example: *.gear headgear*
  - Shortcut: *.gear hg*
- `.gear togglesoulsharddropmanagement`
  - Toggles whether KindredCommands will do soulshard drop management.
  - Shortcut: *.gear tssdm*
- `.gear soulshardflight`
  - will toggle Soul Shards being batbound or not on the server. (Whether or not you can fly with them). Saves out to config file to persist through restarts.
  - Shortcut: *.gear ssf*
- `.gear soulshardlimit (amount) (type)`
  - will set the limit of each type soul shards a server can have. [Monster, Dracula, Winged, Solarus]
  - Example: *.gear soulshardlimit 5 Dracula*
  - Shortcut: *.gear ssl*
- `.gear soulsharddurability (amount) (player)`
  - will set the durability of all soulshards in the player's inventory to the amount specified. 
  - Example: *.gear soulsharddurability 2500 Bob*
  - Shortcut: *.gear ssd*
- `.gear soulshardurabilitytime (seconds)`
  - How many seconds will soulshards last before they break. Do not enter anything for seconds if you want default behavior.  
  - Example: *.gear soulshardurabilitytime 60*
  - Shortcut: *.gear ssdt*
- `.dropitems lifetime (seconds)`
  - will set the lifetime of dropped items while players are present to the seconds specified. Default is 300 seconds. This is a server wide setting, and will persist through restarts.Does not apply to shards or Player Containers.
  - Example: *.dropitems lifetime 600*
  - Shortcut: *.dropitems lt*
- `.dropitems removelifetime`
  - will remove the custom lifetime of dropped items, reverting to the default of 300 seconds. Does not apply to shards or Player Containers.
  - Shortcut: *.dropitems rlt*
- `.dropitems lifetimewhendisabled (seconds)`
  - will set the lifetime of dropped items when no one is around to the seconds specified. Default is 300 seconds. This is a server wide setting, and will persist through restarts. Does not apply to shards or Player Containers.
  - Example: *.dropitems lifetimewhendisabled 600*
  - Shortcut: *.dropitems ltwd*
- `.dropitems clear (radius)`
  - will clear all dropped items in a radius around you. Useful for cleaning up after a boss fight or a large event. Does not apply to shards or Player Containers.
  - Example: *.dropitems clear 10*
  - Shortcut: *.dropitems c*
- `.dropitems clearall`
  - will clear all dropped items on the map. 
  - Shortcut: *.dropitems ca*
- `.dropitems clearshards (radius)`
  - Will clear all dropped shards in a radius around you. 
  - Example: *.dropitems clearshards 10*
  - Shortcut: *.dropitems cs*
- `.dropitems clearallshards`
  - Will clear all dropped shards on the map.
  - Shortcut: *.dropitems cas*
- `.dropitems shardlifetime (seconds)`
  - Will set the lifetime of dropped shards. Default is 3600 seconds. This is a server wide setting, and will persist through restarts
  - Example: *.dropitems shardlifetime 600*
  - shortcut: *.dropitems slt*

### Spawning Commands
- `.bloodpotion (Bloodtype) (Quality) (Amount)`
  - will give Merlot of specified type and quality. You can also specify an amount
  - Example: *.bp creature 100 1*
  - Shortcut: *.bp*
- `.give (Item prefab name) (amount)`			
  - Example: *.give Headgear_arcmageCrown 1*
  - Shortcut: *.g*
- `.search item (name) (page #)`
  - Will respond with the item prefab name needed.
  - Example: *.search item arcmage*
  - Shortcut: *.search i*
- `.search npc (name) (page #)`
  - Will respond with the item prefab name needed.
  - Example: *.search item vblood*
  - Shortcut: *.search n*
- `.spawnnpc (guid) (amount)`
  - Spawns an npc specified at your location in the amount specified
  - Example: *.spawnnpc CHAR_ChurchOfLight_Lightweaver 1*
  - Shortcut: *.spwn*
- `.customspawn (Prefab ID) (BloodType) (BloodQuality) (Consumable: true/false) (duration) (level)`
  - Spawns an npc with specific blood type, quality, whether or not you can 'eat' it, how long it will be up, and at what level.
  - Example: *.customspawn CHAR_ChurchOfLight_Lightweaver scholar 100 true -1 100*
  - Shortcut: *.cspwn*
  - See .search npc above for prefab IDs
- `.despawnnpc (guid) (range)`
  - will kill any entity matching the ID specified. Use sparingly as this is an expensive call, and could cause minor lag depending. Just for the cases where you can't kill something by hand. 
  - Example: *.despawnnpc CHAR_ChurchOfLight_Lightweaver 10*
  - Shortcut: *.dspwn*
- `.spawnban (Prefab GUID name) (reason)`
  - saves a GUID to the banned list, preventing customspawn or spawnnpc from creating it. Helps prevent server crashes and corruption. To remove from the ban list, delete the line from the nospawn.json in the Config folder.
  - Example: *.spawnban CHAR_ChurchOfLight_Lightweaver "This NPC is too cute"*
- `.spawnhorse (speed) (acceleration) (rotation) (Spectral: true/false) (amount)`
  - Spawns a horse at your location with the specified stats and amount..
  - Example: *.spawnhorse 10 10 10 false 1*
  - Shortcut: *.sh*
- `.banishhorse`
  - will teleport all ghost horses to the edge of the unused map beyond the gates in the nether. Useful for cleaning up ghost horses without killing them (despawn will kill them, making them unsummonable/dead)
  - Shortcut: *.bh*
- `.teleporthorse (radius)`
  - will teleport all dominated horses within the radius specified to your location. Useful for cleaning up horses that are stuck in walls or other objects or that are desynced.
  - Example: *.teleporthorse 10*

### Augmenting Player Commands
- `.buff (Buff GUID) (Player) (Duration) (Immortal)`
  - Adds a buff to a player named, or the user if no one is named. Duration 0 for default behavior, -1 for eternal, or whatever number of seconds. Immortal true/false for it to last after death.	Be careful, as some buffs can break things. Always test on a test server first.
  - Example: *.buff 476186894 Bob*
- `.debuff (Buff GUID) (Player)`
  - Removes a buff from a player named, or the user if no one is named. Will work on offline players.
  - Example: *.debuff 476186894 Bob*
- `.listbuffs (Player)`
  - will show all buffs on a player																								
  - Example: *.listbuffs Bob*
- `.resetcooldown (Player)`
  - Resets all ability and skill cooldowns for the player		
  - Example: *.resetcooldown Bob*
  - Shortcut: *.cd*
- `.god (Player)`
  - will toggle godmode on a player named, or the user if no one is named. Super speed, spells, damage, etc: Everything from boosts.																								
  - Example: *.god Bob*
- `.mortal (player)`
  - will toggle godmode off a player named, or the user if no one is named.	Also removes boosts.																							
  - Example: *.mortal Bob*
- `.boost (Type) (Player)`
  - will boost a player with certain types: noaggro, noblooddrain, nocooldown, nodurability, immaterial, invincible, shrouded, fly, suninvulnerable. Remove via use of same command again as a toggle or use .mortal to strip all.
  - Example: *.boost immaterial Bob*
  - Shortcuts: *.boost (na, nb, nc, nd, i, inv, sh, f, suninv)*
- `.boost (Type) (Ammount) (Player)`
  - will boost a player's stats to the amount specified. Types with amounts are the following: attackspeed, damage, health, speed, and yield. 																							
  - Example: *.boost damage 100 Bob*
  - Shortcut: *.boost (as, d, h, s, y)*
- `.boost remove(Type) (Player)`
  - will remove a boost from a player. Used for removing boosts that require an amount set.																							
  - Example: *.boost removedamage Bob*
  - Shortcut: *.boost (ras, rd, rh, rs, ry)*
- `.boost state (Player)`
  - will list all boosts on a player.																							
  - Example: *.boost state Bob*
- `.boost players`
  - provides a list of all boosted players
- `.spectate (Player)`
  - will set the player into spectate mode, where they are invisible and cannot interact with anything. Use again to remove it and teleport them to their prior position.
  - Example: *.spectate Bob*
- `.rename (Old Name) (New Name)`
  - Renames a player. Original name will still show on map to clanmates.
  - Example: *.rename Bob Joe*
- `.revive (Player)`
  - will pick you up from being downed. If fully dead, sends player to coffin.
  - Example: *.revive Joe*
- `.gear [repair/break] (Player)`
  - will repair or break gear on a player (or self if no player specified)
  - Example: *.gear repair Joe* or *.gear break Joe*
  - Shortcut: *.gear r* or *.gear b*
- `.unlock (Player)`
  - will complete a player's journal, vbloods, abilities, waypoints and the map. Does not unlock DLCs. (Thats naughty)
  - Example: *.unlock Bob*
- `.fly (Player)`
  - will toggle flying on a player named, or the user if no one is named.																								
  - Example: *.fly Bob*
- `.flyup (Player)`
  - will move a player up in the air a level from the ground.
  - Example: *.flyup Bob*
- `.flydown (Player)`
  - will move a player down a level in the air.																								
  - Example: *.flyup Bob*
- `.flylevel (Player) (Level)`
  - will set a player's level to the flight level specified. Think of levels like floor heights.																								
  - Example: *.flylevel Bob 5*
- `.flyheight (Player) (Height)`
  - will set a player's fly height to the height specified.																								
  - Example: *.flyheight Bob 10*
- `.flyobstacleheight (Player) (Height)`
  - will set a player's fly obstacle height to the height specified. This is the amount of temporary height you gain when you collide into an obstacle.																								
  - Example: *.flyobstacleheight Bob 10*


### Castle/Clan Commands
- `.claim (Player) `
  - will change a castle heart owner to whomever is named. This will only work if you're on top of a heart, making it very apparent which heart you'll be changing.
- `.clan add (Player) “Clan Name”`
  - adds the player named to the clan named. Can exceed clan limitations through this method. (Kick WIP)
  - Example: *.clan add Joe “The Best Clan”*
  - Shortcut: *.c a*
- `.clan changerole (Player) (Role)`
  - Changes the role of a player in their clan. Roles: member, officer, leader
  - Example: *.clan changerole Joe 1*
  - Shortcut: *.c cr*
 
### Region/Map Commands
- `.incomingdecay`
  - will list 6 of the plots that are closest to decay.
  - Shortcut: *.incd*
- `.plotsowned (page)`
  - will list how many plots are owned per player, in descending order.
  - Example: *.plotsowned*
  - Shortcut: *.po 2*
- `.revealmap (Player)`
  - will reveal the map for a player named, or the user if no one is named.
  - Example: *.revealmap Bob*
- `.revealmapforallplayers`
  - will reveal the map for all players. New players will log in with a revealed map. Currently logged in players will have their map revealed upon relog. There is a config setting to turn this behavior back off.
- `.region lock (Region)`
  - will lock a region, preventing new players from entering it. Players already in the region will not be kicked but cannot reenter if they leave.
  - Example: *.region lock silverlighthills*
- `.region unlock (Region)`
  - will unlock a region, allowing new players to enter it.
  - Example: *.region unlock silverlighthills*
- `.region gate (Region) (level)`
  - will gate a region, preventing new players below the level threshold from entering it. Players already in the region will not be kicked but cannot reenter if they leave. It will keep track of the highest level a player has reached, providing accomodation for gear removal or "prestiging"
  - Example: *.region gate silverlighthills 60*
- `.region ungate (Region)`
  - will ungate a region, allowing all players of all levels to enter it.
  - Example: *.region ungate silverlighthills*
- `.region allow (Player)`
  - will allow a player to enter a locked or gated region.
  - Example: *.region allow Bob*
- `.region remove (Player)`
  - will remove a player from the allowed list for a locked or gated region.
  - Example: *.region remove Bob*
- `.region listplayers`
  - will list all players who are on the allow list to bypass a locked or gated region.
 
### Misc Commands	
- `.boss lock (boss)`
  - will stop a boss from spawning. When attempting to track said boss, it will say it is locked.
  - Example: *.boss lock solarus*
- `.boss unlock (boss)`
  - will allow a boss to spawn normally.
  - Example: *.boss unlock solarus*
- `.boss modify (bossname) (level)`
  - will change the level of the boss to the level specified. Upon respawn, they will be their original level. You can still modify the level of the boss while its in its 'blood walk', and it will spawn with that level. Must be near boss.
  - Example: *.boss modify solarus 100*
  - Shortcut: *.boss m*
- `.boss teleportto (name) (WhichOne)`
  - will teleport you to the boss specified. Must be near boss. If multiple bosses are up, you can specify which one to teleport to. Bosses must have been spawned in at least once on the map to be teleported to.
  - Example: *.boss teleportto TheNameOfTheBoss 1*
  - Shortcut: *.boss tt*
- `.forcerespawn (range)`
  - Sets the chain transition time for nearby spawn chains to now to force them to respawn if they can. Handy for growing plants without adding time.


## Player Accessible Commands:
- `.afk`
  - will put Zzzzz above character and lock wasd movement. It's a toggle.
- `.staff`
  - will list staff who are online.
- `.ping`
  - tells you your latency
- `.clan list (page #)`
  - Shows a list of populated clans (and their message). Newest clans are at the start of the list.
  - Example: *.clan list 1*
  - Shortcut: *.c l*
- `.clan members “Clan Name”`
  - Shows a list and ranking of players within a named clan. Use quotes around a clan name with any spaces.
  - Example: *.clan members “The Best Clan”*
  - Shortcut: *.c m*
- `.time`
  - will tell you the current server time
- `.openplots`
  - will report how many open or decaying plots there are in each region.
  - Shortcut: *.op*
- `.boss list`
  - will list all locked bosses
- `.region list`
  - will list all regions and their current lock or gated status.
- `.gear soulshardstatus`
  - will list the current status of soul shards on the server. (How many of each type that have been dropped, spawned, and whether they can drop or not)
  - Shortcut: *.gear sss*
- `.pace`
  - will slow your movementspeed to whatever NPC you are closest to. It will not speed you up past normal speed. Toggle to turn off.
