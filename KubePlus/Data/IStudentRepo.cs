
using System.Collections.Generic;
using System.Threading.Tasks;
using KubePlus.Models;

namespace KubePlus.Data
{
    public interface IStudentRepo
    {
        Task<IEnumerable<Command>> GetStudents();
        Task<Command> GetStudentById(int id);
        Task<IEnumerable<Command>> GetStudentsByCourse(string course);
        Task DeleteAsync(Command command);
        Task SaveSingleAsync(Command command);
        Task SaveManyAsync(Command[] commands);
        Task SaveBulkAsync(Command[] commands);
    }
}