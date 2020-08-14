using System;

namespace FlappyBird.Web.Models
{
    public class PipeModel
    {
        public int DistanceFromBottom { get; private set; }
        public int DistanceFromLeft { get; private set; }

        public PipeModel()
        {
            DistanceFromBottom = new Random().Next(1, 60);
            DistanceFromLeft = 500;
        }
    }
}