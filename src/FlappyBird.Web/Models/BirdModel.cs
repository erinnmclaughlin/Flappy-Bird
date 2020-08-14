using System.ComponentModel;

namespace FlappyBird.Web.Models
{
    public class BirdModel : INotifyPropertyChanged
    {
        public int DistanceFromBottom { get; private set; } = 100;
        public int JumpStrength { get; private set; } = 50;

        public event PropertyChangedEventHandler PropertyChanged;

        public void Fall(int gravity)
        {
            DistanceFromBottom -= gravity;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DistanceFromBottom)));
        }

        public void Jump()
        {
            if (DistanceFromBottom < 530)
                DistanceFromBottom += JumpStrength;
        }
    }
}