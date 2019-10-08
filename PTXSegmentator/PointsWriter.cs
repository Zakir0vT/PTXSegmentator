using System;
using System.IO;

namespace PTXSegmentator
{
    public class PointsWriter : PointsReader
    {
        private readonly MatrixGridDto _matrixGridDto;
        private readonly ProgressBar _progressBar;
        public sealed override string FilePath { get; set; }
        public override string Line { get; set; }
        public override string[] PointsInLine { get; set; }
        private int _pointsInOneFile;
        public int PointsInOneFile
        {
            private get => _pointsInOneFile;
            set
            {
                _pointsInOneFile = value * 2;
            }
        }

        private int _increment;
        private StreamWriter _streamWriter;

        public PointsWriter(MatrixGridDto matrixGridDto, GetPointsNumb pointsNumb, ProgressBar progressBar)
        {
            _matrixGridDto = matrixGridDto;
            _progressBar = progressBar;
            FilePath = pointsNumb.FilePath ?? throw new ArgumentNullException();
        }

        public void Write()
        {
            Action RuleForWrite = () =>
            {
                if (PointsInLine?.Length == 1)
                {
                    if (_matrixGridDto.GridPoints[_increment] == Line && _increment % PointsInOneFile == 0)
                    {
                        _streamWriter = CreateNewFile();
                        _increment++;
                    }

                    if (_matrixGridDto.GridPoints[_increment] == Line && _increment % PointsInOneFile != 0)
                    {
                        _increment++;
                    }
                }

                _streamWriter?.WriteLine(Line);
            };

            Read(RuleForWrite, _progressBar.ProgressBarWriter);
            _streamWriter.Flush();
            _streamWriter.Close();
        }

        private StreamWriter CreateNewFile()
        {
            _streamWriter?.Flush();
            _streamWriter?.Close();
            var sw = File.CreateText($"{_increment}.ptx");
            return sw;
        }
    }
}
