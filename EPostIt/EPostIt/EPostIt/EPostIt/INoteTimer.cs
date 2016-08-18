using System;

namespace EPostIt
{
    public interface INoteTimer
    {
        void Remind(DateTime dateTime, string title, string message, int id);
        void Cancel(int id);
    }
}
