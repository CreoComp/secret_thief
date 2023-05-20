using System;
using UnityEngine;

namespace Assets.Scripts.Money
{
    public class MoneyStorage
    {
        public static event Action<int> MoneyAmountChanged;
        private const string MONEY_KEY = "Money";
        private const int DEFAULT_MONEY_AMOUNT = 0;

        public int GetMoney()
        {
            int amount = DEFAULT_MONEY_AMOUNT;
            if (PlayerPrefs.HasKey(MONEY_KEY))
                amount = PlayerPrefs.GetInt(MONEY_KEY);
            return amount;
        }

        public void AddMoney(int moneyToAdd)
        {
            if (IsValidTransaction(moneyToAdd) == false)
                throw new ArgumentException($"Невозможная транзакция:\ncurrent: {GetMoney()}\nToAdd: {moneyToAdd}");
            int currentAmount = GetMoney();
            int newAmount = currentAmount + moneyToAdd;
            SaveMoneyAmount(newAmount);
            MoneyAmountChanged?.Invoke(newAmount);
        }

        private void SaveMoneyAmount(int amount)
        {
            if (!IsValidMoneyAmount(amount))
                throw new ArgumentException($"Невозможно сохранить значение \"{amount}\" для монет");
            PlayerPrefs.SetInt(MONEY_KEY, amount);
        }

        public bool IsValidTransaction(int moneyToAdd) 
            => IsValidMoneyAmount(GetMoney() + moneyToAdd);
    
        public bool IsValidMoneyAmount(int amount) 
            => amount >= 0;
    }
}
