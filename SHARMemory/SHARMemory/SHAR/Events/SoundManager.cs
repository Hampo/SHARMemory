using System;

namespace SHARMemory.SHAR.Events.SoundManager;

public class DialogPlayingEventArgs : EventArgs
{
    public Classes.SelectableDialog Dialog { get; }

    public DialogPlayingEventArgs(Classes.SelectableDialog dialog)
    {
        Dialog = dialog;
    }
}