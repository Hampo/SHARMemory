using System;

namespace SHARMemory.SHAR.Events;

public class ErrorEventArgs : EventArgs
{
    public Exception Exception { get; }

    public ErrorEventArgs(Exception exception)
    {
        Exception = exception;
    }
}