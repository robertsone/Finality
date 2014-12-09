using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Asteroid_Belt_Assault
{
    class CollisionManager
    {
        private AsteroidManager asteroidManager;
        private PlayerManager playerManager;
        private EnemyManager enemyManager;
        private ExplosionManager explosionManager;
        private Vector2 offScreen = new Vector2(-500, -500);
        private Vector2 shotToAsteroidImpact = new Vector2(0, -20);
        private int enemyPointValue = 100;
        public int killcounter = 0;
        public Game1 game;
        public CollisionManager(
            Game1 game,
            AsteroidManager asteroidManager,
            PlayerManager playerManager,
            EnemyManager enemyManager,
            ExplosionManager explosionManager)

        {
            this.game = game;
            this.asteroidManager = asteroidManager;
            this.playerManager = playerManager;
            this.enemyManager = enemyManager;
            this.explosionManager = explosionManager;
            this.killcounter = 0;
        }

        private void checkShotToEnemyCollisions()
        {
            foreach (Sprite shot in playerManager.PlayerShotManager.Shots)
            {
                foreach (Enemy enemy in enemyManager.Enemies)
                {
                    if (shot.IsCircleColliding(
                        enemy.EnemySprite.Center,
                        enemy.EnemySprite.CollisionRadius))
                    {
                        if (game.getplanet() != "SUN")
                        {
                            shot.Location = offScreen;
                        }
                        enemy.Destroyed = true;
                        EffectManager.Effect("BasicExplosion").Trigger(enemy.EnemySprite.Center);
                        EffectManager.Effect("StarFireImpact").Trigger(enemy.EnemySprite.Center); EffectManager.Effect("StarFireImpact").Trigger(enemy.EnemySprite.Center); EffectManager.Effect("StarFireImpact").Trigger(enemy.EnemySprite.Center); EffectManager.Effect("StarFireImpact").Trigger(enemy.EnemySprite.Center); EffectManager.Effect("StarFireImpact").Trigger(enemy.EnemySprite.Center); EffectManager.Effect("StarFireImpact").Trigger(enemy.EnemySprite.Center); EffectManager.Effect("StarFireImpact").Trigger(enemy.EnemySprite.Center); EffectManager.Effect("StarFireImpact").Trigger(enemy.EnemySprite.Center); EffectManager.Effect("StarFireImpact").Trigger(enemy.EnemySprite.Center); EffectManager.Effect("StarFireImpact").Trigger(enemy.EnemySprite.Center); EffectManager.Effect("StarFireImpact").Trigger(enemy.EnemySprite.Center); EffectManager.Effect("StarFireImpact").Trigger(enemy.EnemySprite.Center); EffectManager.Effect("StarFireImpact").Trigger(enemy.EnemySprite.Center);
                        EffectManager.Effect("StarFireImpact").Trigger(enemy.EnemySprite.Center);
                        killcounter++;
                        playerManager.PlayerScore += enemyPointValue;
                        explosionManager.AddExplosion(
                            enemy.EnemySprite.Center,
                            enemy.EnemySprite.Velocity / 10);
                    }

                }
            }
        }

        private void checkShotToAsteroidCollisions()
        {
            foreach (Sprite shot in playerManager.PlayerShotManager.Shots)
            {
                EffectManager.Effect("ShieldsUp").Trigger(shot.Center);
                EffectManager.Effect("Enemy Cannon Fire").Trigger(shot.Center);
                EffectManager.Effect("PulseTracker").Trigger(shot.Center);
                EffectManager.Effect("MeteroidCollision").Trigger(shot.Center);
                for (int i=0;i<asteroidManager.Asteroids.Count;i++)
                {
                    if (shot.IsCircleColliding(
                        asteroidManager.Asteroids[i].Center,
                        asteroidManager.Asteroids[i].CollisionRadius))
                    {

                        SoundManager.PlayExplosion(); SoundManager.PlayExplosion();
                        EffectManager.Effect("BasicExplosion").Trigger(asteroidManager.Asteroids[i].Center);
                        EffectManager.Effect("MeteroidExplode").Trigger(asteroidManager.Asteroids[i].Center);
                        EffectManager.Effect("BasicExplosionWithHalo").Trigger(asteroidManager.Asteroids[i].Center);
                        shot.Location = offScreen;
                        asteroidManager.Asteroids.Remove(asteroidManager.Asteroids[i]);
                    }
                }
            }
        }

        private void checkShotToPlayerCollisions()
        {
            foreach (Sprite shot in enemyManager.EnemyShotManager.Shots)
            {
                EffectManager.Effect("Ship Cannon Fire").Trigger(shot.Center);
                EffectManager.Effect("Ship Cannon Fire").Trigger(shot.Center);
                if (shot.IsCircleColliding(
                    playerManager.playerSprite.Center,
                    playerManager.playerSprite.CollisionRadius))
                {
                    EffectManager.Effect("BasicExplosionWithHalo").Trigger(playerManager.playerSprite.Center);
                    shot.Location = offScreen;
                    playerManager.Destroyed = true;
                    
                    explosionManager.AddExplosion(
                        playerManager.playerSprite.Center,
                        Vector2.Zero);
                }
            }
        }

        private void checkEnemyToPlayerCollisions()
        {
            foreach (Enemy enemy in enemyManager.Enemies)
            {
                if (enemy.EnemySprite.IsCircleColliding(
                    playerManager.playerSprite.Center,
                    playerManager.playerSprite.CollisionRadius))
                {
                    enemy.Destroyed = true;
                    EffectManager.Effect("BasicExplosion").Trigger(enemy.EnemySprite.Center);
                    explosionManager.AddExplosion(
                        enemy.EnemySprite.Center,
                        enemy.EnemySprite.Velocity / 10);

                    playerManager.Destroyed = true;

                    explosionManager.AddExplosion(
                        playerManager.playerSprite.Center,
                        Vector2.Zero);
                }
            }
        }

        private void checkAsteroidToPlayerCollisions()
        {
            foreach (Sprite asteroid in asteroidManager.Asteroids)
            {
                if (asteroid.IsCircleColliding(
                    playerManager.playerSprite.Center,
                    playerManager.playerSprite.CollisionRadius))
                {
                    EffectManager.Effect("BasicExplosion").Trigger(playerManager.playerSprite.Center);
                    explosionManager.AddExplosion(
                        asteroid.Center,
                        asteroid.Velocity / 10);

                    asteroid.Location = offScreen;

                    playerManager.Destroyed = true;
                    explosionManager.AddExplosion(
                        playerManager.playerSprite.Center,
                        Vector2.Zero);
                }
            }
        }

        public void CheckCollisions()
        {
            checkShotToEnemyCollisions();
            checkShotToAsteroidCollisions();
            if (!playerManager.Destroyed)
            {
                checkShotToPlayerCollisions();
                checkEnemyToPlayerCollisions();
                checkAsteroidToPlayerCollisions();
            }
        }

    }
}
