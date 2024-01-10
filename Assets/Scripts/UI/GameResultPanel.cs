using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameResultPanel : MonoBehaviour
{
    [SerializeField]
    private Text _victoryText;
    [SerializeField]
    private Text _defeatText;

    private SceneManager _sceneManager;

    [Inject]
    private void Construct(SceneManager sceneManager)
    {
        _sceneManager = sceneManager;
    }

    private void OnEnable()
    {
        _sceneManager.OnGameEnd += OnGameEndCallback;
    }

    private void OnDisable()
    {
        _sceneManager.OnGameEnd -= OnGameEndCallback;
    }

    private void OnGameEndCallback(bool won)
    {
        if (won)
            _victoryText.gameObject.SetActive(true);
        else
            _defeatText.gameObject.SetActive(true);
    }
}
