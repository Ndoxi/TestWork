using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Button))]
public class AttackButton : MonoBehaviour
{
    [SerializeField]
    private PlayerAttackController.AttackType _attackType;
    [SerializeField]
    private Image _cover;

    private Button _button;
    private bool _coolingdown;
    private SceneManager _sceneManager;
    private PlayerAttackService _attackService;

    [Inject]
    private void Construct(SceneManager sceneManager, PlayerAttackService attackService)
    {
        _sceneManager = sceneManager;
        _attackService = attackService;
    }

    private void Awake()
    {
        if (_button == null)
            _button = GetComponent<Button>();

        _cover.enabled = false;
        _button.onClick.AddListener(Press);
    }

    private void OnEnable()
    {
        Attack attack = _attackService.Get(_attackType);
        attack.OnEnable += OnEnabled;
        attack.OnDisable += OnDisabled;
        attack.OnPerformed += OnPerformed;
    }

    private void OnDisable()
    {
        Attack attack = _attackService.Get(_attackType);
        attack.OnEnable -= OnEnabled;
        attack.OnDisable -= OnDisabled;
        attack.OnPerformed -= OnPerformed;
    }

    private void Press()
    {
        _sceneManager.Player.PerformAttack(_attackType);
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
