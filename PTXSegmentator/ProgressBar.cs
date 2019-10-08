using System;
using System.Runtime.CompilerServices;

namespace PTXSegmentator
{
    public class ProgressBar
    {
        public long FileLength { private get; set; }
        private long _percent;
        private long _percentPrev;
        private string progressbarForm = "[__________]";

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ProgressBarWriter(long currentPosition)
        {
            _percent = currentPosition * 100 / FileLength;

            if (_percent != _percentPrev)
            {
                _percentPrev = _percent;
                Console.Write("\r{0}    {1}%   ",ProgressBarForm(), _percent);
            }

            string ProgressBarForm()
            {
                var index = (int)(_percent / 10);
                return  progressbarForm.Remove(index, 1).Insert(index, "#");
            }
        }
    }
}
