using System;
using System.Collections;
using System.Collections.Generic;
using CardMatch.Core;
using CardMatch.Data;
using CardMatch.Editor;
using CardMatch.Gameplay;
using CardMatch.Sound;
using UnityEngine;
using UnityEngine.UI;


namespace CardMatch.UI {
    public class CardCell : MonoBehaviour {
        [SerializeField] private CardAnimation _animation;
        [SerializeField] private Image _imgCardFace;
        [SerializeField] private Button _btn;
        
        [SerializeField, ReadOnly] private CardState _state;
        [SerializeField, ReadOnly] private int _card;
        
        public void Initialize(int card, Sprite face) {
            _card = card;
            _imgCardFace.sprite = face;
            _state = CardState.Hidden;
            _animation.UpdateView(_state);
            _btn.onClick.AddListener(Show);
        }

        public int Card() {
            return _card;
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
                GameManager.Instance().SelectCardCell(this);
            });
            _animation.PlayScaleAnimation(true);
            SfxManager.Instance().PlaySfx(SfxID.CARD_FLIP);
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
    
    


