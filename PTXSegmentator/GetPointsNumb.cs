using System;
using System.IO;

namespace PTXSegmentator
{
    public class GetPointsNumb : PointsReader
    {
        private readonly MatrixGridDto _matrixGridDto;
        private readonly ProgressBar _progressBar;
        public override string Line { get; set; }
        public override string[] PointsInLine { get; set; }

        private string _filePath;
        public override string FilePath
        {
            get => _filePath;
            set => _filePath = string.IsNullOrEmpty(value)
                ? throw new ArgumentNullException($"File name can`t be empty")
                : value;
        }


        public GetPointsNumb(MatrixGridDto matrixGridDto, ProgressBar progressBar)
        {
            _matrixGridDto = matrixGridDto;
            _progressBar = progressBar;
        }

        public void CreateMatrixGrid()
        {
            Action validationRule = () =>
            {
                if (PointsInLine?.Length == 1)
                {
                    _matrixGridDto.GridPoints.Add(Line);
                }
            };

            _progressBar.FileLength = new FileInfo(FilePath + ".ptx").Length;

            Read(validationRule, _progressBar.ProgressBarWriter);
        }
    }
}
