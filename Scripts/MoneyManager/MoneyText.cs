using Assets.Scripts.Money;
using UnityEngine;
using UnityEngine.UI;

public class MoneyText : MonoBehaviour
{
    void Awake()
    {
        MoneyStorage.MoneyAmountChanged += OnMoneyChanged;
    }
    private void OnDestroy()
    {
        MoneyStorage.MoneyAmountChanged -= OnMoneyChanged;

    }

    private void OnMoneyChanged(int moneyAmount)
    {
        GetComponent<Text>().text = moneyAmount + "";
    }
}
