using Apka2.Model;
using System.Collections.Generic;

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
