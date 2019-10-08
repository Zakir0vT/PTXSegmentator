using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace PTXSegmentator
{
    public abstract class PointsReader
    {
        public abstract string FilePath { get; set; }
        public abstract string Line { get; set; }
        public abstract string[] PointsInLine { get; set; }
        public long Position { get; private set; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Read(Action rule, Action<long> progressBar)
        {
            using (var sr = new StreamReader(string.Concat(FilePath, ".ptx")))
            {
                while (!sr.EndOfStream)
                {
                    Line= sr.ReadLine();
                    Position = sr.BaseStream.Position;
                    progressBar(Position);
                    PointsInLine= Line?.Split(' ');
                    rule?.Invoke();
                }
            }
        }
    }
}
