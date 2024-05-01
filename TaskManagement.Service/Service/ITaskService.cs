using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Service.Service
{

    public interface ITaskService
    {
        Task Get(long taskId);

        void Create(string name, string description);

        void Update(string newName);

        Task Remove(long taskId);
    }
}
