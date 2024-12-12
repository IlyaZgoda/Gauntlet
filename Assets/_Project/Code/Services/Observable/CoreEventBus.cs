using System;

namespace Code.Services.Observable
{
    public class CoreEventBus
    {
        public Action<float> PlayerHealthChanged;
        public Action EnemyDied;
        public Action<int> WaveSpawned;
        public Action PlayerDied;
    }
}
