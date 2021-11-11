# Project Design - Extreme Paintball
**Jake Haas, Efe Osah, Matthew Eppel, Aisha Ali**

- [Project Design - Extreme Paintball](#project-design---extreme-paintball)
  - [Design Document](#design-document)
    - [Game concept](#game-concept)
    - [Genre](#genre)
    - [Game flow](#game-flow)
    - [Features](#features)
    - [Game mechanics](#game-mechanics)
  - [Design Analysis](#design-analysis)
    - [The lense of surprise:](#the-lense-of-surprise)
    - [The lense of fun:](#the-lense-of-fun)
    - [The lense of curiosity:](#the-lense-of-curiosity)
    - [The lense of problem solving:](#the-lense-of-problem-solving)
  - [Machinations](#machinations)

## Design Document

### Game concept

Description: 
The main concept of our game is capture the flag. Two teams will be created, each team will have two players and a color; blue or red. A flag will be situated on each side of a symmetrical map in a defensible location. The goal of each team is to steal the other team’s flag and bring it back to their own flag. Each player will be equipped with a variety of weaponry in order to attack other players for offense and defence.

Style: 
Our game will be rendered in a cartoonish style and worlds will be built primarily in grayscale tones. As players use their paintball guns to fight each other, the world will become more and more painted with blue and red splotches, revealing a history of the battle.

Setting: 
The setting of Extreme Paintball is a world where paintball is the most popular sport in history. Epic battle arenas host professional paintball tournaments and are equipped with jump pads, vehicles, and paintball turrets. The game will take place inside these grand arenas.

Story overview: 
For decades, extreme paintball has been the top-ranked sport, driving massive investments and creating worldwide competition. Countries spend fortunes on producing the best athletes so that they can show their dominance on the world stage. Being a professional paintballer means honour, respect, money, and political power. Athletes bet it all to make their way to the top and become number one. In our game, you are a professional paintballer rising through the ranks and competing with other players around the world. 

Key elements: 
Some key elements of this game are the cash system, weapon system, and strategically placed pickups and control points in the environment. The cash system in our game will be responsible for balancing the weapons that the player is able to obtain. As players take out enemy players they will acquire a bounty. When a player with a bounty is defeated, their bounty will be collected by the player who defeated them. This money can then be used to purchase more powerful weapons. The weapon system in our game will feature a variety of weapons including a starting pistol, powerful Sniper, grenade, powerful shotgun, devastating machinegun, and indiscriminate rpg. The focal point of the weapon system is dual wielding, up to two weapons of any type can be held and used simultaneously. Accessories can also be equipped which present tradeoffs to suit each player’s playstyle. Our game will also feature a number of pickups and control points. Health and ammo boxes will be placed at strategic locations to provide advantage to players who control those areas.

How to play: 
When the player opens the game, they will be presented with a main menu screen with a join button and a host button. They will then contact three other people to play with and choose a host. The host will press the host button and be given a gameID, the other players will click join and input the gameID to join the game. Once the game begins, the players will be shuffled into two teams of two and given a color. Players will start with a basic pistol and will begin trying to capture the opposing team’s flag. The player will start in first person mode and be able to switch camera modes to third person and back again by pressing the ‘p’ key. The player can move using the ‘w’, ‘a’, ‘s’, and ‘d’ keys and look around by moving the mouse. The player can also jump by pressing the spacebar. Pickups can be collected simply by walking over them. The weapon shop can be accessed by pressing the ‘o’ key. They will then be presented with a list of weapons and their prices with a buy button next to each. When a weapon is bought it can be put into the character’s left or right hand. When a match is over, the next match will begin immediately and show the overall scores of each team.

How our game is set apart from others: 
Our game is set apart from other capture-the-flag games by its unique weapon system and storyline, as well as a number of unique features such as painting the battlefield throughout combat and dual-wielding guns.


### Genre

Our game is a mixture between many different genres, the primary category being shooter. Other genres include real-time strategy (RTS), multiplayer online battle arena (MOBA), and role playing game (RPG). Our game is primarily considered a shooter because a very skilled player could win the game through their shooter skills alone without developing an advanced strategy or taking full advantage of the arena. 


### Game flow

The game flow in our game is directed by three main elements in its design. The map layout is symmetrical which provides opportunities for the players to learn new strategies, control points are used to direct the player into battle, and the game economy ballences the game to give an advantage to less skilled players. 

When the players enter the game, they are presented with their flag and their teammate, this provides them with a clear understanding of where their defence point is and also where their enemy’s defence point is because of the symmetry of the map. Because the player is spawned next to their partner, it is easier for them to coordinate their attack and defence strategy without having to be familiar with the map. This starts each team off on good footing before the battle even begins. As the players move away from their spawning area, the layout of the map will direct them to the enemy base as well as familiarize them with the enemy base’s layout due to the map’s symmetry. As the players make contact with the enemy, they will discover the best defensive and offensive positions and be able to use those positions to their advantage on either side of the map. 

Players are attracted to certain routes in order to collect and control health and ammo pickup locations as well as control points which buff the team controlling them. This gives the controlling team a huge advantage against their opponent, but this makes room for flanking maneuvers on the lesser traveled routes through the map. Control points will switch sides very frequently because of the design of the game. If one player is defending and the other is attacking, then the attacker holding a control point will not help the team because they will not reach the enemy flag. This means that control points will be passed through on the way to the objective and only held until the attacker is defeated, at which point the other player would have to abandon defence in order to continue holding it. This only works because there are only two players on each team.

As the game progresses, the side with the most kills will have accumulated a higher bounty than the other side. The opposing side can take advantage of this by collecting the bounties of the more skilled players and buying more effective weaponry. This makes the game more fun for less skilled players because as the game progresses they will gain better weapons than their opponents and be able to push back against them and have a chance to win. This also provides the more skilled team with a greater challenge which is more motivating.


### Features

- Automatic paintball turrets  
- Paintball turrets will be an important control point in some of the maps.
By activating and controlling a turret, an extra level of defence is given to the team.
The turret will automatically attack any enemies within its range and can be taken over by enemies that manage to take back control of the control point.
- Paint assault vehicles
- Vehicles will spawn on many of the maps and act as a mobile source of cover and power.
- A variety of paint guns
  - Simple pistol, will be given to the player at the beginning of the game. Can be purchased from the shop again to dual wield.
  - Powerful shotgun
  - Powerful sniper
  - Devastating machinegun
  - Grenade
  - Indiscriminate RPG
- Bounty rewards
- Bounties will be accumulated by players who are on killstreaks, these bounties will be displayed on the jumbotron and will be collected by the player who ends their streak.
- Jumbotron, this jumbotron will display the current score as well as the bounties on each player’s head.
- Weapon store, the weapon store will be an element in the HUD of the player. When opened, it will list all the weapons that can be bought along with their price.
- Health pickups, health pickups will be placed at strategic locations in each map. Once they are picked up there will be a certain amount of time before they respawn.
- Ammo pickups, Ammo pickups will be placed at strategic locations in each map. Once they are picked up there will be a certain amount of time before they respawn.
- A number of control points around each arena. Control points are circles on the ground that must be stood in to be captured. Capturing a control point can have a variety of effects that benefit the holder.
  - Possible effect 1: A buff is given to the team which increases there damage, health, or speed.
  - Possible effect 2: An automatic turret is activated and targets enemies that are in its range.
  - Possible effect 3: A vehicle is spawned and given to the holder.
- Jump pads: Throws the player into the air when it is stepped on.
- Speed pads: Increases the player’s movement speed when it is stood on.
- Ragdoll physics. When a player is defeated, their character model will flop around with physics.
- Taunts. The player can activate a taunt through the GUI which plays a short animation to antagonise the enemy.
- Accessories. Accessories can be bought through the weapon store. Accessories provide tradeoffs between player damage, health, and speed to suit the player’s playstyle.
- Hand made arenas. Arenas will be developed by hand without the use of procedural generation, this means that every map will have the maximum attention to detail and planning put into it.

### Game mechanics

The core mechanics of our game are running, looking around, jumping, shooting, buying weapons, grabbing pickups and collecting flags.
The player runs at a constant rate when the player presses the ‘w’, ‘a’, ‘s’, and ‘d’ keys.
Their movement speed can be increased by running on top of a speed pad or buying an accessory from the gun store.
The player can also look around by moving their mouse in the x and y directions. The player can jump by pressing the spacebar, their jump height is constant.
The player can also fire their weapons with the left and right mouse buttons which correspond to each of the two weapons that can be held.
Pickups are grabbed automatically when the player runs over them. The enemy flag is also collected and deposited automatically when the player steps into it. 

## Design Analysis

### The lense of surprise:

There are a few ways that the player of our game can be surprised.
The primary way that players are likely to be surprised is by their opponents' attacks and strategies.
This falls under the category of player surprising each other rather than being surprised by the elements of the game.
Because our game aims to be a fair and balanced competitive game, there should not be many surprises to be found in the core mechanics of the game because this would feel unfair to the players.

The opponent’s attacks can be surprising to players in a number of ways.
One way that enemy attacks can be surprising is the strategy that is used by the enemy.
Because of the variety of guns available in our game, there is a large number of combinations of weaponry that the enemy can use.
Certain variations of weapons have advantages and disadvantages over others and will be used and countered differently.
For example, if an enemy is equipped with dual wielded shotguns they will try to play at close range and pop around corners,
whereas an enemy equipped with a sniper rifle and a rocket launcher will try to stay at range and avoid cramped corridors.

### The lense of fun:

Our game is made fun by making the player feel in control, challenged, and part of a team.
In addition to how the game makes players feel it also has fun elements in its design.
These elements include a fun aesthetic, satisfying victories, and rewards for achievement.
All these factors work together to make the game overall fun and enjoyable for the player.

The player is made to feel in control through the core mechanics of the game.
Everything from their movement, weapons, accessories, and attack strategies are chosen by the player and their effectiveness throughout the game depends on these choices as well as their skill.
The player is challenged while playing the game by competing against opposing players and devising effective strategies to defend while also attacking.
The player also has to think about which weapons to buy or save up for and which combinations will work best for their situation.
Each of these factors ties into being part of a team because the player must communicate effectively with their teammates throughout the game to be successful.

There are many fun elements built into the game as well, these include its aesthetics and rewards for victory which make the game more satisfying.
The player will be painting the battlefield with their weapons as they play which provides a fun aesthetic to the game.
The game also makes victories more satisfying with ragdoll physics.
The player is also rewarded for playing well because their bounties are posted on the jumbotron which show off their skills.

### The lense of curiosity:

Our game makes the player constantly question the position and strategy of their opponent. Some of the questions that are put into the player’s mind are; where is the enemy’s most vulnerable defence point, where is the enemy located in reference to my own location, should I go after the control point, what weapons would work best in this scenario, and how should I coordinate with my teammate to increase our chances of winning. 

The player cares about these questions because they are the key to victory. When a player discovers the answer to these questions, their chances of winning increase dramatically. The player can answer these questions through knowledge of the map, communicating effectively with their teammates, and scouting out the map to find the enemy players. By introducing the player to these various questions, it engages them and makes the game more interesting and fun to play.


The lense of endogenous value:
There are many elements in our game that the player feels are valuable, these elements include weapons, money, control points, and their team’s score. They feel that these elements are valuable because they give the player an edge against their opponents and contribute to their chances of winning.

Because our game is primarily a competition between two teams, it is fair to say that the players will see value in anything that increases their chances of winning. Acquiring better weapons is one of the key ways that a player can become stronger and gain an advantage over their opponent. Having better weapons makes protecting one’s own flag easier as well as making it easier to steal the other team’s flag and win. By extension to this, money has value because it can be exchanged for various weapons. Having more money means having better weapons, and better weapons lead to having a higher chance at winning. The player also sees control points as valuable because they give the player’s team a buff which gives them a better chance at winning. Ultimately, the most valuable items in the game are capture points and by extension the opponent’s flag. In order to win the game, the team needs to gain a certain number of points. They do this by collecting the enemy's flag and returning it to their own. Because value is based on an item’s ability to increase the chances of winning, it stands to reason that capture points would be the most valuable item that can be possessed in the game.



### The lense of problem solving:
There are many problems to solve in our game and they all branch off completing the main objective of capturing the enemies flag and winning the game. Some problems that the player has to solve include finding the best route to the enemy flag, finding the best defence strategy for the team, and finding the best strategy for offence.
	
Attacking and defending against the enemy is an extreme logistical challenge. There are many factors to consider when choosing a strategy that takes into account all the mechanics in the game and deals with the unknowns in the enemy’s strategy. Choosing a route to the opponent’s flag will require the player to analyse where the enemies are at that moment, where their teammate is, and what the shortest paths are to the enemy base. Another problem that must be solved is how defence will be balanced with offense to create a winning strategy. All these factors together make the game more interesting by providing many problems to solve. 

## Machinations

We’ve made a Machinations of the combat system in the game. Primarily focused on the PvP side of combat, named ‘exchanges’. There’s also some cash system, to be elaborated on later.

