using System;
using System.Collections;
using CardMatch.Editor;
using UnityEngine;

namespace CardMatch.UI {
    public class CardAnimation : MonoBehaviour {
        [SerializeField] private GameObject _viewParent;
        [SerializeField] private GameObject _hiddenView;
        [SerializeField] private GameObject _revealedView;
        [SerializeField] private AnimationCurve _scaleCurve;
        [SerializeField] private GameObject _matchedMark;
        
        [SerializeField, ReadOnly] private bool _isFlipping;
        
        //   configs
        private const float FLIP_DURATION = 0.25f;
        private const float SCALE_DURATION = 0.1f;

        public bool IsAnimating() {
            return _isFlipping;
        }
        
        public void UpdateView(CardState state) {
            _hiddenView.SetActive(state == CardState.Hidden);
            _revealedView.SetActive(state == CardState.Revealed || state == CardState.Matched);
            _matchedMark.SetActive(state == CardState.Matched);
            
        }

        public void PlayFlipAnimation(bool isRevealed, Action onUpdate = null, Action onComplete = null) {
            float rotationY = isRevealed ? 180 : 0;
            StartCoroutine(DoFlip(rotationY, onUpdate, onComplete));
        }

        IEnumerator DoFlip(float rotationY, Action onUpdate, Action onComplete) {
            Quaternion startRotation = _viewParent.transform.rotation;
            Quaternion endRotation = Quaternion.Euler(0, rotationY, 0);
            float elapsedTime = 0;
            bool hasUpdated = false;
            _isFlipping= true;

            while (elapsedTime < FLIP_DURATION) {
                elapsedTime += Time.deltaTime;
                _viewParent.transform.rotation = Quaternion.Slerp(startRotation, endRotation, elapsedTime / FLIP_DURATION);
                if (elapsedTime >= FLIP_DURATION / 2 && !hasUpdated) {
                    onUpdate?.Invoke();
                    hasUpdated = true;
                }
                yield return null;
            }
            
            _viewParent.transform.rotation = endRotation;
            _isFlipping = false;
            onComplete?.Invoke();
        }

        public void PlayScaleAnimation(bool scaleUp, Action onComplete = null) {
            float scale = scaleUp ? 1.15f : 1;
            StartCoroutine(DoScale(scale, onComplete));
        }

        IEnumerator DoScale(float scale, Action onComplete) {
            float startScale = transform.localScale.x;
            float elapsedTime = 0;

            while (elapsedTime < SCALE_DURATION) {
                elapsedTime += Time.deltaTime;
                float curveValue = _scaleCurve.Evaluate(elapsedTime / SCALE_DURATION);
                transform.localScale = Vector3.one * Mathf.LerpUnclamped(startScale, scale, curveValue);
                yield return null;
            }

            transform.localScale = Vector3.one * scale;
            onComplete?.Invoke();
        }
    }
}