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
    public class WaveNumberView : MonoBehaviour
    {
        [SerializeField] TMP_Text _text;
        private string _wave = "Wave ";

        public void SetValue(int count) =>
            _text.text = _wave + count.ToString();

        public IEnumerator WaitForDisableRoutine()
        {
            yield return new WaitForSeconds(3);
            
            gameObject.SetActive(false);
        }

    }
}
