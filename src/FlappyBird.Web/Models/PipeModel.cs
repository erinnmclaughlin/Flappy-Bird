using System;
using System.ComponentModel;

namespace FlappyBird.Web.Models
{
    public class PipeModel : INotifyPropertyChanged
    {
        private readonly int _gameWidth;
        private readonly int _groundHeight;

        private readonly int _width;
        public int Width { get { return _width; } }

        private readonly int _height;
        public int Height { get { return _height; } }

        private readonly int _distanceFromBottom;
        public int DistanceFromBottom { get { return _distanceFromBottom; } }
        public int DistanceFromGround { get { return _height + DistanceFromBottom - _groundHeight; } }

        private readonly int _gap;
        public int Gap { get { return _gap; } }

        private int _distanceFromLeft;
        public int DistanceFromLeft
        {
            get { return _distanceFromLeft; }
            private set
            {
                _distanceFromLeft = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DistanceFromLeft)));
            }
        }        

        public event PropertyChangedEventHandler PropertyChanged;

        public PipeModel(int gameWidth, int groundHeight)
        {
            _gameWidth = gameWidth;
            _groundHeight = groundHeight;
            _distanceFromLeft = gameWidth;
            _distanceFromBottom = new Random().Next(1, 60);
            _gap = gameWidth * 13 / 50;

            _width = gameWidth * 3 / 25;
            _height = gameWidth * 3 / 5;
        }

        public void Move(int speed)
        {
            DistanceFromLeft -= speed;
        }

        public bool IsCentered(int centerWidth)
        {
            // half of the game width minus the width of the pipe
            var centerMin =  _gameWidth / 2 - _width;

            // half of the game width plus half the width of the bird
            var centerMax = (_gameWidth + centerWidth) / 2;

            return (DistanceFromLeft > centerMin && DistanceFromLeft < centerMax);
        }

        public bool IsOffScreen()
        {
            return DistanceFromLeft <= -60;
        }
    }
}