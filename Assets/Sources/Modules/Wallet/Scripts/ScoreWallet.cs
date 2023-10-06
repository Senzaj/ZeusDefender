using System;
using TMPro;
using UnityEngine;

namespace Sources.Modules.Wallet.Scripts
{
    public class ScoreWallet : MonoBehaviour
    {
        [SerializeField] private TMP_Text[] _balanceTexts;

        public event Action<int> BalanceChanged;
        public event Action OnStarted; 

        private int _balance;

        public void Init()
        {
            UpdateText();
            OnStarted?.Invoke();
        }

        public void SetPoints(int points)
        {
            _balance = points;
            UpdateText();
            BalanceChanged?.Invoke(_balance);
        }
        
        public void AddPoints(int points)
        {
            _balance += points;
            UpdateText();
            BalanceChanged?.Invoke(_balance);
        }

        public bool IsPointsEnough(int points)
        {
            return points <= _balance;
        }
        
        public void RemovePoints(int points)
        {
            _balance -= points;
            UpdateText();
            BalanceChanged?.Invoke(_balance);
        }
        
        private void UpdateText()
        {
            foreach (TMP_Text text in _balanceTexts)
                text.text = _balance.ToString();
        }
    }
}
