using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class AttackButton : MonoBehaviour
{
    [SerializeField]
    private PlayerAttackController.AttackType _attackType;
    [SerializeField]
    private Image _cover;

    private Button _button;
    private bool _coolingdown;

    private void Awake()
    {
        if (_button == null)
            _button = GetComponent<Button>();

        _cover.enabled = false;
        _button.onClick.AddListener(Press);
    }

    private void OnEnable()
    {
        Attack attack = PlayerAttackService.Get(_attackType);
        attack.OnEnable += OnEnabled;
        attack.OnDisable += OnDisabled;
        attack.OnPerformed += OnPerformed;
    }

    private void OnDisable()
    {
        Attack attack = PlayerAttackService.Get(_attackType);
        attack.OnEnable -= OnEnabled;
        attack.OnDisable -= OnDisabled;
        attack.OnPerformed -= OnPerformed;
    }

    private void Press()
    {
        SceneManager.Instance.Player.PerformAttack(_attackType);
    }

    private void OnEnabled()
    {
        if (_coolingdown)
            return;

        _button.interactable = true;
    }

    private void OnDisabled()
    {
        if (_coolingdown)
            return;

        _button.interactable = false;
    }

    private void OnPerformed(float cdDuration)
    {
        StartCoroutine(CooldownAnimation(cdDuration));
    }

    private IEnumerator CooldownAnimation(float cdDuration)
    {
        _coolingdown = true;
        _button.interactable = false;
        _cover.enabled = true;
        float timeLeft = cdDuration;

        while (timeLeft > 0)
        {
            _cover.fillAmount = 1 - timeLeft / cdDuration;
            yield return new WaitForEndOfFrame();
            timeLeft -= Time.deltaTime;
        }
                
        _cover.enabled = false;
        _button.interactable = true;
        _coolingdown = false;
    }
}
