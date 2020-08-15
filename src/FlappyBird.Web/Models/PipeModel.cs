using System;
using System.ComponentModel;
using System.Runtime.InteropServices.ComTypes;

namespace FlappyBird.Web.Models
{
    public class PipeModel : INotifyPropertyChanged
    {
        private int _distanceFromLeft = 500;

        public int DistanceFromBottom { get; private set; } = new Random().Next(1, 60);
        
        public int BottomDistanceFromGround
        {
            // pipe height - ground height + distance from bottom 
            get => 300 - 150 + DistanceFromBottom;
        }

        public int TopDistanceFromGround
        {
            // pipe gap - ground height + distance from bottom
            get => 430 - 150 + DistanceFromBottom;
        }

        public int DistanceFromLeft
        {
            get { return _distanceFromLeft; }
            set
            {
                _distanceFromLeft = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DistanceFromLeft)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void Move(int speed)
        {
            DistanceFromLeft -= speed;
        }

        public bool IsCentered()
        {
            var centerMin = (500 - (2 * 60)) / 2;
            var centerMax = (500 + 60) / 2;

            return (DistanceFromLeft > centerMin && DistanceFromLeft < centerMax);
        }
    }
}