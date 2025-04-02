﻿using Microsoft.EntityFrameworkCore;
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

        #endregion

        #endregion


    }
}
