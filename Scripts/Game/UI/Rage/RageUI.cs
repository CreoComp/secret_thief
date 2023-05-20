using Assets.Scripts.Game.Simulation.RageSystem;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class RageUI : MonoBehaviour
{
    private Slider _slider;
    private void Start()
    {
        _slider = GetComponent<Slider>();
    }
    private void Update()
    {
        _slider.value = RageScale.CurrentRage / RageScale.MAXIMUM_RAGE * _slider.maxValue;
    }

}
