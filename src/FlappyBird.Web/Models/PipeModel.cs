using System;
using System.ComponentModel;

namespace FlappyBird.Web.Models
{
    public class PipeModel : INotifyPropertyChanged
    {
        public int DistanceFromBottom { get; private set; }
        public int DistanceFromLeft { get; private set; }

        public PipeModel()
        {
            DistanceFromBottom = new Random().Next(1, 60);
            DistanceFromLeft = 500;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void Move(int speed)
        {
            DistanceFromLeft -= speed;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DistanceFromLeft)));
        }
    }
}