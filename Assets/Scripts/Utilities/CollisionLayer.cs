using System;

namespace Utilities
{
    [Flags]
    public enum CollisionLayer
    {
        Default = 1 << 0,
        TransparentFX = 1 << 1,
        IgnoreRaycast = 1 << 2,
        Enemy = 1 << 3,
        Water = 1 << 4,
        UI = 1 << 5,
        Player = 1 << 6,
        Obstacle = 1 << 7,
        Moneybag = 1 << 8
    }

    public static class CollisionAssistant
    {
        /// <summary>
        /// An NPC or a player
        /// </summary>
        public const CollisionLayer Alive = CollisionLayer.Player | CollisionLayer.Enemy;
        
        /// <summary>
        /// What can be "detected" by an enemy
        /// </summary>
        public const CollisionLayer VisibleToEnemy = CollisionLayer.Player | CollisionLayer.Obstacle | CollisionLayer.Enemy;
    }
}