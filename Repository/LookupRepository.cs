using DataAccess;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public interface ILookupRepository : IRepository<TblLookup>
    {
        IEnumerable<TblLookup> GetLookups(string Lookup, string value);
        List<string> GetLookuplist(string Lookup);

    }

    public class LookupRepository : Repository<TblLookup>, ILookupRepository
    {
        private readonly ApplicationDbContext _DbContext;
        private new ApplicationUser _applicationUser;

        public LookupRepository(ApplicationDbContext Context,ApplicationUser applicationUser) : base(Context,applicationUser)
        {
            _DbContext = Context;
            _applicationUser = applicationUser;
        }

        public List<string> GetLookuplist(string Lookup)
        {
         
            List<string> ToReturn = new List<string>();
            ToReturn.Add("Select Budget Code");
            ToReturn.Add("New Code");

            var result = FindRecords(x => x.Lookup == _applicationUser.Id.ToString() +"_" +Lookup);
            if (result != null)
            {
                foreach (var I in result)
                {
                    ToReturn.Add(I.Value);
                }
            }
            return ToReturn;
        }      

        public IEnumerable<TblLookup> GetLookups(string Lookup)
        {
            return FindRecords(x => x.Lookup == Lookup);
        }

        public IEnumerable<TblLookup> GetLookups(string Lookup, string value)
        {
            throw new NotImplementedException();
        }
    }
}
