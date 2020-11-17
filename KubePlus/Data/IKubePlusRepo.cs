using System.Collections.Generic;
using System.Threading.Tasks;
using KubePlus.Models;

namespace KubePlus.Data
{
    public interface IKubePlusRepo
    {
        IEnumerable<Command> GetAppCommands();
        Command GetCommandById(int Id);

        // Task SaveSingleAsync(Command command);
    }
}