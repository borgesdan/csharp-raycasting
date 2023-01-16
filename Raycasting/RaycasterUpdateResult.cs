using Microsoft.Xna.Framework;

namespace Raycasting
{
    public class RaycasterUpdateResult
    {
        public RaycasterLine Line { get; set; }
        public int Side { get; set; }
        public Point Point { get; set; }   
        public float PerpendicularWallDistance { get; set; }
        public Vector2 RayDirection { get; set; }
        public int LineHeight { get; set; }
        public int DrawStart { get; set; }
        public int DrawEnd { get; set; }        
    }
}