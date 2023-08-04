using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TapPiano
{
    internal class WavBuilder
    {


    }
    public class WaveHeader
    {
        private const string FILE_TYPE_ID = "RIFF";
        private const string MEDIA_TYPE_ID = "WAVE";

        public string FileTypeId { get; private set; }
        public UInt32 FileLength { get; set; }
        public string MediaTypeId { get; private set; }

        public WaveHeader()
        {
            FileTypeId = FILE_TYPE_ID;
            MediaTypeId = MEDIA_TYPE_ID;
            // Minimum size is always 4 bytes
            FileLength = 4;
        }

        public byte[] GetBytes()
        {
            List<Byte> chunkData = new List<byte>();
            chunkData.AddRange(Encoding.ASCII.GetBytes(FileTypeId));
            chunkData.AddRange(BitConverter.GetBytes(FileLength));
            chunkData.AddRange(Encoding.ASCII.GetBytes(MediaTypeId));
            return chunkData.ToArray();
        }
    }

    public class FormatChunk
    {
        private ushort _bitsPerSample;
        private ushort _channels;
        private uint sample_frequency;
        private const string CHUNK_ID = "fmt ";

        public string ChunkId { get; private set; }
        public UInt32 ChunkSize { get; private set; }
        public UInt16 FormatTag { get; private set; }

        public UInt16 Channels
        {
            get { return _channels; }
            set { _channels = value; RecalcBlockSizes(); }
        }

        public UInt32 Frequency
        {
            get { return sample_frequency; }
            set { sample_frequency = value; RecalcBlockSizes(); }
        }

        public UInt32 AverageBytesPerSec { get; private set; }
        public UInt16 BlockAlign { get; private set; }

        public UInt16 BitsPerSample
        {
            get { return _bitsPerSample; }
            set { _bitsPerSample = value; RecalcBlockSizes(); }
        }

        public FormatChunk()
        {
            ChunkId = CHUNK_ID;
            ChunkSize = 16;
            FormatTag = 1;       // MS PCM (Uncompressed wave file)
            Channels = 2;        // Default to stereo
            sample_frequency = 44100 * 2;   // Default to 44100hz
            BitsPerSample = 16;  // Default to 16bits
            RecalcBlockSizes();
        }

        private void RecalcBlockSizes()
        {
            BlockAlign = (UInt16)(_channels * (_bitsPerSample / 8));
            AverageBytesPerSec = sample_frequency * BlockAlign;
        }

        public byte[] GetBytes()
        {
            List<Byte> chunkBytes = new List<byte>();

            chunkBytes.AddRange(Encoding.ASCII.GetBytes(ChunkId));
            chunkBytes.AddRange(BitConverter.GetBytes(ChunkSize));
            chunkBytes.AddRange(BitConverter.GetBytes(FormatTag));
            chunkBytes.AddRange(BitConverter.GetBytes(Channels));
            chunkBytes.AddRange(BitConverter.GetBytes(Frequency));
            chunkBytes.AddRange(BitConverter.GetBytes(AverageBytesPerSec));
            chunkBytes.AddRange(BitConverter.GetBytes(BlockAlign));
            chunkBytes.AddRange(BitConverter.GetBytes(BitsPerSample));

            return chunkBytes.ToArray();
        }

        public UInt32 Length()
        {
            return (UInt32)GetBytes().Length;
        }

    }


    public class DataChunk
    {
        private const string CHUNK_ID = "data";

        public string ChunkId { get; private set; }
        public UInt32 ChunkSize { get; set; }
        public short[] WaveData { get; private set; }

        public DataChunk()
        {
            ChunkId = CHUNK_ID;
            ChunkSize = 0;  // Until we add some data
        }

        public UInt32 Length()
        {
            return (UInt32)GetBytes().Length;
        }

        public byte[] GetBytes()
        {
            List<Byte> chunkBytes = new List<Byte>();

            chunkBytes.AddRange(Encoding.ASCII.GetBytes(ChunkId));
            chunkBytes.AddRange(BitConverter.GetBytes(ChunkSize));
            byte[] bufferBytes = new byte[WaveData.Length * 2];
            Buffer.BlockCopy(WaveData, 0, bufferBytes, 0,
               bufferBytes.Length);
            chunkBytes.AddRange(bufferBytes.ToList());

            return chunkBytes.ToArray();
        }

        public void AddSampleData(short[] leftBuffer,
           short[] rightBuffer)
        {
            WaveData = new short[leftBuffer.Length +
               rightBuffer.Length];
            int bufferOffset = 0;
            for (int index = 0; index < WaveData.Length; index += 2)
            {
                WaveData[index] = leftBuffer[bufferOffset];
                WaveData[index + 1] = rightBuffer[bufferOffset];
                bufferOffset++;
            }
            ChunkSize = (UInt32)WaveData.Length * 2;
        }

    }

    public interface IWave
    {
        // interface for common wave methods to use generelized wave

        short[] generateData();
        ulong period { get; }

    }

    public class SineGenerator : IWave
    {

        //Basic osolating Sine Wave
        // used to produce a simple wave 
        private readonly double _frequency; //Hz
        private const UInt32 _sampleRate = 44100; // samples/sec
        private readonly ulong _nsInLength; // ns
        private short[] _dataBuffer;


        public SineGenerator(double frequency, ulong nsInLength)
        {
            _frequency = frequency;
            _nsInLength = nsInLength;
            data_gen();
        }

        public ulong period
        {
            get { return _nsInLength; }
        }

        private void data_gen()
        {
            ulong bufferSize = (ulong)((((double)(_sampleRate)) / 1E6) * _nsInLength); // (samples*1e6*sec)/(1*e6 *sec) = samples
            _dataBuffer = new short[bufferSize];

            int amplitude = 32760;

            double timePeriod = (Math.PI * 2 * _frequency) /
               (_sampleRate);

            for (ulong index = 0; index < bufferSize - 1; index++)
            {
                _dataBuffer[index] = Convert.ToInt16(amplitude *
                   Math.Sin(timePeriod * index));
            }
        }

        public short[] generateData()
        {

            return _dataBuffer;
        }
    }

    public class CompoundWave : IWave
    {
        //class that represent a compound wave created from super positioning of waves 
        //to create complex sounds with complicated tember
        private const ulong _sampleRate = 44100; //samples/sec
        private readonly ulong _nsInLength; //ns
        private IWave[] waves;
        private short[] _dataBuffer;

        private void superPosition(short[] wav1)
        {
            for (int i = 0; i < _dataBuffer.Length; i++)
                _dataBuffer[i] += wav1[i % wav1.Length];
        }

        public CompoundWave(IWave[] _waves)
        {
            ulong total_period = 1;
            this.waves = _waves;
            foreach (IWave wav in waves)
            {
                total_period *= wav.period;
            }
            _nsInLength = (uint)1E6;
            ulong buffer_size = (ulong)((((double)(_sampleRate)) / 1E6) * _nsInLength);
            _dataBuffer = new short[buffer_size];
            for (int i = 0; i < _dataBuffer.Length; i++)
                _dataBuffer[i] = 0;
            foreach (IWave wav in waves)
            {
                short[] data = wav.generateData();
                superPosition(data);
            }
        }

        public ulong period
        {
            get { return (ulong)_nsInLength; }
        }


        public short[] generateData()
        {
            return _dataBuffer;
        }

    }


}
