using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Code.StaticData.SceneManagement.Spawners
{
    [Serializable]
    public class EnemySpawnerStaticData
    {
        public Vector3 Position;

        public EnemySpawnerStaticData(Vector3 position) =>
            Position = position;
    }
}
