using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Code.Logic
{
    public class HealthPack : MonoBehaviour, IHealthPack
    {
        public int HealingAmount => 50;
    }
}
