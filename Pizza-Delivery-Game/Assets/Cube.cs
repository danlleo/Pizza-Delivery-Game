using UI.MainMenu;
using UnityEngine;

public class Cube : MonoBehaviour
{
    private float _targetScale = 1f;
    private Vector3 _scaleVel;
    private Quaternion _targetRotation;

    private void OnEnable()
    {
        MainMenuScreen.OnScaleChanged += MainMenuScreen_OnScaleChanged;
        MainMenuScreen.OnSpinClicked += MainMenuScreen_OnSpinClicked;
    }
    
    private void OnDisable()
    {
        MainMenuScreen.OnScaleChanged -= MainMenuScreen_OnScaleChanged;
        MainMenuScreen.OnSpinClicked -= MainMenuScreen_OnSpinClicked;
    }

    private void MainMenuScreen_OnSpinClicked()
    {
        _targetRotation = transform.rotation * Quaternion.Euler(Random.insideUnitSphere * 360f);
    }

    private void MainMenuScreen_OnScaleChanged(float newScale)
    {
        _targetScale = newScale;
    }

    private void Update()
    {
        transform.localScale =
            Vector3.SmoothDamp(transform.localScale, _targetScale * Vector3.one, ref _scaleVel, 0.15f);
        
        transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, Time.deltaTime * 5f);
    }
}
