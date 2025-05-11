using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using DG.Tweening; 

public class HealthController : MonoBehaviour
{
    private float _maxHealth = 100f; // Maximum health value
    private float _currentHealth; // Current health value
    [SerializeField] private Image _healthBarFill; // Reference to the health bar UI element
    [SerializeField] private GameController _gameController; // Reference to the GameController script
    [SerializeField] private float _damageAmount, _healthAmount; // Amount of damage to be taken
    [SerializeField] private Transform _healthBarTransform; // Reference to the health bar transform
    private Camera _camera;
    [SerializeField] private float _fillspeed = 0.5f; // Speed of the fill animation
    [SerializeField] private Gradient _colorGradient;

    private void Awake()
    {
        _currentHealth = _maxHealth; 
        _camera = Camera.main; 
    }
    private void Update(){
        _healthBarTransform.rotation = _camera.transform.rotation; 
    }

     private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            TakeDamage(_damageAmount); 
        }
        else if (collision.CompareTag("Health"))
        {
            Heal(_healthAmount); 
            collision.gameObject.SetActive(false); 
        }
    }

    private void TakeDamage(float amount)
    {
        _currentHealth -= amount; 
        _currentHealth= Mathf.Clamp(_currentHealth, 0, _maxHealth); 
        if (_currentHealth == 0 && _gameController != null)
        {
            _gameController.Die(); 
            _currentHealth = _maxHealth; 
        }
        UpdateHealthBar(); 
    }
    private void Heal(float amount)
    {
        _currentHealth += amount; 
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth); 
        UpdateHealthBar(); 
    }
    private void UpdateHealthBar()
    {
        float targetFillAmount = _currentHealth / _maxHealth; // Calculate the target fill amount based on current health
       // _healthBarFill.fillAmount = _currentHealth / _maxHealth;
        _healthBarFill.DOFillAmount(targetFillAmount, _fillspeed); 
        _healthBarFill.DOColor(_colorGradient.Evaluate(targetFillAmount), _fillspeed); // Change color based on health
    }

}
