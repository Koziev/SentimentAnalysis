using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

// http://stackoverflow.com/questions/10189270/tracking-the-position-of-the-line-of-a-streamreader

public class StreamLineReader : IDisposable
{
    const int BufferLength = 1024;

    Stream _Base;
    int _Read = 0, _Index = 0;
    byte[] _Bff = new byte[BufferLength];

    long _CurrentPosition = 0;
    int _CurrentLine = 0;

    /// <summary>
    /// CurrentLine number
    /// </summary>
    public long CurrentPosition
    {
        get { return _CurrentPosition; }
        set
        {
            if (_CurrentPosition != value)
            {
                _CurrentPosition = value;
                _Read = 0;
                _Base.Position = _CurrentPosition;
            }

            return;
        }
    }
    /// <summary>
    /// CurrentLine number
    /// </summary>
    public int CurrentLine { get { return _CurrentLine; } }
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="stream">Stream</param>
    public StreamLineReader(Stream stream) { _Base = stream; }
    /// <summary>
    /// Count lines and goto line number
    /// </summary>
    /// <param name="goToLine">Goto Line number</param>
    /// <returns>Return true if goTo sucessfully</returns>
    public bool GoToLine(int goToLine) { return IGetCount(goToLine, true) == goToLine; }
    /// <summary>
    /// Count lines and goto line number
    /// </summary>
    /// <param name="goToLine">Goto Line number</param>
    /// <returns>Return the Count of lines</returns>
    public int GetCount(int goToLine) { return IGetCount(goToLine, false); }
    /// <summary>
    /// Internal method for goto&Count
    /// </summary>
    /// <param name="goToLine">Goto Line number</param>
    /// <param name="stopWhenLine">Stop when found the selected line number</param>
    /// <returns>Return the Count of lines</returns>
    int IGetCount(int goToLine, bool stopWhenLine)
    {
        _Base.Seek(0, SeekOrigin.Begin);
        _CurrentPosition = 0;
        _CurrentLine = 0;
        _Index = 0;
        _Read = 0;

        long savePosition = _Base.Length;

        do
        {
            if (_CurrentLine == goToLine)
            {
                savePosition = _CurrentPosition;
                if (stopWhenLine) return _CurrentLine;
            }
        }
        while (ReadLine() != null);

        // GoToPosition

        int count = _CurrentLine;

        _CurrentLine = goToLine;
        _Base.Seek(savePosition, SeekOrigin.Begin);

        return count;
    }


    private const int MAX_BYTES = 8192;
    private byte[] bytes = new byte[MAX_BYTES];

    /// <summary>
    /// Read Line
    /// </summary>
    /// <returns></returns>
    public string ReadLine()
    {
        bool found = false;

        int nbytes = 0;

        while (!found)
        {
            if (_Read <= 0)
            {
                // Read next block
                _Index = 0;
                _Read = _Base.Read(_Bff, 0, BufferLength);
                if (_Read == 0)
                {
                    if (nbytes > 0) break;
                    return null;
                }
            }

            for (int max = _Index + _Read; _Index < max;)
            {
                byte ch = _Bff[_Index];
                _Read--; _Index++;
                _CurrentPosition++;

                if (ch == 0 || ch == 13)
                {
                    found = true;
                    break;
                }
                else if (ch == 10) continue;
                else
                {
                    if (nbytes < MAX_BYTES)
                    {
                        bytes[nbytes++] = ch;
                    }
                }
            }
        }

        bytes[nbytes] = 0;

        _CurrentLine++;
        return System.Text.Encoding.UTF8.GetString(bytes, 0, nbytes);
    }
    /// <summary>
    /// Free resources
    /// </summary>
    public void Dispose()
    {
        if (_Base != null)
        {
            _Base.Close();
            _Base.Dispose();
            _Base = null;
        }
    }
}