using System;
using System.ComponentModel;

namespace FlappyBird.Web.Models
{
    public class BirdModel : INotifyPropertyChanged
    {
        private int _distanceFromGround = 100;
        public int DistanceFromGround
        {
            get { return _distanceFromGround; }
            private set
            {
                _distanceFromGround = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DistanceFromGround)));
            }
        }

        public int JumpStrength { get; private set; } = 50;

        public event PropertyChangedEventHandler PropertyChanged;

        public void Fall(int gravity)
        {
            DistanceFromGround -= Math.Min(gravity, DistanceFromGround);
        }

        public void Jump()
        {
            if (DistanceFromGround < 530)
                DistanceFromGround += JumpStrength;
        }

        public bool IsOnGround()
        {
            return DistanceFromGround <= 0;
        }
    }
}