using System.ComponentModel;

namespace FlappyBird.Web.Models
{
    public class BirdModel : INotifyPropertyChanged
    {
        public int _distanceFromBottom = 100;

        public int DistanceFromBottom
        {
            get { return _distanceFromBottom; }
            set
            {
                _distanceFromBottom = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DistanceFromBottom)));
            }
        }
        public int JumpStrength { get; private set; } = 50;

        public event PropertyChangedEventHandler PropertyChanged;

        public void Fall(int gravity)
        {
            DistanceFromBottom -= gravity;
        }

        public void Jump()
        {
            if (DistanceFromBottom < 530)
                DistanceFromBottom += JumpStrength;
        }
    }
}