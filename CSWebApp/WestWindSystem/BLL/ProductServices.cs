using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using WestWindSystem.DAL;
using WestWindSystem.Entities;
#endregion

namespace WestWindSystem.BLL
{
    public class ProductServices
    {
        //this makes a copy of our context usable only by this class.
        //readonly means it can only be set in our constructor.
        private readonly WestWindContext _context;

        internal ProductServices(WestWindContext context)
        {
            _context = context;
        }

        #region Services

        public List<Product> GetProductsByCategoryID(int categoryID)
        {
            IEnumerable<Product> products = _context.Products
                                                    .Include(product => product.Supplier)
                                                    .Where(product => product.CategoryID == categoryID)
                                                    .OrderBy(product => product.ProductName);

            return products.ToList();
        }

        public Product? GetProductByID(int ID)
        {
            Product? product = _context.Products
                                            .Where(prod => prod.ProductID == ID)
                                            .FirstOrDefault();

            return product;
        }

        #region CRUD Services

        public int AddProduct(Product product)
        {
            //Validate we have data

            if(product == null)
            {
                throw new ArgumentNullException("Product Information Required!");
            }

            /*
             * Lets assume we have a business rule that 2 products can not have the same SupplierID,
             * ProductName, and QuantityPerUnit
             */

            //Lets test business logic
            bool exists = false;

            //Check if there are any duplicates in our database based on the business rule above
            exists = _context.Products.Any(
                                            x => x.SupplierID == product.SupplierID &&
                                            x.ProductName.Equals(product.ProductName) &&
                                            x.QuantityPerUnit.Equals(product.QuantityPerUnit)
                                          );

            //If there's a duplicate throw an error
            if(exists)
            {
                throw new ArgumentException("Product already exists!");
            }

            //After this we assume we have good data

            //Step 1: Staging
            //The data is only locally in memory at this point
            //We can make any final changes at this point ie: Foreign Keys, default values, add data not in our form, etc.

            _context.Products.Add(product);

            //Step 2: Commit
            //This pushes our data to our database
            //Any Enity Annotation Validation will happen first

            _context.SaveChanges(); //This also sets our PKey ID

            return product.ProductID;
        }

        public int UpdateProduct(Product product)
        {
            //Check if we have valid data
            if (product == null)
            {
                throw new ArgumentNullException("Product Information Required!");
            }

            //Check to make sure the given ID exists
            if (!_context.Products.Any(prod => prod.ProductID == product.ProductID))
            {
                throw new ArgumentException("Product with this ID does not exist");
            }

            //Lets test business logic, this may be different from Create
            bool exists = false;

            //Check if there are any duplicates in our database based on the business rule above
            exists = _context.Products.Any(
                                            x => x.SupplierID == product.SupplierID &&
                                            x.ProductName.Equals(product.ProductName) &&
                                            x.QuantityPerUnit.Equals(product.QuantityPerUnit) &&
                                            x.ProductID != product.ProductID //Make sure the product is still unique
                                          );

            //If there's a duplicate throw an error
            if (exists)
            {
                throw new ArgumentException("Product already exists!");
            }

            //This handles checking all of our fields and only modifies the ones that changed.
            EntityEntry<Product> updating = _context.Entry(product);

            updating.State = EntityState.Modified;

            //return the number of records updated
            return _context.SaveChanges();
        }
        
        /// <summary>
        /// Logical Delete.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public int DiscontinueProduct(Product product)
        {
            //Check if we have valid data
            if (product == null)
            {
                throw new ArgumentNullException("Product Information Required!");
            }

            //Lets test business logic, this may be different from Create
            Product productToDiscontinue = null;

            //Check if there are any duplicates in our database based on the business rule above
            productToDiscontinue = _context.Products
                                    .FirstOrDefault(x => x.ProductID == product.ProductID);

            //If there's a duplicate throw an error
            if (productToDiscontinue == null)
            {
                throw new ArgumentException("Product doesn't exists!");
            }

            productToDiscontinue.Discontinued = true;

            //This handles checking all of our fields and only modifies the ones that changed.
            EntityEntry<Product> updating = _context.Entry(productToDiscontinue);

            //Will call an Update on the DB.
            updating.State = EntityState.Modified;

            //return the number of records updated
            return _context.SaveChanges();
        }

        public int ActivateProduct(Product product)
        {
            //Check if we have valid data
            if (product == null)
            {
                throw new ArgumentNullException("Product Information Required!");
            }

            //Lets test business logic, this may be different from Create
            Product productToActivate = null;

            //Check if there are any duplicates in our database based on the business rule above
            productToActivate = _context.Products
                                    .FirstOrDefault(x => x.ProductID == product.ProductID);

            //If there's a duplicate throw an error
            if (productToActivate == null)
            {
                throw new ArgumentException("Product doesn't exists!");
            }

            productToActivate.Discontinued = false;

            //This handles checking all of our fields and only modifies the ones that changed.
            EntityEntry<Product> updating = _context.Entry(productToActivate);

            //Will call an Update on the DB.
            updating.State = EntityState.Modified;

            //return the number of records updated
            return _context.SaveChanges();
        }

        /// <summary>
        /// Physical Delete. Data will be destroyed.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public int DeleteProduct(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException("Product Information Required!");
            }

            //Get out early if there is an issue as delete is dangerous
            if(!_context.Products.Any(prod => prod.ProductID == product.ProductID))
            {
                throw new ArgumentException("Product could not be found!");
            }

            //Check if the entry has and children.
            if(_context.Products.Any(prod => prod.ManifestItems.Count > 0 &&
                                             prod.ProductID == product.ProductID))
            {
                throw new ArgumentException("Product has Manifest Items, can not delete.");
            }

            //Check if the entry has and children.
            if (_context.Products.Any(prod => prod.OrderDetails.Count > 0 &&
                                             prod.ProductID == product.ProductID))
            {
                throw new ArgumentException("Product has Order Details, can not delete.");
            }

            //I should be clear to delete with we get here.

            EntityEntry<Product> toBeDeleted = _context.Entry(product);

            //Deleted state will call a Delete on our DB.
            toBeDeleted.State = EntityState.Deleted;

            return _context.SaveChanges();
        }

        #endregion

        #endregion


    }
}
