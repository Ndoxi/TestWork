using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPanel : MonoBehaviour
{
    [SerializeField]
    private AttackButton[] _buttons;

    private void Start()
    {
        foreach (var button in _buttons)
        {
            button.gameObject.SetActive(true);
        }
    }
}
