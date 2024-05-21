using System.Collections;
using System.Collections.Generic;
using CardMatch.Editor;
using UnityEngine;
using UnityEngine.UI;


namespace CardMatch.UI {
    public class Card : MonoBehaviour {
        [SerializeField] private Image _imgFace;
        [SerializeField] private GameObject[] _stateViews;
        [SerializeField] private Button _btn;
        [SerializeField, ReadOnly] private CardState _state;
        
        public void Initialize(Sprite face) {
            _imgFace.sprite = face;
            _state = CardState.FaceDown;
            UpdateView();
            _btn.onClick.RemoveAllListeners();
            _btn.onClick.AddListener(OnSelected);
        }

        private void OnSelected() {
            _state = (CardState)(((int)_state + 1) % 2);
            UpdateView();
        }
        
        private void UpdateView() {
            for (int i = 0; i < _stateViews.Length; i++) {
                _stateViews[i].SetActive(i == (int)_state);
            }
        }
    }
    
    public enum CardState {
        FaceDown,
        FaceUp
    }
}
    
    


