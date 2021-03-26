Estimated time
3 days

Needed classes:

GameManager
*Start
**Create enemies

Counter(Timer) - before games starts

InputController
*OnTap
*OnSwipeLeft
*OnSwipeRight
*OnSwipeUp
*OnSwipeDown

EnemyData
-speed

EnemySystem
*RunTowardsPlayer

PlayerData
-speed
PlayerSystem
*RunForward
*Jump

ShooterData
-target
-precision
ShooterSystem
*Shoot
**PhysicsRaycast.SphereTrigger and get array of near enemies
** get closest enemy
** aim animation to closest enemy
** Instantiate bullet

BulletData
-speed
-damage
BulletSystem
*AddForceImpulse




