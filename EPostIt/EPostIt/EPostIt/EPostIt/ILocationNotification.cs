using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPostIt
{
    public interface ILocationNotification
    {
        void Remind(string title, string message, int id);
    }
}
