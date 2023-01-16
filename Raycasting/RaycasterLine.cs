namespace Raycasting
{
    public struct RaycasterLine
    {
        public int Start;
        public int Length;
        public int End => Length - Start;
        public int LineHeight;

        public RaycasterLine(int start, int end, int lineHeight)
        {
            Start = start;
            Length = end;
            LineHeight = lineHeight;
        }
    }
}
