Estimated time
3 days

Values to tweak
{
	*in gameobjects:

	**LevelBuilder
	*** noOfLevelComponents or no of blocks a Level should have, the more blocks there are, the longer the level
	*** generateLevel - to create a Level

	**Player
	*** speed

	**EnemiesController
	***noOfEnemies to spawn at the start of the game
	***NavMeshAgent here you can tweak speed, rotation speed, and many other agent properties

	*in folders:
	**Resources
	***Enemy
	***Player
	***Bullet

	**Resources\LevelParts
	***first prefab is preceded by 0 to know with what prefab will the level start building
	***second prefab is preceded by 1 to know with what prefab will the level finish building 
	***rest of prefabs will randomly by added to levelCreation
}




Classes structure in short:

GameManager
*Start
*Manage game play

Counter(Timer) - before games starts

InputController
*OnTap
*OnSwipeLeft
*OnSwipeRight
*OnSwipeUp
*OnSwipeDown

EnemiesController
*Manage enemies creation and movement

Enemy
-speed
*RunTowardsPlayer

Player
-speed
*RunForward
*Jump
*AimAtClosestEnemy
**PhysicsRaycast.SphereTrigger and get array of near enemies
** get closest enemy
** aim animation to closest enemy
** Instantiate bullet

Weapon
-spawnPoint
-maxNoOfBullets
*Shoot

Bullet
-speed
*AddForceImpulse




