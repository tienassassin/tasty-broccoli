using System;
using System.Collections;
using System.Collections.Generic;
using CardMatch.Data;
using CardMatch.Editor;
using CardMatch.Gameplay;
using UnityEngine;
using UnityEngine.UI;


namespace CardMatch.UI {
    public class CardCell : MonoBehaviour {
        [SerializeField] private Image _imgFace;
        [SerializeField] private Transform _viewParent;
        [SerializeField] private GameObject[] _stateViews;
        [SerializeField] private Button _btn;
        [SerializeField] private AnimationCurve _scaleCurve;
        [SerializeField, ReadOnly] private CardState _state;
        [SerializeField, ReadOnly] private int _id;
        [SerializeField, ReadOnly] private bool _isFlipping;
        
        //   configs
        private const float FLIP_DURATION = 0.25f;
        private const float SCALE_DURATION = 0.1f;
        
        public void Initialize(Card card) {
            _id = card.ID;
            _imgFace.sprite = card.Face;
            _state = CardState.Hidden;
            _btn.onClick.RemoveAllListeners();
            _btn.onClick.AddListener(OnSelected);
            UpdateView();
        }

        public int ID() {
            return _id;
        }

        public void HideCard() {
            _state = CardState.Hidden;
            PlayTransition();
            Rescale();
        }
        
        public void OnMatched() {
            _viewParent.gameObject.SetActive(false);
        }

        private void OnSelected() {
            if (_isFlipping || _state != CardState.Hidden) return;
            
            _state = (CardState)(((int)_state + 1) % 2);
            PlayTransition(() => {
                GameManager.Instance().SelectCard(this);
            });
            Rescale();
        }
        
        private void UpdateView() {
            for (int i = 0; i < _stateViews.Length; i++) {
                _stateViews[i].SetActive(i == (int)_state);
            }
        }
        
        private void PlayTransition(Action onComplete = null) {
            float rotationY = _state == CardState.Revealed ? 180 : 0;
            StartCoroutine(DoFlip(rotationY, onComplete));
        }

        IEnumerator DoFlip(float rotationY, Action onComplete) {
            Quaternion startRotation = _viewParent.rotation;
            Quaternion endRotation = Quaternion.Euler(0, rotationY, 0);
            float elapsedTime = 0;
            bool hasUpdated = false;
            _isFlipping= true;

            while (elapsedTime < FLIP_DURATION) {
                elapsedTime += Time.deltaTime;
                _viewParent.rotation = Quaternion.Slerp(startRotation, endRotation, elapsedTime / FLIP_DURATION);
                if (elapsedTime >= FLIP_DURATION / 2 && !hasUpdated) {
                    UpdateView();
                    hasUpdated = true;
                }
                yield return null;
            }
            
            _viewParent.rotation = endRotation;
            _isFlipping = false;
            onComplete?.Invoke();
        }

        private void Rescale() {
            float scale = _state == CardState.Revealed ? 1.15f : 1;
            StartCoroutine(DoScale(scale));
        }

        IEnumerator DoScale(float scale) {
            float startScale = transform.localScale.x;
            float elapsedTime = 0;

            while (elapsedTime < SCALE_DURATION) {
                elapsedTime += Time.deltaTime;
                float curveValue = _scaleCurve.Evaluate(elapsedTime / SCALE_DURATION);
                transform.localScale = Vector3.one * Mathf.LerpUnclamped(startScale, scale, curveValue);
                yield return null;
            }

            transform.localScale = Vector3.one * scale;
        }
    }
    
    public enum CardState {
        Hidden,
        Revealed,
        Matched
    }
}
    
    


