using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KubePlus.Models;
using Microsoft.Extensions.Logging;
using Nest;

namespace KubePlus.Data
{
    public class KubePlusRepo : IStudentRepo
    {
        private List<Command> _cache = new List<Command>();
        private readonly IElasticClient _elasticClient;
        private readonly ILogger _logger;

        public KubePlusRepo(IElasticClient elasticClient, ILogger<KubePlusRepo> logger)
        {
            _elasticClient = elasticClient;
            _logger = logger;
        }

        public async Task SaveSingleAsync(Command command)
        {
            if (_cache.Any(p => p.Id == command.Id))
            {
                await _elasticClient.UpdateAsync<Command>(command, u => u.Doc(command));
            }
            else
            {
                _cache.Add(command);
                await _elasticClient.IndexDocumentAsync<Command>(command);
            }
        }

        
        public virtual Task<IEnumerable<Command>> GetStudents()
        {
            var students = _cache;
            return Task.FromResult<IEnumerable<Command>>(students);
        }

        public virtual Task<Command> GetStudentById(int id)
        {
            var student = _cache
              .Where(s => s.Id == id)
              .FirstOrDefault(s => s.Id == id);

            return Task.FromResult(student);
        
        }

        public Task<IEnumerable<Command>> GetStudentsByCourse(string course)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAsync(Command command)
        {
            throw new System.NotImplementedException();
        }

        

        public Task SaveManyAsync(Command[] commands)
        {
            throw new System.NotImplementedException();
        }

        public Task SaveBulkAsync(Command[] commands)
        {
            throw new System.NotImplementedException();
        }
    }
}