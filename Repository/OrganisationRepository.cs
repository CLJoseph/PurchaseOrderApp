using DataAccess;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models.Common;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public interface IOrganisationRepository : IRepository<TblOrganisation>
    {
        IEnumerable<TblOrganisation> GetAllOrganisations();
        TblOrganisation GetOrganisationbyId(Guid Id);
        TblOrganisation GetOrganisationbyIdandItems(Guid Id);

        TblOrganisation GetOrganisationbyName(string Code);
        List<OrgDetail> GetOrganisations();
        List<TblOrganisationItem> getOrganisationItems(Guid Id);
        void AddItem(TblOrganisationItem Item);
    }

    public class OrganisationRepository : Repository<TblOrganisation>, IOrganisationRepository
    {
        private ApplicationDbContext _DbContext;

        public OrganisationRepository(ApplicationDbContext Context,ApplicationUser applicationUser) : base(Context,applicationUser)
        {
            _DbContext = Context;
        }

        public IEnumerable<TblOrganisation> GetAllOrganisations()
        {
            return FindRecords(x => x.ApplicationUser.Id == _applicationUser.Id);
        }

        public TblOrganisation GetOrganisationbyId(Guid Id)
        {
            return GetRecord(Id);
        }

        public TblOrganisation GetOrganisationbyName(string Name)
        {
            return FindRecord(x => x.ApplicationUser.Id == _applicationUser.Id && x.Name == Name);
        }

        public List<OrgDetail> GetOrganisations()
        {
            List<OrgDetail> ToReturn = new List<OrgDetail>();
            var result = GetAllOrganisations();
            ToReturn.Add(new OrgDetail()
            {
                Id= "Select",
                Name = "Select Organisation",
                info = "",
                Email = ""
            });
            ToReturn.Add(new OrgDetail()
            {
                Id = "NewOrganisation",
                Name = "New Organisation",
                info = "",
                Email = ""
            });
            foreach (var O in result)            {
                ToReturn.Add(new OrgDetail()
                {
                    Id = O.ID.ToString(),
                    Name = O.Name,
                    info = O.Name + "<br /> " + O.Contact + "<br /> " + O.ContactNo + "<br /> " + O.ContactEmail + "<br />" + O.Address,
                    Email = O.ContactEmail
                });
            }
            return ToReturn;
        }

        public List<TblOrganisationItem> getOrganisationItems(Guid Id)



        {
            return  (List<TblOrganisationItem>) GetRecord(Id).Items;
        }
        public void AddItem(TblOrganisationItem Item)
        {
            _DbContext.OrganisationItems.Add(Item);
        }
        public TblOrganisation GetOrganisationbyIdandItems(Guid Id)
        {
            TblOrganisation ToReturn = new TblOrganisation();
            if (!string.IsNullOrEmpty(Id.ToString()))
            {
                ToReturn = _DbContext.Organisations.Find(Id);
                if (ToReturn != null)
                {
                    _DbContext.Entry(ToReturn).Collection(c => c.Items).Load();
                }
                return ToReturn;
            }
            return ToReturn;
        }
    }
}
