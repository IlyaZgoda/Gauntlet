using System;

namespace Code.Services.Observable
{
    public class CoreEventBus
    {
        public Action<float> PlayerHealthChanged;
        public Action EnemyDied;
    }
}
