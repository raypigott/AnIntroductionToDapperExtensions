using System.Data.SqlClient;
using System.Linq;
using Dapper;
using DapperExtensions;
using FluentAssertions;
using NUnit.Framework;
using Rnsx.Stockify.Data.DomainModels;
using Rnsx.Stockify.Data.Repositories;

namespace Rnsx.Stockify.Data.IntegrationTests
{
    [TestFixture]
    class SupplierRepositoryTests
    {
        [Test]
        public void Create_GivenAnEntity_CreatesANewEntity()
        {
            var databaseConfiguration = new DatabaseConfiguration();

            var supplierRepository = new SupplierRepository(databaseConfiguration);

            var supplier = new Supplier
            {
                ShortCode = "ShortCode",
                ShortName = "ShortName",
                LongName = "LongName",
                Description = "Description",
                IsActive = true
            };

            var newSupplier = supplierRepository.Create(supplier);

            newSupplier.Id.Should().BeGreaterThan(0);
            newSupplier.ShortCode.Should().Be("ShortCode");
            newSupplier.ShortName.Should().Be("ShortName");
            newSupplier.LongName.Should().Be("LongName");
            newSupplier.Description.Should().Be("Description");
            newSupplier.IsActive.Should().BeTrue();
        }

        [Test]
        public void Get_GivenAnId_ReturnsTheEntity()
        {
            var databaseConfiguration = new DatabaseConfiguration();

            var supplierRepository = new SupplierRepository(databaseConfiguration);

            var supplier = new Supplier
            {
                ShortCode = "ShortCode",
                ShortName = "ShortName",
                LongName = "LongName",
                Description = "Description",
                IsActive = true
            };

            var updatedSupplier = supplierRepository.Create(supplier);

            var retrievedSupplier = supplierRepository.Get(updatedSupplier);

            retrievedSupplier.Id.Should().Be(updatedSupplier.Id);
            retrievedSupplier.ShortCode.Should().Be(updatedSupplier.ShortCode);
            retrievedSupplier.ShortName.Should().Be(updatedSupplier.ShortName);
            retrievedSupplier.LongName.Should().Be(updatedSupplier.LongName);
            retrievedSupplier.Description.Should().Be(updatedSupplier.Description);
            retrievedSupplier.IsActive.Should().Be(updatedSupplier.IsActive);
        }

        [Test]
        public void Get_GivenAnInvalidEntityToSearchFor_ReturnsNull()
        {
            var databaseConfiguration = new DatabaseConfiguration();

            var supplierRepository = new SupplierRepository(databaseConfiguration);

            var supplier = new Supplier
            {
                ShortCode = "ShortCode",
                ShortName = "ShortName",
                LongName = "LongName",
                Description = "Description",
                IsActive = true
            };

            var retrievedSupplier = supplierRepository.Get(supplier);

            retrievedSupplier.Should().BeNull();
        }

        [Test]
        public void Get_SearchOnShortName_ReturnsMoreThanZeroListItems()
        {
            var databaseConfiguration = new DatabaseConfiguration();

            var supplierRepository = new SupplierRepository(databaseConfiguration);

            var supplier = new Supplier
            {
                ShortCode = "ShortCode",
                ShortName = "ShortName",
                LongName = "LongName",
                Description = "Description",
                IsActive = true
            };

            var newSupplier = supplierRepository.Create(supplier);

            var predicate = Predicates.Field<Supplier>(f => f.ShortName, Operator.Eq, "ShortName");

            var suppliers = supplierRepository.Get(predicate);

            suppliers.Count.Should().BeGreaterThan(0);
        }

        [Test]
        public void Update_GivenAnEntity_UpdatesEntityAndReturnsSuccess()
        {
            var databaseConfiguration = new DatabaseConfiguration();

            var supplierRepository = new SupplierRepository(databaseConfiguration);

            var supplier = new Supplier
            {
                ShortCode = "ShortCode",
                ShortName = "ShortName",
                LongName = "LongName",
                Description = "Description",
                IsActive = true
            };

            var newSupplier = supplierRepository.Create(supplier);

            newSupplier.ShortName = "NewShortName";

            var success = supplierRepository.Update(newSupplier);

            success.Should().BeTrue();

            var predicate = Predicates.Field<Supplier>(f => f.ShortName, Operator.Eq, "NewShortName");

            var updatedPerson = supplierRepository.Get(predicate).First();

            updatedPerson.ShortName.Should().Be("NewShortName");
        }

        [Test]
        public void Delete_GivenAnEntity_DeletesEntityAndReturnsSuccess()
        {
            var databaseConfiguration = new DatabaseConfiguration();

            var supplierRepository = new SupplierRepository(databaseConfiguration);

            var supplier = new Supplier
            {
                ShortCode = "ShortCode",
                ShortName = "ShortName",
                LongName = "LongName",
                Description = "Description",
                IsActive = true
            };

            var newSupplier = supplierRepository.Create(supplier);

            var success = supplierRepository.Delete(newSupplier);

            success.Should().BeTrue();

            var readPerson = supplierRepository.Get(newSupplier);

            readPerson.Should().BeNull();
        }

        /// <summary>
        /// Return the database to it's originally state (empty) after running the integration tests.
        /// </summary>
        [TestFixtureTearDown]
        public void TearDown()
        {
            const string tsql = "TRUNCATE TABLE [dbo].[Supplier]";

            var databaseConfiguration = new DatabaseConfiguration();

            using (var sqlConnection = new SqlConnection(databaseConfiguration.GetConnectionString()))
            {
                sqlConnection.Open();

                sqlConnection.Execute(tsql);
            }
        }
    }
}
