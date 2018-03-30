using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumbersRL.Input
{
    public class DelayedInputManager
    {

        public const int DefaultFirstWaitTime = 22;
        public const int DefaultContinuousWaitTime = 2;

        private event EventHandler<InputEventArgs> _inputFireEvent;

        public event EventHandler<InputEventArgs> InputFireEvent {
            add => _inputFireEvent += value;
            remove => _inputFireEvent -= value;
        }

        private int _firstWaitTime = DefaultFirstWaitTime;
        private int _continuousWaitTime = DefaultContinuousWaitTime;

        public int FirstWaitTime {
            get => _firstWaitTime;
            set {
                if (value < 0) throw new ArgumentException("The first wait time value must be greater than or equal to 0.");
                _firstWaitTime = value;
            }
        }

        public int ContinuousWaitTime {
            get => _continuousWaitTime;
            set {
                if (value < 0) throw new ArgumentException("The continuous wait time value must be greater than or equal to 0.");
            }
        }

        private int _timer = 0;

        private KeyboardState _lastState;
        private Keys _lastKey = Keys.None;

        private Keys[] _currentKeys = new Keys[0];
        
        private bool CanFireEvent => _timer <= 0;

        public void Update(KeyboardState keyboardState)
        {
            DecrementTimer();

            _currentKeys = keyboardState.GetPressedKeys();

            Keys currentKey = _currentKeys.Length > 1 ? DifferenceBetween(_lastState.GetPressedKeys(), _currentKeys) : _currentKeys.Length > 0 ? _currentKeys[0] : Keys.None;

            if (_lastKey == Keys.None)
            {
                if (currentKey != Keys.None && CanFireEvent) // First key press.
                {
                    _timer = _firstWaitTime;
                    FireEvent(currentKey);
                }
            }
            else
            {
                if (_lastKey == currentKey && CanFireEvent) // Continuous key press.
                {
                    _timer = _continuousWaitTime;
                    FireEvent(currentKey);
                }
                else if (_lastKey != currentKey) // Different key pressed and still pressing.
                {
                    _timer = _firstWaitTime;
                    FireEvent(currentKey);
                }
                else if (currentKey == Keys.None) // Key has been released.
                {
                    _timer = 0;
                }
            }

            _lastKey = currentKey;
            _lastState = keyboardState;
        }

        private Keys DifferenceBetween(Keys[] oldKeys, Keys[] newKeys)
        {
            if (oldKeys == null) throw new ArgumentNullException("oldKeys", "Cannot compare the difference between Keys[] when oldKeys is null.");
            if (newKeys == null) throw new ArgumentNullException("newKeys", "Cannot compare the difference between Keys[] when newKeys is null.");
            Keys diff = newKeys.Except(oldKeys).FirstOrDefault();
            return diff == Keys.None ? _lastKey : diff;
        }

        private void FireEvent(Keys key)
        {
            _inputFireEvent?.Invoke(this, new InputEventArgs(key));
        }

        private void DecrementTimer()
        {
            if (_timer > 0) _timer--;
        }
    }
}
