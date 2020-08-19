using System;
using System.ComponentModel;

namespace FlappyBird.Web.Models
{
    public class BirdModel : INotifyPropertyChanged
    {
        private readonly int _height;
        public int Height { get { return _height; } }
        private readonly int _width;
        public int Width { get { return _width; } }
        private readonly int _distanceFromLeft;
        public int DistanceFromLeft { get { return _distanceFromLeft; } }
        private int _distanceFromGround;
        public int DistanceFromGround
        {
            get { return _distanceFromGround; }
            private set
            {
                _distanceFromGround = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DistanceFromGround)));
            }
        }
        private int _jumpStrength;
        public int JumpStrength
        {
            // TODO: Add limitations to min/max value (i.e., jump strength cant be negative or more than X percent of game height_
            get { return _jumpStrength; }
            set { _jumpStrength = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public BirdModel(int gameWidth)
        {
            _width = gameWidth * 3 / 25;
            _height = _width * 3 / 4;
            _distanceFromLeft = (gameWidth - _width) / 2;
            _distanceFromGround = gameWidth / 5;
            _jumpStrength = gameWidth / 10;
        }

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