using System;
using System.Collections.Generic;
using System.Linq;
using Apka2.Data;
using Apka2.Model;
using Microsoft.EntityFrameworkCore;

namespace Apka2.DAL
{

    public class LawEntity: ILaw, IDisposable
    {
        private Context _context = new Context();

        public void Delete(int id)
        {
            var laws = _context.Laws.Find(id);
            if (laws != null)
            {
                _context.Laws.Remove(laws);
                _context.SaveChanges();
            }
        }

        public void Dispose()
        {
           // throw new NotImplementedException();
        }

        public IEnumerable<Law> GetSavedLaws()
        {
            _context.Database.EnsureCreated();

            return _context.Laws.ToList();
        }

        public void SaveLaw(Law law)
        {
            _context.Laws.Add(law);
            _context.SaveChanges();
        }

        public void Update(Law law)
        {
            var temp = _context.Laws.Find(law.Id);


            if (law.LawText != null)
                temp.LawText = law.LawText;
           
            _context.Laws.Update(temp);
            _context.SaveChanges();
        }
    }
}
