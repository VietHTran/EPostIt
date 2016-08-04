using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPostIt
{
    public interface INoteTimer
    {
        void Remind(DateTime dateTime, string title, string message, int id);
        void Cancel(int id);
    }
}
