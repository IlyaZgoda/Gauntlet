using System;
using UnityEngine;

namespace Code.StaticData.SceneManagement.Spawners
{
    [Serializable]
    public class HeroSpawnerStaticData
    {
        public Vector3 Position;

        public HeroSpawnerStaticData(Vector3 position) =>
            Position = position;
    }
}
