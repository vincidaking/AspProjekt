using Apka2.Entities;
using Apka2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apka2.DAL
{
    public interface ILaw
    {
        IEnumerable<Law> GetSavedLaws();
        void SaveLaw(Law user);
        void Update(Law user);
        void Delete(int id);


    }
}
