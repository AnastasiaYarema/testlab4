using Xunit;
using System;
using IIG.FileWorker;
using IIG.BinaryFlag;


namespace Lab4Testing
{
    public class BinaryFlagTest
    {
        [Fact]
        public void WriteFlagFalse()
        {

            string mbf = new MultipleBinaryFlag(2, false).GetFlag().ToString();

            BaseFileWorker.Write(mbf, @".\testFile1.txt");

            Assert.Equal(mbf, BaseFileWorker.ReadAll(@".\testFile1.txt"));
        }

        [Fact]
        public void WriteFlagLongFalse()
        {

            string mbf = new MultipleBinaryFlag(10000000000, false).GetFlag().ToString();

            BaseFileWorker.Write(mbf, @".\testFile2.txt");

            Assert.Equal(mbf, BaseFileWorker.ReadAll(@".\testFile2.txt"));
        }

        [Fact]
        public void WriteFlagTrue()
        {
            string mbf = new MultipleBinaryFlag(2, true).GetFlag().ToString();

            BaseFileWorker.Write(mbf, @".\testFile3.txt");

            Assert.Equal(mbf, BaseFileWorker.ReadAll(@".\testFile3.txt"));
        }

        [Fact]
        public void WriteFlagLongTrue()
        {
            string mbf = new MultipleBinaryFlag(10000000000, true).GetFlag().ToString();

            BaseFileWorker.Write(mbf, @".\testFile4.txt");

            Assert.Equal(mbf, BaseFileWorker.ReadAll(@".\testFile4.txt"));
        }

        [Fact]
        public void ResetFlagTrue()
        {
            MultipleBinaryFlag mbf = new MultipleBinaryFlag(10, true);
            mbf.ResetFlag(2);

            string flag = mbf.GetFlag().ToString();

            BaseFileWorker.Write(flag, @".\testFile5.txt");

            Assert.Equal(flag, BaseFileWorker.ReadAll(@".\testFile5.txt"));
        }

        [Fact]
        public void ResetFlagLongTrue()
        {
            MultipleBinaryFlag mbf = new MultipleBinaryFlag(10000000000, true);
            mbf.ResetFlag(2);

            string flag = mbf.GetFlag().ToString();

            BaseFileWorker.Write(flag, @".\testFile6.txt");

            Assert.Equal(flag, BaseFileWorker.ReadAll(@".\testFile6.txt"));
        }

        [Fact]
        public void ResetFlagLongPositionTrue()
        {
            MultipleBinaryFlag mbf = new MultipleBinaryFlag(10000000000, true);
            mbf.ResetFlag(20000000);
            
            string flag = mbf.GetFlag().ToString();

            BaseFileWorker.Write(flag, @".\testFile7.txt");

            Assert.Equal(flag, BaseFileWorker.ReadAll(@".\testFile7.txt"));
        }

        [Fact]
        public void ResetFlagFalse()
        {
            MultipleBinaryFlag mbf = new MultipleBinaryFlag(10, false);
            mbf.SetFlag(3);

            string flag = mbf.GetFlag().ToString();

            BaseFileWorker.Write(flag, @".\testFile8.txt");

            Assert.Equal(flag, BaseFileWorker.ReadAll(@".\testFile8.txt"));
        }

        [Fact]
        public void ResetFlagLongFalse()
        {
            MultipleBinaryFlag mbf = new MultipleBinaryFlag(10000000000, false);
            mbf.SetFlag(3);
            
            string flag = mbf.GetFlag().ToString();

            BaseFileWorker.Write(flag, @".\testFile9.txt");

            Assert.Equal(flag, BaseFileWorker.ReadAll(@".\testFile9.txt"));
        }

        [Fact]
        public void ResetFlagLongPositionFalse()
        {
            MultipleBinaryFlag mbf = new MultipleBinaryFlag(10000000000, false);
            mbf.SetFlag(20000000);

            string flag = mbf.GetFlag().ToString();

            BaseFileWorker.Write(flag, @".\testFile10.txt");

            Assert.Equal(flag, BaseFileWorker.ReadAll(@".\testFile10.txt"));
        }


        [Fact]
        public void ReadLines()
        {
            string mbf = new MultipleBinaryFlag(10, false).GetFlag().ToString();

            BaseFileWorker.Write(mbf, @".\testFile11.txt");

            Assert.Equal(mbf, String.Join("", BaseFileWorker.ReadLines(@".\testFile11.txt")));
        }

        [Fact]
        public void ReadLinesSetFlag()
        {
            MultipleBinaryFlag mbf = new MultipleBinaryFlag(10, false);
            mbf.SetFlag(3);

            string flag = mbf.GetFlag().ToString();

            BaseFileWorker.Write(flag, @".\testFile12.txt");

            Assert.Equal(flag, String.Join("", BaseFileWorker.ReadLines(@".\testFile12.txt")));
        }

        [Fact]
        public void ReadLinesResetFlag()
        {
            MultipleBinaryFlag mbf = new MultipleBinaryFlag(10, false);
            mbf.ResetFlag(3);

            string flag = mbf.GetFlag().ToString();

            BaseFileWorker.Write(flag, @".\testFile13.txt");

            Assert.Equal(flag, String.Join("", BaseFileWorker.ReadLines(@".\testFile13.txt")));
        }

        [Fact]
        public void ReadLinesResetFlagLong()
        {
            MultipleBinaryFlag mbf = new MultipleBinaryFlag(10000000000, false);
            mbf.ResetFlag(2000000);

            string flag = mbf.GetFlag().ToString();

            BaseFileWorker.Write(flag, @".\testFile14.txt");

            Assert.Equal(flag, String.Join("", BaseFileWorker.ReadLines(@".\testFile14.txt")));
        }
    }
}
