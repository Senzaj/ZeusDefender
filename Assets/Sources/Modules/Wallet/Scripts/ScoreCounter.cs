using Sources.Modules.SpawnedObject.Scripts;
using TMPro;
using UnityEngine;

namespace Sources.Modules.Wallet.Scripts
{
    public class ScoreCounter : MonoBehaviour
    {
        [SerializeField] private ScoreWallet _scoreWallet;
        [SerializeField] private TMP_Text _text;
        
        private int _score;
        
        public int FixScore()
        {
            int score = _score;
            _scoreWallet.AddPoints(_score);
            ResetToZero();

            return score;
        }
        
        public void AddCoin(Coin coin)
        {
            coin.Taken += UpdateScore;
            coin.Skipped += RemoveCoin;
        }

        private void RemoveCoin(Coin coin)
        {
            coin.Taken -= UpdateScore;
            coin.Skipped -= RemoveCoin;
        }
        
        private void UpdateScore(Coin coin ,int scoreBoost)
        {
            _score += scoreBoost;
            _text.text = _score.ToString();
            RemoveCoin(coin);
        }

        private void UpdateScore(int scoreBoost)
        {
            _score += scoreBoost;
            _text.text = _score.ToString();
        }

        private void ResetToZero()
        {
            _score = 0;
            UpdateScore(0);
        }
    }
}
