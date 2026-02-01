using System.Collections.Generic;
using Enemy;
using Player;
using UnityEngine;
using Utilities;

namespace Managers
{
    public class LevelManager : Singleton<LevelManager>
    {
        public GameObject playerObject;
        public PlayerBehaviour playerBehaviour;
        public Vector2 playerPosition => playerBehaviour.transform.position;

        /// <summary>
        /// List of shooters with a shot intersecting the moneybag. If non-empty, player cannot collect
        /// the moneybag.
        /// </summary>
        public readonly HashSet<ShooterBehaviour> shootersHittingMoneybag = new();
    }
}