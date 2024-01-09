using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WavesDisplay : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _textMesh;

    private void OnEnable()
    {
        SceneManager.Instance.UpdateWaveInfo += UpdateWavesDisplay;
    }

    private void OnDisable()
    {
        SceneManager.Instance.UpdateWaveInfo -= UpdateWavesDisplay;
    }

    private void UpdateWavesDisplay(int total, int current)
    {
        _textMesh.text = $"Total waves: {total} <br>Current: {current}";
    }
}
