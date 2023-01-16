namespace Raycasting
{
    public struct RaycasterLine
    {
        public int Start;
        public int End;
        public int Size => End - Start;
        public int LineHeight;

        public RaycasterLine(int start, int end, int lineHeight)
        {
            Start = start;
            End = end;
            LineHeight = lineHeight;
        }
    }
}
