using System;
using System.Collections.Generic;
using System.Linq;

namespace MasterMind
{
    internal class Ball
    {
        #region Properties
        /// <summary>
        /// The color you want the ball to have
        /// </summary>
        public BallColor Color { get; set; }

        #endregion

        #region Constructors
        /// <summary>
        /// Empty Constructor - creates a ball with a random color
        /// </summary>
        public Ball()
        {
            Color = GetRandomColor();
        }
        /// <summary>
        /// Constructor - creates a ball with a predefined color
        /// </summary>
        /// <param name="Color"></param>
        public Ball(BallColor color)
        {
            Color = color;
        }

        #endregion

        #region Methods
        /// <summary>
        /// Picks a random color
        /// </summary>
        public BallColor GetRandomColor()
        {
            // Convert the color enumeration to a list, as this is easier to work with
            List<BallColor> ballColors = Enum.GetValues(typeof(BallColor)).Cast<BallColor>().ToList();
            // Remove the first value from the color enumaration, as it is not a color
            ballColors.Remove(BallColor.None);
            // Pick a random value
            Random random = new();
            return ballColors[random.Next(ballColors.Count)];
        }
        #endregion

        /// <summary>
        /// Override the Equal method, as we want to comapre ball colors
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            // If the passed object is null
            if (obj == null)
            {
                return false;
            }
            if (!(obj is Ball))
            {
                return false;
            }
            return (this.Color == ((Ball)obj).Color);
        }
        public override int GetHashCode()
        {
            return Color.GetHashCode();
        }

    }
}
