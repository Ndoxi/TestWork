using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AttackPanel : MonoBehaviour
{
    [SerializeField]
    private AttackButton[] _buttons;

    private SceneManager _sceneManager;

    [Inject]
    private void Construct(SceneManager sceneManager)
    {
        _sceneManager = sceneManager;
    }

    private void OnEnable()
    {
        _sceneManager.OnPlayerAdded += InitializeButtons;
    }

    private void OnDisable()
    {
        _sceneManager.OnPlayerAdded -= InitializeButtons;
    }

    private void InitializeButtons()
    {
        foreach (var button in _buttons)
        {
            button.gameObject.SetActive(true);
        }
    }
}
