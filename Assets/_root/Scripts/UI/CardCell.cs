using System;
using System.Collections;
using System.Collections.Generic;
using CardMatch.Core;
using CardMatch.Data;
using CardMatch.Editor;
using CardMatch.Gameplay;
using UnityEngine;
using UnityEngine.UI;


namespace CardMatch.UI {
    public class CardCell : MonoBehaviour {
        [SerializeField] private CardAnimation _animation;
        [SerializeField] private Image _imgCardFace;
        [SerializeField] private Button _btn;
        
        [SerializeField, ReadOnly] private CardState _state;
        [SerializeField, ReadOnly] private int _id;
        
        public void Initialize(Card card) {
            _id = card.ID;
            _imgCardFace.sprite = card.Face;
            _state = CardState.Hidden;
            _animation.UpdateView(_state);
            _btn.onClick.AddListener(Show);
        }

        public int ID() {
            return _id;
        }
        
        public void Leak(float leakingDuration) {
            _state = CardState.Revealed;
            _animation.PlayFlipAnimation(true, () => {
                _animation.UpdateView(_state);
            }, () => {
                StartCoroutine(DoLeak(leakingDuration));
            });
        }

        IEnumerator DoLeak(float leakingDuration) {
            yield return Common.GetWait(leakingDuration);
            _state = CardState.Hidden;
            _animation.PlayFlipAnimation(false, () => {
                _animation.UpdateView(_state);
            });
        }

        public void Show() {
            if (_animation.IsAnimating() || _state != CardState.Hidden) return;

            _state = CardState.Revealed;
            _animation.PlayFlipAnimation(true, () => {
                _animation.UpdateView(_state);
            }, () => {
                GameManager.Instance().SelectCard(this);
            });
            _animation.PlayScaleAnimation(true);
        }
        
        public void Hide() {
            _state = CardState.Hidden;
            _animation.PlayFlipAnimation(false, () => {
                _animation.UpdateView(_state);
            });
            _animation.PlayScaleAnimation(false);
        }
        
        public void Match() {
            _state = CardState.Matched;
            _animation.PlayScaleAnimation(false, () => {
                _animation.UpdateView(_state);
            });
            _animation.PlayScaleAnimation(false);
        }
    }
    
    public enum CardState {
        Hidden,
        Revealed,
        Matched
    }
}
    
    


