using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using DapperExtensions;
using DapperExtensions.Mapper;
using Rnsx.Stockify.Data.DomainModels;

namespace Rnsx.Stockify.Data.Repositories
{
    /// <summary>
    /// Responsible for CRUD on the <see cref="Supplier"/> entity
    /// </summary>
    class SupplierRepository
    {
        private readonly IDatabaseConfiguration databaseConfiguration;

        public SupplierRepository(IDatabaseConfiguration databaseConfiguration)
        {
            if (databaseConfiguration == null) throw new ArgumentNullException("databaseConfiguration");

            var mapper = new SupplierMapper();

            this.databaseConfiguration = databaseConfiguration;
        }

        /// <summary>
        /// Create a <see cref="Supplier"/> entity.
        /// </summary>
        /// <param name="supplier">The supplier to create</param>
        /// <returns>The new supplier entity</returns>
        public Supplier Create(Supplier supplier)
        {
            if (supplier == null) throw new ArgumentNullException("supplier");

            using (var sqlConnection = new SqlConnection(databaseConfiguration.GetConnectionString()))
            {
                sqlConnection.Open();

                var id = sqlConnection.Insert(supplier);

                supplier.Id = id;
            }

            return supplier;
        }

        /// <summary>
        /// Get <see cref="Supplier"/> by Id
        /// </summary>
        /// <param name="supplier">The supplier to find</param>
        /// <returns>The supplier or null</returns>
        public Supplier Get(Supplier supplier)
        {
            if (supplier == null) throw new ArgumentNullException("supplier");

            using (var sqlConnection = new SqlConnection(databaseConfiguration.GetConnectionString()))
            {
                sqlConnection.Open();

                var value = sqlConnection.Get<Supplier>(supplier.Id);

                return value;
            }
        }

        /// <summary>
        /// Retrieve a list of <see cref="Supplier"/> according to the search criteria determined by the predicate.
        /// </summary>
        /// <param name="predicate">The search criteria</param>
        /// <returns>A list of <see cref="Supplier"/></returns>
        public List<Supplier> Get(IPredicate predicate)
        {
            if (predicate == null) throw new ArgumentNullException("predicate");

            using (var sqlConnection = new SqlConnection(databaseConfiguration.GetConnectionString()))
            {
                sqlConnection.Open();

                var retrievedPersons = sqlConnection.GetList<Supplier>(predicate);

                return retrievedPersons.ToList();
            }
        }

        /// <summary>
        /// Update the <see cref="Supplier"/>
        /// </summary>
        /// <param name="supplier">The <see cref="Supplier"/> to update</param>
        /// <returns>True if update successful, false otherwise</returns>
        public bool Update(Supplier supplier)
        {
            if (supplier == null) throw new ArgumentNullException("supplier");

            using (var sqlConnection = new SqlConnection(databaseConfiguration.GetConnectionString()))
            {
                sqlConnection.Open();

                var success = sqlConnection.Update(supplier);

                return success;
            }
        }

        /// <summary>
        /// Delete the <see cref="Supplier"/>
        /// </summary>
        /// <param name="supplier">The supplier to be deleted</param>
        /// <returns>True if successful, false otherwise</returns>
        public bool Delete(Supplier supplier)
        {
            if (supplier == null) throw new ArgumentNullException("supplier");

            using (var sqlConnection = new SqlConnection(databaseConfiguration.GetConnectionString()))
            {
                sqlConnection.Open();

                var success = sqlConnection.Delete(supplier);

                return success;
            }
        }
    }

    internal sealed class SupplierMapper : ClassMapper<Supplier>
    {
        public SupplierMapper()
        {
            Schema("dbo");
            AutoMap();
        }
    }
}