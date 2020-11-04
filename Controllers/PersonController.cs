﻿using dotnetcoreNHibernateSqlite.DbModel;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using NHibernate;
using NHibernate.Linq;
using System.Collections.Generic;
using System.Linq;
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
                _session.Save(new Person() { Id = 0, Name = "Jim", Age = 10 });
                _session.Save(new Person() { Id = 0, Name = "John", Age = 20 });
                _session.Save(new Person() { Id = 0, Name = "Joe", Age = 30 });
                _session.Save(new Person() { Id = 0, Name = "Sara", Age = 40 });
                _session.Save(new Person() { Id = 0, Name = "Adam", Age = 50 });
                _session.Save(new Person() { Id = 0, Name = "Eve", Age = 90 });
                _session.Save(new Person() { Id = 0, Name = "Jack", Age = 22 });
                _session.Save(new Person() { Id = 0, Name = "‌Betty", Age = 73 });
                _session.Save(new Person() { Id = 0, Name = "Lily", Age = 5 });
                _session.Save(new Person() { Id = 0, Name = "Ali", Age = 56 });
                _session.Save(new Person() { Id = 0, Name = "Tom", Age = 15 });

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

        /// <summary>
        /// Get all persons with Odata without page size
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("odata1")]
        [Produces("application/json")]
        [EnableQuery()]
        public IQueryable<Person> GetOdataWithoutPageSize()
        {
            return _session.Query<Person>();
        }

        /// <summary>
        /// Get all persons with Odata without page size
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("odata2")]
        [Produces("application/json")]
        [EnableQuery(PageSize = 5)]
        public IQueryable<Person> GetOdataWithPageSize()
        {
            return _session.Query<Person>();
        }
    }
}
