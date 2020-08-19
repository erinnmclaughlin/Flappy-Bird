using System;
using System.ComponentModel;

namespace FlappyBird.Web.Models
{
    public class PipeModel : INotifyPropertyChanged
    {
        private int _distanceFromLeft = 500;

        public int DistanceFromBottom { get; private set; } = new Random().Next(1, 60);

        public int DistanceFromLeft
        {
            get { return _distanceFromLeft; }
            set
            {
                _distanceFromLeft = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DistanceFromLeft)));
            }
        }

        public int DistanceFromGround
        {
            // DistanceFromBottom - Height of Ground
            get { return DistanceFromBottom - 150; }
        }

        public int Gap { get; private set; } = 130;
        public int GapDistanceFromGroundMin { get { return DistanceFromBottom + 300 - 150; } }
        public int GapDistanceFromGroundMax { get { return GapDistanceFromGroundMin + Gap; } }

        public event PropertyChangedEventHandler PropertyChanged;

        public void Move(int speed)
        {
            DistanceFromLeft -= speed;
        }

        public bool IsCentered()
        {
            // half of the game width minus the width of the bird
            var centerMin = 500 / 2 - 60;
            // half of the game width plus half the width of the bird
            var centerMax = (500 + 60) / 2;

            return (DistanceFromLeft > centerMin && DistanceFromLeft < centerMax);
        }

        public bool IsOffScreen()
        {
            return DistanceFromLeft <= -60;
        }
    }
}