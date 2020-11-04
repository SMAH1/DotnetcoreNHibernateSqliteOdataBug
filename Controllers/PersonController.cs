using dotnetcoreNHibernateSqlite.DbModel;
using Microsoft.AspNetCore.Mvc;
using NHibernate;
using NHibernate.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnetcoreNHibernateSqlite.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly ISession _session;

        public PersonController(ISession session)
        {
            _session = session;

            var tt = _session.QueryOver<Person>().RowCount();
            if (tt == 0)
            {
                _session.Save(new Person() { Name = "Jim", Age = 10 });
                _session.Save(new Person() { Name = "John", Age = 20 });
                _session.Save(new Person() { Name = "Joe", Age = 30 });
                _session.Save(new Person() { Name = "Sara", Age = 40 });
                _session.Save(new Person() { Name = "Adam", Age = 50 });
                _session.Save(new Person() { Name = "Eve", Age = 90 });
                _session.Save(new Person() { Name = "Jack", Age = 22 });
                _session.Save(new Person() { Name = "‌Betty", Age = 73 });
                _session.Save(new Person() { Name = "Lily", Age = 5 });
                _session.Save(new Person() { Name = "Ali", Age = 56 });
                _session.Save(new Person() { Name = "Tom", Age = 15 });

                _session.Flush();
            }
        }

        /// <summary>
        /// Get all persons without Odata
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("all")]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<Person>>> GetAll()
        {
            return await _session.Query<Person>().ToListAsync();
        }
    }
}
