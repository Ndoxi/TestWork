using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class WavesDisplay : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _textMesh;

    private SceneManager _sceneManager;

    [Inject]
    private void Construct(SceneManager sceneManager)
    {
        _sceneManager = sceneManager;
    }

    private void OnEnable()
    {
        _sceneManager.UpdateWaveInfo += UpdateWavesDisplay;
    }

    private void OnDisable()
    {
        _sceneManager.UpdateWaveInfo -= UpdateWavesDisplay;
    }

    private void UpdateWavesDisplay(int total, int current)
    {
        _textMesh.text = $"Total waves: {total} <br>Current: {current}";
    }
}
