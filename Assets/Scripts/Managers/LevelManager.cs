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
    }
}