using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Code.Logic
{
    public class GameOverView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        public void Show() =>
           gameObject.SetActive(true);
    }
}
