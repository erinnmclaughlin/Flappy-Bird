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

        public event PropertyChangedEventHandler PropertyChanged;

        public void Move(int speed)
        {
            DistanceFromLeft -= speed;
        }
    }
}