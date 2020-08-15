using System;
namespace ZGGame
{

    public class ByteArray
    {
        private byte[] _bytes;
        /// <summary>
        /// 读取游标
        /// </summary>
        public int readOffset = 0;
        /// <summary>
        /// 写入游标
        /// </summary>
        public int writeOffset = 0;
        private int count = 0;
        public ByteArray(byte[] bytes)
        {
            _bytes = bytes;
            if (_bytes == null)
                throw new Exception("ByteArray cannot get a null bytes[]");
        }

        public ByteArray(int len)
        {
            _bytes = new byte[len];
        }

        public byte[] bytes
        {
            get
            {
                return _bytes;
            }
        }

        public int length
        {
            get
            {
                return _bytes.Length;
            }
        }

        public short readShort()
        {
            count = 2;
            byte high = _bytes[readOffset];
            byte low = _bytes[readOffset + count - 1];
            readOffset += count;

            return (short)((high << 8 | low));
        }
        /// <summary>
        /// 读取一个long型数据
        /// </summary>
        /// <returns></returns>
        public long readInt64()
        {
            count = 8;
            long p0 = _bytes[readOffset];
            long p1 = _bytes[readOffset + 1];
            long p2 = _bytes[readOffset + 2];
            long p3 = _bytes[readOffset + 3];
            long p4 = _bytes[readOffset + 4];
            long p5 = _bytes[readOffset + 5];
            long p6 = _bytes[readOffset + 6];
            long p7 = _bytes[readOffset + 7];
            readOffset += count;

            return (p0 << 56 | p1 << 48 | p2 << 40 | p3 << 32 | p4 << 24 | p5 << 16 | p6 << 8 | p7);
        }
        /// <summary>
        /// 读取一个字节
        /// </summary>
        /// <returns></returns>
        public byte readByte()
        {
            count = 1;
            byte b = _bytes[readOffset];
            readOffset += count;
            return b;
        }
        /// <summary>
        /// 获取一定范围内的字节,不更改游标位置
        /// </summary>
        /// <param name="index"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public byte[] getBytes(int index, int len)
        {
            byte[] bs = new byte[len];
            for (int i = 0; i < len; i++)
            {
                bs[i] = _bytes[i + index];
            }
            return bs;
        }
        /// <summary>
        /// 读取指定长度的字节,更改游标位置
        /// </summary>
        /// <param name="len"></param>
        /// <returns></returns>
        public byte[] readBytes(int len)
        {
            byte[] bs = new byte[len];
            for (int i = 0; i < len; i++)
            {
                bs[i] = _bytes[i + readOffset];
            }

            readOffset += len;

            return bs;
        }
        /// <summary>
        /// 读取一个整数
        /// </summary>
        /// <returns></returns>
        public int readInt()
        {
            count = 4;
            byte high = _bytes[readOffset];
            byte highLow = _bytes[readOffset + 1];
            byte low = _bytes[readOffset + 2];
            byte lowLow = _bytes[readOffset + 3];
            readOffset += count;

            return (int)((high << 24 | highLow << 16 | low << 8 | lowLow));
        }


        //======================================================写入=====================

        public void writeShort(short a)
        {
            count = 2;
            byte high = (byte)((0xff00 & a) >> 8);
            byte low = (byte)(0xff & a);
            _bytes[writeOffset] = high;
            _bytes[writeOffset + count - 1] = low;
            writeOffset += count;
        }

        public void writeInt(int a)
        {
            count = 4;
            byte high = (byte)((0xff000000 & a) >> 24);
            byte highLow = (byte)((0xff0000 & a) >> 16);
            byte low = (byte)((0xff00 & a) >> 8);
            byte lowLow = (byte)(0xff & a);
            _bytes[writeOffset] = high;
            _bytes[writeOffset + 1] = highLow;
            _bytes[writeOffset + 2] = low;
            _bytes[writeOffset + count - 1] = lowLow;
            writeOffset += count;
        }

        public void writeByte(byte a)
        {
            count = 1;
            _bytes[writeOffset + count - 1] = a;
            writeOffset += count;
        }

        public void writeBytes(byte[] b, int index, int len)
        {
            for (int i = 0; i < len; i++)
            {
                _bytes[writeOffset + i] = b[i];
            }
            writeOffset += len;
        }

        public void writeBytes(byte[] b)
        {
            for (int i = 0; i < b.Length; i++)
            {
                _bytes[writeOffset + i] = b[i];
            }

            writeOffset += b.Length;
        }








        public string byteStr()
        {
            string t = "";
            for (int i = 0; i < _bytes.Length; i++)
            {
                t += _bytes[i].ToString();
            }

            return t;
        }

        public string byteStrHex()
        {
            string t = "";
            for (int i = 0; i < _bytes.Length; i++)
            {
                t += _bytes[i].ToString("X2") + " ";
            }

            return t;
        }
    }
}